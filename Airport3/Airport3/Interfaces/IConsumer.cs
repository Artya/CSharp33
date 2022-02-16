using System;
using Airport3.Panel;

namespace Airport3.Interfaces
{
    public interface IConsumer : IObserver<Message>
    {
        void SubscribeOn(IProvider provider);
    }
}
