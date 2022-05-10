using Afry.TollCalculator.Application.Command;
using Afry.TollCalculator.Application.CommandHandler;
using Afry.TollCalculator.Domain.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Afry.TollCalculator.Application
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITollCalculatorService, TollCalculatorService>();
            services.AddTransient<ITollCalculationHandler<ITollCalculationCommand>, TollCalculationHandler<ITollCalculationCommand>>();

            return services;
        }
    }
}
