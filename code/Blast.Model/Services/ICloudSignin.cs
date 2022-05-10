using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Models.Services
{
    public interface ICloudStorage
    {
        void Initialize();
        Task<string> AcquireTokenAsync();
        Task<bool> SignOut();
        Task<byte[]> GetPadlockFile();
        Object ParentWindow { get; set; }
    }
}
