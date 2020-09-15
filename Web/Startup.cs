using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.Data;
using Web.Services;

namespace Web
{
    public class Startup
    {
        //1.
        public IConfiguration Configuration { get; }

        //2. Add constructor to recieve configuration
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            services.AddControllers();
            //3. Register our DbContext with app
            services.AddDbContext<HotelDbContext>(options =>
            {
                
                // Equivalent to DATABASE_URL
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
            services.AddTransient<IHotelRepository, DatabaseHotelRepository>();
            services.AddTransient<IRoomRepository, DatabaseRoomRepository>();
            services.AddTransient<IAmenityRepository, DatabaseAmenityRepository>();
            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Hotel Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options=>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Hotel Party");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
               
            });
        }
    }
}
