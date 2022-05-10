using Blast.Models.DataFile;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Blast.Models.Services
{
    public static class PadlockFileReader
    {
        public static DataFile.PadlockFile OpenFile(byte[] source, string unlockPassword)
        {
            EncrDecr ed;
            ed = new EncrDecr();

            byte[] crypt_key; // public key for encrypted doc
            byte[] crypt_iv;  // init vectory for encrypted doc
            
            crypt_key = new byte[EncrDecr.C_KEYSIZE];
            crypt_iv = new byte[EncrDecr.C_IVSIZE];

            using (MemoryStream memory = new MemoryStream(source))
            {
                // Use the memory stream in a binary reader.
                using (BinaryReader br = new BinaryReader(memory, System.Text.Encoding.UTF8))
                {
                    string tmp;

                    // read 'PASSWORD-'
                    tmp = br.ReadString();

                    // set password
                    int i;
                    for (i = 0; i < unlockPassword.Length && i < EncrDecr.C_KEYSIZE; i++)
                        crypt_key[i] = (byte)(unlockPassword[i] & 0xFF);

                    byte[] data = new Byte[memory.Length - (tmp.Length + 1) - (EncrDecr.C_KEYSIZE - i) - EncrDecr.C_IVSIZE];

                    // read crypt-key
                    for (; i < EncrDecr.C_KEYSIZE; i++)
                        crypt_key[i] = br.ReadByte();

                    // read crypt-iv
                    br.Read(crypt_iv, 0, EncrDecr.C_IVSIZE);

                    // read VerifyText
                    byte[] data_test = new Byte[C_VERIFYTEXT.Length * 2 + 16];
                    br.Read(data_test, 0, C_VERIFYTEXT.Length * 2 + 16);

                    string decrypted = ed.Decrypt(data_test, crypt_key, crypt_iv);
                    string verifyText = C_VERIFYTEXT;

                    int compare = decrypted.CompareTo(verifyText);
                    if (compare != 0)
                    {
                        // wrong password return null string
                        return null; 
                    }
                    else
                    {
                        // correct password return xml
                        byte[] data_frag = new Byte[C_BLOCKSIZE * 2 + 16];
                        int realsize;
                        System.Text.StringBuilder xml = new System.Text.StringBuilder(10000);

                        while ((realsize = br.Read(data_frag, 0, C_BLOCKSIZE * 2 + 16)) > 0)
                        {
                            int olds = xml.Length;
                            ed = new EncrDecr();

                            string element = ed.Decrypt(data_frag, crypt_key, crypt_iv);

                            xml.Append(element, 0, element.Length);
                        }

                        PadlockFile pf = ParsePazwordFile(xml.ToString());

                        //string json = JsonConvert.SerializeObject(pf);
                        //PadlockFile pf2 = JsonConvert.DeserializeObject<PadlockFile>(json);

                        return pf;
                    }
                }
            }
        }

        private static PadlockFile ParsePazwordFile(string file)
        {
            var xdoc = XDocument.Parse(file);

            PadlockFile pf = new PadlockFile();

            try
            {
                var info = xdoc.Root.Element("info");
                var guid = info.Attribute("guid").Value;
                var parse = Guid.Parse(guid);

                pf.Id = Guid.Parse(xdoc.Root.Element("info").Attribute("guid").Value);
                foreach (var node in xdoc.Root.Element("nodes").Elements("node"))
                {
                    var card = new DataFile.Card();

                    card.Id = Guid.Parse(node.Attribute("guid").Value);
                    card.Title = node.Attribute("title").Value;
                    card.IsFavotire = node.Attribute("favorite")!= null ? node.Attribute("favorite").Value.Equals("1") : false;
                    card.UsedCounter = node.Attribute("usedcounter") != null ? int.Parse((node.Attribute("usedcounter").Value)) : 0;
                    card.Notes = "";

                    if (node.Element("moreinfo") != null)
                    {
                        foreach (var row in node.Element("moreinfo").Elements("line"))
                        {
                            card.Notes += row.Value + "\r\n";
                        }
                    }

                    foreach (var attr in node.Elements("attribute"))
                    {
                        DataFile.Attribute attribute = new DataFile.Attribute();

                        if (attr.Attribute("value").Value == "")
                        {
                            attribute.Type = AttributeType.TYPE_HEADER;
                        }
                        else
                        {
                            if (attr.Attribute("type") != null)
                            {
                                if (attr.Attribute("type").Value == "password")
                                {
                                    attribute.Type = AttributeType.TYPE_PASSWORD;
                                }
                                if (attr.Attribute("type").Value == "generic")
                                {
                                    attribute.Type = AttributeType.TYPE_STRING;
                                }
                                if (attr.Attribute("type").Value == "URL")
                                {
                                    attribute.Type = AttributeType.TYPE_URL;
                                }
                            }
                        }
                        
                        attribute.Name = attr.Attribute("name").Value;
                        attribute.Value= attr.Attribute("value").Value;

                        card.Rows.Add(attribute);
                    }

                    pf.Cards.Add(card);
                }
            }
            catch 
            {

                
            }     

            return pf;
        }

        #region ENCRYPTION-DECRIPTION STUFF
        private const int C_KEYSIZE = 24;
        private const int C_IVSIZE = 8;
        private const int C_BLOCKSIZE = 2000;
        private const string C_HEX = "0123456789ABCDEF";
        //private const string C_VERIFYTEXT = "Era invevitabile: l'odore delle mandorle amare gli ricordava " +
        //   "sempre il destino degli amori contrastati. Il dottor Juvenal " +
        //   "Urbino lo sent� appena entrato nella casa ancora in penombra, " +
        //   "dove era accorso d'urgenza per occuparsi di un caso che per lui " +
        //   "aveva cessato di essere urgente da molti anni. Il rifugiato " +
        //   "antillano Jeremiah de Saint-Amour, invalido di guerra, foto- " +
        //   "grafo di bambini e il suo avversario di scacchi pi� pietoso, si era " +
        //   "messo in salvo dai tormenti della memoria con un suffumigio di " +
        //   "cianuro di oro.";

        private const string C_VERIFYTEXT = "Era invevitabile: l'odore delle mandorle amare gli ricordava sempre il destino degli amori contrastati. Il dottor Juvenal Urbino lo sentì appena entrato nella casa ancora in penombra, dove era accorso d'urgenza per occuparsi di un caso che per lui aveva cessato di essere urgente da molti anni. Il rifugiato antillano Jeremiah de Saint-Amour, invalido di guerra, foto- grafo di bambini e il suo avversario di scacchi più pietoso, si era messo in salvo dai tormenti della memoria con un suffumigio di cianuro di oro.";
        private static TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
        private static void BuildKey(string key)
        {
            alg.GenerateKey();
            alg.GenerateIV();

            byte[] k = alg.Key;

            int i;
            for (i = 0; i < key.Length && i < C_KEYSIZE; i++)
            {
                k[i] = (byte)(key[i] & 0xFF);
            }
            alg.Key = k;
        }
        public static byte[] Encrypt(string source, byte[] fullkey, byte[] iv)
        {
            byte[] bin = new byte[(2 * source.Length) + 16];
            byte[] bout = new byte[(2 * source.Length) + 16];
            System.IO.MemoryStream fout = new System.IO.MemoryStream(bout, 0, source.Length * 2 + 16, true, true);
            int i, j;

            fullkey = (byte[])fullkey.Clone();
            iv = (byte[])iv.Clone();

            // TEST (UNICODE): source="鯡鯢鯤鯥";

            // convert string to byte array
            for (i = j = 0; i < source.Length; i++, j += 2)
            {
                bin[j] = (byte)(source[i] & 0xFF);
                bin[j + 1] = (byte)((source[i] >> 8) & 0xFF);
            }

            CryptoStream encStream = new CryptoStream(fout, alg.CreateEncryptor(fullkey, iv), CryptoStreamMode.Write);
            encStream.Write(bin, 0, bin.Length);

            return bout;
        }
        public static string Decrypt(byte[] source, byte[] fullkey, byte[] iv)
        {
            byte[] bout = new byte[source.Length];
            System.IO.MemoryStream fout = new System.IO.MemoryStream(bout, 0, source.Length, true, true);
            int i;

            fullkey = (byte[])fullkey.Clone();
            iv = (byte[])iv.Clone();

            CryptoStream decStream = new CryptoStream(fout, alg.CreateDecryptor(fullkey, iv), CryptoStreamMode.Write);

            decStream.Write(source, 0, source.Length);

            string output;

            // convert byte array to string
            output = "";
            for (i = 0; i < bout.Length; i += 2)
            {
                output += (char)(bout[i] + (bout[i + 1] << 8));
            }

            //return output;
            return output.Substring(0, output.IndexOf('\0'));
        }        
        private static byte Hex2Byte(string sin)
        {
            return (byte)(C_HEX.LastIndexOf(sin[0]) * 16 + C_HEX.LastIndexOf(sin[1]));
        }
        #endregion
    }
}
