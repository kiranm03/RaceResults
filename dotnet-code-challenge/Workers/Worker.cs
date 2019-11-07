using System;
using System.Collections.Generic;
using System.Linq;
using dotnet_code_challenge.FileProcessors;
using dotnet_code_challenge.Model;
using dotnet_code_challenge.RacePicker;
using dotnet_code_challenge.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.Workers
{
    public class Worker
    {
        private readonly IRacePicker _racePicker;
        private readonly ILogger<Worker> _logger;
        private readonly IFileProcessor _fileProcessor;
        private readonly IConfiguration _configuration;

        public Worker(IRacePicker racePicker, ILogger<Worker> logger, IConfiguration configuration, IFileProcessor fileProcessor)
        {
            _racePicker = racePicker;
            _logger = logger;
            _configuration = configuration;
            _fileProcessor = fileProcessor;
        }

        public void Work()
        {
            try
            {
                var race = _racePicker.GetRace();
                var fileProcessorType = GetFileProcessorType(race, out string filePath);
                var horses = _fileProcessor.Process(fileProcessorType, filePath);

                // Arrange order of horses in price ascending order
                horses = horses
                    ?.OrderBy(h => h.Price);

                DisplayHorses(horses);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something unexpected happened: {ex.Message}");
                throw;
            }
        }

        private void DisplayHorses(IEnumerable<Horse> horses)
        {
            if (!horses.Any())
            {
                Console.WriteLine("There are no Horses in given file");
                return;
            }

            Console.WriteLine("Horse Name :: Horse Price");
            Console.WriteLine("--------------------------");
            foreach (var horse in horses)
            {
                Console.WriteLine($"{horse.Name} :: {horse.Price}");
            }
        }

        private FileProcessorType GetFileProcessorType(Race race, out string filePath)
        {
            switch (race)
            {
                case Race.CaufieldRace:
                    filePath = _configuration.GetValue<string>(Constants.CAULFIELD_RACE_FILEPATH);
                    return FileProcessorType.XmlProcessor;
                case Race.WolferhamptonRace:
                    filePath = _configuration.GetValue<string>(Constants.WOLFERHAMPTON_RACE_FILEPATH);
                    return FileProcessorType.JsonProcessor;
                default:
                    throw new ArgumentException($"Wrong selection of race: {race}");
            }
        }
    }
}
