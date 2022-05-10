using Blast.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blast.Models
{
    public static class Globals
    {
        public static Object  ParentWindow { get; set; }
        public static string AccessToken { get; set; }
        public static byte[] FileEncrypted { get; set; }
        public static string FileReadable { get; set; }
        public static DataFile.PadlockFile File { get; set; }

        public static ICloudStorage CloudStorage;
    }
}
