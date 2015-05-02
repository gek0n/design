using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DependencyElimination
{
	internal class Program
	{
        private static int totalLinks = 0;
        public static SimpleLogger logger = new SimpleLogger();
	    private static string filename;

		private static void Main(string[] args)
		{
		    if (args.Length != 1)
		    {
		        logger.log("Введите имя файла для сохранения ссылок\n");
		        return;
		    }
		    filename = args[0];
			var sw = Stopwatch.StartNew();
		    
		    ParsePages();          
            PrintEndStat(sw.Elapsed.ToString());
		}

        private static void ParsePages()
        {
            using (var http = new HttpClient())
                for (int page = 1; page < 6; page++)
                {
                    var url = GetPageURL(page);
                    logger.log(url);
                    CheckAndParsePage(http, url);
                }
        }

        private static void CheckAndParsePage(HttpClient http, string url)
        {
            HttpResponseMessage habrResponse = GetResponse(http, url);
            if (habrResponse.IsSuccessStatusCode)
                ParseResponseAndWriteToOutput(habrResponse);
            else
            {
                logger.log("Error: " + habrResponse.StatusCode + " " + habrResponse.ReasonPhrase);
            }
        }

	    private static HttpResponseMessage GetResponse(HttpClient http, string url)
	    {
	        return http.GetAsync(url).Result;
	    }

        private static void ParseResponseAndWriteToOutput(HttpResponseMessage response)
        {
            string content = GetPageContent(response);
            var links = GetLinks(content);
            var count = 0;
            using (var output = new StreamWriter(filename, false))
            {
                foreach (var link in links)
                {
                    output.WriteLine(link.Groups[1].Value);
                    totalLinks++;
                    count++;
                }   
            }
            logger.log("found " + count + " links");
        }


        private static string GetPageURL(int page)
        {
            var res = "http://habrahabr.ru/top/page" + page;
            return res;
        }

        private static string GetPageContent(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        private static IEnumerable<Match> GetLinks(string content)
        {
            return Regex.Matches(content, @"\Whref=[""'](.*?)[""'\s>]").Cast<Match>();
        }

	    private static void PrintEndStat(string res)
	    {
            logger.log("Total links found: " + totalLinks);
            logger.log("Finished");
            logger.log(res);
	    }
	}
}