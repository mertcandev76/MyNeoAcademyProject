using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Entity.Entities
{
    public class AboutFeature
    {
        public int AboutFeatureID { get; set; }
        public string? IconClass { get; set; }
        public string? Text { get; set; }

        public int AboutID { get; set; }
        public About? About { get; set; }
    }
}
