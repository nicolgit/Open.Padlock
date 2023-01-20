using Blast.Model.DataFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blast.ViewModel
{
    public class WelcomeViewModel:ViewModelBase
    {
        Model.Services.Current current;
        Model.Services.Settings settings;
        UIServices.INavigationService navigationService;

        public WelcomeViewModel(
            Model.Services.Settings s,
            Model.Services.Current c,
            UIServices.INavigationService n
            )
        {
            current = c;
            settings = s;
            navigationService = n;
        }

        public async Task TryLoad()
        {
            Task.Delay(5000).Wait();

            try
            {
                switch (settings.StorageType)
                {
                    case Model.Services.Settings.StorageEnum.STORAGE_LOCAL:
                        current.CloudStorage = new Model.Services.Storage.LocalStorage();
                        break;
                    case Model.Services.Settings.StorageEnum.STORAGE_ONEDRIVE:
                        current.CloudStorage = new Model.Services.Storage.OneDriveStorage();
                        break;
                    default:
                        await navigationService.GoToViewModelAsync(nameof(WelcomeSelectStorageViewModel));
                        break;
                }
            }
            catch (Exception)
            {
                await navigationService.GoToViewModelAsync(nameof(WelcomeSelectStorageViewModel));
            }
            
            try
            {
                if (!string.IsNullOrEmpty(settings.FileName))
                {
                    current.File = new BlastFile();
                    current.File.FileEncrypted = await current.CloudStorage.GetFileAsync(settings.FileName);

                    await navigationService.GoToViewModelAsync(nameof(TypePasswordViewModel));
                }
                else
                    await navigationService.GoToViewModelAsync(nameof(WelcomeNewOrExisistingViewModel));
            }
            catch (Exception)
            {
                await navigationService.GoToViewModelAsync(nameof(WelcomeNewOrExisistingViewModel));
            }

        }
    }
}
