using Afry.TollCalculator.Api.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afry.TollCalculator.Api.Validator
{
    public class TollCalculateRequestValidator : AbstractValidator<TollCalculateRequest>
    {
        public TollCalculateRequestValidator()
        {
            RuleFor(x => x.Vehicle)
                .NotNull().NotEmpty()
                .WithMessage("The vehicle cannot be null or empty.");

            RuleForEach(x => x.TollDates)
                .NotNull().NotEmpty()
                .WithMessage("Toll dates cannot be null or empty.");
        }
    }
}
