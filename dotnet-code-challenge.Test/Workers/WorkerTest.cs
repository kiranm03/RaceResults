using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_code_challenge.FileProcessors;
using dotnet_code_challenge.Model;
using dotnet_code_challenge.RacePicker;
using dotnet_code_challenge.Util;
using dotnet_code_challenge.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace dotnet_code_challenge.Test.Workers
{
    [TestClass]
    public class WorkerTest
    {
        private Worker _subject;
        private readonly Mock<IRacePicker> _racePicker = new Mock<IRacePicker>();
        private readonly Mock<IFileProcessor> _fileProcessor = new Mock<IFileProcessor>();
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();
        private readonly Mock<ILogger<Worker>> _logger = new Mock<ILogger<Worker>>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new Worker(
                _racePicker.Object,
                _logger.Object,
                _configuration.Object,
                _fileProcessor.Object);
        }

        [TestMethod]
        public void Work_NoHorsesInFile_DisplayNoHorses()
        {
            //Arrange
            var expectedHorses = Enumerable.Empty<Horse>();

            _racePicker.Setup(x => x.GetRace())
                .Returns(Race.CaufieldRace);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(a => a.Value)
                .Returns("DummyPath");
            _configuration.Setup(a => a.GetSection(Constants.CAULFIELD_RACE_FILEPATH))
                .Returns(mockConfigurationSection.Object);

            _fileProcessor.Setup(x => x.Process(It.IsAny<FileProcessorType>(), It.IsAny<string>()))
                .Returns(expectedHorses);

            //Act
            _subject.Work();
            //Assert
        }

        [TestMethod]
        public void Work_UserPicksCaulfieldRace_DisplaysHorsesInPriceAscendingOrder()
        {
            //Arrange
            var expectedHorses = GetHorses();

            _racePicker.Setup(x => x.GetRace())
                .Returns(Race.CaufieldRace);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(a => a.Value)
                .Returns("DummyPath");
            _configuration.Setup(a => a.GetSection(Constants.CAULFIELD_RACE_FILEPATH))
                .Returns(mockConfigurationSection.Object);

            _fileProcessor.Setup(x => x.Process(It.IsAny<FileProcessorType>(), It.IsAny<string>()))
                .Returns(expectedHorses);

            //Act
            _subject.Work();
            //Assert
        }

        [TestMethod]
        public void Work_UserPicksWolferhamptonRace_DisplaysHorsesInPriceAscendingOrder()
        {
            //Arrange
            var expectedHorses = GetHorses();

            _racePicker.Setup(x => x.GetRace())
                .Returns(Race.WolferhamptonRace);

            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(a => a.Value)
                .Returns("DummyPath");
            _configuration.Setup(a => a.GetSection(Constants.WOLFERHAMPTON_RACE_FILEPATH))
                .Returns(mockConfigurationSection.Object);

            _fileProcessor.Setup(x => x.Process(It.IsAny<FileProcessorType>(), It.IsAny<string>()))
                .Returns(expectedHorses);

            //Act
            _subject.Work();
            //Assert
        }

        private IEnumerable<Horse> GetHorses()
        {
            return new Horse[]
            {
            new Horse { Name="Test1", Price=1.0},
            new Horse { Name="Test2", Price=6.0},
            new Horse { Name="Test3", Price=3.0}
            };
        }
    }
}
