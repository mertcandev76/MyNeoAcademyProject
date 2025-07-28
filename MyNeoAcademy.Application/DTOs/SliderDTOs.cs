using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Common;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
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
    public class UpdateSliderDTO : CreateSliderDTO,IHasId
    {
        public int SliderID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => SliderID;
            set => SliderID = value;
        }
    }
    public class UpdateSliderWithFileDTO : CreateSliderWithFileDTO, IHasId
    {
        public int SliderID { get; set; }
        public int Id
        {
            get => SliderID;
            set => SliderID = value;
        }
    }
}
