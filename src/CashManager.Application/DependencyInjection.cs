using CashManager.Application.Interfaces;
using CashManager.Application.Services;
using CashManager.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Application;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Validators
        services.AddTransient<RegisterUserValidator>();

        // Services
        services.AddSingleton<IResourceManagerProvider, ResourceManagerProvider>();

        // UseCases
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
