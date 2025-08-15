using HtmlAgilityPack;

namespace BourbonAe.Core.Services.Html
{
    public interface IHtmlParserService
    {
        IEnumerable<string> SelectInnerTexts(string html, string xpath);
    }

    public sealed class HtmlParserService : IHtmlParserService
    {
        public IEnumerable<string> SelectInnerTexts(string html, string xpath)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.SelectNodes(xpath)?.Select(n => n.InnerText.Trim()) ?? Enumerable.Empty<string>();
        }
    }
}
