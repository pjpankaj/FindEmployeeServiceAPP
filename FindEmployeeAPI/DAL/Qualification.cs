using System;
using System.Collections.Generic;

#nullable disable

namespace FindEmployeeAPI.DAL
{
    public partial class Qualification
    {
        public Qualification()
        {
            UserProfiles = new HashSet<UserProfile>();
        }

        public int Id { get; set; }
        public string Qualif { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
