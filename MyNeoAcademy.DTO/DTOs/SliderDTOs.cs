using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs
{
    public class CreateSliderDTO
    {

        public string? SubTitle { get; set; }
        public string? Title { get; set; }
        public string? ButtonUrl { get; set; }
        public string? ButtonText { get; set; }
        public string? ImageUrl { get; set; }
    }
    public class CreateSliderWithFileDTO : CreateSliderDTO
    {
        public IFormFile? ImageFile { get; set; }

    }
    public class ResultSliderDTO : CreateSliderDTO
    {
        public int SliderID { get; set; }
    }
    public class UpdateSliderDTO : CreateSliderDTO
    {
        public int SliderID { get; set; }
    }
    public class UpdateSliderWithFileDTO : CreateSliderWithFileDTO
    {
        public int SliderID { get; set; }
    }
}
