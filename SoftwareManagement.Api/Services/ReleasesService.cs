using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Database.DTO;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services.Extentions;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class ReleasesService
{
    private readonly AppDbContext _dbContext;
    public ReleasesService(AppDbContext context) => _dbContext = context;

    public async Task<OperationResult<ApplicationRelease>> GetById(Guid id, bool includeDetails)
    {
        IQueryable<ApplicationReleaseDto> query = _dbContext.Releases.Where(a => a.Id == id);

        if (includeDetails)
            query = query.Include(a => a.Details).Include(a => a.Files);
        
        try
        {
            var result = await query.FirstOrDefaultAsync();
            if (result == null)
                return OperationResult<ApplicationRelease>.NotFound();

            return new OperationResult<ApplicationRelease>(result.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationRelease>.DatabaseError(ex.GetBaseException().Message);
        }
    }
    public async Task<OperationResult<List<ApplicationRelease>>> GetByApplicationId(Guid id, bool includeDetails)
    {
        IQueryable<ApplicationReleaseDto> query = _dbContext.Releases.Where(a => a.ApplicationId == id).OrderBy(a => a.ReleaseDate);

        if (includeDetails)
            query = query.Include(a => a.Details).Include(a => a.Files);

        try
        {
            var result = await query.ToListAsync();
            return new OperationResult<List<ApplicationRelease>>(result.Select(a => a.ToDomain()).ToList());
        }
        catch (Exception ex)
        {
            return OperationResult<List<ApplicationRelease>>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    public async Task<OperationResult<ApplicationRelease>> Create(CreateReleaseRequest request)
    {
        var dto = request.ToDto();
        try
        {
            _dbContext.Releases.Add(dto);
            await _dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationRelease>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationRelease>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    public async Task<OperationResult<ApplicationRelease>> Update(UpdateReleaseRequest request)
    {
        try
        {
            var dto = await _dbContext.Releases.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<ApplicationRelease>.NotFound();

            dto.UpdateWith(request);

            await _dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationRelease>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationRelease>.DatabaseError(ex.GetBaseException().Message);
        }
    }
    public async Task<OperationResult<bool>> Delete(DeleteRequest request)
    {
        try
        {
            var dto = await _dbContext.Releases.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<bool>.NotFound();

            _dbContext.Releases.Remove(dto);
            await _dbContext.SaveChangesAsync();

            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.DatabaseError(ex.GetBaseException().Message);
        }
    }
}
