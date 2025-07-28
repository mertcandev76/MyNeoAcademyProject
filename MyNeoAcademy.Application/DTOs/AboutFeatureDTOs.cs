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

    public class AboutFeatureReferenceDTO
    {
        public int AboutFeatureID { get; set; }
        public string? Text { get; set; }
        public string? IconClass { get; set; }
    }
    public class CreateAboutFeatureDTO
    {
        public string? IconClass { get; set; }
        public string? Text { get; set; }

        public int AboutID { get; set; }
    }
    public class ResultAboutFeatureDTO : CreateAboutFeatureDTO
    {
        public int AboutFeatureID { get; set; }

        public AboutReferenceDTO? About { get; set; }
    }
    public class UpdateAboutFeatureDTO : CreateAboutFeatureDTO,IHasId
    {
        public int AboutFeatureID { get; set; }

        [JsonIgnore]
        public int Id
        {
            get => AboutFeatureID;
            set => AboutFeatureID = value;
        }
    }
}
