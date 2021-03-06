﻿using nicold.Padlock.Models;
using nicold.Padlock.Models.Services;
using nicold.Padlock.ViewModelsArtifacts;
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
            set { showWrongPasswordMessage=value; RaisePropertyChanged(() => ShowWrongPasswordMessage); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password=value; RaisePropertyChanged(() => Password); }
        }

        private bool isWorking;
        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; RaisePropertyChanged(() => IsWorking); }
        }
        #endregion

        #region COMMANDS
        public Command SubmitCommand => new Command(async () => await SubmitCommandImplementation());
        #endregion

        #region COMMANDS_IMPLEMENTATION
        private async Task SubmitCommandImplementation()
        {
            ShowWrongPasswordMessage = false;
            IsWorking = true;
            Globals.File = PadlockFileReader.OpenFile(Globals.FileEncrypted, Password);
            IsWorking = false;

            if (Globals.File != null)
            {
                // TODO use PushAsync
                Application.Current.MainPage = new AppShell();

                // this not working on android!
                //await Navigation.PushAsync(new AppShell());
            }
            else
            {
                ShowWrongPasswordMessage = true;
                MessagingCenter.Send(this, Messages.WRONGPASSWORD, "");
            }

            await Task.Delay(0); // need an await to avoid warning
        }
        #endregion
    }
}
