using Afry.TollCalculator.Api.Dto;
using Afry.TollCalculator.Application.Command;
using Afry.TollCalculator.Application.CommandHandler;
using Afry.TollCalculator.Core.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afry.TollCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollCalculationController : ControllerBase
    {
        private readonly ITollCalculationHandler<ITollCalculationCommand> _tollCalculationHandler;

        public TollCalculationController(ITollCalculationHandler<ITollCalculationCommand> tollCalculationHandler)
        {
            _tollCalculationHandler = tollCalculationHandler;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TollCalcultionResponse>> Calculate([FromBody] TollCalculateRequest dto)
        {
            try
            {
                var reponse = await _tollCalculationHandler.Handle(new DateRangeCommand()
                {
                    Vehicle = dto.Vehicle.GetDefaultVehicle(),
                    TollDates = dto.TollDates
                });

                return Ok(new TollCalcultionResponse()
                {
                    TollAmount = reponse
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
