namespace MicroserviceIdentityAPI.Domain.Domain.Core.Validates
{
    public interface IValidateModel<T>
    {
         Task<List<string>> ValidateModel(T entity);
    }
}