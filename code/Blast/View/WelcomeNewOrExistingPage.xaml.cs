using Blast.ViewModel;

namespace Blast.View;

public partial class WelcomeNewOrExistingPage : ContentPage
{
	public WelcomeNewOrExistingPage(WelcomeNewOrExisistingViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // skip this if already logged-in
        // await Shell.Current.GoToAsync("//MainPage");
    }
}