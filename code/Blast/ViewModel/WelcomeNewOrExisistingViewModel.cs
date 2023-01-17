using Blast.Model.DataFile;
using Blast.Model.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace Blast.ViewModel
{
    /// <summary>
    /// Welcome01 is responsible to ask if user prefers create a new file or open an existing one
    /// </summary>
    public partial class WelcomeNewOrExisistingViewModel : ViewModelBase
    {
        private UIServices.IDialogService dialogService;
        private UIServices.INavigationService navigationService;
        private Settings settings;
        private Model.Services.Current current;

        public WelcomeNewOrExisistingViewModel(Model.Services.Settings s, Model.Services.Current c,
            UIServices.IDialogService ds, UIServices.INavigationService n)
        {
            settings = s;
            current = c;
            dialogService = ds;
            navigationService = n;
        }

        [RelayCommand]
        async Task New()
        {
            settings.FileName = null;
            string result = await dialogService.DisplayPromptAsync("File System VAULT", "Choose a name for your VAULT", initialValue: "LocalVault.blast");
            if (result != null && result.Length>0)
            {
                settings.FileName = result;

                current.CloudStorage = new Model.Services.Storage.LocalStorage();
                
                if (await current.CloudStorage.FileExistsAsync(settings.FileName) == false)
                {
                    settings.FileName = result;
                    current.File = new BlastFile();
                    current.Document = new Model.DataFile.BlastDocument();
                    await navigationService.GoToAsync($"//{nameof(View.CreatePasswordPage)}");
                }
                else
                {
                    await dialogService.DisplayAlertAsync("ERROR", $"file {result} already exists, please choose another name", "continue");
                }
            }
        }

        [RelayCommand]
        async Task Existing()
        {
            //settings.FileName = null;
            string result = await dialogService.DisplayPromptAsync("File System VAULT", "Choose the name for your VAULT", initialValue: "LocalVault.blast");
            if (result != null && result.Length > 0)
            {
                settings.FileName = result;

                current.CloudStorage = new Model.Services.Storage.LocalStorage();
                current.File = new BlastFile();
                
                if (await current.CloudStorage.FileExistsAsync(settings.FileName) == true)
                {
                    settings.FileName = result;

                    current.File = new BlastFile();
                    current.File.FileEncrypted = await current.CloudStorage.GetFileAsync(settings.FileName);
                    
                    await navigationService.GoToAsync($"//{nameof(View.TypePasswordPage)}");
                }
                else
                {
                    await dialogService.DisplayAlertAsync("ERROR", $"file {result} not found, please choose another name", "continue");
                }
            }
        }
    }
}