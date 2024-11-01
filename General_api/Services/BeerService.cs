using General_api.DTOs;
using General_api.Interface;
using General_api.Models;
using Microsoft.EntityFrameworkCore;

namespace General_api.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private readonly StoreContext _context;

        public BeerService(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BeerDto>> GetAll() =>
            await _context.Beers.Select(b => new BeerDto
                {
                    Id = b.BeerID,
                    BrandID = b.BrandID,
                    Name = b.Name,
                    Alcohol = b.Alcohol
                }).ToListAsync();

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    BrandID = beer.BrandID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            // Objeto que vamos almacenar en la BD
            var beer = new Beer
            {
                BrandID = beerInsertDto.BrandID,
                Name = beerInsertDto.Name,
                Alcohol = beerInsertDto.Alcohol
            };

            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();

            // Objeto que vamos a retornar en la respuesta
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                BrandID = beer.BrandID,
                Name = beer.Name,
                Alcohol = beer.Alcohol
            };

            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);

            if(beer != null)
            {
                beer.BrandID = beer.BrandID;
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;

                await _context.SaveChangesAsync();

                // Objeto que vamos a retornar en la respuesta
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    BrandID = beer.BrandID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }

            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if(beer != null)
            {
                // Objeto que vamos a retornar en la respuesta
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    BrandID = beer.BrandID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol
                };

                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();

                return beerDto;
            }

            return null;
        }
    }
}
