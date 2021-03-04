using System;
using NUnit.Framework;
using RomanMath.Impl;

namespace RomanMath.Tests
{
    public class ExceptionTests
    {
        [SetUp]
        public void Setup(){}

        [Test]
        public void Evaluate_IsEmptyExpression_Exception()
        {
            //Arrange
            const string expression = "";
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => Service.Evaluate(expression));
        }
        [Test]
        public void Evaluate_IsNullExpression_Exception()
        {
            //Arrange
            const string expression = null;
            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => Service.Evaluate(expression));
        }
        [Test]
        public void Evaluate_InvalidToken_Exception()
        {
            //Arrange
            const string expression = "AAA , BBB";
            //Act and Assert
            Assert.Throws<ArgumentException>(() => Service.Evaluate(expression));
        }
        [Test]
        public void Evaluate_InvalidExpression_Exception()
        {
            //Arrange
            const string expression = "C + + XX";
            //Act and Assert
            Assert.Throws<ArgumentException>(() => Service.Evaluate(expression));
        }
    }
}