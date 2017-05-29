using System;
using System.Collections.Generic;
using System.IO;

namespace ExtensionChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Downloading extensions database from Wikipedia... Please wait\n");

            WikiApi wikiApi = new WikiApi();
            ExtensionsParser parser = new ExtensionsParser(wikiApi.GetResults());
            Extensions extensions = parser.GetExtensions();

            string path = args[0];
            string extension = "";
            string extensionWithoutPoint = "";

            try
            {
                extension = Path.GetExtension(path);
                extensionWithoutPoint = extension.Remove(0, 1);
            }
            catch(Exception)
            {
                Console.WriteLine("File not found/Wrong Path");
                Console.WriteLine("Press any key to exit");
                Environment.Exit(0);
            }

            if (extensions.Ext.Contains(extensionWithoutPoint.ToUpper()))
            {
                int indexOfValue = extensions.Ext.FindIndex(a => a.Contains(extensionWithoutPoint.ToUpper()));

                Console.WriteLine("Extension:  " + extensions.Ext[indexOfValue]);
                Console.WriteLine("Description:  " + extensions.Description[indexOfValue]);
                Console.WriteLine(extensions.UsedBy[indexOfValue]);
            }
            else
            {
                Console.WriteLine("Extension not found in database");
            }
            
            Console.WriteLine("Press any key to exit\n");
            Console.ReadKey();
        }
    }
}
