using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine.XSpace.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XSpace
{
    public class Player : IPlayer
    {
        #region Fields

        private Texture2D spriteTexture;
        private Vector2 position;

        #endregion

        #region Constructors

        public Player()
        {
            
        }

        public Player(Texture2D spriteTexture, Vector2 position)
        {
            this.SpriteTexture = spriteTexture;
            this.Position = position;
        }

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D SpriteTexture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        #endregion

        #region Methods

        public void MoveLeft()
        {
            this.position.X--;
        }

        public void MoveRight()
        {
            this.position.X++;
        }

        public void MoveUp()
        {
            this.position.Y--;
        }

        public void MoveDown()
        {
            this.position.Y++;
        }



        #endregion
    }
}
