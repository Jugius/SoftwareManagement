using SoftwareManager.Helpers;
using SoftwareManager.Services;
using SoftwareManager.ViewModels.Entities;
using SoftwareManager.ViewModels.Helpers;
using System.Collections.ObjectModel;
using System.Linq;


namespace SoftwareManager.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        private readonly ApiClientService ApplicationsService = new ApiClientService();
        private readonly Services.DialogsProvider DialogProvider = new Services.DialogsProvider();
        public ReadOnlyObservableCollection<ApplicationInfoVM> Applications => ApplicationsService.Applications;    
        public ConfigManagerVM AppSettings { get; } = new ConfigManagerVM();

        public ApplicationInfoVM SelectedApplication
        {
            get => selectedApplication;
            set
            {
                selectedApplication = value;
                OnPropertyChanged(nameof(this.SelectedApplication));
            }
        }
        private ApplicationInfoVM selectedApplication;
        public ApplicationReleaseVM SelectedRelease
        {
            get => selectedRelease;
            set
            {
                selectedRelease = value;
                OnPropertyChanged(nameof(SelectedRelease));
            }
        }
        private ApplicationReleaseVM selectedRelease;

        #region Allications Commands
        public RelayCommand CreateApplication => _createApplication ??= new RelayCommand(async obj =>
        {
            var newApp = this.DialogProvider.ShowApplicationInfoDialog();
            if (newApp == null) return;

            var res = await ApplicationsService.Create(newApp);

            if (res.Success)
                this.SelectedApplication = res.Value;
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");            
        });
        private RelayCommand _createApplication;
        public RelayCommand EditApplication => _editApplication ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationInfoVM appInfo) return;

            var newApp = this.DialogProvider.ShowApplicationInfoDialog(appInfo);
            if (newApp == null) return;

            var res = await ApplicationsService.Edit(newApp);

            if (res.Success)
                this.SelectedApplication = res.Value;
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _editApplication;
        public RelayCommand RemoveApplication => _removeApplication ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationInfoVM appInfo || !DialogProvider.ShowDeleteQuestion(appInfo)) return;

            var res = await ApplicationsService.Remove(appInfo);

            if (res.Success)
                this.SelectedApplication = Applications.FirstOrDefault();
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _removeApplication;
        #endregion

        #region Releases Commands
        public RelayCommand CreateRelease => _createRelease ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationInfoVM appInfo) return;

            var release = DialogProvider.ShowApplicationReleaseDialog(appInfo);
            if (release == null) return;

            var res = await ApplicationsService.Create(release);

            if (res.Success)
                this.SelectedRelease = res.Value;
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _createRelease;
        public RelayCommand EditRelease => _editRelease ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationReleaseVM rel) return;

            var release = DialogProvider.ShowApplicationReleaseDialog(rel);
            if (release == null) return;

            var res = await ApplicationsService.Edit(release);

            if (res.Success)
                this.SelectedRelease = res.Value;
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _editRelease;
        public RelayCommand RemoveRelease => _removeRelease ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationReleaseVM release || !DialogProvider.ShowDeleteQuestion(release)) return;

            var res = await ApplicationsService.Remove(release);

            if (res.Success)
                this.SelectedRelease = SelectedApplication?.Releases.FirstOrDefault();
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _removeRelease;
        #endregion

        #region Details Commands


        public RelayCommand AddReleaseDetail => _addReleaseDetail ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationReleaseVM release) return;

            ReleaseDetailVM detail = DialogProvider.ShowReleaseDetailDialog(release);
            if (detail == null) return;

            var res = await ApplicationsService.Create(detail);

            if (!res.Success)
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");

        });
        private RelayCommand _addReleaseDetail;

        public RelayCommand EditReleaseDetail => _editReleaseDetail ??= new RelayCommand(async obj =>
        {
            if (obj is not ReleaseDetailVM detail) return;

            ReleaseDetailVM updatedDetail = DialogProvider.ShowReleaseDetailDialog(detail);

            if (updatedDetail == null) return;

            var res = await ApplicationsService.Edit(updatedDetail);

            if (!res.Success)
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _editReleaseDetail;

        public RelayCommand RemoveReleaseDetail => _removeReleaseDetail ??= new RelayCommand(async obj =>
        {
            if (obj is not ReleaseDetailVM detail || !DialogProvider.ShowDeleteQuestion(detail)) return;

            var res = await ApplicationsService.Remove(detail);

            if (!res.Success)
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _removeReleaseDetail;

        #endregion

        #region Files Commands

        public RelayCommand AddReleaseFile => _addReleaseFile ??= new RelayCommand(async obj =>
        {
            if (obj is not ApplicationReleaseVM release) return;

            var fi = DialogProvider.ShowOpenFileDialod();
            if (fi == null) return;

            ReleaseFileVM releaseFile = new ReleaseFileVM
            {
                Kind = SoftwareManagement.Api.Domain.Models.FileKind.Update,
                RuntimeVersion = SoftwareManagement.Api.Domain.Models.FileRuntimeVersion.Net6,
                Name = fi.Name,
            };
            releaseFile = DialogProvider.ShowReleaseFileDialog(releaseFile);

            if (releaseFile == null) return;

            releaseFile.ReleaseId = release.Id;

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fi.FullName);
            var res = await ApplicationsService.Create(releaseFile, fileBytes);

            if (!res.Success)
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _addReleaseFile;
        public RelayCommand RemoveReleaseFile => _removeReleaseFile ??= new RelayCommand(async obj =>
        {
            if (obj is not ReleaseFileVM file || !DialogProvider.ShowDeleteQuestion(file)) return;

            var res = await ApplicationsService.Remove(file);

            if (!res.Success)
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка записи в базу");
        });
        private RelayCommand _removeReleaseFile;

        public RelayCommand DownloadReleaseFile => _downloadReleaseFile ??= new RelayCommand(async obj =>
        {
            if (obj is not ReleaseFileVM file) return;

            string filePath = this.DialogProvider.ShowSaveAsFileDialog(file.Name);
            if(filePath == null) return;

            try
            {
                await ApplicationsService.DownloadFile(file, filePath);
                this.DialogProvider.ShowFileInExplorer(filePath);                
            }
            catch (System.Exception ex)
            {
                DialogProvider.ShowException(ex, "Ошибка загрузки файла");
            }


        });
        private RelayCommand _downloadReleaseFile;


        public RelayCommand OpenInBrowser => _openInBrowser ??= new RelayCommand(obj =>
        {
            if (obj is not ReleaseFileVM file) return;

            string filePath = file.GetDownloadRequestString();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
        });
        private RelayCommand _openInBrowser;  

        #endregion

        public RelayCommand ReloadDataset => _reloadDataset ??= new RelayCommand(async obj =>
        {

            var res = await ApplicationsService.ReloadDataset();
            if (res.Success)
                this.SelectedApplication = this.Applications.FirstOrDefault();
            else
                DialogProvider.ShowException(res.ErrorMessage, "Ошибка загрузки базы");
            
        });
        private RelayCommand _reloadDataset;
        public RelayCommand SaveJson => _saveJson ??= new RelayCommand(async obj =>
        {
            if (this.Applications.Count == 0) return;
            string file = DialogProvider.ShowSaveAsJsonFileDialog();
            if (file == null) return;

            try
            {
                await ApplicationsService.SaveJson(file);
                this.DialogProvider.ShowFileInExplorer(file);
            }
            catch (System.Exception ex)
            {
                DialogProvider.ShowException(ex.GetBaseException(), "Ошибка API");
            }
        });
        private RelayCommand _saveJson;

    }
}
