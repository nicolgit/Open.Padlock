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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        Task.Run(() => viewmodel.TryLoad());
    }
}