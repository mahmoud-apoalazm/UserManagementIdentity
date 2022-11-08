﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserManagementIdentity.Models
{
    public class ApplicationUser :IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }=string.Empty;

        [MaxLength(100)]
        public string LastName { get; set; }= string.Empty;

        public byte[]?  ProfilePicture { get; set; }
    }
}
