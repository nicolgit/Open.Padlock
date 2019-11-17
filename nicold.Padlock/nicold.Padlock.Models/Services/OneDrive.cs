﻿using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.IO;

namespace nicold.Padlock.Models.Services
{
    public class OneDrive: ICloudStorage
    {
        readonly private string Folder = "PadlockStorage";
        readonly private string File = "data.pz";
        readonly private int BufferSize = 1024 * 1024; // 1Mbyte

        readonly private string ClientID = "daec7b73-0f47-4ec7-b7b2-058f8d3a1f11"; // tenant nicola.delfino@outlook.com
        readonly private string[] Scopes = { "Files.ReadWrite.All" };
        
        private IPublicClientApplication PCA = null;

        public OneDrive()
        { }

        private object parentwindow;
        object ICloudStorage.ParentWindow { get { return parentwindow; } set { parentwindow = value; } }

        void ICloudStorage.Initialize()
        {
            PCA = PublicClientApplicationBuilder.Create(ClientID).WithRedirectUri($"msal{ClientID}://auth").Build();
        }

        async Task<string> ICloudStorage.AcquireTokenAsync()
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

        async Task<bool> ICloudStorage.SignOut()
        {
            foreach(var account in await PCA.GetAccountsAsync())
            {
                await PCA.RemoveAsync(account);
            }

            return true;
        }

        async Task<byte[]> ICloudStorage.GetPadlockFile()
        {
            var graphServiceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) => 
            {
                requestMessage
                    .Headers
                    .Authorization = new AuthenticationHeaderValue("bearer", await ((ICloudStorage)this).AcquireTokenAsync());
                return;
            }));

            // https://graph.microsoft.com/v1.0/me/drive/root:/documenti/test.txt:/content
            var fileStream = await graphServiceClient.Me.Drive.Root.ItemWithPath(Folder + "/" + File).Content.Request().GetAsync();

            byte[] buffer = new Byte[BufferSize];
            int bytesRead = await fileStream.ReadAsync(buffer, 0, BufferSize);

            byte[] buffer2 = new byte[bytesRead];
            Array.Copy(buffer, buffer2, bytesRead);

            return buffer2;
        }

    }
}