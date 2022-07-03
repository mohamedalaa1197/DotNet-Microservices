using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Ordering.Application.Behaviours;

namespace Ordering.Application
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehviour<,>));
            return services;
        }
    }
}
