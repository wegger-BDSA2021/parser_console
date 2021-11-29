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
using Resource.Builder;

namespace parser
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        async static Task MainAsync(string[] args)
        {
            var input = new InputFromAPI{
                Description = "test yo yo",
                InitialRating = 3,
                TagsCategories = null,
                Title = "this is a test",
                UserId = 1,
                // Url = "https://medium.com/@ergojdev/a-simple-web-scraper-in-30-minutes-with-net-core-and-anglesharp-part-1-51fdf5ecafb1"
                // Url = "https://www.youtube.com/watch?v=zA3PxYEomIk&t=3s&ab_channel=DRNyheder"
                Url = "https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5"
                // Url = "ub.com/WolfgangOfner/.NetCoreRepositoryAndUnitOfWorkPattern"
                // Url = "https://www.uml-diagrams.org/component.html"
            };

            var builder = new Builder(input);
            var director = new Director(builder);

            var result = await director.Make();

            System.Console.WriteLine(result.ToString());

            // await Template(args[0]);
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

