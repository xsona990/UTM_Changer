using System.Threading.Tasks;
using AngleSharp.Dom.Html;

namespace UTM_Changer.Parser.Core
{
    interface IParser<T> where T:class
    {
        T Parse(IHtmlDocument document);
    }
}
