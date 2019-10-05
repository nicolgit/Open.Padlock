using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace nicold.Padlock.Models.Services
{
    public class OneDrive: ICloudSignin
    {
        private string ClientID = "daec7b73-0f47-4ec7-b7b2-058f8d3a1f11"; // tenant nicola.delfino@outlook.com
        private string[] Scopes = { "Files.ReadWrite.All" };
        private IPublicClientApplication PCA = null;

        public OneDrive()
        { }

        private object parentwindow;
        object ICloudSignin.ParentWindow { get { return parentwindow; } set { parentwindow = value; } }

        void ICloudSignin.Initialize()
        {
            PCA = PublicClientApplicationBuilder.Create(ClientID).WithRedirectUri($"msal{ClientID}://auth").Build();
        }

        async Task<string> ICloudSignin.AcquireTokenAsync()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await PCA.GetAccountsAsync();

            try
            {
                try
                {
                    IAccount firstAccount = accounts.FirstOrDefault();
                    authResult = await PCA.AcquireTokenSilent(Scopes, firstAccount).ExecuteAsync();
                }
                catch (MsalUiRequiredException)
                {
                    try
                    {
                        authResult = await PCA.AcquireTokenInteractive(Scopes)
                                                  .WithParentActivityOrWindow(parentwindow)
                                                  .ExecuteAsync();
                    }
                    catch (Exception ex2)
                    {
                        throw new CloudSignInException("Acquire token interactive failed: " + ex2.Message);
                    }
                }
                
                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                  throw new CloudSignInException("Authentication failed: " + ex.Message);
            }
        }

        async Task<bool> ICloudSignin.SignOut()
        {
            foreach(var account in await PCA.GetAccountsAsync())
            {
                await PCA.RemoveAsync(account);
            }

            return true;
        }
    }
}
