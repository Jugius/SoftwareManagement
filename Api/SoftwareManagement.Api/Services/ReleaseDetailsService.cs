using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services.Extentions;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class ReleaseDetailsService
{
    private readonly AppDbContext _dbContext;
    public ReleaseDetailsService(AppDbContext context) => _dbContext = context;
    public async Task<OperationResult<ReleaseDetail>> Create(CreateReleaseDetailRequest request)
    {
        var dto = request.ToDto();
        try
        {
            _dbContext.Details.Add(dto);
            await _dbContext.SaveChangesAsync();
            return new OperationResult<ReleaseDetail>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ReleaseDetail>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    public async Task<OperationResult<ReleaseDetail>> Update(UpdateReleaseDetailRequest request)
    {
        try
        {
            var dto = await _dbContext.Details.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<ReleaseDetail>.NotFound();

            dto.UpdateWith(request);

            await _dbContext.SaveChangesAsync();
            return new OperationResult<ReleaseDetail>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ReleaseDetail>.DatabaseError(ex.GetBaseException().Message);
        }
    }
    public async Task<OperationResult<bool>> Delete(DeleteRequest request)
    {
        try
        {
            var dto = await _dbContext.Details.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<bool>.NotFound();

            _dbContext.Details.Remove(dto);
            await _dbContext.SaveChangesAsync();

            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.DatabaseError(ex.GetBaseException().Message);
        }
    }
}
