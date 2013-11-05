﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WellEmulatorMvc.Extensions;

namespace WellEmulatorMvc.Parsers
{
    public static class WitsmlElementsParser
    {
        public static void Parse(DirectoryInfo directory)
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

        struct Element
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
        }
    }
}