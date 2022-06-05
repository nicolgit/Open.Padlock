using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blast.Model.Services.Storage
{
    public class LocalStorage : IBlastStorage
    {
        object IBlastStorage.ParentWindow { get => null; set { } }

        private string fileName;
        string IBlastStorage.FileName { get => fileName; set => fileName=value; }

        Task<string> IBlastStorage.AcquireTokenAsync()
        {
            return Task.FromResult("");
        }

        async Task<byte[]> IBlastStorage.GetFileAsync()
        {
            string fullPath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            try
            {
                var bytes = await System.IO.File.ReadAllBytesAsync(fullPath);
                return bytes;
            } catch (System.IO.FileNotFoundException)
            {
                return null;
            } catch 
            {
                throw;
            }
        }

        void IBlastStorage.Initialize()
        {
        }

        Task<bool> IBlastStorage.SignOutAsync()
        {
            return Task.FromResult(true);
        }
    }
}
