using Microsoft.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blast.Model.DataFile
{
    public class BlastFile
    {
        private const string FF_PAZWORD = "PASSWORD-";
        private const string FF_BLAST01 = "BLAST01-";
        private static string[] fileFormats = { FF_PAZWORD, FF_BLAST01 };

        public string Password { get; set; }

        public byte[] FileEncrypted { get; set; }
        public string FileReadable { get; set; }

        public BlastFile() { }

        /// <summary>
        /// try to decrypt FileEncrypted using FilePassword 
        /// 
        /// if it works
        ///     FileReadable contains the result in string format
        ///     returns blastDocument contains the result in blast format 
        /// </summary>
        public BlastDocument GetBlastDocument()
        {
            using (MemoryStream encryptedFileInMemory = new MemoryStream(FileEncrypted))
            {
                // Use the encryptedFileInMemory stream in a binary reader.
                using (BinaryReader br = new BinaryReader(encryptedFileInMemory, Encoding.UTF8))
                {
                    

                    string fileTypeIdentifier;

                    // read 'PASSWORD-'
                    fileTypeIdentifier = br.ReadString();
                    if (string.IsNullOrEmpty(fileTypeIdentifier)) throw new Exceptions.BlastFileEmptyException();
                    switch (fileTypeIdentifier)
                    {
                        case FF_PAZWORD:
                            return parsePazwordFile(decryptPazwordFile (encryptedFileInMemory, fileTypeIdentifier, br));
                        case FF_BLAST01:
                            break;
                        default:
                            throw new Exceptions.BlastFileFormatException();
                    }
                    
                    return JsonSerializer.Deserialize<BlastDocument>(FileReadable);
                }
            }
        }

        private BlastDocument GetPazwordDocument()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// encrypt document using FilePassword
        /// 
        /// if it works
        ///     FileReadable contains the document in string format
        ///     FileEncrypted contains the document encrypted
        /// </summary>
        /// <param name="document"></param>
        /// <returns>raise an exception in case of failure</returns>
        public void PutBlastDocument(BlastDocument document)
        {
            /*
            int item;

            this.FileReadable = JsonSerializer.Serialize(document);

            byte[] crypt_key; // public key for encrypted doc
            byte[] crypt_iv;  // init vector for encrypted doc

            crypt_key = new byte[C_KEYSIZE];
            crypt_iv = new byte[C_IVSIZE];

            // generate random crypt_key and crypt_iv
            var rand = new Random();
            for (item = 0; item < C_KEYSIZE; item++)
                crypt_key[item] = (byte)rand.Next(255);
            for (item = 0; item < C_IVSIZE; item++)
                crypt_iv[item] = (byte)rand.Next(255);

            // use password as crypto KEY 
            for (item = 0; item < Password.Length && item < C_KEYSIZE; item++)
                crypt_key[item] = (byte)(Password[item] & 0xFF);

            System.IO.MemoryStream outputStream = new System.IO.MemoryStream();
            System.IO.BinaryWriter outputWriter = new System.IO.BinaryWriter(outputStream, System.Text.Encoding.UTF8);

            // writes file type identifier
            outputWriter.Write(FF_BLAST01);

            // writes the crypto_key bytes that are not the password in the file
            for (item = this.Password.Length; item < C_KEYSIZE; item++)
                outputWriter.Write(crypt_key[item]);

            // writes the crypt_iv in the file
            outputWriter.Write(crypt_iv);

            //writes the encrypted version of VERIFYTEXT
            outputWriter.Write(encryptString(C_VERIFYTEXT, crypt_key, crypt_iv));

            // writes the encrypted version of FileReadable
            for (item = 0; item < FileReadable.Length; item += C_BLOCKSIZE)
            {
                if (item + C_BLOCKSIZE <= FileReadable.Length)
                {
                    outputWriter.Write(encryptString(FileReadable.Substring(item, C_BLOCKSIZE), crypt_key, crypt_iv));
                }
                else
                {
                    outputWriter.Write(encryptString(FileReadable.Substring(item), crypt_key, crypt_iv));
                }
            }

            outputWriter.Close();
            this.FileEncrypted = outputStream.ToArray();
            outputStream.Close();
            */
        }

        #region PAZWORD FILE FORMAT READER
        private BlastDocument parsePazwordFile(string file)
        {
            var xdoc = XDocument.Parse(file);

            var pf = new BlastDocument();

            try
            {
                var info = xdoc.Root.Element("info");
                var guid = info.Attribute("guid").Value;
                var parse = Guid.Parse(guid);

                pf.Id = Guid.Parse(xdoc.Root.Element("info").Attribute("guid").Value);
                foreach (var node in xdoc.Root.Element("nodes").Elements("node"))
                {
                    var card = new Card();

                    card.Id = Guid.Parse(node.Attribute("guid").Value);
                    card.Title = node.Attribute("title").Value;
                    card.IsFavotire = node.Attribute("favorite") != null ? node.Attribute("favorite").Value.Equals("1") : false;
                    card.UsedCounter = node.Attribute("usedcounter") != null ? int.Parse(node.Attribute("usedcounter").Value) : 0;
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
                        Attribute attribute = new Attribute();

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
                        attribute.Value = attr.Attribute("value").Value;

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
        private string decryptPazwordFile(MemoryStream encryptedFileInMemory, string fileTypeIdentifier, BinaryReader br)
        {
            TripleDES algorithm = TripleDES.Create();

            byte[] crypt_key; // public key for encrypted doc
            byte[] crypt_iv;  // init vector for encrypted doc

            crypt_key = new byte[algorithm.Key.Length];
            crypt_iv = new byte[algorithm.IV.Length];

            // use password as crypto KEY 
            int i;
            for (i = 0; i < Password.Length && i < algorithm.Key.Length; i++)
                crypt_key[i] = (byte)(Password[i] & 0xFF);

            byte[] data = new byte[encryptedFileInMemory.Length - (fileTypeIdentifier.Length + 1) - (algorithm.Key.Length - i) - algorithm.IV.Length];

            // read remaining crypto key
            for (; i < algorithm.Key.Length; i++)
                crypt_key[i] = br.ReadByte();

            // read crypt-iv
            br.Read(crypt_iv, 0, algorithm.IV.Length);

            // read encrypted VerifyText
            byte[] data_test = new byte[C_VERIFYTEXT.Length * 2 + 16];
            br.Read(data_test, 0, C_VERIFYTEXT.Length * 2 + 16);

            string decrypted = decryptBytes(algorithm, data_test, crypt_key, crypt_iv);
            string verifyText = C_VERIFYTEXT;

            StringBuilder readableFileString = new StringBuilder(10000);

            int compare = decrypted.CompareTo(verifyText);
            if (compare != 0)
            {
                throw new Exceptions.BlastFileWrongPasswordException();
            }

            // password is correct, returning readable file
            byte[] data_frag = new byte[C_BLOCKSIZE * 2 + 16];
            int realSize;

            while ((realSize = br.Read(data_frag, 0, C_BLOCKSIZE * 2 + 16)) > 0)
            {
                int olds = readableFileString.Length;

                string element = decryptBytes(algorithm, data_frag, crypt_key, crypt_iv);

                readableFileString.Append(element, 0, element.Length);
            }

            FileReadable = readableFileString.ToString();

            return FileReadable;
        }
        #endregion

        #region ENCRYPTION-DECRIPTION STUFF
        private const int C_BLOCKSIZE = 2000;
        private const string C_VERIFYTEXT = "Era invevitabile: l'odore delle mandorle amare gli ricordava sempre il destino degli amori contrastati. Il dottor Juvenal Urbino lo sentì appena entrato nella casa ancora in penombra, dove era accorso d'urgenza per occuparsi di un caso che per lui aveva cessato di essere urgente da molti anni. Il rifugiato antillano Jeremiah de Saint-Amour, invalido di guerra, foto- grafo di bambini e il suo avversario di scacchi più pietoso, si era messo in salvo dai tormenti della memoria con un suffumigio di cianuro di oro.";

        private static byte[] encryptString(SymmetricAlgorithm algorithm, string source, byte[] fullKey, byte[] iv)
        {
            byte[] bin = new byte[2 * source.Length + 16];
            byte[] bout = new byte[2 * source.Length + 16];
            MemoryStream memoryStream = new MemoryStream(bout, 0, source.Length * 2 + 16, true, true);
            int i, j;

            fullKey = (byte[])fullKey.Clone();
            iv = (byte[])iv.Clone();

            // TEST (UNICODE): source="鯡鯢鯤鯥";

            // convert string to byte array
            for (i = j = 0; i < source.Length; i++, j += 2)
            {
                bin[j] = (byte)(source[i] & 0xFF);
                bin[j + 1] = (byte)(source[i] >> 8 & 0xFF);
            }

            CryptoStream encStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(fullKey, iv), CryptoStreamMode.Write);
            encStream.Write(bin, 0, bin.Length);

            return bout;
        }

        private static string decryptBytes(SymmetricAlgorithm algorithm, byte[] source, byte[] fullKey, byte[] iv)
        {
            byte[] bout = new byte[source.Length];
            MemoryStream memoryStream = new MemoryStream(bout, 0, source.Length, true, true);
            int i;

            fullKey = (byte[])fullKey.Clone();
            iv = (byte[])iv.Clone();

            CryptoStream decStream = new CryptoStream(memoryStream, algorithm.CreateDecryptor(fullKey, iv), CryptoStreamMode.Write);

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



        #endregion
    }

}

