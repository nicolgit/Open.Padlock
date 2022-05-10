using Blast.ViewModel;

namespace Blast.View;

public partial class Welcome02Page : ContentPage
{
	public Welcome02Page(Welcome02ViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }
}