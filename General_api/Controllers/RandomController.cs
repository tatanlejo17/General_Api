using General_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _serviceSingleton;
        private IRandomService _serviceScoped;
        private IRandomService _serviceTransient;

        private IRandomService _service2Singleton;
        private IRandomService _service2Scoped;
        private IRandomService _service2Transient;

        public RandomController (
            [FromKeyedServices("serviceSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("serviceScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("serviceTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("serviceSingleton")] IRandomService randomService2Singleton,
            [FromKeyedServices("serviceScoped")] IRandomService randomService2Scoped,
            [FromKeyedServices("serviceTransient")] IRandomService randomService2Transient
            )
        {
            _serviceSingleton = randomServiceSingleton;
            _serviceScoped = randomServiceScoped;
            _serviceTransient = randomServiceTransient;
            _service2Singleton = randomService2Singleton;
            _service2Scoped = randomService2Scoped;
            _service2Transient = randomService2Transient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton-1", _serviceSingleton.Value);
            result.Add("Singleton-2", _service2Singleton.Value);

            result.Add("Scoped-1", _serviceScoped.Value);
            result.Add("Scoped-2", _service2Scoped.Value);

            result.Add("Transient-1", _serviceTransient.Value);
            result.Add("Transient-2", _service2Transient.Value);

            return result;
        }
    }
}
