using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_7
{
    public class ConfigurationComponent : ConfigurationComponentBase
    {
        public ConfigurationComponent(string fileName) : base(fileName)
        {
        }

        [ConfigurationItem("C1", ProviderTypes.ConfigurationManager)]
        public string C1 { get; set; }

        [ConfigurationItem("C2", ProviderTypes.ConfigurationManager)]
        public string C2 { get; set; }

        [ConfigurationItem("C3", ProviderTypes.ConfigurationManager)]
        public string C3 { get; set; }

        [ConfigurationItem("C4", ProviderTypes.ConfigurationManager)]
        public string C4 { get; set; }

        [ConfigurationItem("F1", ProviderTypes.CustomFile)]
        public string F1 { get; set; }

        [ConfigurationItem("F2", ProviderTypes.CustomFile)]
        public string F2 { get; set; }

        [ConfigurationItem("F3", ProviderTypes.CustomFile)]
        public string F3 { get; set; }

        [ConfigurationItem("F4", ProviderTypes.CustomFile)]
        public string F4 { get; set; }    
    }
}
