using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookmark.Data.Models
{
    public class UserProfile
    {
        [Key]
        [Required]
        public string Uid { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(20)]
        public string DisplayName { get; set; }

        [MaxLength(100)]
        public string IconPath { get; set; }

        public string Description { get; set; }
    }
}
