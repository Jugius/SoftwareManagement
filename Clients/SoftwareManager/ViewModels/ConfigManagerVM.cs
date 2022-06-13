using SoftwareManager.ViewModels.Helpers;


namespace SoftwareManager.ViewModels
{
    public class ConfigManagerVM : ViewModelBase
    {
        public ConfigManager.ServerRequestsMode RequestsMode
        {
            get => ConfigManager.AppSettings.ServerMode;
            set { ConfigManager.AppSettings.ServerMode = value; ConfigManager.AppSettings.Save(); OnPropertyChanged(nameof(RequestsMode)); }
        }

        public RelayCommand ShowApiKeyDialog => _showApiKeyDialog ??= new RelayCommand(obj =>
        {
            ConfigManager.ServerRequestsMode mode = obj.ToString() switch
            {
                "Development" => ConfigManager.ServerRequestsMode.Development,
                "Production" => ConfigManager.ServerRequestsMode.Production,
                _ => throw new System.NotImplementedException(),
            };
            Services.DialogsProvider.ShowApiKeyDialog(mode);
        });
        private RelayCommand _showApiKeyDialog;

        public RelayCommand ShowDeveloperBaseApiUrlDialog => _showDeveloperBaseApiUrlDialog ??= new RelayCommand(obj =>
        {            
            Services.DialogsProvider.ShowDeveloperBaseApiUrlDialog();
        });
        private RelayCommand _showDeveloperBaseApiUrlDialog;
    }
}
