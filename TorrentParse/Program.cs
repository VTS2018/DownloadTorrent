using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BencodeNET.Exceptions;
using BencodeNET.IO;
using BencodeNET.Objects;
using BencodeNET.Parsing;
using BencodeNET.Torrents;

namespace TorrentParse
{
    class Program
    {
        static void Main(string[] args)
        {
            string torrentPath = AppDomain.CurrentDomain.BaseDirectory + "a.torrent";

            #region BencodeNET

            // Parse torrent by specifying the file path
            var parser = new BencodeParser(); // Default encoding is Encoding.UT8F, but you can specify another if you need to

            //非BT种子文件 会解析异常
            var torrent = parser.Parse<Torrent>(torrentPath);

            string MagnetLink = torrent.GetMagnetLink(MagnetLinkOptions.None);

            Console.WriteLine(MagnetLink);

            Console.WriteLine(torrent.CreationDate);

            #endregion

            MonoTorrent.Common.Torrent mtorrent = MonoTorrent.Common.Torrent.Load(torrentPath);

            string link1 = "magnet:?xt=urn:btih:5623641b93b175e9b2c8fb0466427ca59de25d32&dn=SAMA-327r-avi";

            string magnetLink = "magnet:?xt=urn:btih:" + BitConverter.ToString(mtorrent.InfoHash.ToArray()).Replace("-", "");

            Console.WriteLine(magnetLink);


            MonoTorrent.MagnetLink link = new MonoTorrent.MagnetLink(link1);

            Console.WriteLine(link.Name);
        }
    }
}