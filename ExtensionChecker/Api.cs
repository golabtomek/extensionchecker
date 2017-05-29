using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ExtensionChecker
{
    public static class Api
    {
        public static string GetResultFromApi(string url)
        {
            try
            {
                return new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            }
            catch(Exception)
            {
                Console.WriteLine("Connection Error");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return "";
        }
        
    }

    public class WikiApi
    {
        private List<string> PagesNames { get; set; }
        private List<int> PageSectionStartNumber { get; set; }
        private List<int> PageSectionsCount { get; set; }
        string url = "https://en.wikipedia.org/w/api.php?action=parse&format=xml&prop=text&";

        public WikiApi()
        {
            PagesNames = new List<string>();
            PageSectionStartNumber = new List<int>();
            PageSectionsCount = new List<int>();

            SetPagesNames();
            SetPageSectionStartNumber();
            SetPageSectionsCount();
        }

        private void SetPagesNames()
        {
            PagesNames.Add("List_of_filename_extensions");
            PagesNames.Add("List_of_filename_extensions_(A–E)");
            PagesNames.Add("List_of_filename_extensions_(F–L)");
            PagesNames.Add("List_of_filename_extensions_(M–R)");
            PagesNames.Add("List_of_filename_extensions_(S–Z)");
        }

        private void SetPageSectionStartNumber()
        {
            PageSectionStartNumber.Add(1);
            PageSectionStartNumber.Add(1);
            PageSectionStartNumber.Add(2);
            PageSectionStartNumber.Add(3);
            PageSectionStartNumber.Add(4);
        }

        private void SetPageSectionsCount()
        {
            PageSectionsCount.Add(2);
            PageSectionsCount.Add(5);
            PageSectionsCount.Add(7);
            PageSectionsCount.Add(6);
            PageSectionsCount.Add(8);
        }

        public List<string> GetResults()
        {
            List<string> Results = new List<string>();
            for(int i = 0; i<5; i++)
            {
                for(int l = PageSectionStartNumber[i]; l<PageSectionStartNumber[i]+PageSectionsCount[i]; l++)
                {
                    string result = Api.GetResultFromApi(url + "page=" + PagesNames[i] + "&section=" + l);
                    Results.Add(result);
                }
            }
            return Results;
        }
    }
}
