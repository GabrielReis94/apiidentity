namespace MicroserviceIdentityAPI.Domain.Models
{
    public class ValidateInputViewModel
    {
        public IEnumerable<ErrorModel> Errors { get; private set; }

        public ValidateInputViewModel(IEnumerable<ErrorModel> _errors)
        {
            Errors = _errors;
        }
    }
}