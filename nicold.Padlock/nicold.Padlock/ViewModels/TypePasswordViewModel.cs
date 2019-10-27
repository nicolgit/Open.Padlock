using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace nicold.Padlock.ViewModels
{
    public class TypePasswordViewModel:BaseViewModel
    {
        public TypePasswordViewModel(INavigation navigation): base(navigation)
        {
            ShowWrongPassword = false;
            Password = "";
        }

        #region PROPERTIES
        public bool ShowWrongPassword { get; set; }
        public string Password { get; set; }
        #endregion
    }
}
