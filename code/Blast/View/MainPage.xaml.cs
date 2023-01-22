using Blast.ViewModel;

namespace Blast.View;

public partial class MainPage : ContentPage
{
	ViewModel.MainViewModel viewmodel;

	public MainPage( MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewmodel = vm;
	}
}

