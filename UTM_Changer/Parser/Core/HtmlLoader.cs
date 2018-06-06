using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UTM_Changer.Parser
{
    [Serializable]
    class HtmlLoader
    {
        #region Variables
        readonly HttpClient client;
        readonly string url;
        #endregion
        #region Constuctors
        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}{settings.Prefix}/";
        }
        #endregion
        #region Methods
        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            var response = await client.GetAsync(currentUrl);
            string source = null;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;

        }
        #endregion
    }
}
