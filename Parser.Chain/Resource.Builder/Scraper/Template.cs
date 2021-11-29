using HtmlAgilityPack;

namespace Resource.Builder
{
    abstract class Template
    {
        public void Parse(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            this.GetTitle(doc);
            this.GetTags(doc);
        }

        protected void GetTitle(HtmlDocument _doc)
        {
            var title = _doc.DocumentNode.SelectSingleNode("//title");
        }

        protected abstract void GetTags(HtmlDocument _doc);
    }

    class ScrapeVideo : Template
    {
        protected override void GetTags(HtmlDocument _doc)
        {
            throw new System.NotImplementedException();
        }
    }

    class ScrapeArticle : Template
    {
        protected override void GetTags(HtmlDocument _doc)
        {
            throw new System.NotImplementedException();
        }
    }
}