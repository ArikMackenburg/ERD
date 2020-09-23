using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Web.Data;
using Web.Models;
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
            services.AddControllers(options=>
            {
                options.Filters.Add(new AuthorizeFilter());
            });
            //3. Register our DbContext with app
            services.AddDbContext<HotelDbContext>(options =>
            {
                
                // Equivalent to DATABASE_URL
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequiredLength = 6;
 
             }).AddEntityFrameworkStores<HotelDbContext>();

            services.AddTransient<IUser, IdentityUserService>();
            services.AddScoped<JwtTokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options=>
                {
                    options.TokenValidationParameters = JwtTokenService.GetValidationParameters(Configuration);
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
            });

            services.AddTransient<IHotel, DBHotel>();
            services.AddTransient<IRoom, DBRoom>();
            services.AddTransient<IAmenity, DBAmenity>();
            services.AddSwaggerGen(options => 
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Hotel Api", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });
                options.OperationFilter<AuthenticationRequirementOperationFilter>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
               
            });
        }
        private class AuthenticationRequirementOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                var hasAnonymous = context.ApiDescription.CustomAttributes().OfType<AllowAnonymousAttribute>().Any();
                if (hasAnonymous)
                    return;

                operation.Security ??= new List<OpenApiSecurityRequirement>();

                var scheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme,
                    },
                };
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [scheme] = new List<string>()
                });
            }
        }
    }
}
