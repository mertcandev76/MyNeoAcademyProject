using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CourseLikeReferenceDTO
    {
        public int Id { get; set; }           
        public int CourseID { get; set; }
        public int AppUserID { get; set; }
        public DateTime LikedDate { get; set; }
    }

    public class ResultCourseLikeDTO
    {
        public int Id { get; set; }           
        public int CourseID { get; set; }
        public int AppUserID { get; set; }
        public DateTime LikedDate { get; set; }

        public ResultCourseDTO? Course { get; set; }
        public ResultAppUserDTO? AppUser { get; set; }
    }

    public class CreateCourseLikeDTO
    {
        public int CourseID { get; set; }
        public int AppUserID { get; set; }
    }

}

