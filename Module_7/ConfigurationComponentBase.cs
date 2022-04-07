using Microsoft.Extensions.Configuration;
using conf = System.Configuration;
using System.Text.Json;

namespace Module_7
{
    public abstract class ConfigurationComponentBase
    {
        private readonly string fileName;

        public ConfigurationComponentBase(string fileName)
        {
            this.fileName = fileName;
        }

        public void LoadSettings()
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            IConfiguration customFileConfig = new ConfigurationBuilder()
               .AddJsonFile(fileName, optional: true, reloadOnChange: true)
               .Build();

            var type = this.GetType();
            var properties = type.GetProperties()
                .Where(prop => prop.IsDefined(typeof(ConfigurationItemAttribute), false));

            foreach (var prop in properties)
            {
                var attr = (ConfigurationItemAttribute)Attribute.GetCustomAttribute(prop, typeof(ConfigurationItemAttribute));

                string? setting = null;
                if (attr.providerType == ProviderTypes.ConfigurationManager)
                {
                    setting = conf.ConfigurationManager.AppSettings[attr.settingName];
                }
                if (attr.providerType == ProviderTypes.CustomFile)
                {
                    setting = customFileConfig[attr.settingName];
                }

                if (prop.PropertyType == typeof(string))
                {
                    prop.SetValue(this, setting);
                }
                if (prop.PropertyType == typeof(int))
                {
                    int value;
                    int.TryParse(setting, out value);
                    prop.SetValue(this, value);
                }
                if (prop.PropertyType == typeof(float))
                {
                    float value;
                    float.TryParse(setting, out value);
                    prop.SetValue(this, value);
                }
                if (prop.PropertyType == typeof(TimeSpan))
                {
                    TimeSpan value;
                    TimeSpan.TryParse(setting, out value);
                    prop.SetValue(this, value);
                }
            }
        }
        public void SaveSettings()
        {
            var type = this.GetType();
            var properties = type.GetProperties()
                .Where(prop => prop.IsDefined(typeof(ConfigurationItemAttribute), false));

            var list = new Dictionary<string, string>();

            foreach (var prop in properties)
            {
                var attr = (ConfigurationItemAttribute)Attribute.GetCustomAttribute(prop, typeof(ConfigurationItemAttribute));
                var value = prop.GetValue(this);

                if (value == null) continue;

                if (attr.providerType == ProviderTypes.ConfigurationManager)
                {
                    AddUpdateAppSetting(attr.settingName, value.ToString());
                }
                if (attr.providerType == ProviderTypes.CustomFile)
                {
                    list.Add(attr.settingName, value.ToString());
                }
            }
            var json = JsonSerializer.Serialize(list);
            File.WriteAllText(fileName, json);
        }

        private void AddUpdateAppSetting(string key, string value)
        {
            try
            {
                var configFile = conf.ConfigurationManager.OpenExeConfiguration(conf.ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(conf.ConfigurationSaveMode.Modified);
                conf.ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (conf.ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}

