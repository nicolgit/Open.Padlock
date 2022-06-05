using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Model.Services.Storage
{
    public interface IBlastStorage
    {
        void Initialize();
        Task<string> AcquireTokenAsync();
        Task<bool> SignOutAsync();
        Task<byte[]> GetFileAsync();
        object ParentWindow { get; set; }
        public string FileName { get; set; }
    }
}
