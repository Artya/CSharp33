namespace Airport3.Panel
{
    public class Message
    {
        public readonly string Text;
        public readonly string AdditionalText;
        public readonly MessageType MessageType;
        public readonly Flight Flight;
        public readonly Passenger Passenger;

        public Message(MessageType messageType, string text = null)
        {
            this.MessageType = messageType;
            this.Text = text;
        }

        public Message(MessageType messageType, string text, string additionalText)
            : this(messageType, text)
        {
            this.AdditionalText = additionalText;
        }

        public Message(MessageType messageType, Flight flight)
            : this(messageType)
        {
            this.Flight = flight;
        }

        public Message(MessageType messageType, Passenger passenger)
            : this(messageType)
        {
            this.Passenger = passenger;
        }
    }
}
