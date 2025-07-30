using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs
{
    public class AboutReferenceDTO
    {
        public int AboutID { get; set; }
        public string? Subtitle { get; set; }
    }
    public class CreateAboutDTO
    {
        public string? Subtitle { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
        public string? ImageFrontUrl { get; set; }
        public string? ImageBackUrl { get; set; }
    }
    public class CreateAboutWithFileDTO : CreateAboutDTO
    {

        public IFormFile? ImageFrontFile { get; set; }
        public IFormFile? ImageBackFile { get; set; }
    }
    public class ResultAboutDTO : CreateAboutDTO
    {
        public int AboutID { get; set; }
        public List<AboutFeatureReferenceDTO> Features { get; set; } = new List<AboutFeatureReferenceDTO>();
    }
    public class UpdateAboutDTO : CreateAboutDTO
    {
        public int AboutID { get; set; }

    }
    public class UpdateAboutWithFileDTO : CreateAboutWithFileDTO
    {
        public int AboutID { get; set; }
    }
}
