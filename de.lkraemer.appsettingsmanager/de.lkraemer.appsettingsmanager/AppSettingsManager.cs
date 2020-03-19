/**
 * MIT License
 * 
 * Copyright (c) 2019 lk-code
 * see more at https://github.com/lk-code/xamarin-forms-appsettings-manager
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
        private readonly JObject _secrets;

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
