using WeatherSimulator.Core.Common;
using WeatherSimulator.Core.Interfaces;
using System;

namespace WeatherSimulator.Core.Planets
{
	public class Planet: IRotation
    {
		public PlanetCoordinates GalaxyPosition;
              
        /// <summary>
        /// current position on grades
        /// </summary>
		public int AnglePosition;

		public int InitialPosition;

		public Enumerators.PlanetTranslation Direction;

		public int radio;

		/// <summary>
        /// grades by day
        /// </summary>
        public int PlanetSpeed;

        /// <summary>
        /// The quantity of days by year. first approach 360 days is a year.
        /// </summary>
		//readonly int QuantityDaysByYear = 360;

		public Planet()
        {
			this.GalaxyPosition = new PlanetCoordinates(0.0,0.0);
			this.PlanetSpeed = 0;
			this.Direction = Enumerators.PlanetTranslation.Clockwise;
			this.radio = 1;
        }
        
		public void Rotate(int days = 0)
		{
			switch (this.Direction)
            {
                case Enumerators.PlanetTranslation.Clockwise:
					this.AnglePosition = this.InitialPosition - (this.PlanetSpeed * days);
                    break;
                case Enumerators.PlanetTranslation.Counterclockwise:
					this.AnglePosition = this.InitialPosition + (this.PlanetSpeed * days);
                    break;
            }
			CalculatePosition();
		}

        /// <summary>
        /// it calculates the position on the plane. it sets galaxy position object
        /// </summary>
		public void CalculatePosition()
		{
			// x = r.cos(a) , 90>a>0,  cos(0) = 1 , x>0
			// y = r.sen(a) , 90>a>0,  sen(0) = 0 , y>0         

			// 180>a>90  => a = 180 - a  , x<0 , y>0
			// 270>a>180 => a = a - 180  , x<0 , y<0
			// 360>a>270 => a = a - 270  , x>0 , y<0

			Double angle = Math.PI * this.AnglePosition / 180.0;         
			this.GalaxyPosition.X = Math.Round((double)this.radio * Math.Cos(angle),2);
			this.GalaxyPosition.Y = Math.Round((double)this.radio * Math.Sin(angle),2);         
		}
	}
}
