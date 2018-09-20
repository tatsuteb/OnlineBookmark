using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookmark.Data.Models
{
    public class Bookmark
    {
        [Key]
        [Required]
        public string Bid { get; set; }

        [Required]
        public string BaseBid { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
