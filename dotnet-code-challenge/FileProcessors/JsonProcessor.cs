using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnet_code_challenge.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace dotnet_code_challenge.FileProcessors
{
    public class JsonProcessor: ProcessorStrategy
    {
        private readonly ILogger<JsonProcessor> _logger;

        public JsonProcessor(ILogger<JsonProcessor> logger)
        {
            _logger = logger;
        }

        public override FileProcessorType ProcessorType
        {
            get
            {
                return FileProcessorType.JsonProcessor;
            }
        }

        public override IEnumerable<Horse> Process(string filePath)
        {
            try
            {
                ValidateFilePath(filePath);

                dynamic data = JsonConvert.DeserializeObject(File.ReadAllText(filePath));

                IEnumerable<dynamic> markets = data.RawData.Markets;
                IEnumerable<dynamic> selections = markets
                    .Select(m => m.Selections).First();

                var horses = selections
                    .Select(s => new Horse
                    {
                        Name = s.Tags.name,
                        Price = Convert.ToDouble(s.Price)
                    });

                return horses;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while processing Json file: {ex.Message}");
                throw;
            }
        }
    }
}
