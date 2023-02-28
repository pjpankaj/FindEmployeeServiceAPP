using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;

namespace FindUserProfile.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int? Experience { get; set; }
        public string CurrentCompany { get; set; }
        public string Qualification { get; set; }
        public int QualificationId { get; set; }
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }
        public string PreferredLocation { get; set; }      
    }
}
