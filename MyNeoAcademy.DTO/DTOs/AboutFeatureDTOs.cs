using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs
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
    public class UpdateAboutFeatureDTO : CreateAboutFeatureDTO
    {
        public int AboutFeatureID { get; set; }
    }
}
