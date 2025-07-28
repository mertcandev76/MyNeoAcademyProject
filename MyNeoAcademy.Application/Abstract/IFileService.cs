using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.Abstract
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string webRootPath, string folderPath);
    }
}
