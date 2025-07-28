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
        public class UpdateAboutDTO : CreateAboutDTO, IHasId
        {
            public int AboutID { get; set; }
            [JsonIgnore]
            public int Id
            {
                get => AboutID;
                set => AboutID = value;
            }

        }
        public class UpdateAboutWithFileDTO : CreateAboutWithFileDTO, IHasId
        {
            public int AboutID { get; set; }
            public int Id
            {
                get => AboutID;
                set => AboutID = value;
            }
        }
    }
