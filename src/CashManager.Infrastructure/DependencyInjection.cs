using CashManager.Domain.Entities;
using FlexRepo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Infrastructure;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<CashManagerDbContext>(options =>
            options.UseInMemoryDatabase("CashManagerInMemoryDb"));

        services.AddScoped<IRepository<User, Guid>, UsersRepository>();
    }
}
