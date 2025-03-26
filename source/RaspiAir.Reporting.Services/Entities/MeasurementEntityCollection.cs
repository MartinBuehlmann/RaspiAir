namespace RaspiAir.Reporting.Services.Entities;

using System.Collections.Generic;

internal class MeasurementEntityCollection<TEntity>
{
    public List<TEntity> Items { get; } = new();
}