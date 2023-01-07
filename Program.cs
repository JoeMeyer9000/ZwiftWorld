using System;
using System.IO;
using System.Xml;

// Reference:
// https://zwiftinsider.com/world-tag

// Author:
// Joe Meyer joe.meyer@iceteagroup.com

namespace ZwiftWorld
{
    internal class Program
    {
        private struct World
        {
            public int Number;
            public string Name;
        }

        private static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private static readonly World[] Worlds = new World[] {
                new World{ Number = 1, Name = "Watopia" },
                new World{ Number = 2, Name = "Richmond" },
                new World{ Number = 3, Name = "London" },
                new World{ Number = 4, Name = "New York" },
                new World{ Number = 5, Name = "Innsbruck" },
                new World{ Number = 7, Name = "Yorkshire" },
                new World{ Number = 9, Name = "Makuri Islands" },
                new World{ Number = 10, Name = "France" },
                new World{ Number = 11, Name = "Paris" },
            };

        static void Main()
        {
            try
            {
                // open file
                string Filename = Path.Combine(MyDocuments, "Zwift", "prefs.xml");
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(Filename);

                // find WORLD node
                XmlElement worldNode = FindWorldNode(xmlDoc);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string s = worldNode is null ? "undefined" : GetWorldName(worldNode.InnerText);
                Console.WriteLine($"Current world is \"{s}\"");
                Console.WriteLine();
                Console.ResetColor();

                // output the list of worlds together with their number
                Console.WriteLine("Available Worlds:");
                foreach (var world in Worlds)
                    Console.WriteLine($"{world.Number,-2} {world.Name}");

                // enter the new world number
                Console.WriteLine();
                Console.Write("Select world number or press ENTER to exit: ");
                var selected = Console.ReadLine();

                // if no input then abort
                if (string.IsNullOrEmpty(selected))
                    Environment.Exit(2);

                // if the user entered a number it must be a valid world number
                if (!int.TryParse(selected, out int id) || string.IsNullOrEmpty(GetWorldName(id)))
                    throw new Exception("Invalid input, a world numbers is expected");

                // create WORLD node if it doesn't exist
                if (worldNode == null)
                {
                    worldNode = xmlDoc.CreateElement("WORLD");
                    xmlDoc.DocumentElement.InsertBefore(worldNode, xmlDoc.DocumentElement.FirstChild);
                }

                // change world and save
                worldNode.InnerText = id.ToString();
                xmlDoc.Save(Filename);

                // done, wait for key press
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine();
                Console.WriteLine($"The Zwift world has been changed to \"{GetWorldName(id)}\"");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                // oops, something went wrong
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        private static XmlElement FindWorldNode(XmlDocument xmlDoc)
        {
            // by design the WORLD node is expected to be the first sub node under the ZWIFT node
            var expectedWorldNode = xmlDoc.DocumentElement.FirstChild;
            if (expectedWorldNode is XmlElement element && element.Name == "WORLD")
                return element;
            return null;
        }

        private static string GetWorldName(string worldNumber) => string.IsNullOrEmpty(worldNumber) ? "" : GetWorldName(int.Parse(worldNumber));

        private static string GetWorldName(int worldNumber)
        {
            foreach (var world in Worlds)
                if (world.Number == worldNumber)
                    return world.Name;
            return "";
        }
    }
}
