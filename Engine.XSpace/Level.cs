namespace XSpace
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level
    {
        public int MapDimensionsTilesWidth { get; set; }
        public int MapDimensionsTilesHeight { get; set; }

        public Texture2D LevelMapTexture { get; set; }
        public int[] CollisionMap { get; set; }

        public List<Rectangle> LevelMapRectangles { get; set; }
    }
}
