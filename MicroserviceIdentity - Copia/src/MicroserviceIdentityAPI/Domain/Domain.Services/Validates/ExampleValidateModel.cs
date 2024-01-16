using MicroserviceIdentityAPI.Domain.Domain.Core.Validates;
using MicroserviceIdentityAPI.Domain.Entities;

namespace MicroserviceIdentityAPI.Domain.Domain.Services.Validates
{
    public class ExampleValidateModel : IValidateModel<Example>
    {
        public Task<List<string>> ValidateModel(Example entity)
        {
            throw new NotImplementedException();
        }

    }
}