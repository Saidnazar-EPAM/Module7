using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ConfigurationItemAttribute : Attribute
    {
        public readonly string settingName;
        public readonly ProviderTypes providerType;

        public ConfigurationItemAttribute(string settingName, ProviderTypes providerType)
        {
            this.settingName = settingName;
            this.providerType = providerType;
        }
    }

    public enum ProviderTypes
    {
        CustomFile,
        ConfigurationManager
    }
}
