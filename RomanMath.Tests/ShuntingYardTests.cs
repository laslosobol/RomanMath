using System.Collections.Generic;
using NUnit.Framework;

namespace RomanMath.Tests
{
    public class ShuntingYardTests
    {
        [SetUp]
        public void Setup(){}

        [Test]
        public void ShuntingYard()
        {
            //Arrange
            const string expression = "19 * 2 - 6";
            var expected = new List<string>() {"19","2","*","6","-"};
            //Act
            var actual = Impl.ShuntingYard.ToPostfix(expression);
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}