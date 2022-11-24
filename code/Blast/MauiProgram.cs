using CommunityToolkit.Maui;

namespace Blast;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        // views
        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<Blast.View.Welcome01Page>();
		builder.Services.AddTransient<Blast.View.Welcome02Page>();
        builder.Services.AddTransient<Blast.View.CreatePasswordPage>();
        builder.Services.AddTransient<Blast.View.SettingsPage>();

        // view models
        builder.Services.AddSingleton<Blast.ViewModel.MainViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.Welcome01ViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.Welcome02ViewModel>();
        builder.Services.AddTransient<Blast.ViewModel.CreatePasswordViewModel>();
        builder.Services.AddTransient<Blast.ViewModel.SettingsViewModel>();

		// models
		builder.Services.AddSingleton<Model.Services.Settings>();
		builder.Services.AddSingleton<Model.Services.Current>();
		builder.Services.AddSingleton<Model.Services.PasswordsHelper>();

		// infrastructure
		builder.Services.AddSingleton<IPreferences>(Microsoft.Maui.Storage.Preferences.Default);

		return builder.Build();
	}
}
