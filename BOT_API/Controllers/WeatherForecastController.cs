using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOT_API.Controllers {
    [ApiController]
    [Route("API")]
    public class WeatherForecastControllerrrrrr : ControllerBase {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastControllerrrrrr> _logger;

        public WeatherForecastControllerrrrrr(ILogger<WeatherForecastControllerrrrrr> logger) {
            _logger = logger;
        }

        [HttpGet("test")]
        public IEnumerable<TestRes> Get_test() {
            TestRes r = new TestRes();
            return new TestRes[1]{r};
        }
    }
}
