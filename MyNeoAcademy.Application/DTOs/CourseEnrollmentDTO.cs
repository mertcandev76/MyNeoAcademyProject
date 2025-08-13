using MyNeoAcademy.Application.DTOs.User;
using MyNeoAcademy.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Application.DTOs
{
    public class CourseEnrollmentReferenceDTO
    {
        public int Id { get; set; }  
        public int CourseID { get; set; }
        public int AppUserID { get; set; }
        public DateTime EnrolledDate { get; set; }
    }

    public class ResultCourseEnrollmentDTO
    {
        public int Id { get; set; }  

        public int CourseID { get; set; }
        public int AppUserID { get; set; }
        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;

        public ResultCourseDTO? Course { get; set; }
        public ResultAppUserDTO? AppUser { get; set; }
    }

    public class CreateCourseEnrollmentDTO
    {
        public int CourseID { get; set; }
        public int AppUserID { get; set; }
    }

}

