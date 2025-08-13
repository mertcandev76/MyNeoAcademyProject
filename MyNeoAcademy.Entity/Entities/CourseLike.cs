using MyNeoAcademy.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class CourseLike:IHasId
    {
        public int Id { get; set; } 
        public int CourseID { get; set; }
        public Course Course { get; set; } = null!;

        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; } = null!;

        public DateTime LikedDate { get; set; } = DateTime.UtcNow;


    }
}
