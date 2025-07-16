using MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutDTOs
{
    public class ResultAboutDTO:CreateAboutDTO
    {
        public int AboutID { get; set; }


        // About'a ait feature'ları taşıyacak
        public List<ResultAboutFeatureDTO>? Features { get; set; }
    }
}
