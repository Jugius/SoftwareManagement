using Microsoft.AspNetCore.Mvc;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Controllers;


[Route("software/api/[controller]")]
[ApiController]
public class FilesController : Controller
{
    readonly ReleaseFilesService _releaseFilesService;
    readonly RequestValidationService _requestValidationService;

    public FilesController(ReleaseFilesService service, RequestValidationService requestValidationService)
    {
        this._releaseFilesService = service;
        this._requestValidationService = requestValidationService;
    }


    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Contracts.Requests.CreateReleaseFileRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var createResult = await _releaseFilesService.Create(request);

        return createResult.Success ? Json(createResult.Value.ToResponse()) : Json(createResult.Error.ToResponse());

    }  

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Contracts.Requests.DeleteRequest request)
    {
        var validateResult = _requestValidationService.Validate(request);
        if (!validateResult.Success)
            return Json(validateResult.Error.ToResponse());

        var updateResult = await _releaseFilesService.Delete(request);

        return updateResult.Success ? Json(Contracts.Response.Ok) : Json(updateResult.Error.ToResponse());
    }

    [HttpGet("Download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        if (id == Guid.Empty)
            return Json(Contracts.Response.InvalidRequest);

        var fileResult = await _releaseFilesService.GetById(id);
        if (!fileResult.Success) return NotFound();

        var streamResult = _releaseFilesService.GetFileTream(id);
        if (!streamResult.Success) return NotFound();

        return new FileStreamResult(streamResult.Value, "application/octet-stream") 
        {
            FileDownloadName = fileResult.Value.Name 
        };
    }

}
