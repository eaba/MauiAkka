using Akka.Actor;
using MauiAkka.Model;

namespace MauiAkka
{
    
    public class Receive: ReceiveActor
    {
        private List<string> _messages;
        public Receive() 
        {
            _messages = new List<string>();
            Receive<Send>(s => _messages.Add(s.Message));
            Receive<SendGet>(s => 
            { 
                if(_messages.Count > 0)
                {
                    var m = _messages.First();
                    _messages.RemoveAt(0);
                    Sender.Tell(m);
                }
                   
            });
        }  
        public static Props Prop()
        {
            return Props.Create(() => new Receive());
        }
    }
}
