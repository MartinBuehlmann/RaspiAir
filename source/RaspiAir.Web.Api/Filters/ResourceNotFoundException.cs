namespace RaspiAir.Web.Api.Filters;

using System;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message)
        : base(message)
    {
    }
}