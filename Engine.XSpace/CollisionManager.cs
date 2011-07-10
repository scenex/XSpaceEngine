using Engine.XSpace.Contracts;

namespace XSpace
{
    using Microsoft.Xna.Framework;

    class CollisionManager
    {
        private PlayerCollision playerCollision;
        public Rectangle playerTileRectangle;

        public CollisionManager()
        {
            playerCollision = new PlayerCollision();
        }

        public PlayerCollision CheckCollisionPlayerWithTerrain(IPlayer player, Level level)
        {
            playerCollision.Reset();

            playerTileRectangle = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.SpriteTexture.Bounds.Width - 1, player.SpriteTexture.Bounds.Height - 1);

            for (int i = 0; i < level.MapDimensionsTilesHeight; i++)
            {
                for (int j = 0; j < level.MapDimensionsTilesWidth; j++)
                {
                    if (level.CollisionMap[(i * level.MapDimensionsTilesWidth) + j] == 1)
                    {
                        Rectangle levelMapRectangle = level.LevelMapRectangles[(i * level.MapDimensionsTilesWidth) + j];

                        int playerRectangleWidth = playerTileRectangle.Width;
                        int playerRectangleHeight = playerTileRectangle.Height;


                        // Top Left - Direction Up
                        if (levelMapRectangle.Contains(playerTileRectangle.X, playerTileRectangle.Y - 1))
                        {
                            playerCollision.TopLeftCornerDirectionUp = true;
                        }

                        // Top Right - Direction Up
                        if (levelMapRectangle.Contains(playerTileRectangle.X + playerRectangleWidth, playerTileRectangle.Y - 1))
                        {
                            playerCollision.TopRightCornerDirectionUp = true;
                        }

                        // Top Left - Direction Left
                        if (levelMapRectangle.Contains(playerTileRectangle.X - 1, playerTileRectangle.Y))
                        {
                            playerCollision.TopLeftCornerDirectionLeft = true;
                        }

                        // Bottom Left - Direction Left
                        if (levelMapRectangle.Contains(playerTileRectangle.X - 1, playerTileRectangle.Y + playerRectangleHeight))
                        {
                            playerCollision.BottomLeftCornerDirectionLeft = true;
                        }

                        // Bottom Left - Direction Down //!!
                        if (levelMapRectangle.Contains(playerTileRectangle.X, playerTileRectangle.Y + playerRectangleHeight + 1))
                        {
                            playerCollision.BottomLeftCornerDirectionDown = true;
                        }

                        // Bottom Right - Direction Down //!!
                        if (levelMapRectangle.Contains(playerTileRectangle.X + playerRectangleWidth, playerTileRectangle.Y + playerRectangleHeight + 1))
                        {
                            playerCollision.BottomRightCornerDirectionDown = true;
                        }

                        // Bottom Right - Direction Right //!!
                        if (levelMapRectangle.Contains(playerTileRectangle.X + playerRectangleWidth + 1, playerTileRectangle.Y + playerRectangleHeight))
                        {
                            playerCollision.BottomRightCornerDirectionRight = true;
                        }

                        // Top Right - Direction Right //!!
                        if (levelMapRectangle.Contains(playerTileRectangle.X + playerRectangleWidth + 1, playerTileRectangle.Y))
                        {
                            playerCollision.TopRightCornerDirectionRight = true;
                        }
                    }
                }
            }

            return playerCollision;
        }
    }
}
