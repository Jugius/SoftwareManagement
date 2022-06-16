using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Api.Contracts.Requests;
using SoftwareManagement.Api.Database;
using SoftwareManagement.Api.Domain.Models;
using SoftwareManagement.Api.Mapping;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class ReleaseFilesService
{
    private readonly AppDbContext _dbContext;
    private readonly FileSystemService _fileSystemService;
    public ReleaseFilesService(AppDbContext context, FileSystemService fileSystemService)
    {
        _dbContext = context;
        _fileSystemService = fileSystemService;
    }

    public async Task<OperationResult<ReleaseFile>> GetById(Guid id)
    {
        try
        {
            var dto = await _dbContext.Files.FirstOrDefaultAsync(a => a.Id == id);
            if (dto == null)
                return OperationResult<ReleaseFile>.NotFound();

            return new OperationResult<ReleaseFile>(dto.ToDomain());            
        }
        catch (Exception ex)
        {
            return OperationResult<ReleaseFile>.DatabaseError(ex.GetBaseException().Message);
        }
    }
    public OperationResult<Stream> GetFileTream(Guid id)
    {
        return _fileSystemService.GetFileTream(id);
    }

    public async Task<OperationResult<ReleaseFile>> Create(CreateReleaseFileRequest request)
    {
        var dto = request.ToDto();

        dto.CheckSum = _fileSystemService.GetCheckSum(request.FileBytes);
        dto.Size = request.FileBytes.Length;

        try
        {
            _dbContext.Files.Add(dto);
            await _dbContext.SaveChangesAsync();            
        }
        catch (Exception ex)
        {
            return OperationResult<ReleaseFile>.DatabaseError(ex.GetBaseException().Message);
        }

        var saveFileRes = await _fileSystemService.SaveFile(request.FileBytes, dto.Id);
        if (saveFileRes.Success)
        {
            return new OperationResult<ReleaseFile>(dto.ToDomain());
        }
        else
        {
            _dbContext.Files.Remove(dto);
            await _dbContext.SaveChangesAsync();
            return new OperationResult<ReleaseFile>(saveFileRes.Error);
        }
    }
    public async Task<OperationResult<bool>> Delete(DeleteRequest request)
    {
        try
        {
            var dto = await _dbContext.Files.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (dto == null)
                return OperationResult<bool>.NotFound();

            var delFileRes = _fileSystemService.DeleteFile(dto.Id);

            if (delFileRes.Success)
            {
                _dbContext.Files.Remove(dto);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<bool>(true);
            }
            else
            {
                return delFileRes;
            }            
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.DatabaseError(ex.GetBaseException().Message);
        }
    }

    public class FileResult     
    {
        public Stream FileStream { get; set; }
        public string FileName { get; set; }
    }

}
