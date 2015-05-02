using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DependencyElimination
{
    public class Page
    {
        public string url { get; set; }
        public IEnumerable<Match> links { get; set; }

        public Page()
        {
            this.url = GetPageURL(1);
        }

        public Page(int pageNumber)
        {
            this.url = GetPageURL(pageNumber);
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
    }
}