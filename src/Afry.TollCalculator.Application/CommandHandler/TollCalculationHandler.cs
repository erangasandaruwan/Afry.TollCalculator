using Afry.TollCalculator.Application.Command;
using Afry.TollCalculator.Domain.Service;
using System.Threading.Tasks;

namespace Afry.TollCalculator.Application.CommandHandler
{
    public class TollCalculationHandler<ITollCalculationCommand> : ITollCalculationHandler<ITollCalculationCommand>
    {
        private readonly ITollCalculatorService _tollCalculatorService;
        public TollCalculationHandler(ITollCalculatorService tollCalculatorService)
        {
            _tollCalculatorService = tollCalculatorService;
        }

        public Task<int> Handle(DateCommand command)
        {
            var tollAmount = _tollCalculatorService.GetTollFee(command.Vehicle, new System.DateTime[] { command.TollDate });

            return Task.FromResult(tollAmount);
        }

        public Task<int> Handle(DateRangeCommand command)
        {
            var tollAmount = _tollCalculatorService.GetTollFee(command.Vehicle, command.TollDates);

            return Task.FromResult(tollAmount);
        }
    }
}
