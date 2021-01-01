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

        #region PROPERTIES
        private bool isWorking;
        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; RaisePropertyChanged(() => IsWorking); }
        }

        public bool IsAuthenticated => Globals.AccessToken != null;
        #endregion

        #region COMMANDS
        public Command SignInCommand { get; set; }
        #endregion

        private async Task SignInCommandImplementation()
        {
            Globals.AccessToken = await Models.Globals.CloudStorage.AcquireTokenAsync();
            
            if (IsAuthenticated)
            {
                IsWorking = true;
                Globals.FileEncrypted = await Globals.CloudStorage.GetPadlockFile();
                IsWorking = false;
                
                await Navigation.PushAsync(new TypePasswordPage());
            }
        }

        
    }
}
