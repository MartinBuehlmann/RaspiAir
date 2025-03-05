namespace RaspiAir.Reporting.Services.Entities;

using System.Collections.Generic;

internal class MeasurementEntityCollection<TEntity>
{
    public MeasurementEntityCollection()
    {
        this.Items = new List<TEntity>();
    }

    public List<TEntity> Items { get; }
}