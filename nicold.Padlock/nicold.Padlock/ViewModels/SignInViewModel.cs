using nicold.Padlock.ViewModelsArtifacts;
using nicold.Padlock.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
                MessagingCenter.Send(this, Messages.SIGNIN, "");
                await Navigation.PopModalAsync();
            }
        }

        public bool IsAuthenticated => Globals.AccessToken != null;
    }
}
