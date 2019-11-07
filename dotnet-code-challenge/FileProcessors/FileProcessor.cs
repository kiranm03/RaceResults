using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_code_challenge.Model;

namespace dotnet_code_challenge.FileProcessors
{
    public class FileProcessor : IFileProcessor
    {
        private ProcessorStrategy[] _processors;

        public FileProcessor(ProcessorStrategy[] processors)
        {
            _processors = processors;
        }

        public IEnumerable<Horse> Process(FileProcessorType fileProcessorType, string filePath)
        {
            return _processors
                .Single(p => p.ProcessorType.Equals(fileProcessorType))
                .Process(filePath);
        }
    }
}
