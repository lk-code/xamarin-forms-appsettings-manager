using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace de.lkraemer.appsettingsmanager
{
    public class AppSettingsManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static AppSettingsManager _instance;

        /// <summary>
        /// 
        /// </summary>
        private JObject _secrets;

        /// <summary>
        /// 
        /// </summary>
        private static string _namespace;

        /// <summary>
        /// 
        /// </summary>
        private static string _fileName;

        /// <summary>
        /// 
        /// </summary>
        private AppSettingsManager()
        {
            if(string.IsNullOrEmpty(AppSettingsManager._namespace)
                || string.IsNullOrWhiteSpace(AppSettingsManager._namespace)
                || string.IsNullOrEmpty(AppSettingsManager._fileName)
                || string.IsNullOrWhiteSpace(AppSettingsManager._fileName))
            {
                throw new InvalidOperationException("the app namespace or settings filename is empty or invalid");
            }

            string appSettingFileResourceName = $"{AppSettingsManager._namespace}.{AppSettingsManager._fileName}";

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
            Stream stream = assembly.GetManifestResourceStream(appSettingFileResourceName);

            using (StreamReader reader = new StreamReader(stream))
            {
                string json = reader.ReadToEnd();
                _secrets = JObject.Parse(json);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appNamespace"></param>
        /// <param name="settingsFileName"></param>
        public static void LoadSettings(string appNamespace, string settingsFileName)
        {
            AppSettingsManager._namespace = appNamespace;
            AppSettingsManager._fileName = settingsFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                var path = name.Split(':');

                JToken node = _secrets[path[0]];
                for (int index = 1; index < path.Length; index++)
                {
                    node = node[path[index]];
                }

                return node.ToString();
            }
        }
    }
}
