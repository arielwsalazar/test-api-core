using System;
using Newtonsoft.Json;

namespace WeatherSimulatorAPI.Classes
{
    public class WeatherResponse
    {
		[JsonProperty("Dia")]
		public int Id;

		[JsonProperty("Clima")]
		public string Description;
    }
}
