using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookmark.Data.Models
{
    public class BookmarkBase
    {
        [Key]
        [Required]
        public string BaseBid { get; set; }

        [Required]
        public string OwnerUid { get; set; }

        public string LinkedUrl { get; set; }

        public string ImageFilePath { get; set; }
    }
}
