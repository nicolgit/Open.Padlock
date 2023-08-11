using Blast.ViewModel;

namespace Blast.View;

public partial class WelcomeSelectStoragePage : ContentPage
{
    private WelcomeSelectStorageViewModel viewModel;

	public WelcomeSelectStoragePage(WelcomeSelectStorageViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewModel = vm;
	}


    //private async void ButtonFile_Clicked(object sender, EventArgs e)
    //{
        
    //    string result = await DisplayPromptAsync("File System VAULT", "Choose a name for your VAULT", initialValue: "LocalVault.blast");
    //    if (result != null)
    //    {
    //        await viewModel.LocalFileCommand.ExecuteAsync(result);
    //    }
    //}

    //private async void ButtonOneDrive_Clicked(object sender, EventArgs e)
    //{
    //    string result = await DisplayPromptAsync("OneDrive VAULT", "Choose a name for your VAULT", initialValue: "OneDriveVault.blast");
    //    if (result != null)
    //    {
    //        await viewModel.OneDriveCommand.ExecuteAsync(result);
    //    }
    //}
}