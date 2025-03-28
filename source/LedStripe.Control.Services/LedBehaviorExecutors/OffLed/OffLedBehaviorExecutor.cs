﻿namespace LedStripe.Control.Services.LedBehaviorExecutors.OffLed;

using System.Drawing;
using LedStripe.Control.LedBehaviors;

internal class OffLedBehaviorExecutor(OffLedBehavior behavior) : ILedBehaviorExecutor
{
    public Color GetColor()
        => behavior.Color;
}