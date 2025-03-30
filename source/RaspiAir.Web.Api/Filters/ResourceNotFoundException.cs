namespace RaspiAir.Web.Api.Filters;

using System;

public class ResourceNotFoundException(string message) : Exception(message);