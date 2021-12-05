using MarsRover.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MarsRover.Domain.Tests
{
    [TestClass]
    public class Test_Input_Should_Be_Work
    {
        private readonly Plateau _plateau;

        public Test_Input_Should_Be_Work()
        {
            _plateau = new Plateau(new NullLogger());
            _plateau.Initialize(5, 5);
        }

        [TestMethod]
        [DataRow(1, 2, "N", "LMLMLMLMM", 1, 3, "N")]
        [DataRow(3, 3, "E", "MMRMMRMRRM", 5, 1, "E")]
        public void SampleDataShouldBeWork(int x, int y, string direction, string commands, int expectedX, int expectedY, string expectedDirection)
        {
            Assert.IsTrue(Enum.TryParse(typeof(Direction), direction, out var directionEnum));
            Assert.IsTrue(_plateau.TryLocateRover(x, y, (Direction)directionEnum, out var rover));
            rover.SetNavigation(commands);
            rover.GoToNavigation();

            Assert.AreEqual(rover.State.X, expectedX);
            Assert.AreEqual(rover.State.Y, expectedY);
            Assert.IsTrue(Enum.TryParse(typeof(Direction), expectedDirection, out var expectedDirectionEnum));
            Assert.AreEqual(rover.State.Direction, expectedDirectionEnum);
        }
    }
}
