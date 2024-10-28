using General_api.Models;
using Microsoft.AspNetCore.Mvc;
using General_api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace General_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public BeerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        // Methods
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _storeContext.Beers.Select(b => new BeerDto {
                    Id = b.BeerID,
                    BrandID = b.BrandID,
                    Name = b.Name,
                    Alcohol = b.Alcohol
                }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if(beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                BrandID = beer.BrandID,
                Name = beer.Name,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            // Objeto que vamos almacenar en la BD
            var beer = new Beer
            {
                BrandID = beerInsertDto.BrandID,
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            // Objeto que vamos a retornar en la respuesta
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                BrandID = beer.BrandID,
                Name = beer.Name,
                Alcohol = beer.Alcohol
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);

            if(beer == null)
            {
                return NotFound();
            }

            beer.BrandID = beer.BrandID;
            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;

            await _storeContext.SaveChangesAsync();

            // Objeto que vamos a retornar en la respuesta
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                BrandID = beer.BrandID,
                Name = beer.Name,
                Alcohol = beer.Alcohol
            };

            return Ok(beerDto);
        }
    }
}
