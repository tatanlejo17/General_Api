using General_api.DTOs;
using General_api.Interface;
using General_api.Models;
using General_api.Repository;
using Microsoft.EntityFrameworkCore;

namespace General_api.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        private readonly IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beers = await _beerRepository.Get();

            return beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                BrandID = b.BrandID,
                Name = b.Name,
                Alcohol = b.Alcohol
            });
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

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

            await _beerRepository.Add(beer);
            await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

            if(beer != null)
            {
                beer.BrandID = beer.BrandID;
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

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
            var beer = await _beerRepository.GetById(id);

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

                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }

            return null;
        }
    }
}
