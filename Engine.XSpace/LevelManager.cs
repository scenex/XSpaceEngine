namespace XSpace
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class LevelManager : IDisposable
    {

        #region Fields

        private readonly Game game;

        private const int MapTileWidth = 32;
        private const int MapTileHeight = 32;

        private const int MapDimensionsTilesWidth  = 24;
        private const int MapDimensionsTilesHeight = 12;

        private const int MipmapLevel = 0;

        private readonly List<Rectangle> tilesetTileRectangles;
        private readonly List<Rectangle> levelMapRectangles;
        private readonly int[] collisionMap;

        private readonly Color[] tileBuffer;

        private readonly Level level;

        private readonly int[] levelMapRaw = new int[] 
        { 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1, 1, 5, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1, 1, 6, 8, 8, 7, 1, 1,
            1, 1, 1, 1, 1, 1, 1, 3, 0, 0, 2, 1, 1, 1, 1, 6, 8, 8, 8, 8, 8, 8, 8, 8
        };

        private readonly int[] collidableTiles = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
       
        private Texture2D tileset;
        private Texture2D levelMapTexture;

        #endregion

        #region Constructor

        public LevelManager(Game gameInstance)
        {
            this.game = gameInstance;

            this.tileBuffer = new Color[MapTileWidth * MapTileHeight];
            this.tilesetTileRectangles = new List<Rectangle>();

            this.levelMapRectangles = new List<Rectangle>();
            this.collisionMap = new int[this.levelMapRaw.Length];
            this.level = new Level();
        }

        #endregion

        #region Public Methods

        public Texture2D CreateLevelMap(Texture2D tilesetTexture)
        {
            this.tileset = tilesetTexture;

            GenerateLevelMapTexture();
            GenerateCollisionMap();

            return this.levelMapTexture;
        }

        public Level GetCreatedLevel()
        {
            level.MapDimensionsTilesWidth = MapDimensionsTilesWidth;
            level.MapDimensionsTilesHeight = MapDimensionsTilesHeight;
            level.LevelMapTexture = this.levelMapTexture;
            level.CollisionMap = this.collisionMap;
            level.LevelMapRectangles = this.levelMapRectangles;

            return this.level;
        }

        #endregion

        #region Private Methods

        private Texture2D GenerateLevelMapTexture()
        {
            if (this.tileset.Bounds.Width % MapTileWidth == 0 && this.tileset.Bounds.Height % MapTileHeight == 0)
            {
                this.GenerateTilesetRectangles(this.tileset.Bounds.Width, this.tileset.Bounds.Height);
                this.levelMapTexture = new Texture2D(game.GraphicsDevice, MapDimensionsTilesWidth * MapTileWidth, MapDimensionsTilesHeight * MapTileHeight);

                for (int i = 0; i < MapDimensionsTilesHeight; i++)
                {
                    for (int j = 0; j < MapDimensionsTilesWidth; j++)
                    {
                        int tilesetIndex = this.levelMapRaw[(i * MapDimensionsTilesWidth) + j];
                        Color[] tileToDraw = this.GetTileFromTileset(tilesetIndex);

                        var tileRectangle = new Rectangle(j * MapTileWidth, i * MapTileHeight, MapTileWidth, MapTileHeight);
                        this.levelMapTexture.SetData<Color>(MipmapLevel, tileRectangle, tileToDraw, 0, this.tileBuffer.Length);
                        this.StoreLevelMapRectangles(tileRectangle);
                    }
                }

                return this.levelMapTexture;
            }
            else
            {
                throw new ArgumentException("Tile dimensions do not correspond");
            }
        }

        private void GenerateCollisionMap()
        {
            for (int i = 0; i < MapDimensionsTilesHeight; i++)
            {
                for (int j = 0; j < MapDimensionsTilesWidth; j++)
                {
                    int tilesetIndex = this.levelMapRaw[(i * MapDimensionsTilesWidth) + j];

                    if (this.collidableTiles.Contains<int>(tilesetIndex))
                    {
                        this.collisionMap[(i * MapDimensionsTilesWidth) + j] = 1;
                    }
                    else
                    {
                        this.collisionMap[(i * MapDimensionsTilesWidth) + j] = 0;
                    }
                }
            }
        }

        private void GenerateTilesetRectangles(int tilesetWidth, int tilesetHeight)
        {
            int amountTilesWidth = tilesetWidth / MapTileWidth;
            int amountTilesHeight = tilesetHeight / MapTileHeight;
            int amountTilesTileset = amountTilesWidth * amountTilesHeight;

            for (int i = 0; i < amountTilesTileset; i++)
            {
                this.tilesetTileRectangles.Add(
                    new Rectangle(
                        (i * MapTileWidth) % (amountTilesWidth * MapTileWidth),
                        ((i * MapTileWidth) / (amountTilesWidth * MapTileWidth)) * MapTileHeight,
                        MapTileWidth,
                        MapTileHeight));
            }
        }

        private Color[] GetTileFromTileset(int index)
        {
            this.tileset.GetData<Color>(MipmapLevel, this.tilesetTileRectangles[index], this.tileBuffer, 0, this.tileBuffer.Length);
            return this.tileBuffer;
        }

        private void StoreLevelMapRectangles(Rectangle rect)
        {
            this.levelMapRectangles.Add(rect);
        }

        #endregion

        #region Dispose Stuff

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.levelMapTexture != null)
                {
                    this.levelMapTexture.Dispose();
                    this.levelMapTexture = null;
                }
            }
        }

        #endregion
    }
}
