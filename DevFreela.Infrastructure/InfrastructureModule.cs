﻿using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure;
public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRepositories()
            .AddData(configuration);

        return services;
    }
    private static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        //builder.Services.AddDbContext<DevFreelaDbContext>(o => o.UseInMemoryDatabase("DevFreelaDb"));
        var connectionString = configuration.GetConnectionString("DevFreelaCs");
        services.AddDbContext<DevFreelaDbContext>(o => o.UseSqlServer(connectionString));

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

}
