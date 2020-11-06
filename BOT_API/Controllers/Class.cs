using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BOT_API.Controllers {
    [ApiController]
    [Route("API/BOT")]
    public class BOT : ControllerBase {

        private readonly ILogger<WeatherForecastControllerrrrrr> _logger;
        
        public BOT(ILogger<WeatherForecastControllerrrrrr> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TestRes> GetVersion() {
            TestRes r = new TestRes();
            return new TestRes[1] { r };
        }
    }
}
