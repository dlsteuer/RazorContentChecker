using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using static System.Console;

namespace RazorContentChecker
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (!args.Any())
            {
                WriteLine("Usage: RazorContentCheck csprojPath");
                Environment.Exit(1);
            }

            var csproj = args[0];
            var file = File.ReadAllText(csproj);
            var document = XDocument.Parse(file);

            ProcessFile(document.Descendants());

            Environment.Exit(0);

        }

        private static void ProcessFile(IEnumerable<XElement> descendants)
        {
            var badFiles = new List<string>();
            foreach (var node in descendants)
            {
                if (node.Attribute("Include") != null && node.Attribute("Include").Value.EndsWith(".cshtml", StringComparison.CurrentCulture))
                {
                    var file = node.Attribute("Include").Value;
                    WriteLine($"{file} has node type ${node.Name.LocalName}");
                    if (node.Name.LocalName != "Content")
                    {
                        badFiles.Add(file);
                        WriteError($"ERROR: ${file} has the wrong Build Action, it should be $Content instead it is ${node.Name.LocalName}");
                    }
                }
            }

            if (badFiles.Any())
            {
                WriteLine();
                WriteError("Files To Check");
                foreach (var badFile in badFiles)
                    WriteError(badFile);
                WriteLine();
                Environment.Exit(1);
            }
        }

        private static void WriteError(string format)
        {
            var existingColor = ForegroundColor;
            ForegroundColor = ConsoleColor.Red;
            WriteLine(format);
            ForegroundColor = existingColor;
        }
    }
}
