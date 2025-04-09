namespace AppServices.EventBroker;

using System;

public class EventBrokerException : Exception
{
    public EventBrokerException()
    {
    }

    public EventBrokerException(string message)
        : base(message)
    {
    }

    public EventBrokerException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}