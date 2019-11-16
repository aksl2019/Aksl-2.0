using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Contoso.Infrastructure.Data.Context;

namespace Contoso.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContosoContext>(dbContextOptionsBuilder =>
            {
                //string sqliteConnectionString = Configuration.GetConnectionString("ContosoSqlite");
                //dbContextOptionsBuilder.UseSqlite(sqliteConnectionString);

                ////将EF迁移文件生成到当前应用程序的程序集下
                //dbContextOptionsBuilder.UseSqlite(sqliteConnectionString, b => b.MigrationsAssembly("Contoso.Api"));

                string sqlServerConnectionString = Configuration.GetConnectionString("ContosoSqlServer");
                dbContextOptionsBuilder.UseSqlServer(sqlServerConnectionString);

                // 将EF迁移文件生成到当前应用程序的程序集下
                dbContextOptionsBuilder.UseSqlServer(sqlServerConnectionString, b => b.MigrationsAssembly("Contoso.Api"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
