using System;
namespace WeatherSimulator.Core.Enumerators
{
    public enum PlanetTranslation
    {
		Clockwise = 0,
		Counterclockwise = 1
    }

    public enum Weathercondition
	{
		Drought = 0,
        OptimalConditions = 1,
        Rain = 2,
        MaxRain = 3,
        Normal = 4
	}

	public enum ProcessType
	{
		IncludeOrigin = 0,
        ExcludeOrigin = 1
	}
}
