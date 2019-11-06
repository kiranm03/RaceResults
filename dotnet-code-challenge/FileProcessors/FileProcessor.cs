using System;
using System.Collections.Generic;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.FileProcessors
{
    public class FileProcessor : IFileProcessor
    {
        public FileProcessor()
        {
        }

        public IEnumerable<Horse> Process(FileProcessorType fileProcessorType, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
