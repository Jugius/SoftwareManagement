using System.Text;
using SoftwareManagement.Api.Services.Helpers;

namespace SoftwareManagement.Api.Services;
public class FileSystemService
{
    private readonly string _uploadDirectory;
    public FileSystemService(IWebHostEnvironment env)
    {
        this._uploadDirectory = Path.Combine(env.ContentRootPath, "UploadedFiles");
    }

    public Task<OperationResult<bool>> SaveFile(byte[] fileBytes, Guid fileId)
    { 
        string fileName = Helpers.Guider.ToStringFromGuid(fileId);
        return SaveFile(fileBytes, fileName);
    }
    public async Task<OperationResult<bool>> SaveFile(byte[] fileBytes, string fileName)
    {
        string filePath = Path.Combine(_uploadDirectory, fileName);
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(fileBytes);
            }
            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.FileSystemError(ex.GetBaseException().Message);
        }
    }


    public OperationResult<bool> DeleteFile(Guid fileId)
    {
        string fileName = Helpers.Guider.ToStringFromGuid(fileId);
        return DeleteFile(fileName);
    }
    public OperationResult<bool> DeleteFile(string fileName)
    {
        string filePath = Path.Combine(_uploadDirectory, fileName);

        if(!System.IO.File.Exists(filePath))
            return OperationResult<bool>.FileSystemError("File not exist");

        try
        {
            System.IO.File.Delete(filePath);
            return new OperationResult<bool>(true);
        }
        catch (Exception ex)
        {
            return OperationResult<bool>.FileSystemError(ex.GetBaseException().Message);
        }
    }



    public string GetCheckSum(byte[] bytes)
    {
        using var stream = new MemoryStream(bytes);
        byte[] hash = System.Security.Cryptography.MD5.Create().ComputeHash(stream);
        return MakeHashString(hash);
    }
    private static string MakeHashString(byte[] hash)
    {
        StringBuilder s = new StringBuilder(hash.Length * 2);

        foreach (byte b in hash)
            s.Append(b.ToString("X2").ToLower());

        return s.ToString();
    }
}
