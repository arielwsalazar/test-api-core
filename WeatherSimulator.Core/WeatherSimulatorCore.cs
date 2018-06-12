using System;
using System.Collections.Generic;
using WeatherSimulator.Core.Common;
using WeatherSimulator.Core.Planets;

namespace WeatherSimulator.Core
{
	/// <summary>
    /// Weather simulator core.
	/// I assume that year has 360 days.
    /// </summary>
    public class WeatherSimulatorCore
    {
		Ferengi ferengi;
		Vulcano vulcano;
		Betasoide betasoide;
		Sun sun;
		const double ERROR_DECIMAL = 0.1;
        
		public WeatherSimulatorCore(){
			ferengi = new Ferengi();
			vulcano = new Vulcano();
			betasoide = new Betasoide();
			sun = new Sun();
		}

		/// <summary>
		/// Run the specified day.
		/// </summary>
		/// <returns>the day and weather condition</returns>
		/// <param name="day">Day</param>
		public Enumerators.Weathercondition Run(int day)
		{
			this.ferengi.Rotate(day);
			this.vulcano.Rotate(day);
			this.betasoide.Rotate(day);

			if (isDrought())
			{
				return Enumerators.Weathercondition.Drought;
			}

			if (isOptimalCondition())
			{
				return Enumerators.Weathercondition.OptimalConditions;
			}

            if (IsRain())
			{
				if(isMaxRain())
				{
					return Enumerators.Weathercondition.MaxRain;
				}
				return Enumerators.Weathercondition.Rain;
			}
            
			return Enumerators.Weathercondition.Normal;
		}


        /// <summary>
        /// in circle the max perimeter for a triangle inside is an isoceles
        /// </summary>
        /// <returns><c>true</c>, if max rain was ised, <c>false</c> otherwise.</returns>
		private bool isMaxRain()
		{
			//to calculate the isoceles, select the farest planet from that place mesuare the distance of two segments are the same.
			PlanetCoordinates A = betasoide.GalaxyPosition;
			PlanetCoordinates B = vulcano.GalaxyPosition;
			PlanetCoordinates C = ferengi.GalaxyPosition;
			double xs1 = Math.Round(B.X - A.X, 2);
			double ys1 = Math.Round(B.Y - A.Y, 2);
			double distanceAB = Math.Round( Math.Sqrt(Math.Pow(xs1,2) + Math.Pow(ys1,2)), 2);

			double xs2 = Math.Round(C.X - A.X, 2);
			double ys2 = Math.Round(C.Y - A.Y, 2);
			double distanceAC = Math.Round( Math.Sqrt(Math.Pow(xs2,2)+ Math.Pow(ys2,2)), 2);

			double xs3 = Math.Round(B.X - C.X, 2);
			double ys3 = Math.Round(B.Y - C.Y, 2);
			double distanceBC = Math.Round( Math.Sqrt(Math.Pow(xs3,2)+Math.Pow(ys3,2)), 2);
			double error = ERROR_DECIMAL;

			error = GetMinimalError(distanceAB,distanceAC);
			if (Math.Abs(distanceAB - distanceAC) <= error)
			{
				return true;
			}

			error = GetMinimalError(distanceAB,distanceBC);
			if (Math.Abs(distanceAB - distanceBC) <= error)
            {
                return true;
            }

			error = GetMinimalError(distanceAC,distanceBC);
			if (Math.Abs(distanceAC - distanceBC) <= error)
            {
                return true;
            }

			return false;
		}

		private double GetMinimalError(double numberA, double numberB)
		{
			double value;

			if (numberA < numberB)
            {
				value = numberA * ERROR_DECIMAL;
            }
            else
            {
				value = numberB * ERROR_DECIMAL;
            }

			return value;
		}

		private bool IsRain()
		{
			return IsSunInsideTriangle();
		}

		double TriangleArea(PlanetCoordinates coord1, PlanetCoordinates coord2, PlanetCoordinates coord3)
        {
			//triangle area cord1, cord2, cord3, area > 0 
			return (coord1.X - coord3.X) * (coord2.Y - coord3.Y) - (coord1.Y - coord3.Y) * (coord2.X - coord3.X);

        }//

		bool IsSunInsideTriangle()
        {
			PlanetCoordinates coord1 = betasoide.GalaxyPosition;
			PlanetCoordinates coord2 = vulcano.GalaxyPosition;
			PlanetCoordinates coord3 = ferengi.GalaxyPosition;
			PlanetCoordinates P = sun.GalaxyPosition;

			if (TriangleArea(coord1, coord2, coord3) >= 0)
			{
				return TriangleArea(coord1, coord2, P) >= 0 && TriangleArea(coord2, coord3, P) >= 0 && TriangleArea(coord3, coord1, P) >= 0;
			}
			else
			{
				return TriangleArea(coord1, coord2, P) <= 0 && TriangleArea(coord2, coord3, P) <= 0 && TriangleArea(coord3, coord1, P) <= 0;
			}

        }//

		/// <summary>
		/// Ises the drought. With two points I generate the matematical line function then check if
		/// other planets and sun are alined.
		/// </summary>
		/// <returns><c>true</c>, if drought was ised, <c>false</c> otherwise.</returns>
		private bool isDrought()
		{
			return MathLineFunction(Enumerators.ProcessType.IncludeOrigin);
		}

		private bool isOptimalCondition()
		{
			return MathLineFunction(Enumerators.ProcessType.ExcludeOrigin);
		}


		private bool MathLineFunction(Enumerators.ProcessType processType)
		{
			//calculates the slope with coordinates of two planets.
            //this time vulcano, betasoide
            //line function =>  y = m.x + b
            double denominator = (vulcano.GalaxyPosition.X - betasoide.GalaxyPosition.X);

            if (denominator.Equals(0.0))
            {            
				return pointValidation(processType);
            }

            //Slope
			double m = Math.Round( (vulcano.GalaxyPosition.Y - betasoide.GalaxyPosition.Y) / denominator , 2);

            //choosing a planet calculate the b point.
			double b = Math.Round( vulcano.GalaxyPosition.Y - (m * vulcano.GalaxyPosition.X) ,2 );

            //check if ferengi is aligned.
            double yResult = Math.Round(ferengi.GalaxyPosition.Y, 2);
            double functionResult = Math.Round((m * ferengi.GalaxyPosition.X) + b);
			double difference = Math.Round(Math.Abs(yResult * ERROR_DECIMAL));

            //check if sun is aligned 
            if (Math.Abs(yResult - functionResult) <= difference)
            {
				double differenceB = Math.Abs(b * ERROR_DECIMAL);
                if (Math.Abs(0.0 - b) <= differenceB)
                {
					if (processType == Enumerators.ProcessType.IncludeOrigin)
					{
						return true;
					}               
				}else{
					if (processType == Enumerators.ProcessType.ExcludeOrigin)
					{
						return true;
					}
				}
            }

            return false;
		}

        /// <summary>
        /// it checks if x value is 0 in each point.
        /// </summary>
        /// <returns><c>true</c> sun is aligned <c>false</c> otherwise.</returns>
        /// <param name="processType">if calculation includes the sun or not.</param>
		private bool pointValidation(Enumerators.ProcessType processType)
		{
			double denominatorSegmentTwo = betasoide.GalaxyPosition.X - ferengi.GalaxyPosition.X;
            if (denominatorSegmentTwo.Equals(0.0))
            {
                if (ferengi.GalaxyPosition.X.Equals(0.0))
                {
                    if (Enumerators.ProcessType.IncludeOrigin == processType)
                    {
                        return true;
                    }
                }
            }

			return false;
		}
	}
}
