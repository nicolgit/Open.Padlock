using Blast.ViewModel;

namespace Blast.View;

public partial class Welcome01Page : ContentPage
{
	public Welcome01Page(Welcome01ViewModel vm)
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
    private async void Button_Clicked(object sender, EventArgs e)
    {
		// remove the back stack and go to next page
		await Shell.Current.GoToAsync($"//{nameof(View.Welcome02Page)}");
    }
}