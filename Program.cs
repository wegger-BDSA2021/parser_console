using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using ConcurrentCollections;
using HtmlAgilityPack;

namespace parser
{
    class Program
    {
        // private static string siteUrl = "https://medium.com/@ergojdev/a-simple-web-scraper-in-30-minutes-with-net-core-and-anglesharp-part-1-51fdf5ecafb1";
        // private static string siteUrl = "https://www.intel.com/content/www/us/en/developer/articles/technical/how-to-use-loop-blocking-to-optimize-memory-use-on-32-bit-intel-architecture.html";

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async static Task MainAsync(string[] args)
        {
            tags = new List<string>();
            for (int i = 0; i < 10000000; i++)
            {
                tags.Add(i.ToString() + "aaabbbbabbababababbababab");
            }

            await Template(args[0]);
        }

        private static string[] QueryTerms { get; } =
        {
            "anglesharp", "docker",
            "dotnet" , ".NET",
            "web scraping", "syntax",
            "xpath", "html",
            "github", "htmlagilitypack",
            "c#", "pascal", "DTO",
            "design pattern", "stopwatch"
        };

        private static ICollection<string> tags;
        static HttpClient client = new HttpClient();

        async static Task Template(string url)
        {
            var timer = new Stopwatch();
            timer.Start();

            var content = await getContentFromUrl(url);

            var html = getHtml(content);

            var title = getTitle(html);
            var tagsFound = getTags(html);

            System.Console.WriteLine("\nTitle of given url:");
            System.Console.WriteLine("    - " + title);
            System.Console.WriteLine();
            System.Console.WriteLine("Amount of tags found: " + tagsFound.Count());
            System.Console.WriteLine();
            System.Console.WriteLine("Tags:");
            tagsFound.ToList().ForEach(t => System.Console.WriteLine("    - " + t));
            System.Console.WriteLine();

            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            System.Console.WriteLine(foo);
        }

        async static Task<string> getContentFromUrl(string url)
        {
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        static HtmlDocument getHtml(string content)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            return doc;
        }

        static string getTitle(HtmlDocument doc)
        {
            var title = doc.DocumentNode.SelectSingleNode("//title");
            return title.InnerText;
        }

        static ICollection<string> getTags(HtmlDocument doc)
        {
            var foundTagsConcurrent = new ConcurrentBag<string>();
            var foundTags = new List<string>();

            var content = doc.DocumentNode.SelectNodes("//p");

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
                tags, tag =>
                {
                    if (cleanedText.Contains(tag.ToLower()))
                    {
                        foundTagsConcurrent.Add(tag);
                    }
                }
            );

            // foreach (var tag in tags)
            // {
            //     if (cleanedText.Contains(tag.ToLower()))
            //     {
            //         foundTags.Add(tag);
            //     }
            // }

            // return foundTags.ToList();
            return foundTagsConcurrent.ToList();
        }

        // 1) get html doument async

        // 2) clean html document and disect inro relevant string data
        //  - get title (done)
        //  - get content of article (done)
        //  - get date of article 
        //  - get links from article 
        //  - detmernie video or article
        //  - read packages 
        //  - check if source url is from official documentation
    }
}

