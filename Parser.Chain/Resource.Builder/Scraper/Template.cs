using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Resource.Builder
{
    abstract class Template
    {

        // instead of this, retrieve tags from tagRepository
        protected static string[] QueryTerms { get; } =
        {
            "anglesharp", "docker",
            "dotnet" , ".NET",
            "web scraping", "syntax",
            "xpath", "html",
            "github", "htmlagilitypack",
            "c#", "pascal", "DTO",
            "design pattern", "stopwatch"
        };

        public void Parse(string content, ResourceProduct product)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);

            product.TitleFromSource = this.GetTitle(doc);
            product.TagsFoundInSource = this.GetTags(doc);
        }

        protected string GetTitle(HtmlDocument _doc)
        {
            var title = _doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        protected abstract ICollection<string> GetTags(HtmlDocument _doc);
    }

    class ScrapeVideo : Template
    {
        protected override ICollection<string> GetTags(HtmlDocument _doc)
        {
            // use some kind of api like this : 
            // https://mattw.io/youtube-metadata/
            // to get relevant meta data and date from the video
            throw new System.NotImplementedException();
        }
    }

    class ScrapeArticle : Template
    {
        protected override ICollection<string> GetTags(HtmlDocument _doc)
        {
            var foundTagsConcurrent = new ConcurrentBag<string>();
            var foundTags = new List<string>();

            var content = _doc.DocumentNode.SelectNodes("//p");

            if (content is null)
                throw new Exception();

            StringBuilder sb = new StringBuilder();

            foreach (var item in content)
            {
                sb.Append(item.InnerText.ToLower() + " ");
            }

            var cleanedText = sb.ToString();

            Parallel.ForEach
            (
                QueryTerms, tag =>
                {
                    if (cleanedText.Contains(tag.ToLower()))
                    {
                        foundTagsConcurrent.Add(tag);
                    }
                }
            );

            return foundTagsConcurrent.ToList();
        }
    }
}