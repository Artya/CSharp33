using System;
using System.Collections.Generic;
using Airport3.Panel;

namespace Airport3.Interfaces
{
    public interface IProvider : IObservable<Message>, IDisposable
    {
        List<Flight> Flights { get; }
        void Recieve(Message message);
    }
}
