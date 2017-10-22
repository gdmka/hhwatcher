using System;
using HtmlAgilityPack;

namespace hhWatcher
{
    public class HCrawler
    {
        private readonly string _url;
        private HtmlWeb crawler;

        public HCrawler(string url = null)
        {
            _url = url ?? throw new ArgumentNullException($"{url} is mandatory");
            crawler = new HtmlWeb();
        }

        public HtmlDocument Crawl()
        {
            HtmlDocument page = crawler.Load(_url);
            return page;
        }

    }
}
