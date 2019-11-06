using System;
using System.Collections.Generic;
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
            ValidateFilePath(filePath);
            return null;
        }
    }
}
