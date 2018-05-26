using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTM_Changer.Properties;

namespace UTM_Changer
{
    [Serializable]
    public class UserPrefs
    {
        private bool shouldSaveData = true;
        private string utm_source = "facebook.com";
        private string utm_medium = "social";

        public string Utm_source { get => utm_source; set => utm_source = value; }
        public string Utm_medium { get => utm_medium; set => utm_medium = value; }
        public bool ShouldSaveData { get => shouldSaveData; set => shouldSaveData = value; }


    }
}
