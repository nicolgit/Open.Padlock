namespace Blast;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// views
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<Blast.View.Welcome01Page>();
		builder.Services.AddTransient<Blast.View.Welcome02Page>();

		// view models
		builder.Services.AddSingleton<Blast.ViewModel.MainViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.Welcome01ViewModel>();
		builder.Services.AddTransient<Blast.ViewModel.Welcome02ViewModel>();

		// models
		builder.Services.AddSingleton<Model.Settings>();

		return builder.Build();
	}
}
