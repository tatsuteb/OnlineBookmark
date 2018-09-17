using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineBookmark.Areas.Identity.Data;
using OnlineBookmark.Data;
using OnlineBookmark.Data.Interfaces;
using OnlineBookmark.Models;

[assembly: HostingStartup(typeof(OnlineBookmark.Areas.Identity.IdentityHostingStartup))]
namespace OnlineBookmark.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OnlineBookmarkIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OnlineBookmarkIdentityContextConnection")));

                services.AddDefaultIdentity<OnlineBookmarkUser>()
                    .AddEntityFrameworkStores<OnlineBookmarkIdentityContext>();
            });
        }
    }
}