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
        public class CategoryReferenceDTO
        {
            public int CategoryID { get; set; }
            public string? Name { get; set; }
        }
        public class CreateCategoryDTO
        {


            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? IconClass { get; set; }

        }

        public class ResultCategoryDTO : CreateCategoryDTO
        {
            public int CategoryID { get; set; }
           //public int BlogsCount { get; set; } = 0;
            public List<BlogReferenceDTO> Blogs { get; set; } = new List<BlogReferenceDTO>();
            public List<CourseReferenceDTO> Courses { get; set; } = new List<CourseReferenceDTO>();

    }

        public class UpdateCategoryDTO : CreateCategoryDTO,IHasId
        {
            public int CategoryID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => CategoryID;
            set => CategoryID = value;
        }
    }
    
}
