using System;
using System.IO;
using dotnet_code_challenge.FileProcessors;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace dotnet_code_challenge.Test.FileProcessors
{
    [TestClass]
    public class JsonProcessorTest
    {
        private JsonProcessor _subject;
        private readonly Mock<ILogger<JsonProcessor>> _logger = new Mock<ILogger<JsonProcessor>>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new JsonProcessor(_logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Process_EmptyPath_ThrowsArgumentException()
        {
            //Arrange
            var filePath = string.Empty;
            //Act
            var actual = _subject.Process(filePath);
            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Process_FileNotFoundInPath_ThrowsFileNotFoundException()
        {
            //Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            //Act
            var actual = _subject.Process(filePath);
            //Assert
        }
    }
}
