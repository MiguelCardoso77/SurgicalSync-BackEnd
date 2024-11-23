using System;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.Appointments;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.OperationType;
using DDDNetCore.Domain.Patients;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain.SurgeryRooms;
using DDDNetCore.Domain.Users;
using DDDNetCore.Infraestructure;
using DDDNetCore.Infraestructure.Appointments;
using DDDNetCore.Infraestructure.OperationRequests;
using DDDNetCore.Infraestructure.OperationTypes;
using DDDNetCore.Infraestructure.Patients;
using DDDNetCore.Infraestructure.Staff;
using DDDNetCore.Infraestructure.SurgeryRooms;
using DDDNetCore.Infraestructure.Users;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDDNetCore
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder.WithOrigins(new[] { "http://localhost:53052", "http://localhost:4200" }).AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("surgicalsync-d5bd5-firebase-adminsdk-7v461-12fb9fe637.json")
            });
            
            ConfigureMyServices(services);
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SurgicalSyncContext context)
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

            app.UseRouting();
            
            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            var planningBootstrap = new PlanningBootstrap(context);
            Console.WriteLine("Bootstrap data loaded.");
        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddDbContext<SurgicalSyncContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddTransient<PlanningBootstrap>();

            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<PatientService>();
            services.AddTransient<PatientMapper>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserService>();
            services.AddTransient<UserMapper>();
            
            services.AddTransient<IStaffRepository, StaffRepository>();
            services.AddTransient<StaffService>();
            services.AddTransient<StaffMapper>();

            services.AddTransient<IOperationTypeRepository, OperationTypeRepository>();
            services.AddTransient<OperationTypeService>();
            services.AddTransient<OperationTypeMapper>();

            services.AddTransient<IOperationRequestRepository, OperationRequestRepository>();
            services.AddTransient<OperationRequestService>();
            services.AddTransient<OperationRequestMapper>();

            services.AddTransient<ISurgeryRoomsRepository, SurgeryRoomRepository>();
            services.AddTransient<SurgeryRoomService>();
            services.AddTransient<SurgeryRoomMapper>();

            services.AddTransient<IAppointmentsRepository, AppointmentRepository>();
            services.AddTransient<AppointmentService>();
            services.AddTransient<AppointmentMapper>();
            
            services.AddTransient<PatientNameMicroService>();
            services.AddTransient<PatientMicroService>();
            services.AddTransient<UserEmailMicroService>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<DeletePatientMicroService>();
            services.AddTransient<OperationTypeNameMicroService>();
            services.AddTransient<EmailService>();
        }
    }
}