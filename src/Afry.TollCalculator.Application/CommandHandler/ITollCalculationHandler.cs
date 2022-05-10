using Afry.TollCalculator.Application.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afry.TollCalculator.Application.CommandHandler
{
    public interface ITollCalculationHandler<ITollCalculationCommand>
    {
        Task<int> Handle(DateCommand command);
        Task<int> Handle(DateRangeCommand command);
    }
}
