using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookmark.Models.Bookmarks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBookmark.Controllers
{
    [Authorize]
    public class BookmarksController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public BookmarksController(IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBookmark(BookmarkCreationModel model, string returnUrl)
        {
            // 画像が送られてきたら保存
            if (model.ImageFile != null)
            {
                // TODO: この辺の処理を別クラスへ切り出す

                var tempDirPath = Path.Combine(new string[]
                {
                    this._hostingEnvironment.WebRootPath,
                    "bookmarks",
                    "images",
                    Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                        .Replace("/", "-")
                        .Replace("+", "_")
                        .Replace("=", "")
                });

                if (!Directory.Exists(tempDirPath))
                {
                    try
                    {
                        Directory.CreateDirectory(tempDirPath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return BadRequest("Fail to save the bookmark.");
                    }
                }

                var tempFilePath = Path.Combine(new string[] {
                    tempDirPath,
                    $"{DateTime.Now.ToString("yyyyMMddHHmmss")}{Path.GetExtension(model.ImageFile.FileName)}"
                });

                try
                {
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest("Fail to save the bookmark.");
                }

            }


            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Top");

            return Redirect(returnUrl);
        }
    }
}
