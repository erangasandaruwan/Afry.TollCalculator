using Afry.TollCalculator.Application.Command;
using Afry.TollCalculator.Application.CommandHandler;
using Afry.TollCalculator.Domain.Service;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Afry.TollCalculator.UnitTest
{
    static class DependencyResolver
    {
        public static IServiceProvider RegisterAfriServices()
        {
            IServiceCollection services = new ServiceCollection();
            var containerBuilder = new ContainerBuilder();

            services.AddTransient<ITollCalculatorService, TollCalculatorService>();
            services.AddTransient<ITollCalculationHandler<ITollCalculationCommand>, TollCalculationHandler<ITollCalculationCommand>>();

            containerBuilder.Populate(services);

            IServiceProvider serviceProvider = new AutofacServiceProvider(containerBuilder.Build());

            return serviceProvider;
        }

        public static IConfigurationRoot ConfigurationRoot()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return configuration;
        }

        public static T GetService<T>(this IServiceProvider provider)
        {
            return (T)provider.GetService(typeof(T));
        }
    }
}
