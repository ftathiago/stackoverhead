using MediatR;

namespace StackOverHead.LibCommon.Events
{
    public class DomainNotification : INotification
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}