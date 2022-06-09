using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Controllers;


[Route("software/api/[controller]")]
[ApiController]
public class ReleasesController : Controller
{
    readonly ReleasesService _releasesService;
    readonly RequestValidationService _requestValidationService;

    public ReleasesController(ReleasesService service, RequestValidationService requestValidationService)
    {
        this._releasesService = service;
        this._requestValidationService = requestValidationService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Contracts.Requests.CreateReleaseRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var createResult = await _releasesService.Create(request);

        return createResult.Success ? Json(createResult.Value.ToResponse()) : Json(createResult.Error.ToResponse());

    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Contracts.Requests.UpdateReleaseRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _releasesService.Update(request);

        return updateResult.Success ? Json(updateResult.Value.ToResponse()) : Json(updateResult.Error.ToResponse());
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Contracts.Requests.DeleteRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _releasesService.Delete(request);

        return updateResult.Success ? Json(Contracts.BaseResponse.Ok) : Json(updateResult.Error.ToResponse());
    }


}
