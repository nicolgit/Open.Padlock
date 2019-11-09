﻿using Microsoft.Graph;
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
        public bool ShowWrongPasswordMessage { get; set; }
        public string Password { get; set; }
        #endregion

        #region COMMANDS
        public Command SubmitCommand => new Command(async () => await SubmitCommandImplementation());
        #endregion

        #region COMMANDS_IMPLEMENTATION
        private async Task SubmitCommandImplementation()
        {
            Globals.File = PadlockFileReader.OpenFile(Globals.FileEncrypted, Password);

        }
        #endregion
    }
}
