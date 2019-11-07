using nicold.Padlock.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace nicold.Padlock.Models
{
    public static class Globals
    {
        public static Object  ParentWindow { get; set; }
        public static string AccessToken { get; set; }
        public static byte[] FileEncrypted { get; set; }
        public static string FileReadable { get; set; }

        public static ICloudStorage CloudStorage;
    }
}
