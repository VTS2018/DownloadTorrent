using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Threading;
using AnalTorrentFile;
using VTS.Excel;

namespace DemoYunfile
{
    public partial class Form1 : Form
    {
        #region 全局变量定义===========================================================

        Thread tance;//探测文件地址的线程
        List<string> afolder;//存放探测文件列表中的文件夹地址
        List<string> afile = new List<string>();//存放探测文件列表中的文件地址
        public delegate void MyYunfileDelegate(string msg);

        /// <summary>
        /// yunfile提交请求的URL
        /// </summary>
        public static string strPOSTUrl = "http://www.yunfile.com/explorer/list.html";

        /// <summary>
        /// 导出使用的数据表
        /// </summary>
        public DataTable dt = new DataTable();

        public string myStringFloder = string.Empty;

        #endregion

        #region 1.窗体构造函数=========================================================
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region 2.窗体初始化代码=======================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            //加载cookie
            LoadCookie();

            //string str = string.Empty;
            //foreach (byte b in Encoding.ASCII.GetBytes(Environment.NewLine))
            //{
            //    str += b;
            //}
            //MessageBox.Show(Encoding.ASCII.GetBytes(Environment.NewLine).Length.ToString());
            //MessageBox.Show(str);
        }

        #endregion

        #region 3.选择导入文本文件地址=================================================

        private void btnIMPOR_Click(object sender, EventArgs e)
        {
            //需要支持Excel文件
            string strTxtpath = VTS.Common.VTSFormsCommon.GetFilePath(new OpenFileDialog(), "txt文件(*.txt)|*.txt|Excel2007文件(*.xlsx)|*.xlsx|Excel2003文件(*.xls)|*.xls");

            if (strTxtpath == "")
            {
                MessageBox.Show("未选择文件地址！");
                return;
            }
            //导入前清空掉数据
            if (this.afile.Count > 0)
            {
                this.afile.Clear();
            }
            //初始化文件加载地址
            this.myStringFloder = strTxtpath;
            this.txtFilePath.Text = strTxtpath;

            //加载到全局变量中去
            this.afile = VTS.Common.ImportData.LoadDataFromfile(strTxtpath).ToList();
            this.txtFileURLList.Lines = this.afile.ToArray();
        }

        #endregion

        #region 4.yunfile用户文件列表探测模块==========================================

        private void btnTANce_Click(object sender, EventArgs e)
        {
            /*探测原理：使用一个线程进行执行，防止出现界面假死的情况*/
            tance = new Thread(TanCe);
            tance.Start();
        }

        //探测使用的方法
        public void TanCe()
        {
            MyYunfileDelegate d = new MyYunfileDelegate(UpdateListUI);
            this.Invoke(d, "开始探测了...\r\n");

            this.afile = PostClass.GetURLFile(this.txtUserURL.Text.Trim(), out this.afolder);

            #region 文件夹部分判断==============

            if (this.afolder.Count > 0)
            {
                this.Invoke(d, "\r\n扫描到了" + this.afolder.Count + "个文件夹地址...\r\n");
                foreach (string item in this.afolder)
                {
                    this.Invoke(d, item + "\n");
                }
            }
            else
            {
                this.Invoke(d, "哈哈！你的运气太差了哦！一个文件夹也没有找到...\n");
            }

            #endregion

            #region 文件部分判断================

            if (this.afile.Count > 0)
            {
                this.Invoke(d, "\r\n扫描到了" + this.afile.Count + "个文件地址...\r\n");
                foreach (string item in this.afile)
                {
                    this.Invoke(d, item + "\n");
                }
            }
            else
            {
                this.Invoke(d, "哈哈！你的运气太差了哦！一个也没有找到...\n");
            }
            #endregion

            string strLab = "文件夹数量：" + afolder.Count + "\r\r" + "文件数量：" + this.afile.Count;

            this.Invoke(d, strLab);
            this.Invoke(d, "扫描完成...\n");
        }

        //更新界面的函数
        public void UpdateListUI(string strText)
        {
            if (strText.Contains("文件夹数量："))
            {
                this.label5.Text = strText;
            }
            else
            {
                this.txtFileURLList.AppendText(strText);
            }
        }

        #endregion

        #region 5.每100个地址导出成一个文件============================================
        private void btnImportList_Click(object sender, EventArgs e)
        {
            //每100个地址导出成一个文件,如果本身比较小的话直接进行转存
            string path = string.Empty;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath + "\\";
            }
            if (path == "")
            {
                MessageBox.Show("没有设置好保存目录！");
                return;
            }
            else
            {
                StringBuilder sbr = new StringBuilder();
                int i = 1;
                int num = int.Parse(this.txtNumber.Text);
                foreach (string item in this.afile)
                {
                    sbr.Append(item + Environment.NewLine);
                    if (i % num == 0)
                    {
                        VTS.Common.VTSCommon.CreateFile(sbr.ToString().TrimEnd(new char[] { Convert.ToChar(13), Convert.ToChar(10) }), path + i + ".txt");
                        sbr.Clear();
                    }
                    i++;
                }
                VTS.Common.VTSCommon.CreateFile(sbr.ToString(), path + "最后一个" + ".txt");
                MessageBox.Show("处理成功了，共导出" + i + "个文件！");
            }
        }
        #endregion

        #region 转存V1版本

        Thread yunfilepath;
        private void btnSaveAS_Click(object sender, EventArgs e)
        {
            //输入一个用户地址的URL进行转存，只用于比较小数量的文件 线程开始执行
            yunfilepath = new Thread(DoWork);
            yunfilepath.Start();

            #region 单步测试代码

            //string strCookies = this.txtCookie.Text.Trim();
            //CookieCollection cookies = PostClass.CreateCookies(strCookies, "/", "www.yunfile.com");

            //string strUID = string.Empty;//用户的ID
            //string strFID = string.Empty;//文件的ID
            //string strFileName = string.Empty;//文件的名字

            //string[] arrList = this.txtFileURLList.Lines; //文件的地址列表

            //Random rm = new Random();

            //foreach (string item in arrList)
            //{
            //    string strL = item.Split('#')[0];

            //    string[] atmep = PostClass.GetUIDFid(strL.Trim(), cookies);

            //    strUID = atmep[0];
            //    strFID = atmep[1];
            //    strFileName = item.Split('#')[1];

            //    //获得Post数据
            //    string PostData = PostClass.CreatePOSTData(strFID, strUID, "/Share", strFileName);

            //    //开始转存
            //    string strCode = PostClass.RequestPost(strPOSTUrl, "", PostData, cookies, 1);
            //} 
            #endregion
        }

        //开始进行苦逼的操作了
        public void DoWork()
        {
            //当文件处理过多的时候容易中断退出，建议使用小个文件进行转存
            MyYunfileDelegate d = new MyYunfileDelegate(UpdateUI);

            //需要同过一组源代码解析其中的一组 ULR---文件名集合
            this.Invoke(d, "开始工作了了...\n先看看有没有文件夹\r\n");

            List<string> afolder;
            //List<string> afile = PostClass.GetURLFile("http://page1.yunfile.com/ls/lmpbjs1988/", out afolder);
            List<string> afile = PostClass.GetURLFile(this.txtFileURLList.Text.Trim(), out afolder);

            #region 文件夹地址扫描

            if (afolder.Count > 0)
            {
                this.Invoke(d, "\r\n扫描到了" + afolder.Count + "个文件夹地址...\r\n");
                foreach (string item in afolder)
                {
                    this.Invoke(d, item + "\n");
                }
            }
            else
            {
                this.Invoke(d, "\r\n哈哈！你的运气太差了哦！一个也没有找到...\r\n");
            }
            #endregion

            #region 文件列表转存

            if (afile.Count > 0)
            {
                this.Invoke(d, "\r\n共计扫描到" + afile.Count + "个文件地址...\r\n");

                //思路分析：两种参数输入 文件的地址列表  用户的地址【蜘蛛算法 不停的便利获得下一级别 不推荐使用速度较慢】
                this.Invoke(d, "注意！开始进行转存了...\r\n");

                //设置cookie
                string strCookies = this.txtCookie.Text.Trim();
                CookieCollection cookies = VTS.Common.NetHelper.CreateCookies(strCookies, "/", "www.yunfile.com");
                this.Invoke(d, "Cookie已经设置完毕,正在进行努力登陆.....\r\n");

                string strUID = string.Empty;//用户的ID
                string strFID = string.Empty;//文件的ID
                string strFileName = string.Empty;//文件的名字

                string[] arrList = afile.ToArray(); //文件的地址列表

                Random rm = new Random();

                int i = 0;
                foreach (string item in arrList)
                {
                    #region 1.阶段一:获取文件的ID和用户的ID

                    string strL = item.Split('#')[0];

                    //文件的名字
                    strFileName = item.Split('#')[1];

                    this.Invoke(d, "开始获取文件的ID和用户ID...\r\n");

                    string[] atmep = PostClass.GetUIDFid(strL.Trim(), cookies);

                    //this.Invoke(d, "获取完毕...\r\n进入到转存阶段\r\n**************************************************\r\n");
                    if (atmep != null)
                    {
                        strUID = atmep[0];
                        strFID = atmep[1];
                    }
                    else
                    {
                        this.Invoke(d, "该文件有问题,转存失败了\r\n");
                        continue;
                    }
                    this.Invoke(d, "正在处理第" + i + "个文件，用户ID:" + strUID + ",文件ID:" + strFID + "\r\n");


                    #endregion

                    //让当前的线程歇一会
                    if (yunfilepath != null)
                    {
                        //this.Invoke(d, "第一次休息中...\n");
                        yunfilepath.Join(rm.Next(200, 2000));
                    }

                    #region 2.阶段二：转存用户的文件信息

                    //获得Post数据
                    string PostData = PostClass.CreatePOSTData(strFID, strUID, "/Share", strFileName);

                    i++;

                    //开始转存
                    string strCode = VTS.Common.NetHelper.HttpPost(strPOSTUrl, "", PostData, cookies, 1);

                    this.Invoke(d, "成功啦！返回代码：" + strCode + "\n");

                    this.Invoke(d, "\n********************分割线*******************\n");

                    #endregion

                    //让当前的线程歇一会
                    if (yunfilepath != null)
                    {
                        //this.Invoke(d, "第二次在休息中...\n");
                        yunfilepath.Join(rm.Next(300, 1000));
                    }
                }
                this.Invoke(d, "已经全部搞定了...\n");
            }
            #endregion
        }

        //更新界面的UI操作
        public void UpdateUI(string strText)
        {
            this.txtCode.AppendText(strText);
        }

        #endregion

        #region 转存V2版本

        //使用带参数的方式进行操作
        Thread thSaveV2;

        private void btnSaveV2_Click(object sender, EventArgs e)
        {
            //使用带参数的进行传递数据
            ParameterizedThreadStart pt = new ParameterizedThreadStart(SaveASV2);
            thSaveV2 = new Thread(pt);
            object obj = new Pobject()
            {
                alist = this.afile,
                scookie = this.txtCookie.Text.Trim(),
                scookiepath = "/",
                scookieDomain = "www.yunfile.com"
            };
            thSaveV2.Start(obj);
        }

        public void SaveASV2(object obj)
        {
            MyYunfileDelegate d = new MyYunfileDelegate(UpdateUI);

            Pobject p = (Pobject)obj;
            string[] arrList = p.alist.ToArray(); //文件的地址列表

            string strL = string.Empty;
            string strUID = string.Empty;//用户的ID
            string strFID = string.Empty;//文件的ID
            string strFileName = string.Empty;//文件的名字
            string PostData = string.Empty;

            //设置cookie
            string strCookies = p.scookie;

            //转换为Cookie对象
            CookieCollection cookies = VTS.Common.NetHelper.CreateCookies(strCookies, p.scookiepath, p.scookieDomain);

            Random rm = new Random();

            int i = 0;
            foreach (string item in arrList)
            {
                #region 1.阶段一:获取文件的ID和用户的ID

                strL = item.Split('#')[0];
                strFileName = item.Split('#')[1];

                string[] atmep = PostClass.GetUIDFid(strL.Trim(), cookies);

                if (atmep == null)
                {
                    continue;
                }
                strUID = atmep[0];
                strFID = atmep[1];

                this.Invoke(d, "\r\n正在处理用户ID:" + strUID + ",文件ID:" + strFID + "\r\n");

                #endregion

                //让当前的线程歇一会
                if (thSaveV2 != null)
                {
                    this.Invoke(d, "第一次休息中...\n");
                    thSaveV2.Join(rm.Next(400, 900));
                }

                #region 2.阶段二：转存用户的文件信息

                //1.获得Post数据
                PostData = PostClass.CreatePOSTData(strFID, strUID, "/Share", strFileName);

                i++;

                //开始转存
                this.Invoke(d, "\r\n开始转存第" + i + "个文件...\r\n");

                string strCode = VTS.Common.NetHelper.HttpPost(strPOSTUrl, "", PostData, cookies, 1);

                this.Invoke(d, "\r\n成功啦！返回代码：" + strCode + "\r\n");

                this.Invoke(d, "\n********************分割线*******************\n");
                #endregion

                //让当前的线程歇一会
                if (thSaveV2 != null)
                {
                    this.Invoke(d, "第二次在休息中...\n");
                    thSaveV2.Join(rm.Next(300, 900));
                }

            }
            this.Invoke(d, "已经全部搞定了...\n");
        }

        //包装一个参数炸弹进行传递
        public class Pobject
        {
            public string scookie = "";
            public string scookiepath = "/";
            public string scookieDomain = "www.yunfile.com";
            public List<string> alist = new List<string>();
        }
        #endregion

        #region 2.1探测城通网盘真实文件下载地址========================================
        private void btnChengGetURL_Click(object sender, EventArgs e)
        {
            //string strFilePath = "http://www.400gb.com/file/25484374";

            //设置cookie
            string strCookies = this.txtCookie.Text.Trim();
            CookieCollection cookies = VTS.Common.NetHelper.CreateCookies(strCookies, "/", "www.400gb.com");
            string strFilename = string.Empty;

            //从代码中解析中一级地址
            string[] arrList = this.txtFileURLList.Lines;
            foreach (string item in arrList)
            {
                string strCode = VTS.Common.NetHelper.HttpGet(item, cookies, 0, VTS.Common.NetHelper.BtUserAgent);

                string[] al = PostClass.GetDownLoadURLFromChengTong(strCode, cookies);

                this.txtCode.AppendText("文件：" + al[0] + "-" + item + Environment.NewLine + al[1] + Environment.NewLine);

                //延时设置
                System.Threading.Thread.Sleep(new Random().Next(2, 3) * 1000);
            }
            //封装为一个函数，传入文件的地址数据 得到一个数组：文件的地址 文件的下载地址 文件的名字
        }

        #endregion

        #region 2.2转存城通网盘中的文件================================================

        private void btnZhuanChengtong_Click(object sender, EventArgs e)
        {
            //应该使用的定时器 每隔一段的时间去请求一下 
            //使用弹出栈的策略 配合多线程进行使用 

            string strUrl = "http://www.400gb.com/ajax.php?action=copyfile&task=docopy&";
            //file_id=25484374&ref=http%3A%2F%2Fwww.400gb.com%2Ffile%2F25484374";


            string strCookies = this.txtCookie.Text.Trim();
            CookieCollection cookies = VTS.Common.NetHelper.CreateCookies(strCookies, "/", "www.400gb.com");

            string[] arrList = this.txtFileURLList.Lines;
            foreach (string item in arrList)
            {
                //文件的ID
                string strFileID = VTS.Common.VTSCommon.StringSubLast(item);

                //文件的地址
                string strURLRef = System.Web.HttpUtility.UrlEncode(item);

                //需要提交的地址
                string strAjax = strUrl + CreateURLParams(strFileID, strURLRef);

                string code = VTS.Common.NetHelper.HttpGet(strAjax, cookies, 1, VTS.Common.NetHelper.BtUserAgent);
                this.txtCode.AppendText(item + " is " + code + Environment.NewLine);

                //延时设置
                // System.Threading.Thread.Sleep(new Random().Next(2, 3) * 1000);

            }
        }

        #region 构造参数

        /// <summary>
        /// 构造参数
        /// </summary>
        /// <param name="strParms"></param>
        /// <returns></returns>
        public static string CreateURLParams(params string[] strParms)
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Append("file_id=");
            sbr.Append(strParms[0]);
            sbr.Append("&ref=");
            sbr.Append(strParms[1]);
            return sbr.ToString();
        }
        #endregion

        #endregion

        #region 3.1导出真实的文件下载地址==============================================
        private void btnImportDURL_Click(object sender, EventArgs e)
        {
            string excelPath = string.Empty;

            SaveFileDialog sf = new SaveFileDialog();
            sf.AddExtension = true;
            sf.Title = "请选择Excel保存的位置：";
            sf.Filter = "Excel2007文件(*.xlsx)|*.xlsx";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                excelPath = sf.FileName;
                if (this.dt != null && !string.IsNullOrEmpty(excelPath))
                {
                    ExcelFile.SetData(this.dt, excelPath, ExcelVersion.Excel12, HDRType.Yes);
                    MessageBox.Show("导出成功！");
                }
                else
                {
                    MessageBox.Show("没有需要导出的数据");
                }
            }
        }
        #endregion

        #region B1.开始下载BT工厂中的数据==============================================

        Thread thGongchang;//最大支持100个

        private void btBTGongchang_Click(object sender, EventArgs e)
        {
            //采用的是多线程 开启异步的方式
            MyBTObjectInfo b = new MyBTObjectInfo(this.afile, this.myStringFloder);

            //先创建出存放BT的目录
            b.CreateFloder();

            //MessageBox.Show(b.strFloder);

            //改线程执行一个带有参数的委托
            thGongchang = new Thread(new ParameterizedThreadStart(BtGongchangWork));

            //线程开始的时候调用对象的方法
            thGongchang.Start(b);
        }

        public void BtGongchangWork(object obj)
        {
            #region 同步版本的实现思路

            //List<string> ls = obj as List<string>;
            //Random rm = new Random();
            //for (int i = 0; i < ls.Count; i++)
            //{
            //    BTFuzhuaClass btLink = new BTFuzhuaClass(ls[i]);
            //    string dir = "E:\\BT\\";
            //    string filename = i + "-" + btLink.GetBTData_Filename();

            //    string code = RequestPost(btLink.GetBTData_PostURL(), ls[i], btLink.GetBTData_PostData(), dir + filename + ".torrent", null, 1);
            //    if (thGongchang != null)
            //    {
            //        this.Invoke(new Action<string>(UpdateUI), code);
            //    }
            //    this.Invoke(new Action<string>(UpdateUI), i + "成功\n");

            //    if (thGongchang != null)
            //    {
            //        this.Invoke(new Action<string>(UpdateUI), "第一次休息中...\n");
            //        thGongchang.Join(rm.Next(400, 900));
            //    }
            //}

            #endregion

            #region 异步版本的实现思路

            MyBTObjectInfo bt = (MyBTObjectInfo)obj;
            List<string> ls = bt.Lbtlist;

            Random rm = new Random();
            for (int i = 0; i < ls.Count; i++)
            {
                try
                {
                    BTFuzhuaClass btLink = new BTFuzhuaClass(ls[i]);
                    string dir = bt.strFloder;

                    string filename = i + "-" + btLink.GetBTData_Filename();
                    RequestPostAsync(btLink.GetBTData_PostURL(), ls[i], btLink.GetBTData_PostData(), dir + filename + ".torrent", null);
                    this.Invoke(new Action<string>(UpdateUI), i + "-成功\n");

                    if (thGongchang != null)
                    {
                        //this.Invoke(new Action<string>(UpdateUI), "第一次休息中...\n");
                    }
                }
                catch
                {
                    continue;
                }
            }
            #endregion
        }

        #endregion

        #region BT工厂 ASP.NET POST 方式提交数据！[同步版本]测试代码===================

        private void btnBTTongbu_Click(object sender, EventArgs e)
        {
            //测试BT
            string strPostURL = "http://www3.17domn.com/bt9/down.php";

            string strURL = "http://www3.17domn.com/bt9/file.php/MIDDXYN.html";

            string strPost = "type=torrent&id=MIDDXYN&name=MIDDXYN";

            RequestPost(strPostURL, strURL, strPost, "E:\\MIDDXYN.torrent", null, 1);
        }



        /// <summary>
        /// ASP.NET POST 方式提交数据！[同步版本]
        /// </summary>
        /// <param name="TheURL">Post提交的地址</param>
        /// <param name="strReferer">引用页地址</param>
        /// <param name="strPOST">Post提交的数据</param>
        /// <param name="cookies">登陆信息cookie</param>
        /// <returns></returns>
        public static string RequestPost(string TheURL, string strReferer, string strPOST, string strFileSavePath, CookieCollection cookies, int type)
        {
            string backstr = string.Empty;
            string url = TheURL;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string s = strPOST;

            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
            req.ProtocolVersion = HttpVersion.Version11;

            req.ServicePoint.Expect100Continue = false;

            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            req.Headers["Accept-Charset"] = "GB2312,utf-8;q=0.7,*;q=0.7";
            req.Headers["Accept-Encoding"] = "gzip,deflate";
            req.Headers["Accept-Language"] = "zh-cn,zh;q=0.5";

            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20100101 Firefox/6.0";
            req.Method = "POST";

            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = requestBytes.Length;

            req.Referer = strReferer;

            if (cookies != null)
            {
                req.CookieContainer = new CookieContainer();
                req.CookieContainer.Add(cookies);
            }

            //添加需要提交的数据
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            switch (type)
            {
                case 0:
                    StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
                    backstr = sr.ReadToEnd();
                    sr.Close();
                    break;

                case 1:
                    HttpStatusCode code = res.StatusCode;
                    backstr = code.ToString();

                    //if (!res.ContentType.ToLower().StartsWith("text/"))
                    //{
                    //res.Headers["Content-Disposition"]
                    byte[] bs = new byte[4096];

                    Stream outStream = System.IO.File.Create(strFileSavePath);
                    Stream inStream = res.GetResponseStream();

                    int l;
                    do
                    {
                        l = inStream.Read(bs, 0, bs.Length);//每次读取指定的单位
                        if (l > 0)
                            outStream.Write(bs, 0, l);
                    } while (l > 0);

                    outStream.Close();
                    inStream.Close();
                    //}
                    break;
                default:
                    break;
            }
            res.Close();
            return backstr.ToString();
        }


        #endregion

        #region BT工厂 ASP.NET POST 方式提交数据！[同步版本]异步测试===================

        //private static ManualResetEvent allDone = new ManualResetEvent(false);
        private void btnBTYibu_Click(object sender, EventArgs e)
        {
            //测试BT
            string strPostURL = "http://www3.17domn.com/bt9/down.php";

            string strURL = "http://www3.17domn.com/bt9/file.php/MTOPZXB.html";

            string strPost = "type=torrent&id=MTOPZXB&name=MTOPZXB";

            RequestPostAsync(strPostURL, strURL, strPost, "E:\\MIDDXYN.torrent", null);
        }

        /// <summary>
        /// ASP.NET POST 方式提交数据！[同步版本]
        /// </summary>
        /// <param name="TheURL">Post提交的地址</param>
        /// <param name="strReferer">引用页地址</param>
        /// <param name="strPOST">Post提交的数据</param>
        /// <param name="cookies">登陆信息cookie</param>
        /// <returns></returns>
        public static void RequestPostAsync(string TheURL, string strReferer, string strPOST, string strFileSavePath, CookieCollection cookies)
        {
            try
            {
                string backstr = string.Empty;
                string url = TheURL;

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                string s = strPOST;

                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);

                req.ProtocolVersion = HttpVersion.Version11;
                req.ServicePoint.Expect100Continue = false;
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Headers["Accept-Charset"] = "GB2312,utf-8;q=0.7,*;q=0.7";
                req.Headers["Accept-Encoding"] = "gzip,deflate";
                req.Headers["Accept-Language"] = "zh-cn,zh;q=0.5";
                req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:6.0) Gecko/20100101 Firefox/6.0";
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                req.ContentLength = requestBytes.Length;
                req.Referer = strReferer;

                if (cookies != null)
                {
                    req.CookieContainer = new CookieContainer();
                    req.CookieContainer.Add(cookies);
                }

                RequestContext r = new RequestContext(req, requestBytes, strFileSavePath);


                req.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), r);
            }
            catch
            {

                throw;
            }

            //allDone.WaitOne();
        }

        private static void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                RequestContext rc = (RequestContext)asynchronousResult.AsyncState;

                HttpWebRequest request = rc.Request;

                // End the operation
                if (asynchronousResult != null)
                {

                    Stream postStream = request.EndGetRequestStream(asynchronousResult);

                    if (postStream != null)
                    {
                        byte[] byteArray = ((RequestContext)asynchronousResult.AsyncState).PostData;

                        // Write to the request stream.
                        postStream.Write(byteArray, 0, byteArray.Length);
                        postStream.Close();

                        // Start the asynchronous operation to get the response
                        request.BeginGetResponse(new AsyncCallback(GetResponseCallback), rc);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private static void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = ((RequestContext)asynchronousResult.AsyncState).Request;

                // End the operation
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                byte[] bs = new byte[4096];

                Stream outStream = System.IO.File.Create(((RequestContext)asynchronousResult.AsyncState).Filename);
                Stream inStream = response.GetResponseStream();

                int l;
                do
                {
                    l = inStream.Read(bs, 0, bs.Length);//每次读取指定的单位
                    if (l > 0)
                        outStream.Write(bs, 0, l);
                } while (l > 0);

                outStream.Close();
                inStream.Close();

                response.Close();
                //allDone.Set();
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region B2.比较导出没有下载成功的BT种子========================================
        private void btnSetDir_Click(object sender, EventArgs e)
        {
            //设置目录
            string strFloder = VTS.Common.VTSFormsCommon.GetFloderSelected(new FolderBrowserDialog());
            if (string.IsNullOrEmpty(strFloder))
            {
                MessageBox.Show("没有设置文件的目录！");
                return;
            }
            //获得lsBT名字列表
            List<string> ls = GetBTID(strFloder);
            string str = GetNolist(ls, this.afile);
            VTS.Common.VTSCommon.CreateFile(str.ToString(), "E:\\a.txt");
            System.Diagnostics.Process.Start("E:\\a.txt");
        }

        /// <summary>
        /// 获得BT目录下的所有BT种子的名字列表
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public List<string> GetBTID(string strPath)
        {
            List<string> ls = new List<string>();
            string[] arrFilelist = VTS.Common.VTSCommon.GetDirFile(strPath, "*.torrent", false);
            for (int i = 0; i < arrFilelist.Length; i++)
            {
                int index = arrFilelist[i].IndexOf('-');

                ls.Add(arrFilelist[i].Substring(index + 1).Replace(".torrent", ""));
            }
            return ls;
        }

        //AB子集查找函数，在集合A中将B全部剔除掉只留下C
        public string GetNolist(List<string> arrYuanlist, List<string> baocunlist)
        {
            StringBuilder sbr = new StringBuilder();
            StringBuilder sbr2 = new StringBuilder();//记录一些不存在的BT种子

            int count = 1;//记录在哪个地方发生了异常信息

            for (int i = 0; i < arrYuanlist.Count; i++)
            {
                try
                {
                    //在baocunlist查找时候有这个文件名字
                    var sps = (from p in baocunlist
                               where (new BTFuzhuaClass(p).GetBTData_Filename()) == arrYuanlist[i]
                               select p).First();

                    //序列不包含任何元素
                    /*
                     * B是A的一个子集， B在A中一定存在，这种情况下就不会抛出异常情况
                     * 用FirstOrDefault或者Find。First代表一定能找到，找不到就抛出异常
                     */

                    if (!string.IsNullOrEmpty(sps))
                    {
                        baocunlist.Remove(sps);
                        count++;
                    }
                }
                catch
                {
                    sbr2.Append(arrYuanlist[i] + Environment.NewLine);
                }
            }

            if (baocunlist.Count > 0)
            {
                foreach (var item in baocunlist)
                {
                    sbr.Append(item + Environment.NewLine);
                }
            }
            return sbr.ToString() + sbr2.ToString();
        }
        #endregion

        #region B3.通过BT种子分析类得到BT种子的真实名字================================
        private void btnGetBTName_Click(object sender, EventArgs e)
        {
            //导出文件的路径和真实的名字 641-MZDCLBH.torrent
            DataTable dt = new DataTable();

            DataColumn dc1 = new DataColumn("path", System.Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("主图", System.Type.GetType("System.String"));
            DataColumn dc3 = new DataColumn("名字", System.Type.GetType("System.String"));

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            DataRow dr;

            string[] alist = VTS.Common.VTSCommon.GetDirFile(this.txtDir.Text.Trim(), "*.torrent", true);

            for (int i = 0; i < alist.Length; i++)
            {
                TorrentFile torrentFile = new TorrentFile(alist[i]);
                dr = dt.NewRow();

                dr["path"] = alist[i];
                dr["主图"] = torrentFile.TorrentNameUTF8;
                dr["名字"] = torrentFile.TorrentName;

                dt.Rows.Add(dr);
                //sbr.Append(alist[i] + "|" + torrentFile.TorrentNameUTF8 + "|" + torrentFile.TorrentName + Environment.NewLine);
            }
            //CommonSpace.Conmmon.CreateFile(sbr.ToString(), this.txtDir.Text.Trim() + "into.txt", Encoding.Default);
            ExcelFile.SetData(dt, this.txtDir.Text.Trim() + "SpiderResult.xls", ExcelVersion.Excel8, HDRType.Yes);
            MessageBox.Show("ok");
            System.Diagnostics.Process.Start(this.txtDir.Text.Trim() + "SpiderResult.xls");
        }
        #endregion

        #region B4.BT种子的完整性检查==================================================

        private void btnCheckTorrent_Click(object sender, EventArgs e)
        {
            //假设一个BT种子没有下载完成 会怎样呢？
            TorrentFile torr = new TorrentFile(@"E:\T\BT包\019\吉泽明步.torrent");
            IList<TorrentFileInfo> ls = torr.TorrentFileInfo;
            List<string> lsP = new List<string>();
            foreach (var item in ls)
            {
                if (!lsP.Contains(item.Path))
                {
                    lsP.Add(item.Path);
                }
            }
            foreach (var item in lsP)
            {
                this.txtCode.AppendText(item + "\r\n");
            }

        }

        #endregion

        #region B5.去除重复数据【Email邮件筛选使用】===================================

        private void btnDel_Click(object sender, EventArgs e)
        {
            //需要支持Excel文件
            string strTxtpath = VTS.Common.VTSFormsCommon.GetFilePath(new OpenFileDialog(), "txt文件(*.txt)|*.txt|Excel2007文件(*.xlsx)|*.xlsx|Excel2003文件(*.xls)|*.xls");
            if (strTxtpath == "")
            {
                MessageBox.Show("未选择文件地址！");
                return;
            }

            //集合B
            List<string> ls = VTS.Common.ImportData.LoadDataFromfile(strTxtpath).ToList();
            //参数 集合B--集合A

            string str = VTS.Common.VTSCommon.CompareList(ls, this.afile);
            VTS.Common.VTSCommon.CreateFile(str.ToString(), "E:\\mail.txt");
            System.Diagnostics.Process.Start("E:\\mail.txt");

            //算法改进
            //找出相同的元素
            //没有在A里面的元素
        }
        #endregion

        #region 公共的默认辅助的函数
        //加载一个cookie
        public void LoadCookie()
        {
            string strCookiePath = AppDomain.CurrentDomain.BaseDirectory + "cookie.txt";
            this.txtCookie.Text = File.ReadAllText(strCookiePath, Encoding.Default).Trim();
        }
        #endregion

        #region Sunfile网盘，模块实现了一部分
        private void btnSunFileDownLoad_Click(object sender, EventArgs e)
        {
            //http://www.suwpan.com/file/07a39763f5920f32.html
            string strFilePath = "http://www.sufile.com/vip/dbc84da61b4daad1.html";
            //设置cookie
            string strCookies = this.txtCookie.Text.Trim();
            CookieCollection cookies = VTS.Common.NetHelper.CreateCookies(strCookies, "/", "www.sufile.com");
            string strCode = VTS.Common.NetHelper.HttpGet(strFilePath, cookies, 0, VTS.Common.NetHelper.BtUserAgent);
            this.txtCode.AppendText(strCode);
        }
        #endregion

        #region 测试
        private void btnParseTorrent_Click(object sender, EventArgs e)
        {
            string btFile = AppDomain.CurrentDomain.BaseDirectory + "b.torrent";

            TorrentFile torrent = new TorrentFile(btFile);

            MessageBox.Show(torrent.TorrentName);

            //string[] torrentList = VTS.Common.VTSCommon.GetDirFile(@"G:\T\04-BT", "*.torrent", true);
            //int i = 1;
            //foreach (var item in torrentList)
            //{
            //    TorrentFile torrent = new TorrentFile(item);
            //    this.txtCode.AppendText(i + "：" + torrent.TorrentName + Environment.NewLine);
            //    i++;
            //}
        }
        #endregion
    }

    #region Model信息

    //请求信息的上下文对象
    public class RequestContext
    {
        public HttpWebRequest Request = null;
        public byte[] PostData = null;
        public string Filename = string.Empty;
        public RequestContext(HttpWebRequest req, byte[] bytePostData, string strFilename)
        {
            this.Request = req;
            this.PostData = bytePostData;
            this.Filename = strFilename;
        }
    }

    //BT工厂辅助信息类
    public class BTFuzhuaClass
    {
        public string URL { get; set; }

        public BTFuzhuaClass(string strURL)
        {
            this.URL = strURL;
        }

        /// <summary>
        /// 获得BT种子的文件名字
        /// </summary>
        /// <returns></returns>
        public string GetBTData_Filename()
        {
            try
            {
                string str = System.IO.Path.GetFileName(this.URL).Replace(".html", "");
                return str;
            }
            catch
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 获得BTPOST
        /// </summary>
        /// <returns></returns>
        public string GetBTData_PostURL()
        {
            try
            {
                return this.URL.Substring(0, this.URL.LastIndexOf('/', this.URL.LastIndexOf('/') - 1)) + "/down.php";
            }
            catch
            {
                return Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 获得提交的POST数据
        /// </summary>
        /// <returns></returns>
        public string GetBTData_PostData()
        {
            string str = string.Empty;
            str = "type=torrent&id=" + GetBTData_Filename() + "&name=" + GetBTData_Filename();
            return str;
        }
    }

    /// <summary>
    /// 辅助信息类
    /// </summary>
    public class MyBTObjectInfo
    {
        public List<string> Lbtlist = new List<string>();

        public string strFloder = "D:\\BT\\";

        public string strLink = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="link"></param>
        public MyBTObjectInfo(List<string> ls, string link)
        {
            this.Lbtlist = ls;
            this.strLink = link;
        }

        /// <summary>
        /// 创建BT存储用的文件夹
        /// </summary>
        public void CreateFloder()
        {
            //获得需要创建的文件名字
            string strName = Path.GetFileName(strLink).Replace(".txt", "");

            //创建保存目录
            Directory.CreateDirectory(strFloder + strName);

            //并初始创建目录
            this.strFloder = strFloder + strName + "\\";
        }
    }
    #endregion
}
