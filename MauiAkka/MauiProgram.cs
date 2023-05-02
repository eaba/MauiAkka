using Microsoft.Extensions.Logging;

namespace MauiAkka;

public static class MauiProgram
{//https://github.com/ChilliCream/workshops/blob/main/crypto-maui/backend/solutions/example5/Helpers/AssetPriceChangeProcessor.cs
    //https://github.com/dotnet/maui/pull/2137
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
		builder.Services.AddSingleton<HostService>();

        builder.Services.AddHostedService<HostService>(h => h.GetRequiredService<HostService>());

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
