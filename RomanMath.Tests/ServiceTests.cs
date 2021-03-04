using System.Collections.Generic;
using NUnit.Framework;
using RomanMath.Impl;

namespace RomanMath.Tests
{
    public class ServiceTests
    {
        [SetUp]
        public void Setup(){}

        [Test]
        public void GetRomanValue()
        {
            //Arrange
            const char roman = 'C';
            const int expected = 100;
            //Act
            var actual = Service.GetRomanValue(roman);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Convert()
        {
            //Arrange
            const string expression = "XC";
            const int expected = 90;
            //Act
            var actual = Service.Convert(expression);
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Calculate()
        {
            //Arrange
            var expression = new List<string>() {"19","2","*","6","-"};
            const int expected = 32;
            //Act
            var actual = Service.Calculate(expression);
            //Assert
            Assert.AreEqual(expected, (int) actual);
        }
        [Test]
        public void Evaluate()
        {
            //Arrange
            const string expression = "XIX * II - VI";
            const int expected = 32;
            //Act
            var actual = Service.Evaluate(expression);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}