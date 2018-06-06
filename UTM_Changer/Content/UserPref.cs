using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTM_Changer.Parser;
using UTM_Changer.Properties;

namespace UTM_Changer
{
    [Serializable]
    public class UserPrefs
    {
        private bool shouldSaveData = true;
        private string utm_source = "facebook.com";
        private string utm_medium = "social";

        private Dictionary<string, ParserCreator> parsers = new Dictionary<string, ParserCreator>();



        public string Utm_source { get => utm_source; set => utm_source = value; }
        public string Utm_medium { get => utm_medium; set => utm_medium = value; }
        public bool ShouldSaveData { get => shouldSaveData; set => shouldSaveData = value; }
        internal Dictionary<string, ParserCreator> Parsers { get => parsers; private set => parsers = value; }
        internal void addParser(string key, ParserCreator value)
        {
                parsers.Add(key, value);       
        }

    }
}
