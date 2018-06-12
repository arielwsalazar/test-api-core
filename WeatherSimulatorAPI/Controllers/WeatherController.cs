using System;
using Microsoft.AspNetCore.Mvc;
using WeatherSimulator.Core;
using WeatherSimulatorAPI.Classes;

namespace WeatherSimulatorAPI.Controllers
{
	[Route("api/[controller]")]
	public class WeatherController : Controller
    {
        public WeatherController()
        {
        }

		[HttpGet("{id}")]
		public JsonResult Get(int id)
        {
			WeatherResponse weatherResponse = new WeatherResponse();
			weatherResponse.Id = id;

			WeatherSimulatorCore w = new WeatherSimulatorCore();
            
			switch (w.Run(id))
            {
				case WeatherSimulator.Core.Enumerators.Weathercondition.Drought:
					weatherResponse.Description = "Sequía";
                    break;
				case WeatherSimulator.Core.Enumerators.Weathercondition.Rain:
					weatherResponse.Description = "LLuvia";
                    break;
				case WeatherSimulator.Core.Enumerators.Weathercondition.MaxRain:
					weatherResponse.Description = "Pico Máximo lluvia";
                    break;
				case WeatherSimulator.Core.Enumerators.Weathercondition.OptimalConditions:
					weatherResponse.Description = "Condiciones Óptimas";
                    break;
				default:
					weatherResponse.Description = "Normal";
					break;
            }

			return Json(weatherResponse);
        }
    }
}
