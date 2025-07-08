using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DTO.DTOs.BannerDTOs
{
    public class UpdateBannerDTO:CreateBannerDTO
    {
        public int BannerID { get; set; }
    }
}
