using Blast.ViewModel;

namespace Blast.View;

public partial class CardViewPage : ContentPage
{
    private CardViewViewModel viewModel;

    public CardViewPage(CardViewViewModel vm)
	{
		InitializeComponent();
        BindingContext = viewModel = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        viewModel.Initialize();
    }

}