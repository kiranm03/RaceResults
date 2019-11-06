using System;
using System.Collections.Generic;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.FileProcessors
{
    public class XmlProcessor : ProcessorStrategy
    {
        public XmlProcessor()
        {
        }

        public override FileProcessorType ProcessorType => throw new NotImplementedException();

        public override IEnumerable<Horse> Process(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
