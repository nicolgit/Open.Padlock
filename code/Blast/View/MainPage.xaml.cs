using Blast.ViewModel;
using CommunityToolkit.Mvvm.Messaging;

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

        WeakReferenceMessenger.Default.Register<MainViewModel.OpenSearchBarMessage>(this, (r, m) =>
        {
            // if I am showing the searchBar, set also the focus on it
            if(m.Value == true)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    // https://github.com/xamarin/Xamarin.Forms/issues/2094
                    await System.Threading.Tasks.Task.Delay(250);
                    searchBar.Focus();
                });
            }
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<MainViewModel.OpenSearchBarMessage>(this);
    }

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            viewModel.OpenCommand.Execute(( Blast.ViewModel.Row.MainViewModelItem)e.SelectedItem);
        }
    }

    private void searchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        viewModel.LoadCardsCommand.Execute(null);
    }
}

