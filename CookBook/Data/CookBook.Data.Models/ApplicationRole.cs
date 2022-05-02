using System;
using CookBook.Data.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace CookBook.Data.Models
{
    public class ApplicationRole : IdentityRole<int>, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}