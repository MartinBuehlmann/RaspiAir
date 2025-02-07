namespace RaspiRobot.Lights.Common;

using System;

public class EventArgs<T> : EventArgs
{
    public EventArgs(T value)
    {
        this.Value = value;
    }

    public T Value { get; }

    public override string ToString()
    {
        return $"EventArgs<{typeof(T)}>: {this.Value}";
    }
}