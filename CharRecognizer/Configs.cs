using System.Configuration;

namespace CharRecognizer
{
    class Configs
    {
        private static Configs _instance;

        private Configs() { }

        public static Configs GetInstance()
        {
            return _instance ?? (_instance = new Configs());
        }

        public string GetPathToData()
        {
            return ConfigurationManager.AppSettings.Get("path_to_data");
        }

        public string GetPathToFileStorage()
        {
            return ConfigurationManager.AppSettings.Get("path_to_file_storage");
        }
    }
}
