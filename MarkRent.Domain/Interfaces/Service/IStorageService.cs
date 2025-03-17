namespace MarkRent.Domain.Interfaces.Service
{
    public interface IStorageService
    {
        Task<string> UploadAsync(byte[] fileData, string fileName, string contentType);
    }
}
