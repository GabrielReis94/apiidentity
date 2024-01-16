using System.Text.Json;
using MicroserviceIdentityAPI.Shared.Models;

namespace MicroserviceIdentityAPI.Shared.Utils
{
    public static class Assistant
    {
        public static string FormatRequest(string method, object request, string client, string searchKey = "")
        {
            try
            {
                var log = new LogRequest
                {
                    Id = Guid.NewGuid(),
                    Method = method,
                    Client = client,
                    LogDate = DateTime.Now,
                    SearchKey = searchKey,
                    Request = request
                };

                return JsonSerializer.Serialize(log);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static string FormartResponse(string method, object response, string searchKey = "")
        {
            try
            {
                var log = new LogResponse
                {
                    Id = Guid.NewGuid(),
                    LogDate = DateTime.Now,
                    Method = method,
                    SearchKey = searchKey,
                    Response = response
                };

                return JsonSerializer.Serialize(log);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public static string FormatError(string method, string errorMessage, string stackTrace, string innerException, string searchKey = "")
        {
            try
            {
                var log = new LogError
                {
                    Id = Guid.NewGuid(),
                    LogDate = DateTime.Now,
                    Method = method,
                    ErrorMessage = errorMessage,
                    InnerException = innerException,
                    StackTrace = stackTrace,
                    SearchKey = searchKey
                };

                return JsonSerializer.Serialize(log);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}