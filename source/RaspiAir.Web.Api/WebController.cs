namespace RaspiAir.Web.Api;

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RaspiAir.Web.Api.Filters;

[ApiExplorerSettings(GroupName = WebConstants.Route)]
[Route($"{WebConstants.Route}/[controller]")]
[ResourceException]
[Produces(MediaTypeNames.Application.Json)]
public abstract class WebController : Controller;