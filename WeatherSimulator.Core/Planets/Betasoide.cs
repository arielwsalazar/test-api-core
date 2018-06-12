using System;
using WeatherSimulator.Core.Interfaces;

namespace WeatherSimulator.Core.Planets
{
	public class Betasoide : Planet
    {
		readonly int R = 2000;
		readonly int ANGLE_SPEED = 3;
		readonly int ANGLE_INITIAL_POSITION = 90;

		public Betasoide()
        {
			base.radio = R;
			base.PlanetSpeed = ANGLE_SPEED;
			base.Direction = Enumerators.PlanetTranslation.Clockwise;
			base.AnglePosition = ANGLE_INITIAL_POSITION;
			base.InitialPosition = ANGLE_INITIAL_POSITION;
        }
        

    }
}
