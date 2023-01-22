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
    }
}