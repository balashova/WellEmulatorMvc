using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WellEmulator.Service.Extension;

namespace WellEmulator.Service.Parsers
{
    public static class WitsmlElementsParser
    {
        struct Element
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
        }

        public static IEnumerable<string> GetWitsmlObjects(string standard)
        {
            var directory = new DirectoryInfo(string.Format(@"{1}\Standards\{0}", standard ?? "WITSML", AppDomain.CurrentDomain.BaseDirectory));

            var objects = new Dictionary<string, IEnumerable<Element>>();

            foreach (var file in directory.GetFiles("*.xsd"))
            {
                var document = XDocument.Load(file.FullName);
                if (document.Root == null) continue;

                var elements = new List<Element>();
                Parse(document.Root.Elements(), ref elements);

                if (elements.Any()) objects.Add(file.Name, elements);
            }

            return objects.Select(o => o.Key.Split('.').First());
        }

        public static IEnumerable<string> GetWitsmlElements(string standard, string @object)
        {
            FileInfo file;
            if (@object == null)
            {
                var dir = new DirectoryInfo(string.Format(@"{1}\Standards\{0}", standard ?? "WITSML", AppDomain.CurrentDomain.BaseDirectory));
                file = dir.GetFiles().First();
            }
            else
            {
                file = new FileInfo(string.Format(@"{2}\Standards\{0}\{1}.xsd", standard ?? "WITSML", @object, AppDomain.CurrentDomain.BaseDirectory));
            }
            var document = XDocument.Load(file.FullName);
            var elements = new List<Element>();
            if (document.Root != null) Parse(document.Root.Elements(), ref elements);
            return elements.Select(t => t.Name);
        }

        private static void Parse(DirectoryInfo directory)
        {
            var objects = new Dictionary<string, IEnumerable<Element>>();

            foreach (var file in directory.GetFiles("*.xsd"))
            {
                var document = XDocument.Load(file.FullName);
                if (document.Root == null) continue;

                var elements = new List<Element>();
                Parse(document.Root.Elements(), ref elements);

                if (elements.Any()) objects.Add(file.Name, elements);
            }
        }

        private static void Parse(IEnumerable<XElement> nodes, ref List<Element> elements)
        {
            foreach (var node in nodes)
            {
                if (node.Name.LocalName.Equals("element"))
                {
                    var name = node.Attribute("name");
                    var type = node.Attribute("type");

                    if (name != null && type != null /*&& !type.Value.Contains('_')*/)
                    {
                        elements.Add(new Element
                        {
                            Name = name.Value,
                            Type = type.Value.Split(':').Last(),
                            Description = node.Value.RemoveTabs()
                        });
                    }
                }
                else
                {
                    var subNodes = node.Elements().ToList();
                    if (subNodes.Any()) Parse(node.Elements(), ref elements);
                }
            }
        }
    }
}