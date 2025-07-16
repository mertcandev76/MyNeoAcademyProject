using MyNeoAcademy.DTO.DTOs.AboutDTOs;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.AboutFeatureDTOs
{
    public class ResultAboutFeatureDTO:CreateAboutFeatureDTO
    {
        public int AboutFeatureID { get; set; }

        // Navigation property kaldırıldı — döngü engellendi
    }
}
