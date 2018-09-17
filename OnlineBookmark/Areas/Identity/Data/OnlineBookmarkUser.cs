using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OnlineBookmark.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the OnlineBookmarkUser class
    public class OnlineBookmarkUser : IdentityUser
    {
        [PersonalData]
        public string Uid { get; set; }
    }
}
