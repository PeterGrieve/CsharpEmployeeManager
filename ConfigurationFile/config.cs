using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ConfigurationFile
{
    public class configurationFile
    {
        public static string getSetting(string key)
        {
            // adapted from: 
            // https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings(v=vs.110).aspx
            // see app.config file for how to add the setting

            string setting = "";

            try
            {
                var appSettings = ConfigurationSettings.AppSettings;
                setting = appSettings[key] ?? "Not Found"; // if appSettings[key] != null, then appSettings[key], else "Not found"
            }
            catch (ConfigurationException)
            {
                setting = "ERROR: Not found";
            }

            return setting;
        }
    }
}
