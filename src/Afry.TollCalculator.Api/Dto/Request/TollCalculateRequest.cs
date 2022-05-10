using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Afry.TollCalculator.Api.Dto
{
    public class TollCalculateRequest
    {
        public string Vehicle { get; set; }
        public DateTime[] TollDates { get; set; }
    }
}
