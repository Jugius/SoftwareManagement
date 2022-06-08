using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Database.DTO;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Helpers;
using SoftwareManagement.Api.Mapping;

namespace SoftwareManagement.Api.Services;
public class ApplicationsService
{
    private readonly AppDbContext dbContext;
    public ApplicationsService(AppDbContext context) => dbContext = context;
    public async Task<List<ApplicationInfo>> GetAll(bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = dbContext.Applications;

        if (includeDetails)
        {
            query = query
                .Include(a => a.Releases).ThenInclude(a => a.Details).AsSplitQuery()
                .Include(a => a.Releases).ThenInclude(a => a.Files).AsSplitQuery();
        }
        var result = await query.ToListAsync();
        return result.Select(a => a.ToDomain()).ToList();
    }

    public async Task<OperationResult<ApplicationInfo>> GetById(Guid id, bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = dbContext.Applications.Where(a => a.Id == id);

        if (includeDetails)
            query = query.Include(a => a.Releases).ThenInclude(a => a.Details);
        try
        {
            var result = await query.FirstOrDefaultAsync();
            return GetOkResultOrNotFound(result?.ToDomain());
        }
        catch (Exception ex)
        {
            var error = new Exceptions.ApiException(Contracts.Common.Enums.Status.DatabaseError,ex.GetBaseException().Message);
            return new OperationResult<ApplicationInfo>(error);
        }
    }
    private static OperationResult<T> GetOkResultOrNotFound<T>(T obj) where T : new()
    {
        return obj == null ?
                new OperationResult<T>(new Exceptions.ApiException(Contracts.Common.Enums.Status.NotFound)) :
                new OperationResult<T>(obj);
    }
        

    public async Task<OperationResult<ApplicationInfo>> GetByName(string name, bool includeDetails)
    {
        IQueryable<ApplicationInfoDto> query = dbContext.Applications.Where(a => a.Name == name);

        if (includeDetails)
            query = query.Include(a => a.Releases).ThenInclude(a => a.Details);

        var result = await query.FirstOrDefaultAsync();
        return new OperationResult<ApplicationInfo>(result?.ToDomain());
    }    
    public async Task<OperationResult<ApplicationInfo>> Create(CreateApplicationRequest request)
    {
        var dto = request.ToDto();
        try
        {
            dbContext.Applications.Add(dto);
            await dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationInfo>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            var error = new Exceptions.ApiException(Contracts.Common.Enums.Status.DatabaseError, ex.GetBaseException().Message);
            return new OperationResult<ApplicationInfo>(error);
        }
    }

    public async Task<OperationResult<ApplicationInfo>> Update(UpdateApplicationRequest request)
    {
        try
        {
            var dto = await dbContext.Applications.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return new OperationResult<ApplicationInfo>(new Exceptions.ApiException(Contracts.Common.Enums.Status.NotFound));

            dto.Name = request.Name;
            dto.Description = request.Description;
            dto.IsPublic = request.IsPublic;

            await dbContext.SaveChangesAsync();
            return new OperationResult<ApplicationInfo>(dto.ToDomain());
        }
        catch (Exception ex)
        {
            var error = new Exceptions.ApiException(Contracts.Common.Enums.Status.DatabaseError, ex.GetBaseException().Message);
            return new OperationResult<ApplicationInfo>(error);
        }        
    }
    public async Task<OperationResult<bool>> Delete(DeleteRequest request)
    {
        try
        {
            var dto = await dbContext.Applications.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return new OperationResult<bool>(new Exceptions.ApiException(Contracts.Common.Enums.Status.NotFound));

            dbContext.Applications.Remove(dto);
            await dbContext.SaveChangesAsync();

            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            var error = new Exceptions.ApiException(Contracts.Common.Enums.Status.DatabaseError, ex.GetBaseException().Message);
            return new OperationResult<bool>(error);
        }
    }
}
