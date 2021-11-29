using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Resource.Builder
{
    class ScrapeArticle : Template
    {
        protected override async Task<ICollection<string>> GetTags(HtmlDocument _doc)
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

            var result = foundTagsConcurrent.ToList();
            return await Task.FromResult(result);
        }
    }
}