using Microsoft.AspNetCore.Identity;
using System;

namespace TaskManager.Infrastucture.Identity
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsMember { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
