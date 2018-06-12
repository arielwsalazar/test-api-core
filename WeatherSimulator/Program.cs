using System;
using System.Collections.Generic;
using WeatherSimulator.Core;

namespace WeatherSimulator
{
    class Program
    {      
		const int maxDay = 3600;
        static void Main(string[] args)
        {
			int Drought = 0;
            int OptimalCondition=0;
            int Rain=0;
            int MaxRain=0;
            List<int> MaxRainDays = new List<int>();

            Console.WriteLine("Starting app");
            
			WeatherSimulatorCore w = new WeatherSimulatorCore();
            for (int i = 1; i <= maxDay; i++)
			{
				Console.WriteLine($"Processing Day #: {i}");
				switch (w.Run(i))
				{
					case Core.Enumerators.Weathercondition.Drought:
						Drought++;
						break;
					case Core.Enumerators.Weathercondition.Rain:
						Rain++;
						break;
					case Core.Enumerators.Weathercondition.MaxRain:
						Rain++;
						MaxRain++;
						MaxRainDays.Add(i);
						break;
					case Core.Enumerators.Weathercondition.OptimalConditions:
						OptimalCondition++;
						break;
				}
			}

			Console.WriteLine("-------------------------");
			Console.WriteLine($"sequias: {Drought}");
			Console.WriteLine($"optimal: {OptimalCondition}");
			Console.WriteLine($"raining: {Rain}");
			Console.WriteLine($"max raining: {MaxRain}");
			Console.WriteLine("------end app----");

		}
    }
}
