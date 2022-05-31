using Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestingSystemAPI.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using TestingSystemAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestingSystemAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ControllersFactory.m_ContentFolderName = configuration.GetValue<string>("Content");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors( b => b
            .AddPolicy("Credentials", options => options
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSingleton(ControllersFactory.Instance.ContentBC);
            services.AddSingleton(ControllersFactory.Instance.StatisticBC);
            services.AddSingleton(ControllersFactory.Instance.UserBC);
            services.AddDbContext<UserDbContext>(o => o.UseNpgsql(Configuration.GetConnectionString("postgress")));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IModuleService, ModuleService>();
            #region Cookie Auth
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/api/auth");
                    options.LogoutPath = new PathString("/api/logout");
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                });
            #endregion
            #region JWT_Authentication
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    /*options.RequireHttpsMetadata = true;*/
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateLifetime = false,

            //        ValidateIssuer = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],

            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),

            //        ValidateAudience = true,
            //        ValidAudience = Configuration["Jwt:Audience"]

            //    };
            //});
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Credentials");
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action?}/{id?}");
            });
        }
    }
}
