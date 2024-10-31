using General_api.DTOs;

namespace General_api.Services
{
    public interface IPlaceholderService
    {
        public Task<IEnumerable<PlaceholderDto>> Get();
    }
}
