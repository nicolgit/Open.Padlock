using Blast.ViewModel;

namespace Blast.View;

public partial class WelcomePage : ContentPage
{
    private WelcomeViewModel viewmodel;
	public WelcomePage(WelcomeViewModel vm)
	{
		InitializeComponent();
        BindingContext = viewmodel = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Run(() => viewmodel.TryLoad());
    }
}