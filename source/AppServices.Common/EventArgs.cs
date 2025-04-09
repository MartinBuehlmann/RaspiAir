namespace AppServices.Common;

using System;

public class EventArgs<T>(T value) : EventArgs
{
    public T Value { get; } = value;

    public override string ToString()
    {
        return $"EventArgs<{typeof(T)}>: {this.Value}";
    }
}