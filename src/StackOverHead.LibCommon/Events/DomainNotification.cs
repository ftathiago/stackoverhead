using MediatR;

namespace StackOverHead.LibCommon.Events
{
    public class DomainNotification : INotification
    {
        public string Key { get; }
        public string Value { get; }

        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}