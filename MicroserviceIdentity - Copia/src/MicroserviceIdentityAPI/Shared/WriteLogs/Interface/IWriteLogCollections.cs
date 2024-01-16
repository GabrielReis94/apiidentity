namespace MicroserviceIdentityAPI.Shared.WriteLogs.Interface
{
    public interface IWriteLogCollections
    {
         void WriteRequest(string json);
         void WriteResponse(string json);
         void WriteError(string json);
    }
}