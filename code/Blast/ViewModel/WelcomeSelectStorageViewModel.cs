﻿using Blast.UIServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Blast.ViewModel
{
    public partial class WelcomeSelectStorageViewModel : ViewModelBase
    {
        private Model.Services.Current current;
        private Model.Services.Settings settings;
        private INavigationService navigationService;

        public WelcomeSelectStorageViewModel(Model.Services.Current c, Model.Services.Settings s, INavigationService n)
        {
            current = c;
            settings = s;
            navigationService = n;
        }

        [RelayCommand]
        async Task LocalFile()
        {
            settings.StorageType = Model.Services.Settings.StorageEnum.STORAGE_LOCAL;
            await navigationService.GoToAsync($"//{nameof(View.WelcomeNewOrExistingPage)}");
            

            //settings.FileName = fileName;
            //settings.StorageType = Model.Services.Settings.StorageEnum.STORAGE_LOCAL;

            //current.CloudStorage = new Blast.Model.Services.Storage.LocalStorage();
            //current.CloudStorage.FileName = fileName;
            //current.RawFile = await current.CloudStorage.GetFileAsync();
            //current.File = new Models.DataFile.BlastDocument();

            //if (current.RawFile == null)
            //{
            //    // creating a new file
            //    await Shell.Current.GoToAsync($"//{nameof(View.CreatePasswordPage)}?NextPage={nameof(View.MainPage)}");
            //}
            //else
            //{
            //    // file exists
            //    await Shell.Current.GoToAsync($"//{nameof(View.TypePasswordPage)}?NextPage={nameof(View.MainPage)}");
            //}
        }

        [RelayCommand]
        async Task OneDrive()
        {
            settings.StorageType = Model.Services.Settings.StorageEnum.STORAGE_ONEDRIVE;
            await navigationService.GoToAsync($"//{nameof(View.WelcomeNewOrExistingPage)}");
        }
    }
}
