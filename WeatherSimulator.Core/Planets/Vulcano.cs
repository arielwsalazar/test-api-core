using System;
namespace WeatherSimulator.Core.Planets
{
	public class Vulcano : Planet
    {
		readonly int ANGLE_SPEED = 5;
        readonly int R = 1000;
		readonly int ANGLE_INITIAL_POSITION = 90;
        
        public Vulcano()
        {
			base.PlanetSpeed = ANGLE_SPEED;
			base.radio = R;
			base.Direction = Enumerators.PlanetTranslation.Counterclockwise;
			base.AnglePosition = this.ANGLE_INITIAL_POSITION;
			base.InitialPosition = this.ANGLE_INITIAL_POSITION;
        }

        
    }
}
