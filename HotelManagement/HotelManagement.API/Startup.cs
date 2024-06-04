using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HotelBookingAPI.Application.Services;
using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Infrastructure.Data;
using HotelBookingAPI.Infrastructure.Data.Repositories;
using HotelManagement.Core.Interfaces;
using MySolution.Infrastructure.Services;
using MySolution.Infrastructure.Data;


namespace HotelBookingAPI
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


            services.AddControllers();


            // Configura los servicios SMTP desde appsettings.json o un archivo de configuración
            var smtpServer = Configuration["Smtp:Server"];
            var smtpPort = int.Parse(Configuration["Smtp:Port"]);
            var smtpUser = Configuration["Smtp:User"];
            var smtpPass = Configuration["Smtp:Pass"];

            // Registrar EmailService con DI container
            services.AddScoped<IEmailService>(provider => new EmailService(smtpServer, smtpPort, smtpUser, smtpPass));


            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<HotelService>();
            services.AddScoped<RoomService>();
            services.AddScoped<ReservationService>();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Booking API", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Booking API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
