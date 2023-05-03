using Akka.Configuration;
using Akka.Hosting;
using Akka.Remote.Hosting;
using MauiAkka.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.Reflection;

namespace MauiAkka;

public static class MauiProgram
{
    //https://github.com/dotnet/maui/pull/2137
    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder(true);
		builder
			.UseMauiApp<App>()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddAkka("MyActorSystem", configurationBuilder =>
        {
            var config = ConfigurationFactory.ParseString(@"
				akka
				{
					loglevel = DEBUG
					log-config-on-start = on 
					actor 
					{              
						  debug 
						  {
							  receive = on
							  autoreceive = on
							  lifecycle = on
							  event-stream = on
							  unhandled = on
						  }  
					}
					coordinated-shutdown
					{
						exit-clr = on
					}
				}");
            configurationBuilder
            .AddHocon(config, HoconAddMode.Append)
            /*.WithRemoting(new RemoteOptions
            {
                HostName = "127.0.0.1",
                Port = 5055,
            })
            .WithActors((system, registry) =>
            {
                var maui = system.ActorOf(Receive.Prop(), "maui-app");
                registry.Register<Receive>(maui);
            })
            .AddStartup((system, registry) =>
            {
                var maui = system.ActorOf(Receive.Prop(), "maui-app");
                registry.Register<Receive>(maui);
            })
            .StartActors((system, registry, resolver) =>
            {
                resolver.Props<Receive>();
                var maui = system.ActorOf(Receive.Prop(), "maui-app");
                registry.Register<Receive>(maui);
            })
            .WithActors((system, registry) =>
            {
                var maui = system.ActorOf(Receive.Prop(), "maui-app");
                registry.Register<Receive>(maui);
            })*/;

        });
        builder.Services.AddSingleton<MainPage>();
        return builder.Build();
	}
}
