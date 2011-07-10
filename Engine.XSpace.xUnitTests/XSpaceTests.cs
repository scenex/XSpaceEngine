namespace XUnitTests
{
    using Engine.XSpace.Contracts;
    using FluentAssertions;
    using Microsoft.Xna.Framework;
    using Xunit;
    using XSpace;

    public class XSpaceTests
    {
        [Fact]
        public void WhenPlayerMovesDownYCoordinateIsExpectedToIncrease()
        {
            var expectedValue = new Vector2(0, 1);
            IPlayer player = new Player(null, new Vector2(0, 0));           
            player.MoveDown();
            //Assert.Equal(expectedValue, player.Position);
            expectedValue.Should().Be(player.Position);
        }

        [Fact]
        public void WhenPlayerMovesUpYCoordinateIsExpectedToDecrease()
        {
            var expectedValue = new Vector2(0, -1);
            IPlayer player = new Player(null, new Vector2(0, 0));
            player.MoveUp();
            //Assert.Equal(expectedValue, player.Position);
            expectedValue.Should().Be(player.Position);
        }

        [Fact]
        public void WhenPlayerMovesLeftXCoordinateIsExpectedToDecrease()
        {
            var expectedValue = new Vector2(-1, 0);
            IPlayer player = new Player(null, new Vector2(0, 0));
            player.MoveLeft();
            //Assert.Equal(expectedValue, player.Position);
            expectedValue.Should().Be(player.Position);
        }

        [Fact]
        public void WhenPlayerMovesRightXCoordinateIsExpectedToIncrease()
        {
            var expectedValue = new Vector2(1, 0);
            IPlayer player = new Player(null, new Vector2(0, 0));
            player.MoveRight();
            //Assert.Equal(expectedValue, player.Position);
            expectedValue.Should().Be(player.Position);
        }
    }
}
