using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class CreatePasswordViewModel: ViewModelBase, IQueryAttributable
    {
        private Model.Services.PasswordsHelper passwordsHelper;
        private Model.Services.Current current;

        public CreatePasswordViewModel(
            Model.Services.PasswordsHelper ph,
            Model.Services.Current cu)
        {
            passwordsHelper = ph;
            current = cu;
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

        public string ComplexityText
        {
            get
            {
                if (Complexity < 1000000000)
                    return "less than a bilion iterations";
                else
                    return $"{Complexity.ToString("#,##0,,,B", CultureInfo.InvariantCulture)} (Billions) iterations";
            }
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Complexity))]
        [NotifyPropertyChangedFor(nameof(ComplexityText))]
        [NotifyPropertyChangedFor(nameof(NotAllowedChar))]
        [NotifyPropertyChangedFor(nameof(CanCreatePassword))]
        [NotifyPropertyChangedFor(nameof(IsPasswordBlank))]
        [NotifyPropertyChangedFor(nameof(IsPasswordVeryWeak))]
        [NotifyPropertyChangedFor(nameof(IsPasswordWeak))]
        [NotifyPropertyChangedFor(nameof(IsPasswordMedium))]
        [NotifyPropertyChangedFor(nameof(IsPasswordStrong))]
        [NotifyPropertyChangedFor(nameof(IsPasswordVeryStrong))]
        private string newPassword;

        public double Complexity
        {
            get
            {
                return passwordsHelper.BruteForceItearions(newPassword);
            }
        }

        public bool CanCreatePassword => passwordsHelper.CheckStrength(newPassword) >= Model.Services.PasswordsHelper.PasswordScore.Medium;

        [RelayCommand]
        async Task CreatePassword()
        {
            current.File = new Models.DataFile.BlastDocument();
            await Shell.Current.GoToAsync($"//{nameof(View.MainPage)}");
        }

    }
}
