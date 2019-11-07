using System;
using Microsoft.Extensions.Logging;

namespace dotnet_code_challenge.RacePicker
{
    public class RacePicker : IRacePicker
    {
        private readonly ILogger<RacePicker> _logger;

        public RacePicker(ILogger<RacePicker> logger)
        {
            _logger = logger;
        }

        public Race GetRace()
        {
            var race = Race.None;
            bool showInstructionsAgain;
            do
            {
                ShowInstructions();

                var userSelection = Console.ReadLine();

                showInstructionsAgain = false;

                switch (userSelection)
                {
                    case "1":
                        race = Race.CaufieldRace;
                        break;
                    case "2":
                        race = Race.WolferhamptonRace;
                        break;
                    default:
                        _logger.LogDebug($"User has selected an wrong value : {userSelection}");
                        showInstructionsAgain = true;
                        break;
                }
            } while (showInstructionsAgain);

            return race;
        }

        private static void ShowInstructions()
        {
            Console.WriteLine("Please select from one of the following options");
            Console.WriteLine("Press '1' for Caulfield_Race1.xml");
            Console.WriteLine("Press '2' for Wolferhampton_Race1.json");
        }
    }
}
