using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Model.Services.Storage
{
    public interface IBlastStorage
    {
        public void Initialize();
        public Task<string> AcquireTokenAsync();
        public Task<bool> SignOutAsync();
        public Task<byte[]> ReadFileAsync(string fileName);
        public Task WriteFileAsync(string fileName, byte[] data);

        public Task<bool> FileExistsAsync(string fileName);
        object ParentWindow { get; set; }

    }
}
