using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.IO;
using Blast.Models;

namespace Blast.Model.Services.Storage
{
    public class OneDriveStorage : IBlastStorage
    {
        readonly private string Folder = "PadlockStorage";
        readonly private string File = "data.pz";
        readonly private int BufferSize = 1024 * 1024; // 1Mbyte

        // the same clied ID must present in AndroidManifest.xml FILE
        // for reference: <data android:scheme="msal4926943b-ab46-4176-a639-ef6438ceb351" android:host="auth" />
        readonly private string ClientID = "4926943b-ab46-4176-a639-ef6438ceb351"; // tenant nicoladelfinooutlook.onmicrosoft.com (dac2b1d5-5420-4fad-889e-1280ffdc8003)
        readonly private string[] Scopes = { "Files.ReadWrite.All" };

        private IPublicClientApplication PCA = null;

        public OneDriveStorage()
        { }

        private object parentwindow;
        object IBlastStorage.ParentWindow { get { return parentwindow; } set { parentwindow = value; } }

        void IBlastStorage.Initialize()
        {
            PCA = PublicClientApplicationBuilder
                .Create(ClientID)
                .WithRedirectUri($"msal{ClientID}://auth")
                .WithAuthority(AadAuthorityAudience.PersonalMicrosoftAccount)
                .Build();
        }

        async Task<string> IBlastStorage.AcquireTokenAsync()
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

        async Task<bool> IBlastStorage.SignOutAsync()
        {
            foreach (var account in await PCA.GetAccountsAsync())
            {
                await PCA.RemoveAsync(account);
            }

            return true;
        }

        async Task<byte[]> IBlastStorage.GetFileAsync(string fileName)
        {
            var graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
            {
                requestMessage
                    .Headers
                    .Authorization = new AuthenticationHeaderValue("bearer", await ((IBlastStorage)this).AcquireTokenAsync());
                return;
            }));

            // https://graph.microsoft.com/v1.0/me/drive/root:/documenti/test.txt:/content
            var fileStream = await graphServiceClient.Me.Drive.Root.ItemWithPath(Folder + "/" + File).Content.Request().GetAsync();

            byte[] buffer = new byte[BufferSize];
            int bytesRead = await fileStream.ReadAsync(buffer, 0, BufferSize);

            byte[] buffer2 = new byte[bytesRead];
            Array.Copy(buffer, buffer2, bytesRead);

            return buffer2;
        }

        Task<bool> IBlastStorage.FileExistsAsync(string fileName)
        {
            //TODO implement fileexistsasync here
            throw new NotImplementedException();
        }
    }
}
