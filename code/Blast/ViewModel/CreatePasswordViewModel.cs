using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class CreatePasswordViewModel: ViewModelBase, IQueryAttributable
    {
        private Model.Services.PasswordsHelper passwordsHelper;
        public CreatePasswordViewModel(Model.Services.PasswordsHelper ph)
        {
            passwordsHelper = ph;
        }           

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public bool NotAllowedChar => !passwordsHelper.OnlyAllowedChars(newPassword);

        public string AllowedSymbols => Model.Services.PasswordsHelper.AllowedSymbols;

        public bool IsPasswordBlank => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.Blank;
        public bool IsPasswordVeryWeak => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.VeryWeak;
        public bool IsPasswordWeak => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.Weak;
        public bool IsPasswordMedium => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.Medium;
        public bool IsPasswordStrong => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.Strong;
        public bool IsPasswordVeryStrong => passwordsHelper.CheckStrength(newPassword) == Model.Services.PasswordsHelper.PasswordScore.VeryStrong;

        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(Complexity))]
        [AlsoNotifyChangeFor(nameof(NotAllowedChar))]
        [AlsoNotifyChangeFor(nameof(IsPasswordBlank))]
        [AlsoNotifyChangeFor(nameof(IsPasswordVeryWeak))]
        [AlsoNotifyChangeFor(nameof(IsPasswordWeak))]
        [AlsoNotifyChangeFor(nameof(IsPasswordMedium))]
        [AlsoNotifyChangeFor(nameof(IsPasswordStrong))]
        [AlsoNotifyChangeFor(nameof(IsPasswordVeryStrong))]
        private string newPassword;

        public string Complexity
        {
            get
            {
                return $"{passwordsHelper.BruteForceItearions(newPassword)} iterations";
            }
        }
    }
}
