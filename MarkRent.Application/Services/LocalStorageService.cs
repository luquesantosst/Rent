using MarkRent.Domain.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkRent.Application.Services
{
    public class LocalStorageService : IStorageService
    {
        private readonly string _storagePath;

        public LocalStorageService(IConfiguration configuration)
        {
            _storagePath = configuration["Storage:LocalPath"] ?? "wwwroot/uploads";

            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }

        public async Task<string> UploadAsync(byte[] fileData, string fileName, string contentType)
        {
            if (fileData == null || fileData.Length == 0)
            {
                throw new ArgumentException("Nenhum arquivo foi enviado.");
            }

            string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            string filePath = Path.Combine(_storagePath, newFileName);

            await File.WriteAllBytesAsync(filePath, fileData);

            return $"/uploads/{newFileName}";
        }
    }
}
