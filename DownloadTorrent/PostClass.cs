using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace DemoYunfile
{
    public class PostClass
    {
        #region 5.构造Post数据提交格式
        /// <summary>
        /// 构造Post数据提交格式
        /// </summary>
        /// <param name="strfileId">文件ID</param>
        /// <param name="struserId">用户ID</param>
        /// <param name="strparentPath">保存路径</param>
        /// <param name="strfileName">保存的文件名字</param>
        /// <returns></returns>
        public static string CreatePOSTData(string strfileId, string struserId, string strparentPath, string strfileName)
        {
            //数据格式：module=explorer&action=doFileSaveAs&fileId=00342cc1&userId=lmpbjs1988&parentPath=%2FShare&fileName=1234456678687.torrent
            const string strOpear = "module=explorer&action=doFileSaveAs&";
            StringBuilder sbr = new StringBuilder();
            sbr.Append(strOpear);
            sbr.Append("fileId=" + strfileId + "&");
            sbr.Append("userId=" + struserId + "&");
            sbr.Append("parentPath=" + System.Web.HttpUtility.UrlEncode(strparentPath) + "&");
            sbr.Append("fileName=" + System.Web.HttpUtility.UrlEncode(strfileName));
            return sbr.ToString();
        }
        #endregion

        #region 6.解析页面获得目标的URL地址

        #region 6.1 yunfile网盘处理模块

        /// <summary>
        /// 1.从文件的URL中获得文件的用户ID 和 文件的ID
        /// </summary>
        /// <param name="strURL">加密之后的文件URL</param>
        /// <param name="cookies">登陆的cookie信息</param>
        /// <returns></returns>
        public static string[] GetUIDFid(string strURL, CookieCollection cookies)
        {
            string[] arr = new string[2];

            try
            {
                if (!string.IsNullOrEmpty(strURL))
                {
                    //原始的格式：http://page1.yunfile.com/file/lmpbjs1988/3356cf0f/
                    //1.获得目标页面的html源代码

                    string strHtml = VTS.Common.NetHelper.HttpGet(strURL, cookies, 0, VTS.Common.NetHelper.BtUserAgent);
                    HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
                    htmldoc.LoadHtml(strHtml);

                    //2.从源代码中找到包含文件ID的代码
                    //HtmlAgilityPack.HtmlNode node = htmldoc.DocumentNode.SelectSingleNode(".//span[@style=\"font-weight:bold;\"]");
                    string strHTML = htmldoc.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[2]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/table[1]/tr[1]/td[1]/div[1]/div[1]/span[2]").InnerHtml.Trim();

                    //3.开始进行邪恶的解析工作
                    int length = strHTML.Length;
                    int index = strHTML.IndexOf("file/");
                    int lastindex = strHTML.LastIndexOf("\">");

                    if (index != -1 && lastindex != -1)
                    {
                        string[] alist = strHTML.Substring(index + 5, lastindex - index - 5).Split('/');
                        if (alist != null && alist.Length == 2)
                        {
                            arr[0] = alist[0];//用户的ID
                            arr[1] = alist[1];//文件的ID
                        }
                    }
                }
            }
            catch
            {
                arr = null;
            }
            return arr;
        }

        /// <summary>
        /// 2.从用户的URL中获得一组文件列表和文件夹列表
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static List<string> GetURLFile(string URL, out List<string> alistFolder)
        {
            //从结果中得到两组数据：文件夹列表  文件列表
            List<string> alistFile = new List<string>();
            alistFolder = new List<string>();

            //1.先获取文件的源代码
            string strHTMLCode = VTS.Common.NetHelper.HttpWebClient(URL, VTS.Common.NetHelper.WebClientUserAgent);

            if (strHTMLCode != "error")
            {
                //初始化一个HtmlDocument实例对象
                HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();

                htmldoc.LoadHtml(strHTMLCode);

                //获得一个html节点集合
                HtmlAgilityPack.HtmlNodeCollection hrefList = htmldoc.DocumentNode.SelectNodes(".//td[@style=\" width:720px; text-align:left; overflow:hidden; white-space:normal; word-wrap:break-word; word-break:break-all;  \"]");

                int i = 0;

                string strInnerHtml = string.Empty;
                string strInnerText = string.Empty;

                foreach (HtmlAgilityPack.HtmlNode node in hrefList)
                {
                    strInnerHtml = node.InnerHtml.Trim();
                    strInnerText = node.InnerText.Trim();

                    if (strInnerHtml.Contains("file/folder.png"))
                    {
                        alistFolder.Add(GetHref(strInnerHtml) + "#" + strInnerText);
                    }
                    else
                    {
                        //文件地址和文件名字使用"#"分割
                        alistFile.Add(GetHref(strInnerHtml) + "#" + strInnerText);
                    }
                    i++;
                }
            }
            return alistFile;
        }

        /// <summary>
        /// 3.解析出文件的href链接
        /// </summary>
        /// <param name="strHTML"></param>
        /// <returns></returns>
        public static string GetHref(string strHTML)
        {
            string str = string.Empty;

            if (!string.IsNullOrEmpty(strHTML))
            {
                string strTemp = strHTML.Replace("<a href=\"", "");
                int index = strTemp.IndexOf('"');
                str = strTemp.Substring(0, index);
            }

            return str;
        }
        #endregion

        #region 6.2 400GB网盘文件地址处理

        /// <summary>
        /// 分析400gb网盘中源代码
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static string[] GetDownLoadURLFromChengTong(string strHTMLCode, CookieCollection cookies)
        {
            string[] arrResutle = new string[2];

            string strDownload = string.Empty;

            //思路：先解析出文件的第一级别的地址，再向第二个界面发出请求解析真实的下载地址

            string strInnerHTML = GetURLLink(strHTMLCode, ".//div[@style=\"width: 100%; clear: both;\"]", NodeType.InnerHtml);
            string strFilename = GetURLLink(strHTMLCode, ".//title", NodeType.InnerText);
            string strFistURL = GetFirstPageLink(strInnerHTML);

            //开始想第二个页面发出请求
            string strSecondCode = VTS.Common.NetHelper.HttpGet(strFistURL, cookies, 0, VTS.Common.NetHelper.BtUserAgent);
            string strDownLink = GetURLLink(strSecondCode, ".//a[@class=\"thunder\"]", NodeType.OuterHtml);

            strDownload = GetthunderLink(strDownLink);

            arrResutle[0] = strFilename;
            arrResutle[1] = strDownload;

            return arrResutle;
        }
        public static string GetFirstPageLink(string strInnerHTML)
        {
            int localIndex = strInnerHTML.IndexOf("window.location.href='");
            int loacllast = strInnerHTML.LastIndexOf('\'');
            string strFistURL = "http://www.400gb.com" + strInnerHTML.Substring(localIndex, loacllast - localIndex).Replace("window.location.href='", "");
            return strFistURL;
        }
        public static string GetthunderLink(string strDownLink)
        {
            int thunderFirstIndex = strDownLink.IndexOf("thunderhref=");
            int thunderLastIndex = strDownLink.LastIndexOf("\">") + 1;
            string strDownload = strDownLink.Substring(thunderFirstIndex, thunderLastIndex - thunderFirstIndex).Replace("\"", "").Replace("thunderhref=", "");
            return strDownload;
        }

        #endregion

        /// <summary>
        /// 从HTML源代码中获取指定的HTML节点
        /// </summary>
        /// <param name="HTMLcode">HTML源代码</param>
        /// <param name="strXpath">XPath表达式</param>
        /// <param name="nodetype">节点的类型</param>
        /// <returns></returns>
        public static string GetURLLink(string HTMLcode, string strXpath, NodeType nodetype)
        {
            string strResult = string.Empty;

            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(HTMLcode);
            HtmlAgilityPack.HtmlNode node = htmldoc.DocumentNode.SelectSingleNode(strXpath);

            switch (nodetype)
            {
                case NodeType.InnerHtml:
                    strResult = node.InnerHtml;
                    break;
                case NodeType.InnerText:
                    strResult = node.InnerText;
                    break;
                case NodeType.OuterHtml:
                    strResult = node.OuterHtml;
                    break;
                default:
                    break;
            }
            return strResult.Trim();
        }

        public enum NodeType
        {
            InnerHtml,
            InnerText,
            OuterHtml
            //NextSibling 
            #region node的属性
            //node.InnerHtml;
            //node.InnerText;
            //node.OuterHtml,
            //node.NextSibling 
            #endregion
        }
        #endregion
    }
}