﻿using CommunityToolkit.Maui;

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
				fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
            });

        // views
        builder.Services.AddSingleton<Blast.View.MainPage>();
		builder.Services.AddSingleton<Blast.View.WelcomePage>();
		builder.Services.AddTransient<Blast.View.WelcomeNewOrExistingPage>();
		builder.Services.AddTransient<Blast.View.WelcomeSelectStoragePage>();
        builder.Services.AddTransient<Blast.View.CreatePasswordPage>();
		builder.Services.AddTransient<Blast.View.TypePasswordPage>();
        builder.Services.AddTransient<Blast.View.SettingsPage>();

		// UI Services
		builder.Services.AddSingleton<Blast.UIServices.IDialogService>(new Blast.UIServices.DialogService());
		builder.Services.AddSingleton<Blast.UIServices.INavigationService>(new Blast.UIServices.NavigationService());

        // view models
        builder.Services.AddSingleton<Blast.ViewModel.MainViewModel>();
		builder.Services.AddSingleton<Blast.ViewModel.WelcomeViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.WelcomeNewOrExisistingViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.WelcomeSelectStorageViewModel>();
        builder.Services.AddTransient<Blast.ViewModel.CreatePasswordViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.TypePasswordViewModel>();
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
