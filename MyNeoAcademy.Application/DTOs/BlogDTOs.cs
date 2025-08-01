﻿using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class BlogReferenceDTO
    {
        public int BlogID { get; set; }
        public string? Title { get; set; }
    }
    public class CreateBlogDTO
    {
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }

        public int? AuthorID { get; set; }

        public int? CategoryID { get; set; }


    }

    public class CreateBlogWithFileDTO : CreateBlogDTO
    {
        public IFormFile? ImageFile { get; set; }
    }
    public class ResultBlogDTO : CreateBlogDTO
    {
        public int BlogID { get; set; }
        public DateTime PublishDate { get; set; }

        public AuthorReferenceDTO? Author { get; set; }
        public CategoryReferenceDTO? Category { get; set; }

        public List<CommentReferenceDTO> Comments { get; set; } = new List<CommentReferenceDTO>();
        public List<TagReferenceDTO> Tags { get; set; } = new List<TagReferenceDTO>();

    }
    public class UpdateBlogDTO : CreateBlogDTO, IHasId
    {
        public int BlogID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => BlogID;
            set => BlogID = value;
        }

    }
    public class UpdateBlogWithFileDTO : CreateBlogWithFileDTO, IHasId
    {
        public int BlogID { get; set; }
        public int Id
        {
            get => BlogID;
            set => BlogID = value;
        }
    }
}

