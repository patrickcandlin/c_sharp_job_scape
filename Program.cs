using System;
using System.Net.Http;

namespace jobScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "https://denver.craigslist.org/search/bia?query=kids";
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url);

            Console.WriteLine(html.Result);

            Console.ReadLine();
        }
    }
}
