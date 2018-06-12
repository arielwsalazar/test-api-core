using System;
using System.Collections.Generic;
using System.Linq;
using WeatherSimulator.Core;

namespace WeatherSimulator
{
    class Program
    {   
		/// <summary>
        /// The max day, I asume that year has 360 days and 10 years are 3600 days.
        /// </summary>
		const int maxDay = 3600;

        static void Main(string[] args)
        {
			int Drought = 0;
            int OptimalCondition=0;
            int Rain=0;
            int MaxRain=0;
            List<int> MaxRainDays = new List<int>();
			Dictionary<int, string> DailyBrief = new Dictionary<int, string>();

            Console.WriteLine("Starting app");
            
			WeatherSimulatorCore w = new WeatherSimulatorCore();
            for (int i = 1; i <= maxDay; i++)
			{
				Console.WriteLine($"Processing Day #: {i}");
				switch (w.Run(i))
				{
					case Core.Enumerators.Weathercondition.Drought:
						Drought++;
						DailyBrief.Add(i, "Sequia");
						break;
					case Core.Enumerators.Weathercondition.Rain:
						Rain++;
						DailyBrief.Add(i, "Lluvia");
						break;
					case Core.Enumerators.Weathercondition.MaxRain:
						Rain++;
						MaxRain++;
						MaxRainDays.Add(i);
						DailyBrief.Add(i,"Lluvia maxima");
						break;
					case Core.Enumerators.Weathercondition.OptimalConditions:
						OptimalCondition++;
						DailyBrief.Add(i, "Condiciones Optimas");
						break;
					default:
						DailyBrief.Add(i, "Normal");
						break;
				}
			}

			Console.WriteLine("-------------------------");
			Console.WriteLine($"sequias: {Drought}");
			Console.WriteLine($"optimal: {OptimalCondition}");
			Console.WriteLine($"raining: {Rain}");
			Console.WriteLine($"max raining: {MaxRain}");
			Console.WriteLine($"--- max raining days:");
			foreach (var item in MaxRainDays)
			{
				Console.WriteLine($"max raining day: {item}");
			}
			Console.WriteLine("----Creating csv file----");

            //Create a cvs file 
			String csv = String.Join( 
			                         Environment.NewLine,
			                         DailyBrief.Select(d => d.Key + "," + d.Value)
			                        );
			
            System.IO.File.WriteAllText("db.csv", csv);
			Console.WriteLine("------end app----");
		}
    }
}
