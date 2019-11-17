using Microsoft.Graph;
using nicold.Padlock.Models;
using nicold.Padlock.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class TypePasswordViewModel:BaseViewModel
    {
        public TypePasswordViewModel(INavigation navigation): base(navigation)
        {
            ShowWrongPasswordMessage = false;
            Password = "";
        }

        #region PROPERTIES
        private bool showWrongPasswordMessage;
        public bool ShowWrongPasswordMessage
        {
            get { return showWrongPasswordMessage; }
            set { SetProperty(ref showWrongPasswordMessage, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        #endregion

        #region COMMANDS
        public Command SubmitCommand => new Command(async () => await SubmitCommandImplementation());
        #endregion

        #region COMMANDS_IMPLEMENTATION
        private async Task SubmitCommandImplementation()
        {
            Globals.File = PadlockFileReader.OpenFile(Globals.FileEncrypted, Password);

            if (Globals.File != null)
            {
                MessagingCenter.Send(this, Messages.FILEOPEN, "");
                await Navigation.PopModalAsync();
            }
            else
            {
                ShowWrongPasswordMessage = true;
            }

        }
        #endregion
    }
}
