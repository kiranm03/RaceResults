using System;
using dotnet_code_challenge.FileProcessors;

namespace dotnet_code_challenge.Factory
{
    public class ProcessorStrategyFactory
    {
        private readonly XmlProcessor _xmlProcessor;
        private readonly JsonProcessor _jsonProcessor;

        public ProcessorStrategyFactory(XmlProcessor xmlProcessor, JsonProcessor jsonProcessor)
        {
            _xmlProcessor = xmlProcessor;
            _jsonProcessor = jsonProcessor;
        }

        public ProcessorStrategy[] Create()
        {
            return new ProcessorStrategy[] { _xmlProcessor, _jsonProcessor };
        }
    }
}
