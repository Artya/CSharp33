using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest;

namespace UnitTestTests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void IsBaseColor_When_ColorIsBlack_Then_MustBeTrue()
        {
            // Arrange
            const string color = "black";
            const bool expected = true;

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsBaseColor_When_ColorIsWhite_Then_MustBeTrue()
        {
            // Arrange
            const string color = "white";
            const bool expected = true;

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsBaseColor_When_ColorIsRandomColor_Then_MustBeFalse()
        {
            // Arrange
            const string color = "green";
            const bool expected = false;

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
