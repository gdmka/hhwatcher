using System;
using System.Threading;

namespace hhWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HCrawler Crawler = new HCrawler(args[0]);

                while (true)
                {
                    var doc = Crawler.Crawl();
                    Determinator.Parse(doc);
                    Thread.Sleep(60 * 60 * 1000); // Check every hour
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("URL is mandatory!");
                Environment.Exit(0);
            }

        }

    }
}
