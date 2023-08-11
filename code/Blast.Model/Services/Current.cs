using Blast.Model.Services.Storage;
using Blast.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blast.Model.Services
{
    public class Current
    {
        public Current()
        {
        }

        public string ApplicationVersion = "0.0.1"; // DO NOT CHANGE - updated during the build process

        public Model.DataFile.Card Card { get; set; }
        public Model.DataFile.BlastDocument Document { get; set; }
        public Model.DataFile.BlastFile File { get; set; }
        public IBlastStorage CloudStorage;
    }
}
