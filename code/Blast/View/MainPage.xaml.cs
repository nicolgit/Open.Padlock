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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        MessagingCenter.Subscribe<MainViewModel, string>(viewModel, viewModel.MESSAGE_OPENSEARCH, OnSearchOpen);
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            viewModel.OpenCommand.Execute(( Blast.ViewModel.Row.MainViewModelItem)e.SelectedItem);
        }
    }

    private void OnSearchOpen(MainViewModel arg1, string arg2)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            // https://github.com/xamarin/Xamarin.Forms/issues/2094
            await System.Threading.Tasks.Task.Delay(250);
            searchBar.Focus();
        });
    }

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}

