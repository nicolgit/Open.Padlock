using Blast.Model.Services.Storage;
using Blast.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blast.Model.Services
{
    public class Current
    {
        //public object ParentWindow { get; set; }
        //public string AccessToken { get; set; }
        public byte[] FileEncrypted { get; set; }
        public string FileReadable { get; set; }
        public byte[] RawFile { get; set; }
        public Models.DataFile.BlastDocument File { get; set; }

        public IBlastStorage CloudStorage;
    }
}
