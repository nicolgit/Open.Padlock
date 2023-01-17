using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public partial class TypePasswordViewModel: ViewModelBase
    {
        private Model.Services.Settings settings;
        private Model.Services.Current current;

        public TypePasswordViewModel(Model.Services.Current c,
            Model.Services.Settings s)
        {
            current = c;
            settings = s;

            password = errorMessage = "";            
        }

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        async Task OpenFile()
        {
            current.File.Password = password;

            try
            {
                current.Document = current.File.GetBlastDocument();
                settings.SaveAll();
                await Shell.Current.GoToAsync($"//{nameof(View.MainPage)}");
            }
            catch (Model.Exceptions.BlastFileWrongPasswordException)
            {
                ErrorMessage = "WRONG Password";
            }
            


           
        }
    }
}
