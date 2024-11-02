using General_api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace General_api.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private readonly StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get() =>
            await _context.Beers.ToListAsync();

        public async Task<Beer> GetById(int id)
            => await _context.Beers.FindAsync(id);

        public async Task Add(Beer beer)
            => await _context.Beers.AddAsync(beer);

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer);
            _context.Beers.Entry(beer).State = EntityState.Modified;
        }

        public void Delete(Beer beer)
            => _context.Beers.Remove(beer);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
