using System;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace hhWatcher
{
    public static class Determinator
    {
        private static string _positions = "";
        public static string positions
        {
            get => _positions;
            set
            {
                OnChange(value);
            }
        }

        private static HtmlDocument _doc;

        private static Dictionary<int, string> jobData = new Dictionary<int, string>();

        private static string OnChange(string val)
        {

            HtmlNode table = _doc.DocumentNode.SelectSingleNode($"//table[contains(@class, '{tableSelector}')]");
            List<string> newVerbose = new List<string>();
            List<int> t = new List<int>();

            foreach (var node in table.ChildNodes)
            {
                string VerboseDesc = node.InnerText;
                int HashCode = VerboseDesc.GetHashCode();

                if (!jobData.ContainsKey(HashCode))
                {
                    jobData.Add(HashCode, VerboseDesc);
                    newVerbose.Add(VerboseDesc);
                }

                t.Add(HashCode);
                cleanDict(t);
            }

            SendNotify(val, newVerbose);
            _doc = null;
            return val;
        }
        const string posSelector = "b-employerpage-vacancies-hint";
        const string tableSelector = "l-table";

        public static void Parse(HtmlDocument doc)
        {
            string pos = doc.DocumentNode.SelectSingleNode($"//span[contains(@class, '{posSelector}')]").InnerHtml;
            _doc = doc;
            PosChanged(pos);
        }

        public static NotifyDeletgate SendNotify = Notifier.Notify;

        private static void PosChanged(string pos)
        {

            if (positions == "") { positions = pos; }            else if (Int32.Parse(positions) != Int32.Parse(pos))
            {
                positions = pos;
            }
        }

        private static void cleanDict(List<int> jobs)
        {
            var oldRecords = jobData.Keys.ToList().Except(jobs).ToList();
            foreach (var record in oldRecords)
            {
                jobData.Remove(record);
            }
        }

    }
}
