using SoftwareManager.ViewModels.Entities;
using System.Text;

namespace SoftwareManager.Helpers;

public static class Extentions
{
    

    public static string GetDownloadRequestString(this ReleaseFileVM file)
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
