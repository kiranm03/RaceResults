using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using dotnet_code_challenge.Models;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.FileProcessors
{
    public class XmlProcessor : ProcessorStrategy
    {
        private readonly ILogger<XmlProcessor> _logger;

        public XmlProcessor(ILogger<XmlProcessor> logger)
        {
            _logger = logger;
        }

        public override FileProcessorType ProcessorType
        {
            get
            {
                return FileProcessorType.XmlProcessor;
            }
        }

        public override IEnumerable<Horse> Process(string filePath)
        {
            try
            {
                ValidateFilePath(filePath);

                var doc = XDocument.Load(filePath);

                var horseElements = doc
                    .Descendants("race")
                    .Select(r => r.Descendants("horses")).First();

                var horseNames = horseElements
                    .First()
                    .Descendants("horse")
                    .Select(h => new Horse
                    {
                        Id = h.Element("number").Value,
                        Name = h.Attribute("name").Value
                    });

                var horses = horseElements
                    .Last()
                    .Descendants("horse")
                    .Select(h => new Horse
                    {
                        Id = h.Attribute("number").Value,
                        Price = Convert.ToDouble(h.Attribute("Price").Value),
                        Name = horseNames.Single(x => x.Id.Equals(h.Attribute("number").Value)).Name
                    });

                return horses;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while processing XML file: {ex.Message}");
                throw;
            }
        }
    }
}
