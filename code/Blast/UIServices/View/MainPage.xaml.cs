using Blast.ViewModel;

namespace Blast.View;

public partial class MainPage : ContentPage
{
	ViewModel.MainViewModel viewModel;

	public MainPage( MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = viewModel = vm;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		viewModel.Initialize();
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            viewModel.OpenCommand.Execute( (Model.DataFile.Card)e.SelectedItem);
        }
    }
}

