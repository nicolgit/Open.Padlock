using Blast.ViewModel;

namespace Blast.View;

public partial class CreatePasswordPage : ContentPage
{
    private CreatePasswordViewModel viewModel;

	public CreatePasswordPage(CreatePasswordViewModel vm)
	{
		InitializeComponent();
        BindingContext = viewModel = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await System.Threading.Tasks.Task.Delay(250);
            PasswordEntry.Focus();
        });
    }
}