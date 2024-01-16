using MicroserviceIdentityAPI.Application.Interfaces;
using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.Domain.Core.Interfaces.Services;
using MicroserviceIdentityAPI.Domain.DTO;

namespace MicroserviceIdentityAPI.Application.Services
{
    public class ApplicationServiceExample : IApplicationServiceExample
    {
        private readonly IServiceExample _serviceExample;
        private readonly IMapperExample _mapperExample;

        public ApplicationServiceExample(IServiceExample serviceExample, IMapperExample mapperExample)
        {
            _serviceExample = serviceExample;
            _mapperExample = mapperExample;
        }

        public async Task<bool> AddAsync(ExampleDTO obj)
        {
            try
            {
                var objExample = _mapperExample.MapperToEntity(obj);
                return await _serviceExample.AddAsync(objExample);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        // public async Task<bool> DeleteAsync(int id)
        // {
        //     try
        //     {
        //         return await _serviceExample.DeleteAsync(id);
        //     }
        //     catch (Exception ex)
        //     {                
        //         throw new Exception(ex.Message);
        //     }
        // }

        public async Task<List<ExampleDTO>> GetAllAsync()
        {
            try
            {
                var lstExamples = await _serviceExample.GetAllAsync();

                return _mapperExample.MapperListExamples(lstExamples);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<ExampleDTO> GetByIdAsync(int id)
        {
            try
            {
                var example = await _serviceExample.GetByIdAsync(id);

                return _mapperExample.MapperToDTO(example);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(ExampleDTO obj)
        {
            try
            {
                var example = _mapperExample.MapperToEntity(obj);

                return await _serviceExample.UpdateAsync(example);
            }
            catch (Exception ex)
            {                
                throw new Exception(ex.Message);
            }
        }
    }
}