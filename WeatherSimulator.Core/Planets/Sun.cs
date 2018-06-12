using System;
using System.Drawing;
using WeatherSimulator.Core.Common;

namespace WeatherSimulator.Core.Planets
{
	public class Sun
    {
		public readonly PlanetCoordinates GalaxyPosition;

        public Sun()
        {			
			this.GalaxyPosition = new PlanetCoordinates(0.0, 0.0);
        }
    }
}
