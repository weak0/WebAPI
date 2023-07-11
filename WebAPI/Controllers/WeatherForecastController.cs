using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Reflection;
using WebAPI;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatrherForcastSerivces _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatrherForcastSerivces service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("generate")]
        public ActionResult<WeatrherForcastSerivces> Get(WeatherParams param)
        {
            if (param.min > param.max)
            {
                var ErrorResult = new WeatherForecast
                { 
                    Summary = "Erorr 404",
                };
                return StatusCode(400, ErrorResult);
            }

            var result = _service.Get(param.results, param.min, param.max );
            return  StatusCode(200, result);
        }
    }
}