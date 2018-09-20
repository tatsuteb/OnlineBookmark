using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookmark.Data.Models
{
    public class UserBookmark
    {
        [Key]
        [Required]
        public Int64 Seq { get; set; }

        [Required]
        public string Uid { get; set; }

        [Required]
        public string Bid { get; set; }

        [Required]
        public bool IsPrivate { get; set; }
    }
}
