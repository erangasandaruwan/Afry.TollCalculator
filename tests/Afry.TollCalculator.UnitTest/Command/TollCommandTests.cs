using Afry.TollCalculator.Application.Command;
using Afry.TollCalculator.Application.CommandHandler;
using Afry.TollCalculator.Core.Model;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Afry.TollCalculator.UnitTest.Command
{
    public class TollCommandTests
    {
        private const int NoToll = 0;
        private const int LowToll = 8;
        private const int MidToll = 13;
        private const int HighToll = 18;

        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceProvider = DependencyResolver.RegisterAfriServices();
        }

        #region Toll calculation based on vehicle type

        [Test]
        public async Task ShouldBeNoTollOnMotoBikeAsync()
        {
            DateRangeCommand command = new DateRangeCommand()
            {
                Vehicle = new MotorBike(),
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 5, 6, 8, 0, 0),
                    new DateTime(2022, 5, 6, 8, 29, 0)
                }
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeNoTollOnTractorAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 13, 45, 42),
                Vehicle = new Tractor()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeNoTollOnEmergencyAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 13, 45, 59),
                Vehicle = new Emergency()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeNoTollOnDiplomatAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 14, 45, 0),
                Vehicle = new Diplomat()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeNoTollOnForeignAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 14, 50, 40),
                Vehicle = new Foreign()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeNoTollOnMilitaryAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 13, 35, 10),
                Vehicle = new Military()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(NoToll, result);
        }

        [Test]
        public async Task ShouldBeATollOnCarAsync()
        {
            DateCommand command = new DateCommand()
            {
                TollDate = new DateTime(2022, 5, 6, 12, 35, 40),
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreNotEqual(0, result);
        }

        #endregion

        #region Toll calculation based on time durations and dates

        [Test]
        public async Task ShouldReturnZeroTollOnWeekendsAsync()
        {
            var saturday = new DateTime(2022, 5, 7, 15, 0, 0);
            var sunday = new DateTime(2022, 5, 8, 15, 0, 0);
            var dates = new DateTime[] { saturday, sunday };

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = dates,
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task ShouldReturnZeroOnDefaultHolidaysAsync()
        {
            var beforeChritmas = new DateTime(2021, 12, 24);
            var christmasDay = new DateTime(2021, 12, 25);
            var dates = new DateTime[] { beforeChritmas, christmasDay };

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = dates,
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task ShouldReturnTollZeroOffPeekHoursAsync()
        {
            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 5, 4, 5, 30,0 ),
                    new DateTime(2021, 5, 4, 19, 0, 0)
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task ShouldReturnDoubleMaxTollOnPeakHoursAsync()
        {
            var expectedTotalFee = 2 * HighToll;

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 1, 5, 7, 30,0 ),
                    new DateTime(2021, 1, 5, 16, 0, 0)
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(expectedTotalFee, result);
        }

        [Test]
        public async Task ShouldReturnFourMediumTollOnModerateHoursAsync()
        {
            var expectedTotalFee = 4 * MidToll;

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 9, 1, 6, 40,0 ),
                    new DateTime(2021, 9, 1, 8, 15, 0),
                    new DateTime(2021, 9, 1, 15, 15, 0),
                    new DateTime(2021, 9, 1, 17, 30, 0)
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(expectedTotalFee, result);
        }

        [Test]
        public async Task ShouldReturnLowTollForThirtyMinsForACarOnSixthAsync()
        {
            Car car = new Car();

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 5, 6, 6, 0, 0),
                    new DateTime(2022, 5, 6, 6, 29, 0),
                },
                Vehicle = car
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(8, result);
        }

        [Test]
        public async Task ShouldReturnMidTollForOneHourForACarOnSixthAsync()
        {
            Car car = new Car();

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 5, 6, 6, 0, 0),
                    new DateTime(2022, 5, 6, 6, 59, 58),
                },
                Vehicle = car
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(13, result);
        }

        [Test]
        public async Task ShouldReturnHiTollForOneHourForACarOnForthAsync()
        {
            Car car = new Car();

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 5, 4, 7, 0, 0),
                    new DateTime(2022, 5, 4, 7, 45, 0),
                },
                Vehicle = car
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(18, result);
        }

        [Test]
        public async Task ShouldReturnMidTollForThirtyMinsForACarOnSecondAsync()
        {
            Car car = new Car();

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 4, 4, 8, 0, 0),
                    new DateTime(2022, 4, 4, 8, 26, 0),
                },
                Vehicle = car
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(13, result);
        }

        [Test]
        public async Task ShouldReturnLowTollForOneHourForACarOnEightCarAsync()
        {
            Car car = new Car();

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[]
                {
                    new DateTime(2022, 3, 8, 9, 15, 0),
                    new DateTime(2022, 3, 8, 9, 26, 0),
                },
                Vehicle = car
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(8, result);
        }

        [Test]
        public async Task ShouldReturnOnlyTheHighestFeeWithinEveryHourAsync()
        {
            var expectedTotalFee = HighToll + HighToll + LowToll;

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 9, 1, 6, 45, 0),  //13
                    new DateTime(2021, 9, 1, 7, 15, 0),   //18
                    new DateTime(2021, 9, 1, 16, 59, 0),  //18
                    new DateTime(2021, 9, 1, 17, 0, 0),   //13
                    new DateTime(2021, 9, 1, 18, 29, 0)   //8
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(expectedTotalFee, result);
        }

        [Test]
        public async Task ShouldReturnPossibleMaximumTollFeeAsync()
        {
            var maximumTollFee = 60;

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 9, 1, 6, 0, 0 ),
                    new DateTime(2021, 9, 1, 7, 15, 0),
                    new DateTime(2021, 9, 1, 8, 29, 0),
                    new DateTime(2021, 9, 1, 15, 0, 0),
                    new DateTime(2021, 9, 1, 16, 30, 0),
                    new DateTime(2021, 9, 1, 18, 15, 0)
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(maximumTollFee, result);
        }

        [Test]
        public async Task ShouldReturnOnlyFeeFOrEveryHourForUnsortedDatesAsync()
        {
            var expectedTotalFee = HighToll + HighToll + LowToll;

            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = new DateTime[] {
                    new DateTime(2021, 9, 1, 17, 0, 0),
                    new DateTime(2021, 9, 1, 7, 15, 0),
                    new DateTime(2021, 9, 1, 6, 45, 0 ),
                    new DateTime(2021, 9, 1, 18, 29, 0),
                    new DateTime(2021, 9, 1, 16, 59, 0)
                },
                Vehicle = new Car()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(expectedTotalFee, result);
        }

        #endregion

        [Test]
        public async Task ShouldReturnZeroOnEmptyDatesArrayAsync()
        {
            DateRangeCommand command = new DateRangeCommand()
            {
                TollDates = Array.Empty<DateTime>(),
                Vehicle = new MotorBike()
            };

            var result = await _serviceProvider.GetService<ITollCalculationHandler<ITollCalculationCommand>>().Handle(command);
            Assert.AreEqual(0, result);
        }
    }
}
