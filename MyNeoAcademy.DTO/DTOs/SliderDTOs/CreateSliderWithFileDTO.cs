using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.SliderDTOs
{
    public class CreateSliderWithFileDTO : CreateSliderDTO
    {
        public IFormFile? ImageFile { get; set; }

    }
}
