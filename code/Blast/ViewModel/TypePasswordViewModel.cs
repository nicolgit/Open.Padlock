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
        private UIServices.INavigationService navigationService;

        public TypePasswordViewModel(Model.Services.Current c, Model.Services.Settings s, UIServices.INavigationService n)
        {
            current = c;
            settings = s;
            navigationService = n;

            password = errorMessage = "";            
        }

        internal void Initialize()
        {
            Password = "";
            FileURI = $"{settings.StorageType.ToString()} - {settings.FileName}"; 
        }

        [ObservableProperty]
        private string fileURI;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        [RelayCommand]
        public async Task OpenFile()
        {
            current.File.Password = Password;

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

        [RelayCommand]
        async Task Cancel() {
            settings.FileName = "";
            settings.StorageType = Model.Services.Settings.StorageEnum.NONE;
            settings.SaveAll();

            current.CloudStorage = null;
            current.Document = new Model.DataFile.BlastDocument();
            current.File = new Model.DataFile.BlastFile();

            await navigationService.GoToViewModelAsync(nameof(WelcomeSelectStorageViewModel));
        }
    }
}
