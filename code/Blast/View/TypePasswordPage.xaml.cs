using Blast.ViewModel;

namespace Blast.View;

public partial class TypePasswordPage : ContentPage
{
    private TypePasswordViewModel viewmodel;

    public TypePasswordPage(TypePasswordViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewmodel = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        viewmodel.Initialize();

        EntryPassword.Focus();
    }

    private async void Password_Completed(object sender, EventArgs e) {
        await viewmodel.OpenFile();
    }

    void Password_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        viewmodel.ErrorMessage = "";
    }
}