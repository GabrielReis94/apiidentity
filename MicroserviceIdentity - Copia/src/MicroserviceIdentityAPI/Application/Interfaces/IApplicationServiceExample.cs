using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Interfaces
{
    public interface IApplicationServiceExample
    {
         Task<bool> AddAsync(ExampleDTO obj);
         Task<ExampleDTO> GetByIdAsync(int id);
         Task<List<ExampleDTO>> GetAllAsync();
         Task<bool> UpdateAsync(ExampleDTO obj);           
    }
}