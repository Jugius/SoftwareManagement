using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Database.DTO;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services.Extentions;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class ApplicationsService
{
    private readonly AppDbContext _dbContext;
    public ApplicationsService(AppDbContext context) => _dbContext = context;
    public async Task<List<ApplicationInfo>> GetAll(bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = _dbContext.Applications;

        if (includeDetails)
            query = query
                    .Include(a => a.Releases).ThenInclude(a => a.Details).AsSplitQuery()
                    .Include(a => a.Releases).ThenInclude(a => a.Files).AsSplitQuery();

        var result = await query.ToListAsync();
        return result.Select(a => a.ToDomain()).ToList();
    }

    public async Task<OperationResult<ApplicationInfo>> GetById(Guid id, bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = _dbContext.Applications.Where(a => a.Id == id);

        if (includeDetails)
            query = query.Include(a => a.Releases).ThenInclude(a => a.Details);
        try
        {
            var result = await query.FirstOrDefaultAsync();

            if (result == null)
                return OperationResult<ApplicationInfo>.NotFound();                
            
            return new OperationResult<ApplicationInfo>(result.ToDomain());
        }
        catch (Exception ex)
        {            
            return OperationResult<ApplicationInfo>.DatabaseError(ex.GetBaseException().Message);
        }
    }        



    public async Task<OperationResult<ApplicationInfo>> GetByName(string name, bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = _dbContext.Applications.Where(a => a.Name == name);

        if (includeDetails)
            query = query.Include(a => a.Releases).ThenInclude(a => a.Details);

        try
        {
            var result = await query.FirstOrDefaultAsync();

            if (result == null)
                return OperationResult<ApplicationInfo>.NotFound();

            return new OperationResult<ApplicationInfo>(result.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationInfo>.DatabaseError(ex.GetBaseException().Message);
        }
    }    
    public async Task<OperationResult<ApplicationInfo>> Create(CreateApplicationRequest request)
    {
        var dto = request.ToDto();
        try
        {
            _dbContext.Applications.Add(dto);
            await _dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationInfo>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationInfo>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    public async Task<OperationResult<ApplicationInfo>> Update(UpdateApplicationRequest request)
    {
        try
        {
            var dto = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<ApplicationInfo>.NotFound();

            dto.UpdateWith(request);

            await _dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationInfo>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationInfo>.DatabaseError(ex.GetBaseException().Message);
        }        
    }
    public async Task<OperationResult<bool>> Delete(DeleteRequest request)
    {
        try
        {
            var dto = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<bool>.NotFound();

            _dbContext.Applications.Remove(dto);
            await _dbContext.SaveChangesAsync();

            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.DatabaseError(ex.GetBaseException().Message);
        }
    }
}
