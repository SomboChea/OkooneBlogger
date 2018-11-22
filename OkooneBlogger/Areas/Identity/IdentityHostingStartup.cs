using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OkooneBlogger.Models;

[assembly: HostingStartup(typeof(OkooneBlogger.Areas.Identity.IdentityHostingStartup))]
namespace OkooneBlogger.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OkooneBloggerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OkooneBloggerContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<OkooneBloggerContext>();
            });
        }
    }
}