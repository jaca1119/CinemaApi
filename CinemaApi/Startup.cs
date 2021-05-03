using AutoMapper;
using CinemaApi.Data;
using CinemaApi.Models;
using CinemaApi.Repositories;
using CinemaApi.Repositories.Interfaces;
using CinemaApi.Services;
using CinemaApi.Services.Interfaces;
using CinemaApi.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace CinemaApi
{
    public class Startup
    {
        private readonly string Origins = "origins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            //services
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISnackService, SnackService>();

            //repositories
            services.AddTransient(typeof(IRepositoryBase<>), typeof(BaseRepository<>));
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ISeatRepository, SeatRepository>();
            services.AddTransient<IHallRepository, HallRepository>();
            services.AddTransient<ISnackRepository, SnackRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options =>
            {
                options.AddPolicy(name: Origins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .WithMethods("GET", "POST", "PUT", "OPTIONS")
                        .AllowAnyHeader();                    
                    });
            });

            services.AddHttpClient();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Origins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
