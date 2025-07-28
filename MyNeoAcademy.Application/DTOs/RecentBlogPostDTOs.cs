using Microsoft.AspNetCore.Http;
using MyNeoAcademy.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CreateRecentBlogPostDTO
    {
        public string CompactTitle { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
    }

    public class CreateRecentBlogPostWithFileDTO : CreateRecentBlogPostDTO
    {
        public IFormFile? ImageFile { get; set; }
    }

    public class ResultRecentBlogPostDTO : CreateRecentBlogPostDTO
    {
        public int RecentBlogPostID { get; set; }
        public DateTime PublishDate { get; set; }
    }

    public class UpdateRecentBlogPostDTO : CreateRecentBlogPostDTO, IHasId
    {
        public int RecentBlogPostID { get; set; }

        [JsonIgnore]
        public int Id
        {
            get => RecentBlogPostID;
            set => RecentBlogPostID = value;
        }
    }

    public class UpdateRecentBlogPostWithFileDTO : CreateRecentBlogPostWithFileDTO, IHasId
    {
        public int RecentBlogPostID { get; set; }

        public int Id
        {
            get => RecentBlogPostID;
            set => RecentBlogPostID = value;
        }
    }
}
