using General_api.DTOs;

namespace General_api.Interface
{
    public interface ICommonService<T, TI, TU>
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TI insertDto);
        Task<T> Update(int id, TU updateDto);
        Task<T> Delete(int id);
    }
}
