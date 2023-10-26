using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BeverlyHillsZoo.Tests.Animals
{
    public class AnimalsValidationTests
    {
        private readonly AnimalInputValidator _validator = new AnimalInputValidator();

        [Fact]
        public void ValidateAnimalName_ValidName_ShouldReturnSuccsess()
        {
            var result = _validator.ValidateAnimalName("Lion");
            Assert.True(result.Successful);
        }
        [Fact]
        public void ValidateAnimalName_NameOver50Chars_ShouldReturnError()
        {
            var result = _validator.ValidateAnimalName("ThisNameExceeds50CharactersAndShouldThereforeNotBeValidExtraWordsToMakeSureItIsMoreThan50Characters");
            Assert.False(result.Successful);
        }

        [Fact]
        public void ValidateAnimalName_NameIsWhiteSpace_ShouldReturnError()
        {
            var result = _validator.ValidateAnimalName("    ");
            Assert.False(result.Successful);
        }

        //  Negative, zero & over max
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1001)]
        public void ValidateAltitude_InvalidAltitudes_ShouldReturnError(int altitude)
        {
            var result = _validator.ValidateAltitude(altitude);
            Assert.False(result.Successful);
        }
        // All within range
        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        [InlineData(1000)]
        public void ValidateAltitude_ValidAltitudes_ShouldReturnSuccsess(int altitude)
        {
            var result = _validator.ValidateAltitude(altitude);
            Assert.True(result.Successful);
        }


        // Negativem, zero & over max
        [Theory] 
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1001)]
        public void ValidateDivingDepth_InvalidDepths_ShouldReturnError(int divingDepth)
        {
            var result = _validator.ValidateDivingDepth(divingDepth);
            Assert.False(result.Successful);
        }
        // all within range
        [Theory]
        [InlineData(1)]
        [InlineData(500)]
        [InlineData(1000)]
        public void ValidateDivingDepth_ValidDepths_ShouldReturnSuccsess(int divingDepth)
        {
            var result = _validator.ValidateDivingDepth(divingDepth);
            Assert.True(result.Successful);
        }
        // Negative, Zero & over max
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(201)]
        public void ValidateSpeed_InvalidSpeeds_ShouldReturnError(int speed)
        {
            var result = _validator.ValidateSpeed(speed);
            Assert.False(result.Successful);
        }
        // all within range
        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(200)]
        public void ValidateSpeed_ValidSpeeds_ShouldReturnSuccsess(int speed)
        {
            var result = _validator.ValidateSpeed(speed);
            Assert.True(result.Successful);
        }
    }
}


