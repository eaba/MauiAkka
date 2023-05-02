using Akka.Actor;
using MauiAkka.Model;

namespace MauiAkka;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;
		HostService.ActorRef.Tell(new Send(count.ToString()));
        var m = HostService.ActorRef.Ask<string>(SendGet.Get).GetAwaiter().GetResult();
        if (count == 1)			
            CounterBtn.Text = $"Clicked {m} time";
		else
			CounterBtn.Text = $"Clicked {m} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

