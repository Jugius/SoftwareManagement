using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Controllers;


[Route("software/api/[controller]")]
[ApiController]
public class DetailsController : Controller
{
    readonly ReleaseDetailsService _releaseDetailsService;
    readonly RequestValidationService _requestValidationService;

    public DetailsController(ReleaseDetailsService service, RequestValidationService requestValidationService)
    {
        this._releaseDetailsService = service;
        this._requestValidationService = requestValidationService;
    }



    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Contracts.Requests.CreateReleaseDetailRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var createResult = await _releaseDetailsService.Create(request);

        return createResult.Success ? Json(createResult.Value.ToResponse()) : Json(createResult.Error.ToResponse());

    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Contracts.Requests.UpdateReleaseDetailRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _releaseDetailsService.Update(request);

        return updateResult.Success ? Json(updateResult.Value.ToResponse()) : Json(updateResult.Error.ToResponse());
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Contracts.Requests.DeleteRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _releaseDetailsService.Delete(request);

        return updateResult.Success ? Json(Contracts.BaseResponse.Ok) : Json(updateResult.Error.ToResponse());
    }

}
