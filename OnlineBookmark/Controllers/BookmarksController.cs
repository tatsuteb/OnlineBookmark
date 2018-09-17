using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookmark.Models.Bookmarks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookmark.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        [HttpPost]
        public IActionResult CreateBookmark(BookmarkCreationModel model, string returnUrl)
        {
            Console.WriteLine("Create Bookmark.");

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Top");

            return Redirect(returnUrl);
        }
    }
}
