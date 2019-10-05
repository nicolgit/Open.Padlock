using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nicold.Padlock.Models.Services
{
    public interface ICloudSignin
    {
        void Initialize();

        Task<string> AcquireTokenAsync();
        Task<bool> SignOut();

        Object ParentWindow { get; set; }
    }
}
