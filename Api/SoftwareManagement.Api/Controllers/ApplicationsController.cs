using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Controllers;

[Route("software/api/[controller]")]
[ApiController]
public class ApplicationsController : Controller
{
    readonly ApplicationsService _applicationsService;
    readonly RequestValidationService _requestValidationService;
    public ApplicationsController(ApplicationsService service, RequestValidationService requestValidationService)
    {
        this._applicationsService = service;
        this._requestValidationService = requestValidationService;
    }

    [HttpGet]
    public async Task<ActionResult> Get(bool? detailed)
    {
        var apps = await _applicationsService.GetAll(detailed.GetValueOrDefault(false));
        return Json(apps.ToResponse());
    }

    [HttpGet("ById")]
    public async Task<ActionResult> GetById(Guid id)
    {
        if (id == Guid.Empty)
            return Json(Contracts.Response.InvalidRequest);

        var result = await _applicationsService.GetById(id, true);
        return result.Success ? Json(result.Value.ToResponse()) : Json(result.Error.ToResponse());
    }

    [HttpGet("ByName")]
    public async Task<ActionResult> GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return Json(Contracts.Response.InvalidRequest);

        var result = await _applicationsService.GetByName(name, true);
        return result.Success ? Json(result.Value.ToResponse()) : Json(result.Error.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Contracts.Requests.CreateApplicationRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var createResult = await _applicationsService.Create(request);

        return createResult.Success ? Json(createResult.Value.ToResponse()) : Json(createResult.Error.ToResponse());

    }
    
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Contracts.Requests.UpdateApplicationRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _applicationsService.Update(request);

        return updateResult.Success ? Json(updateResult.Value.ToResponse()) : Json(updateResult.Error.ToResponse());
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Contracts.Requests.DeleteRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _applicationsService.Delete(request);

        return updateResult.Success ? Json(Contracts.Response.Ok) : Json(updateResult.Error.ToResponse());
    }

}
