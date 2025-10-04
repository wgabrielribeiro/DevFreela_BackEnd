using DevFreela.Application.Commands.InsertProject;
using DevFreela.Application.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddServices()
                .AddHandlers();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => 
                config.RegisterServicesFromAssemblyContaining<InsertProjectCommand>()); //ele vai buscar tudo que contém o command

            //Adicionando o comportamento de validação do comando InsertProjectCommand
            services.AddTransient<IPipelineBehavior<InsertProjectCommand, ResultViewModel<int>>, ValidateInsertProjectCommandBehavior>();

            return services;
        }

    }
}
