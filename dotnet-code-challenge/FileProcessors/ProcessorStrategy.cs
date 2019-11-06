using System;
using System.Collections.Generic;
using System.IO;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.FileProcessors
{
    public abstract class ProcessorStrategy
    {
        public abstract FileProcessorType ProcessorType { get; }

        public abstract IEnumerable<Horse> Process(string filePath);

        public void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException($"File path cannot be empty");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File is missing at this location: {filePath}");
        }
    }
}
