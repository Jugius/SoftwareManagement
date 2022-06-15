using SoftwareManagement.ApiClient.Entities.Common.Enums;
using SoftwareManager.Helpers;
using SoftwareManager.Mapping;
using SoftwareManager.ViewModels.Entities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SoftwareManager.Services
{
    public class ApiClientService
    {
        private readonly ObservableCollection<ApplicationInfoVM> _applications  = new ObservableCollection<ApplicationInfoVM>();
        public ReadOnlyObservableCollection<ApplicationInfoVM> Applications { get; }
        public ApiClientService()
        {
            Applications = new ReadOnlyObservableCollection<ApplicationInfoVM>(_applications);
        }
        public async Task<OperationResult<bool>> ReloadDataset()
        {
            var request = new SoftwareManagement.ApiClient.Entities.Applications.Requests.GetApplicationsRequest 
            {
                Detailed = true 
            };
            FillWithConfig(request);           

            var response = await SoftwareManagement.ApiClient.Applications.Get.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var apps = response.Applications.Select(a => a.ToModelView());//.ToArray();
                this._applications.Clear();

                foreach (var application in apps)
                    this._applications.Add(application);

                return new OperationResult<bool>(true);
            }
            else
                return GetErrorResult<bool>(response);
        }

        #region Applications CRUD Commands
        internal async Task<OperationResult<ApplicationInfoVM>> Create(ApplicationInfoVM newApp)
        {
            if (this._applications.Any(a => a.Name.Equals(newApp.Name, StringComparison.OrdinalIgnoreCase)))
                return new OperationResult<ApplicationInfoVM>($"Приложение с названием {newApp.Name} уже существует в базе.");

            var request = newApp.ToRequestCreate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Applications.Create.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var result = response.Application.ToModelView();
                _applications.Add(result);
                return new OperationResult<ApplicationInfoVM>(result);
            }                
            else return GetErrorResult<ApplicationInfoVM>(response);
        }

        internal async Task<OperationResult<ApplicationInfoVM>> Edit(ApplicationInfoVM newApp)
        {
            var request = newApp.ToRequestUpdate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Applications.Update.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var existing = _applications.First(a => a.Id == response.Application.Id);
                existing.UpdatePropertiesBy(response.Application);
                return new OperationResult<ApplicationInfoVM>(existing);
            }            
            else return GetErrorResult<ApplicationInfoVM>(response);
        }

        internal async Task<OperationResult<bool>> Remove(ApplicationInfoVM appInfo)
        {           
            var request = appInfo.ToRequestDelete();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Applications.Delete.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var existing = _applications.First(a => a.Id == appInfo.Id);
                _applications.Remove(existing);
                return new OperationResult<bool>(true);
            }
            else return GetErrorResult<bool>(response);
        }
        #endregion

        #region Releases CRUD Commands
        internal async Task<OperationResult<ApplicationReleaseVM>> Create(ApplicationReleaseVM release)
        {
            var request = release.ToRequestCreate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Releases.Create.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var result = response.Release.ToModelView();
                var app = _applications.First(a => a.Id == result.ApplicationId);
                app.Releases.Add(result);
                return new OperationResult<ApplicationReleaseVM>(result);
            }
            else return GetErrorResult<ApplicationReleaseVM>(response);
        }

        internal async Task<OperationResult<ApplicationReleaseVM>> Edit(ApplicationReleaseVM release)
        {
            var request = release.ToRequestUpdate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Releases.Update.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var existing = _applications.First(a => a.Id == response.Release.ApplicationId)
                    .Releases.First(a => a.Id == response.Release.Id);
                existing.UpdatePropertiesBy(response.Release);
                return new OperationResult<ApplicationReleaseVM>(existing);
            }
            else return GetErrorResult<ApplicationReleaseVM>(response);
        }

        internal async Task<OperationResult<bool>> Remove(ApplicationReleaseVM release)
        {
            var request = release.ToRequestDelete();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Releases.Delete.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var app = _applications.First(a => a.Id == release.ApplicationId);
                var existing = app.Releases.First(a => a.Id == release.Id);
                app.Releases.Remove(existing);
                return new OperationResult<bool>(true);
            }
            else return GetErrorResult<bool>(response);
        }
        #endregion

        #region Details CRUD Commands
        internal async Task<OperationResult<ReleaseDetailVM>> Create(ReleaseDetailVM detail)
        {
            var request = detail.ToRequestCreate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Details.Create.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var result = response.Detail.ToModelView();
                var release = _applications.SelectMany(a => a.Releases).First(a => a.Id == result.ReleaseId);
                release.Details.Add(result);
                return new OperationResult<ReleaseDetailVM>(result);
            }
            else return GetErrorResult<ReleaseDetailVM>(response);
        }
        internal async Task<OperationResult<ReleaseDetailVM>> Edit(ReleaseDetailVM detail)
        {
            var request = detail.ToRequestUpdate();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Details.Update.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var origin = _applications.SelectMany(a => a.Releases)
                    .First(a => a.Id == response.Detail.ReleaseId)
                    .Details.First(a => a.Id == response.Detail.Id);
                origin.UpdatePropertiesBy(response.Detail);
                return new OperationResult<ReleaseDetailVM>(origin);
            }
            else return GetErrorResult<ReleaseDetailVM>(response);
        }
        internal async Task<OperationResult<bool>> Remove(ReleaseDetailVM detail)
        {
            var request = detail.ToRequestDelete();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Details.Delete.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var release = _applications.SelectMany(a => a.Releases)
                    .First(a => a.Id == detail.ReleaseId);

                var d = release.Details.First(a => a.Id == detail.Id);
                release.Details.Remove(d);
                return new OperationResult<bool>(true);
            }
            else return GetErrorResult<bool>(response);
        }
        #endregion

        #region Files CRUD Commands
        internal async Task<OperationResult<ReleaseFileVM>> Create(ReleaseFileVM releaseFile, byte[] fileBytes)
        {
            var request = releaseFile.ToRequestCreate(fileBytes);
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Files.Create.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var result = response.File.ToModelView();
                var release = _applications.SelectMany(a => a.Releases).First(a => a.Id == result.ReleaseId);
                release.Files.Add(result);
                return new OperationResult<ReleaseFileVM>(result);
            }
            else return GetErrorResult<ReleaseFileVM>(response);
        }
        internal async Task<OperationResult<bool>> Remove(ReleaseFileVM releaseFile)
        {
            var request = releaseFile.ToRequestDelete();
            FillWithConfig(request);

            var response = await SoftwareManagement.ApiClient.Files.Delete.QueryAsync(request);

            if (response.Status == Status.Ok)
            {
                var release = _applications.SelectMany(a => a.Releases)
                    .First(a => a.Id == releaseFile.ReleaseId);                
                release.Files.Remove(releaseFile);
                return new OperationResult<bool>(true);
            }
            else return GetErrorResult<bool>(response);
        }

        internal async Task SaveJson(string file)
        {
            var apps = this._applications.ToArray();

            JsonSerializerOptions jOptions = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

            };
            using var stream = System.IO.File.Create(file);
            await JsonSerializer.SerializeAsync(stream, apps, jOptions);
        }

        internal async Task DownloadFile(ReleaseFileVM file, string filePath)
        {
            string path = file.GetDownloadRequestString();
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(path))
                using (var fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
        }
        #endregion



        private static OperationResult<T> GetErrorResult<T>(SoftwareManagement.ApiClient.Entities.BaseResponse response)
        {
            string message = string.IsNullOrEmpty(response.ErrorMessage) ?
                   $"Response status: {response.Status}" :
                   $"Response status: {response.Status}\n{response.ErrorMessage}";
            return new OperationResult<T>(message);
        }
        private static void FillWithConfig(SoftwareManagement.ApiClient.Entities.BaseRequest request)
        {

            request.DomainName = ConfigManager.AppSettings.ServerMode == ConfigManager.ServerRequestsMode.Development ?
                ConfigManager.AppSettings.DevelopmentDomainName :
                ConfigManager.AppSettings.ProductionDomainName;


            if (request is SoftwareManagement.ApiClient.Entities.Interfaces.IKeyRequired keyRequired)
            {
                keyRequired.Key = ConfigManager.AppSettings.ServerMode == ConfigManager.ServerRequestsMode.Development ?
                    ConfigManager.AppSettings.DeveloperApiKey :
                    ConfigManager.AppSettings.ProductionApiKey;
            }

        }

        private static string GetDownloadRequestString(ReleaseFileVM file)
        {
            StringBuilder path = new StringBuilder(@"https://");
            if (ConfigManager.AppSettings.ServerMode == ConfigManager.ServerRequestsMode.Development)
                path.Append(ConfigManager.AppSettings.DevelopmentDomainName);
            else
                path.Append(@"software.oohelp.net/software/api/");

            path.Append(@"files/download/").Append(file.Id.ToString());
            return path.ToString();
        }
    }
}
