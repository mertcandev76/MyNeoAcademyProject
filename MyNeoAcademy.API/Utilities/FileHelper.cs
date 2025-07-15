namespace MyNeoAcademy.API.Utilities
{
    public static class FileHelper
    {
        public static async Task<string> SaveFileAsync(IFormFile file, string webRootPath, string folderPath)
        {
            var directory = Path.Combine(webRootPath, folderPath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var fullPath = Path.Combine(directory, uniqueFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderPath, uniqueFileName).Replace("\\", "/"); // örnek: img/sliders/abc.jpg
        }
    }
}
