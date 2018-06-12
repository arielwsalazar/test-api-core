using System;
namespace WeatherSimulator.Core.Planets
{
	public class Ferengi : Planet
    {
		readonly int ANGLE_SPEED = 1;
		readonly int R = 500;
		readonly int ANGLE_INITIAL_POSITION = 90;

        public Ferengi()
        {
			base.PlanetSpeed = ANGLE_SPEED;
			base.Direction = Enumerators.PlanetTranslation.Clockwise;
			base.radio = R;
			base.AnglePosition = ANGLE_INITIAL_POSITION;
			base.InitialPosition = ANGLE_INITIAL_POSITION;
        }
    }
}
