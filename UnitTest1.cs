using CinemaTicketSystem;

namespace Fedorov_PR_33_pract3_OKFCS
{
    public class UnitTest1
    {
        [Fact]
        public void CalculatePrice_ShouldReturnBasePrice_ForAdultWithoutDiscounts ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(18, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(300m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldReturnZero_ForChildUnder6 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 5,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Saturday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(0m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApply40PercentDiscount_ForChild6To17 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 10,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Tuesday,
                SessionTime = new TimeSpan(15, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(180m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApply20PercentDiscount_ForStudent18To25 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 20,
                IsStudent = true,
                IsVip = false,
                Day = DayOfWeek.Thursday,
                SessionTime = new TimeSpan(16, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(240m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApply50PercentDiscount_For65Plus ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 70,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Friday,
                SessionTime = new TimeSpan(17, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(150m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApply15PercentMorningDiscount_ForSessionBefore12 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(11, 59, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(255m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApply30PercentWensdayDiscount ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Wednesday,
                SessionTime = new TimeSpan(18, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(210m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApplyVip_AfterDiscount ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = true,
                Day = DayOfWeek.Wednesday,
                SessionTime = new TimeSpan(18, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(420m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApplyOnlyMaxDiscount_WhenMultipleDiscounts ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 20,
                IsStudent = true,
                IsVip = false,
                Day = DayOfWeek.Wednesday,
                SessionTime = new TimeSpan(10, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(210m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApplyOnlyMaxDiscount_WithVip ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 12,
                IsStudent = false,
                IsVip = true,
                Day = DayOfWeek.Wednesday,
                SessionTime = new TimeSpan(10, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(360m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldRoundPrice ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(11, 30, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(255m, result);
            Assert.Equal(result, Math.Round(result, 0, MidpointRounding.AwayFromZero));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void CalculatePrice_ShouldReturnZero_ForAgesUnder6 (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(0m, result);
        }
        [Theory]
        [InlineData(6)]
        [InlineData(17)]
        public void CalculatePrice_ShouldApply40PercentDiscount_ForAges6To17 (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(180m, result);
        }
        [Theory]
        [InlineData(18)]
        [InlineData(25)]
        public void CalculatePrice_ShouldApply20PercentDiscount_ForStudentsAges18To25 (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = true,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(240m, result);
        }
        [Theory]
        [InlineData(65)]
        [InlineData(100)]
        [InlineData(120)]
        public void CalculatePrice_ShouldApply50PercentDiscount_For65To120 (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(150m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldTransitionFromFreeToChildDiscount_AtAge6 ()
        {
            var calculator = new TicketPriceCalculator( );
            var age5 = new TicketRequest( )
            {
                Age = 5,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var age6 = new TicketRequest( )
            {
                Age = 6,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var price5 = calculator.CalculatePrice(age5);
            var price6 = calculator.CalculatePrice(age6);
            Assert.Equal(0m, price5);
            Assert.Equal(180m, price6);
        }
        [Fact]
        public void CalculatePrice_ShouldTransitionFromChildToStudentDiscount_AtAge18 ()
        {
            var calculator = new TicketPriceCalculator( );
            var age17 = new TicketRequest( )
            {
                Age = 17,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var age18 = new TicketRequest( )
            {
                Age = 18,
                IsStudent = true,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var price17 = calculator.CalculatePrice(age17);
            var price18 = calculator.CalculatePrice(age18);
            Assert.Equal(180m, price17);
            Assert.Equal(240m, price18);
        }
        [Fact]
        public void CalculatePrice_ShouldTransitionFromStudentToRegular_AtAge26 ()
        {
            var calculator = new TicketPriceCalculator( );
            var age25 = new TicketRequest( )
            {
                Age = 25,
                IsStudent = true,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var age26 = new TicketRequest( )
            {
                Age = 26,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var price25 = calculator.CalculatePrice(age25);
            var price26 = calculator.CalculatePrice(age26);
            Assert.Equal(240m, price25);
            Assert.Equal(300m, price26);
        }
        [Fact]
        public void CalculatePrice_ShouldTransitionFromRegularTo65Plus_AtAge65 ()
        {
            var calculator = new TicketPriceCalculator( );
            var age64 = new TicketRequest( )
            {
                Age = 64,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var age65 = new TicketRequest( )
            {
                Age = 65,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var price64 = calculator.CalculatePrice(age64);
            var price65 = calculator.CalculatePrice(age65);
            Assert.Equal(240m, price64);
            Assert.Equal(300m, price65);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(120)]
        public void CalculatePrice_ShouldAccept0And120Age (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.True(result >= 0m);
        }
        [Fact]
        public void CalculatePrice_ShouldThrowArgumentNullException_WhenRequestIsNull ()
        {
            var calculator = new TicketPriceCalculator( );
            Assert.Throws<ArgumentNullException>(() => calculator.CalculatePrice(null));
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void CalculatePrice_ShouldThrowArgumentOutOfRangeException_WhenAgeIsNegative (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculatePrice(request));
        }
        [Theory]
        [InlineData(121)]
        [InlineData(150)]
        [InlineData(200)]
        public void CalculatePrice_ShouldThrowArgumentOutOfRangeException_WhenAgeIsMore120 (int age)
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = age,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => calculator.CalculatePrice(request));
        }
        [Fact]
        public void CalculatePrice_ShouldNotApplyStudentDiscount_IfIsStudentFalse ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 20,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(14, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(300m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldNotApplyMorningDiscount_At12_00 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(12, 0, 0)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(300m, result);
        }
        [Fact]
        public void CalculatePrice_ShouldApplyMorningDiscount_At11_59_59 ()
        {
            var calculator = new TicketPriceCalculator( );
            var request = new TicketRequest( )
            {
                Age = 30,
                IsStudent = false,
                IsVip = false,
                Day = DayOfWeek.Monday,
                SessionTime = new TimeSpan(11,59, 59)
            };
            var result = calculator.CalculatePrice(request);
            Assert.Equal(255m, result);
        }
    }
}