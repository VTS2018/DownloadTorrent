using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ReadTorrentFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var path =
                @"myfile.torrent";

            using (var reader = File.OpenText(path))
            using (var tr = new TorrentReader(reader))
            {
                Console.WriteLine(tr.ReadAsJson());
            }
        }
    }


    public class TorrentReader : IDisposable
    {
        private readonly TextReader reader;
        public TorrentReader(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            this.reader = reader;
        }


        public JToken ReadAsJson()
        {
            return ReadToken();
        }

        private JToken ReadToken()
        {
            var marker = ReadChar();
            switch (marker)
            {
                case char.MinValue:
                case 'e': // found end of object
                    return null;
                case 'i': // read integer.
                    return ReadNumber('e');
                case 'l':// read list
                    return ReadList();
                case 'd':// read dictionary
                    return ReadDictionary();
            }

            // is this string?
            if (marker >= '0' && marker <= '9')
            {
                var count = ReadNumber(':', marker);
                return ReadString(count.Value<int>());
            }
            throw new InvalidDataException("Unrecognized token");
        }


        private JValue ReadString(int count)
        {
            var buffer = new char[count];
            reader.Read(buffer, 0, count);
            return new JValue(new String(buffer));
        }


        private JObject ReadDictionary()
        {
            // read tokens until null
            var ret = new JObject();

            while (true)
            {
                var key = ReadToken();
                if (key == null)
                {
                    return ret;
                }
                var value = ReadToken();
                if (value == null)
                {
                    return ret;
                }
                ret.Add(key.Value<string>(), value);
            }
        }


        private JArray ReadList()
        {
            // read tokens until null
            var ret = new JArray();

            var value = ReadToken();
            while (value != null)
            {
                ret.Add(value);
                value = ReadToken();
            }
            return ret;
        }


        private JValue ReadNumber(char terminator, char? prefix = null)
        {
            var str = ReadUntil(terminator, prefix);
            int value;
            if (int.TryParse(str, out value))
            {
                return new JValue(value);
            }
            throw new InvalidDataException("can't convert string '" + str + "' into number");
        }


        private string ReadUntil(char terminator, char? prefix = null)
        {
            var sb = new StringBuilder();
            if (prefix.HasValue)
            {
                sb.Append(prefix.Value);
            }

            var ch = ReadChar();
            while (ch != terminator && ch != char.MinValue)
            {
                sb.Append(ch);
                ch = ReadChar();
            }
            return sb.ToString();
        }


        char ReadChar()
        {
            var buffer = new char[1];
            var len = reader.Read(buffer, 0, 1);
            if (len == 0)
            {
                return char.MinValue;
            }
            return buffer[0];
        }


        public void Dispose()
        {
            this.reader.Dispose();
        }
    }
}
