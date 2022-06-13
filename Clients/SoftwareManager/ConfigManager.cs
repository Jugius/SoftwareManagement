using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoftwareManager
{
    public class ConfigManager
    {
        public enum ServerRequestsMode
        { 
            Development,
            Production
        }
        public static class FilesAndPathes
        {            
            public static string RoamingDirectory { get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\OoHelpWebApi"; } }
        }
        private const string DefaultConfigName = "SoftwareManager.json";
        private static readonly JsonSerializerOptions jOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,

        };
        public string DeveloperApiKey { get; set; }        
        public string ProductionApiKey { get; set; }
        public ServerRequestsMode ServerMode { get; set; }

        public string DevelopmentDomainName { get; set; }
        public string ProductionDomainName { get; set; }

        private static ConfigManager config;
        public static ConfigManager AppSettings => config ??= LoadOrDefault();
        public void Save()
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this, jOptions);
            string fileName = Path.Combine(FilesAndPathes.RoamingDirectory, DefaultConfigName);
            if (!Directory.Exists(FilesAndPathes.RoamingDirectory))
                Directory.CreateDirectory(FilesAndPathes.RoamingDirectory);
            File.WriteAllText(fileName, jsonString);
        }
        private static ConfigManager LoadOrDefault()
        {
            string fileName = Path.Combine(FilesAndPathes.RoamingDirectory, DefaultConfigName);
            if (!File.Exists(fileName))
                return Default;
            try
            {
                ConfigManager? configuration = JsonSerializer.Deserialize<ConfigManager>(File.ReadAllText(fileName), jOptions);
                return configuration ?? Default;
            }
            catch
            {
                return Default;
            }
        }
        public static ConfigManager Default => _default;
        private static readonly ConfigManager _default = new ConfigManager
        {
            DeveloperApiKey = "testKey",
            DevelopmentDomainName = "localhost:7164",
            ServerMode = ServerRequestsMode.Development
        };
    }
}
