

namespace MauiAkka.Model
{
    public interface ISendMessage
    {
        Task Message(Send send);
    }
}
