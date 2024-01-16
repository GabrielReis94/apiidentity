using MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Interfaces;
using MicroserviceIdentityAPI.Domain.DTO;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.CrossCutting.Adapter.Maps.Map
{
    public class MapperExample : IMapperExample
    {
        private List<ExampleDTO> lstExampleDto = new();

        public List<ExampleDTO> MapperListExamples(List<Example> lstExamples)
        {
            if(lstExamples != null)
            {
                foreach (var item in lstExamples)
                {
                    lstExampleDto.Add(new ExampleDTO()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        RegisterDate = item.RegisterDate.ToString("dd-MM-yyyy HH:mm:ss")
                    });
                }
            }

            return lstExampleDto;
        }

        public ExampleDTO MapperToDTO(Example example)
        {
            var exampleDto = new ExampleDTO
            {
                Id = example.Id,
                Name = example.Name,
                RegisterDate = example.RegisterDate.ToString("dd/MM/yyyy HH:mm:ss")
            };

            return exampleDto;
        }

        public Example MapperToEntity(ExampleDTO exampleDTO)
        {
            var example = new Example
            {
                Id = exampleDTO.Id,
                Name = exampleDTO.Name,
                RegisterDate = Convert.ToDateTime(exampleDTO.RegisterDate)
            };

            return example;
        }
    }
}