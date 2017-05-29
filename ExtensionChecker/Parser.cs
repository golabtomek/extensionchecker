using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ExtensionChecker
{
    public class Parser
    {
        public List<string> DeserializeXml(string XmlString)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(XmlString);

            XmlNodeList nodes = xml.DocumentElement.SelectNodes("/api/parse/text");

            List<string> extensionsTables = new List<string>();

            foreach (XmlNode node in nodes)
            {
                string table = node.InnerText;
                //Replacing <th> with <td> because of few cells which are in <th>
                string tableReplaced = table.Replace("<th>", "<td>").Replace("</th>", "</td>");

                extensionsTables.Add(tableReplaced);
            }

            return extensionsTables;
        }

        public Extensions ParseHTMLTable(List<string> HTMLTable)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(HTMLTable[0]);

            List<List<string>> table = html.DocumentNode.SelectSingleNode("//table")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

            Extensions extensions = new Extensions();

            foreach (List<string> column in table)
            {
                extensions.Ext.Add(column[0]);
                extensions.Description.Add(column[1]);
                if (column.Count == 3)
                {
                    extensions.UsedBy.Add(column[2]);
                }
                else
                {
                    extensions.UsedBy.Add(" ");
                }
            }

            return extensions;
        }

    }
}
