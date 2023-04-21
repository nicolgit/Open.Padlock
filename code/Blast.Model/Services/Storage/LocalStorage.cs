using Microsoft.Maui.Storage;
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

        Task<string> IBlastStorage.AcquireTokenAsync()
        {
            return Task.FromResult("");
        }

        async Task<byte[]> IBlastStorage.ReadFileAsync(string fileName)
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

        Task<bool> IBlastStorage.FileExistsAsync(string fileName)
        {
            string fullPath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            return Task.FromResult(System.IO.File.Exists(fullPath));
        }

        async Task IBlastStorage.WriteFileAsync(string fileName, byte[] data)
        {
            string fullPath = Path.Combine(FileSystem.Current.AppDataDirectory, fileName);
            await System.IO.File.WriteAllBytesAsync(fullPath, data);
            return;
        }

        Task<string> IBlastStorage.GetFileURI(string fileName)
        {
            return Task.FromResult(Path.Combine(FileSystem.Current.AppDataDirectory, fileName));
        }
    }
}
