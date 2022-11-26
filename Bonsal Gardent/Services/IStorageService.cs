using System.Net.Http.Headers;


namespace Bonsal_Gardent.Services
{


    public class FileStorageService
    {
        private readonly string _userContentFolder;

        public FileStorageService()
        {
            string  webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _userContentFolder = webRootPath + "/image/Pic";
        }

        public string GetFileUrl(string fileName)
        {
            return fileName;
        }

        public string SaveFileAsync(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName?.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
             file.OpenReadStream().CopyTo(output);
            return fileName;
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }
    }
}