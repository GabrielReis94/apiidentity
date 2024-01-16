namespace MicroserviceIdentityAPI.Domain.Models
{
    public class ValidationProblemsResponse
    {
        public IDictionary<string, string[]> Errors {get; private set;}

        public ValidationProblemsResponse(IDictionary<string, string[]> _errors)
        {
            Errors = _errors;
        }
    }
}