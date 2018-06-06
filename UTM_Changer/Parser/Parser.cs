using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTM_Changer.Parser
{
    [Serializable]
    class Parser : IParser<ParsingResult[]>
    {
        public Parser()
        { }
        public ParsingResult[] Parse(IHtmlDocument document, string querySelector, string className)
        {
            var list = new List<ParsingResult>();
            var items = document.QuerySelectorAll(querySelector).Where(item => item.ClassName != null && item.ClassName.Contains(className));
            foreach (var item in items)
            {
                list.Add(new ParsingResult(item.TextContent, item.OuterHtml));
            }
            return list.ToArray();
        }
    }
    [Serializable]
    public class ParsingResult
    {
        string textContent { get; set; }
        string outerHTML { get; set; }

        public string getText()
        {
            return textContent;
        }
        public string getOuterHTML()
        {
            return outerHTML;
        }
        public string getHrefLink()
        {
            return outerHTML.Split('"', '"')[1]; ;
        }

        public ParsingResult(string textContent, string outerHTML)
        {
            this.textContent = textContent;
            this.outerHTML = outerHTML;
        }
    }
}
