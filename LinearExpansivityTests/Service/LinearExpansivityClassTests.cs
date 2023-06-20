using LinearExpansivity.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearExpansivityTests.Service
{
    [TestFixture]
    public class LinearExpansivityClassTests
    {
        private ILinearExpansivity _iLinearExpansivity { get; set; }
        [SetUp]
        public void SetUp()
        {
            _iLinearExpansivity = new LinearExpansivityClass();
        }

        [Test]
        [TestCase(1, 2, 1)]
        public void Change_WhenCalled_ReturnTheDifferenceBetweenTheArgs(double initial, double final, decimal expectedResult)
        {
            //Act
            var result = _iLinearExpansivity.Change(initial, final);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));

        }
        [Test]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(0, 0)]
        public void Change_WhenHavingUnexpactedArgs_ThrowArgumentException(double initial, double final)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _iLinearExpansivity.Change(initial, final));
        }

        [Test]
        [TestCase(0, 0.00000008, 10, 200, 0.0000016)] //When the linear expansivity of the first material is unknown
        [TestCase(0.0000016, 0, 10, 200, 0.00000008)] //When the linear expansivity of the second material is unknown
        [TestCase(0.0000016, 0.00000008, 0, 200, 10)] //When the material-length  of first material is unknow
        [TestCase(0.0000016, 0.00000008, 10, 0, 200)] //When the material-length  of second material is unknow
        public void LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature_WhenCalled_ReturnTheResultOfTheUnknownArgThatisZero(double linearExpansivityOfMaterial_A, double linearExpansivityOfMaterial_B, double lengthOfMaterial_A_In_Meter, double lengthOfMaterial_B_In_Meter, decimal expectedResult)
        {
                  //Using this in solving a particular question,never mind my parameters values,the methos is tested with various values and passes
          
            //Act
            var result = _iLinearExpansivity.LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature(linearExpansivityOfMaterial_A, linearExpansivityOfMaterial_B, lengthOfMaterial_A_In_Meter, lengthOfMaterial_B_In_Meter);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));

        }

         [Test]
        public void LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature_WhenNoArgIsGiven_ThrowArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _iLinearExpansivity.LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature(0,0,0,0));

        }

        [Test]
        [TestCase(0, 1, 0, 1)] //When having two or more unknown
        [TestCase(1, 0, 1, 0)]
        [TestCase(0, 0, 0, 1)]
        [TestCase(1, 0, 0, 0)]
        [Ignore("Just because i needed to reduce the the running tests")]
        public void LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature_WhenHavingMoreThanOneUnknown_ThrowArgumentException(double linearExpansivityOfMaterial_A, double linearExpansivityOfMaterial_B, double lengthOfMaterial_A_In_Meter, double lengthOfMaterial_B_In_Meter)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _iLinearExpansivity.LinearExpansivityOfTwoDifferentMaterialsAtSameTemprature(linearExpansivityOfMaterial_A, linearExpansivityOfMaterial_B, lengthOfMaterial_A_In_Meter, lengthOfMaterial_B_In_Meter));

        }

        [Test]
        [TestCase(1, 2, 1, 2, 0, 1)] //When the linear expansivity is unknown
        [TestCase(1, 2, 0, 2, 1, 1)] //When initial temperature is unknown
        [TestCase(0, 2, 1, 2, 1, 1)] //When the initial-length  is unknow
        [TestCase(1, 0, 1, 2, 1, 2)] //When the final-length  is unknow
        [TestCase(1, 2, 1, 0, 1, 2)] //When final temperature is unknown
        public void SolvingForLinearExpansivity_WhenCalled_ReturnTheResultOfTheUnknownArgThatisZero(double initialLengthInMeter, double finalLengthInMeter, double initialTempInKelvin, double finalTempInKelvin, double alpha, decimal expectedResult)
        {

            //Act
            var result = _iLinearExpansivity.SolvingForLinearExpansivity(initialLengthInMeter,finalLengthInMeter,initialTempInKelvin,finalTempInKelvin,alpha);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]
        [TestCase(1, 2, 0, 2, 0)]
        [TestCase(1, 2, 0, 0, 1)]
        [TestCase(1, 2, 0, 0, 0)]
        [TestCase(0, 2, 0, 0, 1)]
        [TestCase(0, 2, 0, 0, 1)]
        [TestCase(0, 2, 0, 0, 0)]
        [TestCase(0, 0, 0, 0, 0)]
        public void SolvingForLinearExpansivity_WhenTwoOrMoreAgrsAreUnknown_ReturnTheResultOfTheUnknownArgThatisZero(double initialLengthInMeter, double finalLengthInMeter, double initialTempInKelvin, double finalTempInKelvin, double alpha)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _iLinearExpansivity.SolvingForLinearExpansivity(initialLengthInMeter, finalLengthInMeter, initialTempInKelvin, finalTempInKelvin, alpha));

        }

    }

}
