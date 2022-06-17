using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Database.DTO;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class UpdatesService
{
    private readonly AppDbContext _dbContext;
    private readonly FileSystemService _fileSystemService;
    public UpdatesService(AppDbContext context, FileSystemService fileSystemService)
    {
        _dbContext = context;
        _fileSystemService = fileSystemService;
    }
    public async Task<OperationResult<List<ApplicationRelease>>> GetNewestReleases(string name, Version version)
    {
        try
        {
            var app = await _dbContext.Applications
                .Include(a => a.Releases).ThenInclude(a => a.Details).AsSplitQuery()
                .Include(a => a.Releases).ThenInclude(a => a.Files).AsSplitQuery()
                .Where(a => a.Name == name).AsNoTracking()
                .FirstOrDefaultAsync();

            if (app == null)
                return OperationResult<List<ApplicationRelease>>.NotFound();

            var releases = app.Releases.Select(a => a.ToDomain()).Where(a => a.Version > version).ToList();
            return new OperationResult<List<ApplicationRelease>>(releases);
        }
        catch (Exception ex)
        {
            return OperationResult<List<ApplicationRelease>>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    internal async Task<OperationResult<ApplicationRelease>> GetLastReleases(string name, FileRuntimeVersion? runtimeVersion)
    {
        try
        {
            var app = await _dbContext.Applications.FirstOrDefaultAsync(a => a.Name == name);               

            if (app == null)
                return OperationResult<ApplicationRelease>.NotFound();

            List<ApplicationReleaseDto> releases = runtimeVersion.HasValue ?
                await _dbContext.Releases
                    .Include(a => a.Details)
                    .Include(a => a.Files)
                    .Where(a => a.ApplicationId == app.Id && a.Files.Any(b => b.RuntimeVersion == (int)runtimeVersion))
                    .AsNoTracking().ToListAsync() :
                await _dbContext.Releases
                    .Include(a => a.Details)
                    .Include(a => a.Files)
                    .Where(a => a.ApplicationId == app.Id)
                    .AsNoTracking().ToListAsync();

            return new OperationResult<ApplicationRelease>(releases.Select(a => a.ToDomain()).MaxBy(a => a.Version));
        }
        catch (Exception ex)
        {
            return OperationResult<ApplicationRelease>.DatabaseError(ex.GetBaseException().Message);
        }
    }
}
