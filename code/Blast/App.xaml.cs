namespace Blast;

public partial class App : Application
{
	public App(ViewModel.SettingsViewModel settingsvm)
	{
		InitializeComponent();

		MainPage = new AppShell();

		settingsvm.ThemeChangedCommand.Execute(null);
    }
}
