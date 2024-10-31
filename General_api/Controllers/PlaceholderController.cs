using General_api.DTOs;
using General_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceholderController : ControllerBase
    {
        private readonly IPlaceholderService _placeholderService;

        public PlaceholderController(IPlaceholderService placeholderService)
        {
            _placeholderService = placeholderService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlaceholderDto>> Get() => 
            await _placeholderService.Get();
    }
}
