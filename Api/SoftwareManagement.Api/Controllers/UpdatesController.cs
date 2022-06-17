using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Controllers;

[Route("software/api/[controller]")]
[ApiController]
public class UpdatesController : Controller
{
    readonly UpdatesService _updatesService;    
    public UpdatesController(UpdatesService service)
    {
        this._updatesService = service;        
    }

    [HttpPost("NewReleases")]
    public async Task<ActionResult> GetNewestReleases(GetNewestReleasesRequest request)
    { 
        if(string.IsNullOrEmpty(request?.Name) || request?.CurrentVersion == null)
            return Json(Contracts.Response.InvalidRequest);

        var result = await _updatesService.GetNewestReleases(request.Name, request.CurrentVersion);
        return result.Success ? Json(result.Value.ToResponse()) : Json(result.Error.ToResponse());
    }

    [HttpPost("LastRelease")]
    public async Task<ActionResult> GetLastRelease(GetLastReleaseRequest request)
    {
        if (string.IsNullOrEmpty(request?.Name))
            return Json(Contracts.Response.InvalidRequest);

        var result = await _updatesService.GetLastReleases(request.Name, request.RuntimeVersion);
        return result.Success ? Json(result.Value.ToResponse()) : Json(result.Error.ToResponse());
    }



}
