using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTest;

namespace UnitTestTests
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void Handle_When_ColorIsBlack_Then_MustBeTrue()
        {
            // Arrange
            const string color = "black";

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Handle_When_ColorIsWhite_Then_MustBeTrue()
        {
            // Arrange
            const string color = "white";

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Handle_When_ColorIsRandomColor_Then_MustBeFalse()
        {
            // Arrange
            const string color = "green";

            // Act
            bool actual = color.IsBaseColor();

            // Assert
            const bool expected = false;
            Assert.AreEqual(expected, actual);
        }
    }
}
