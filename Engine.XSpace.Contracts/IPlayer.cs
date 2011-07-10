using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.XSpace.Contracts
{
    public interface IPlayer
    {
        Vector2 Position { get; set; }
        Texture2D SpriteTexture { get; set; }
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
    }
}