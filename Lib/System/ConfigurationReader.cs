using Newtonsoft.Json;

namespace HogWarp.Lib.System
{
    public class ConfigurationReader<T>
    {
        private static string JSON_FILE_EXTENSION = ".json";
        private Logger _logger = new Logger("ConfigReader");
        private string _configPath;

        public ConfigurationReader(string pluginName, string configName)
        {
            _configPath = Path.Join("plugins", pluginName, configName);
            FileExist();
            IsValidExtension();
        }

        public ConfigurationReader( string configName)
        {
            _configPath = Path.Join("config", configName);
            FileExist();
            IsValidExtension();
        }

        private bool IsValidExtension()
        {
            bool validExtension = Path.GetExtension(_configPath).Equals(JSON_FILE_EXTENSION);
            if(!validExtension)
                _logger.Error($"The confifuration file {_configPath} is not .json");
            return validExtension;
        }

        public bool FileExist()
        {
            bool exist = File.Exists(_configPath);
            if (!exist)
                _logger.Error($"The confifuration file {_configPath} not exist");
            return exist;
        }

        public T? ReadFile()
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(_configPath));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error reading or deserializing the configuration file {_configPath}: {ex.Message}");
                return default(T);
            }
        }
    }
}
