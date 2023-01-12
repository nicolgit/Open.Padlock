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
        public Task<byte[]> GetFileAsync(string fileName);
        public Task<bool> FileExistsAsync(string fileName);
        object ParentWindow { get; set; }

    }
}
