namespace RaspiAir.Web;

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using RaspiAir.Web.Filters;

[ApiExplorerSettings(GroupName = WebConstants.Route)]
[Route($"{WebConstants.Route}/[controller]")]
[ResourceException]
[Produces(MediaTypeNames.Application.Json)]
public abstract class WebController : Controller;