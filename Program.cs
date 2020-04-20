using System;
using System.Linq;
using System.Net.Http;
using HtmlAgilityPack;

namespace jobScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();

            Console.ReadLine();
        }
        private static async void GetHtmlAsync()
        {
            var url = "https://denver.craigslist.org/search/bia?query=kids";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var bikesHtml = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("rows")).ToList();

            var bikesList = bikesHtml[0].Descendants("li")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("result-row")).ToList();



            foreach (var bike in bikesList )
            {
                Console.WriteLine(bike.Descendants("time")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("result-date")).FirstOrDefault().InnerText);

                Console.WriteLine(bike.Descendants("a")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("result-title")).FirstOrDefault().InnerText);
                
                Console.WriteLine(bike.Descendants("span")
                .Where(node => node.GetAttributeValue("class", "")
                    .Equals("result-meta"))
                    .FirstOrDefault().Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("result-price"))
                        .FirstOrDefault().InnerText);


                Console.WriteLine();
            }
            // Console.WriteLine(bikesHtml[0].InnerHtml);
        }
    }
}
