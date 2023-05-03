using Akka.Actor;
using Akka.Hosting;
using MauiAkka.Model;

namespace MauiAkka;

public partial class MainPage : ContentPage
{
	int count = 0;
    private readonly IServiceProvider _provider;
    private readonly IActorRegistry _registry;
    private readonly ActorSystem _system;
    public MainPage(IServiceProvider provider)
    {
        _provider = provider;
        _system = _provider.GetRequiredService<ActorSystem>();
        _registry = _provider.GetRequiredService<IActorRegistry>();
        var maui = _system.ActorOf(Receive.Prop(), "maui-app");
        _registry.Register<Receive>(maui);
        InitializeComponent();
    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
        var actor = _provider.GetRequiredService<IRequiredActor<Receive>>();
        var a = actor.ActorRef;
        a.Tell(new Send(count.ToString()));
        var m = a.Ask<string>(SendGet.Get).GetAwaiter().GetResult();
        if (count == 1)			
            CounterBtn.Text = $"Clicked {m} time";
		else
			CounterBtn.Text = $"Clicked {m} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

}

