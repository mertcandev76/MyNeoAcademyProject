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
    public class CreateNewsletterDTO
    {

        public string Email { get; set; } = null!;
    }
    public class ResultNewsletterDTO : CreateNewsletterDTO
    {
        public int NewsletterID { get; set; }
        public DateTime SubscribedDate { get; set; }
    }
    public class UpdateNewsletterDTO : CreateNewsletterDTO, IHasId
    {
        public int NewsletterID { get; set; }
        [JsonIgnore]
        public int Id
        {
            get => NewsletterID;
            set => NewsletterID = value;
        }
    }
}
