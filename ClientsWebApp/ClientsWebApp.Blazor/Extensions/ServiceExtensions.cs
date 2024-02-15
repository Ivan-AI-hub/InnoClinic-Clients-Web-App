using BlazorRtc.Client.WebRtc;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Managers;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Documents;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Domain.Results;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Services.Services;

namespace ClientsWebApp.Blazor.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IOfficeService, OfficeService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IReceptionistService, ReceptionistService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<ISpecializationService, SpecializationService>();

            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<WebRtcService>();
        }

        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<IIdentityManager, IdentityManager>();
            services.AddScoped<IAppointmentManager, AppointmentManager>();
            services.AddScoped<IDoctorManager, DoctorManager>();
            services.AddScoped<IOfficeManager, OfficeManager>();
            services.AddScoped<IPatientManager, PatientManager>();
            services.AddScoped<IReceptionistManager, ReceptionistManager>();
            services.AddScoped<IResultManager, ResultManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ISpecializationManager, SpecializationManager>();

            services.AddScoped<IStorageService, LocalStorageService>();
        }

        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthorizedClient, AuthorizedClient>();
        }
    }
}
