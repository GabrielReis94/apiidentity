using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces
{
    public interface IMapperExample
    {
         Example MapperToEntity(ExampleDTO exampleDTO);
         List<ExampleDTO> MapperListExamples(List<Example> lstExamples);
         ExampleDTO MapperToDTO(Example example);
    }
}