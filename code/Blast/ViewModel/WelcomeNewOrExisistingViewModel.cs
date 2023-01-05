using Blast.Model.Services;
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
            if (result != null)
            {
                settings.FileName = result;

                current.File = new Models.DataFile.BlastDocument();
                await navigationService.GoToAsync($"//{nameof(View.MainPage)}");
            }
        }

        [RelayCommand]
        async Task Existing()
        {
            throw new NotImplementedException();
        }
    }
}