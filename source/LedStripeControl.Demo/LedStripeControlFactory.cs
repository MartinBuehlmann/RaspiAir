﻿namespace LedStripeControl.Demo;

public class LedStripeControlFactory : ILedStripeControlFactory
{
    public ILedStripeControl Create(int ledCount)
        => new LedStripeControl(ledCount)
            .Initialize();
}