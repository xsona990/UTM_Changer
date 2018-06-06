using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTM_Changer.Parser
{
    [Serializable]
    class ParserSettings : IParserSettings
    {
        public ParserSettings(string baseUrl, string prefix, int startPoint, int endPoint)
        {
            BaseUrl = baseUrl;
            Prefix = prefix;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public string BaseUrl { get; set; } //= "https://habr.com";
        public string Prefix { get; set; } //= "page{CurrentId}";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
