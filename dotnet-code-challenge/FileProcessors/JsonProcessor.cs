using System;
using System.Collections.Generic;
using dotnet_code_challenge.Model;
using Microsoft.Extensions.Logging;

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
            ValidateFilePath(filePath);

            return null;
        }
    }
}
