//using System.Collections.ObjectModel;
//using System.Linq;

//namespace SoftwareManager.Helpers;

//public static class Converter
//{
//    #region ApplicationInfo
//    public static SoftwareManagement.Api.Domain.Models.ApplicationInfo ConvertToApiEntity(this SoftwareManager.ViewModels.Entities.ApplicationInfoVM dbInfo)
//    {
//        var apiInfo = new SoftwareManagement.Api.Domain.Models.ApplicationInfo
//        {
//            Name = dbInfo.Name,
//            Id = dbInfo.Id,            
//            Description = dbInfo.Description, 
//            IsPublic = dbInfo.IsPublic
//        };
//        return apiInfo;        
//    }
        

//    #endregion

//    #region ApplicationRelease
//    public static SoftwareManagement.Api.Domain.Models.ApplicationRelease ConvertToApiEntity(this ViewModels.Entities.ApplicationReleaseVM dbRelease)
//    {
//        var apiRelease = new SoftwareManagement.Api.Domain.Models.ApplicationRelease
//        {
//            Id = dbRelease.Id,
//            Kind = (SoftwareManagement.Api.Domain.Models.ReleaseKind)(int)dbRelease.Kind,
//            ReleaseDate = dbRelease.ReleaseDate,
//            Version = dbRelease.Version,
//            ApplicationId = dbRelease.ApplicationId
//        };
        
//        return apiRelease;
//    }
    
//    #endregion

//    #region ReleaseDetail
//    public static SoftwareManagement.Api.Domain.Models.ReleaseDetail ConvertToApiEntity(this ViewModels.Entities.ReleaseDetailVM dbDetail)
//    {
//        var apiDetail = new SoftwareManagement.Api.Domain.Models.ReleaseDetail
//        {
//            Id = dbDetail.Id,
//            Description = dbDetail.Description,
//            ReleaseId = dbDetail.ReleaseId,
//            Kind = (SoftwareManagement.Api.Domain.Models.DetailKind)(int)dbDetail.Kind
//        };
//        return apiDetail;
//    }

    


//    #endregion

//    #region ReleaseFile
//    public static SoftwareManagement.Api.Domain.Models.ReleaseFile ConvertToApiEntity(this ViewModels.Entities.ReleaseFileVM dbFile) =>
//        new SoftwareManagement.Api.Domain.Models.ReleaseFile
//        {
//            Id = dbFile.Id,
//            Name = dbFile.Name,
//            Kind = (SoftwareManagement.Api.Domain.Models.FileKind)(int)dbFile.Kind,                   
//            CheckSum = dbFile.CheckSum,
//            RuntimeVersion = dbFile.RuntimeVersion,
//            Description = dbFile.Description,
//            ReleaseId = dbFile.ReleaseId,
//            Size = dbFile.Size,
//            Uploaded = dbFile.Uploaded
//        };

    
//    #endregion

//}
