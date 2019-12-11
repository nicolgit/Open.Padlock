using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using nicold.Padlock.Views;

namespace nicold.Padlock.ViewModels
{
    public class SignInViewModel: BaseViewModel
    {
        public SignInViewModel(INavigation navigation) : base (navigation)
        {
            SignInCommand = new Command(async () => await SignInCommandImplementation());
        }

        #region COMMANDS
        public Command SignInCommand { get; set; }
        #endregion

        private async Task SignInCommandImplementation()
        {
            Globals.AccessToken = await Models.Globals.CloudStorage.AcquireTokenAsync();
            
            if (IsAuthenticated)
            {
                Globals.FileEncrypted = await Globals.CloudStorage.GetPadlockFile();
                await Navigation.PushAsync(new TypePasswordPage());
            }
        }

        public bool IsAuthenticated => Globals.AccessToken != null;
    }
}
