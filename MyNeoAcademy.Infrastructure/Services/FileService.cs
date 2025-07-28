using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Infrastructure.Services
{
    public class FileService:IFileService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string rootPath, string folder)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var savePath = Path.Combine(rootPath, folder, fileName);

            Directory.CreateDirectory(Path.Combine(rootPath, folder));

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folder, fileName).Replace("\\", "/");
        }

    }
}
