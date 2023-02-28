using System;
using System.Collections.Generic;

#nullable disable

namespace FindEmployeeAPI.DAL
{
    public partial class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int? Experience { get; set; }
        public string CurrentCompany { get; set; }
        public int? QualificationId { get; set; }
        public string PreferredLocation { get; set; }
        public string ImageName { get; set; }

        public virtual Qualification Qualification { get; set; }
    }
}
