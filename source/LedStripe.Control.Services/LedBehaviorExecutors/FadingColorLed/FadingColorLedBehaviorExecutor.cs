namespace LedStripe.Control.Services.LedBehaviorExecutors.FadingColorLed;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using LedStripe.Control.LedBehaviors;

public class FadingColorLedBehaviorExecutor : ILedBehaviorExecutor
{
    private const int StepCount = 20;
    private readonly Color[] fadingColors;
    private int currentStep;

    public FadingColorLedBehaviorExecutor(FadingColorLedBehavior behavior)
    {
        int redStepSize = behavior.Color.R / StepCount;
        int greenStepSize = behavior.Color.G / StepCount;
        int blueStepSize = behavior.Color.B / StepCount;
        Color[] fadeInColors = Enumerable.Range(0, StepCount)
            .Select(
                step =>
                    Color.FromArgb(
                        255,
                        Math.Min(255, step * redStepSize),
                        Math.Min(255, step * greenStepSize),
                        Math.Min(255, step * blueStepSize)))
            .ToArray();

        var fadingColors = new List<Color>(fadeInColors);
        for (int index = fadeInColors.Length - 1; index >= 0; index--)
        {
            fadingColors.Add(fadeInColors[index]);
        }

        this.fadingColors = fadingColors.ToArray();
    }

    public Color GetColor()
    {
        Color currentColor = this.fadingColors[this.currentStep++];
        if (this.currentStep == this.fadingColors.Length)
        {
            this.currentStep = 0;
        }

        return currentColor;
    }
}