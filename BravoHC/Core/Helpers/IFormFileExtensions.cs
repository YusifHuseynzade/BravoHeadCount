using Microsoft.AspNetCore.Http;

namespace Core.Helpers
{
    public static class IFormFileExtensions
    {
        public async static Task<string> SaveAsync(this IFormFile file, string path, string folder)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "The base path cannot be null or empty.");

            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException(nameof(folder), "The folder cannot be null or empty.");

            var filePath = Path.Combine(path, folder);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var resultPath = Path.Combine(filePath, fileName);

            using (var fileStream = new FileStream(resultPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return $"{folder}/{fileName}".Replace("\\", "/");
        }

        public static void Delete(string path, string folder, string fileName)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path), "The base path cannot be null or empty.");

            if (string.IsNullOrEmpty(folder))
                throw new ArgumentNullException(nameof(folder), "The folder cannot be null or empty.");

            var filePath = Path.Combine(path, folder, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
