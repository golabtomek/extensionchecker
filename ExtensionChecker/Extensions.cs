using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionChecker
{
    public class Extensions
    {
        public List<string> Ext { get; set; }
        public List<string> Description { get; set; }
        public List<string> UsedBy { get; set; }

        public Extensions()
        {
            Ext = new List<string>();
            Description = new List<string>();
            UsedBy = new List<string>();
        }
    }

    public class ExtensionsParser
    {
        private WikiApi wikiApi;
        private List<string> results;
        private Parser parser;
        private Extensions Extensions { get; set; }

        public ExtensionsParser(List<string> results)
        {
            this.results = results;
            wikiApi = new WikiApi();
            parser = new Parser();
            Extensions = new Extensions();
        }

        private List<List<string>> DeserializeResults()
        {
            List<List<string>> extensionsTables = new List<List<string>>();
            foreach(string result in results)
            {
                List<string> table = parser.DeserializeXml(result);
                extensionsTables.Add(table);
            }
            return extensionsTables;
        }

        private void ParseHTMLTables(List<List<string>> Tables)
        {
            foreach(List<string> table in Tables)
            {
                Extensions ext = parser.ParseHTMLTable(table);
                Extensions.Ext.AddRange(ext.Ext);
                Extensions.Description.AddRange(ext.Description);
                Extensions.UsedBy.AddRange(ext.UsedBy);
            }
        }

        public Extensions GetExtensions()
        {
            List<List<string>> Tables = DeserializeResults();
            ParseHTMLTables(Tables);
            return Extensions;
        }
    }
}
