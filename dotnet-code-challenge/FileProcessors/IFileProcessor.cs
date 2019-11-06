using System;
using System.Collections.Generic;
using dotnet_code_challenge.Model;

namespace dotnet_code_challenge.FileProcessors
{
    public interface IFileProcessor
    {
        IEnumerable<Horse> Process(FileProcessorType fileProcessorType, string filePath);
    }
}
