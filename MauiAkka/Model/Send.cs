

namespace MauiAkka.Model
{
    public class Send
    {
        public readonly string Message;
        public Send(string message) 
        { 
            Message = message;
        }
    }
    public class SendGet
    {
       public static SendGet Get = new SendGet();
    }
}
