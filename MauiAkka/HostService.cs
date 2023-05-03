using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using MauiAkka.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MauiAkka
{
    public sealed class HostService : IHostedService, ISendMessage
    {
        private ActorSystem _system;  
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        public static IActorRef ActorRef;
        private IHostApplicationLifetime _applicationLifetime;

        public HostService(IServiceProvider serviceProvider, IHostApplicationLifetime applicationLifetime, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;
            _configuration = configuration; 
        }
        public async Task Message(Send send)
        {
            ActorRef.Tell(send);
            await Task.CompletedTask;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
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

            // start ActorSystem
            _system = ActorSystem.Create("maui");


            ActorRef = _system.ActorOf(Receive.Prop(), "maui-app");
            // add a continuation task that will guarantee shutdown of application if ActorSystem terminates
            _system.WhenTerminated.ContinueWith(tr => {
                _applicationLifetime.StopApplication();
            });


            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
          
            await CoordinatedShutdown.Get(_system).Run(CoordinatedShutdown.ClrExitReason.Instance);
        }
    }
}
