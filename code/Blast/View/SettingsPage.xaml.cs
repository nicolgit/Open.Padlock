using Blast.ViewModel;

namespace Blast.View;

public partial class SettingsPage : ContentPage
{
	private ViewModel.SettingsViewModel vm;

    public SettingsPage(ViewModel.SettingsViewModel settingsViewModel)
	{
		InitializeComponent();

		vm = settingsViewModel;
		BindingContext = vm;
	}
    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        vm.SaveAllCommand.Execute(null);
    }

    private void Theme_Changed(object sender, EventArgs e)
    {
        vm.ThemeChangedCommand.Execute(null);
    }
}