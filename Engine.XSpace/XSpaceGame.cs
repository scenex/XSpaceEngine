using Engine.XSpace.Contracts;

namespace XSpace
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public sealed class XSpaceGame : IDisposable
    {
        #region Fields

        private readonly Game game;
        private readonly GraphicsDevice device;
        private readonly SpriteBatch spriteBatch;

        private readonly LevelManager levelManager;
        private readonly CollisionManager collisionManager;
        private readonly PlayerInput playerInput;

        private IPlayer player;

        private ContentManager content;
        private Texture2D tileset;
        private Texture2D dude;
        private SpriteFont spriteFont;
        private PlayerCollision playerCollision; 
        

        private int currentGameState = (int)GameState.Init;

        #endregion

        #region Constructor

        public XSpaceGame(Game gameInstance, IPlayer player)
        {
            this.game = gameInstance;
            this.device = gameInstance.GraphicsDevice;
            this.spriteBatch = new SpriteBatch(gameInstance.GraphicsDevice);
            this.levelManager = new LevelManager(gameInstance);
            this.collisionManager = new CollisionManager();
            this.playerInput = new PlayerInput();

            this.player = player;
        }

        #endregion

        #region Enums
        /// <summary>
        /// State engine for the different game states
        /// </summary>
        private enum GameState
        {
            /// <summary>
            /// The game is in the Init state while all initialization takes place
            /// </summary>
            Init,

            /// <summary>
            /// The game is in the Running state when the game is ready to take input from the player
            /// </summary>
            Running
        }

        #endregion

        public void LoadContent()
        {
            this.content = new ContentManager(this.game.Services, "Content");
            this.tileset = this.content.Load<Texture2D>("tiles_jnr");
            this.dude = this.content.Load<Texture2D>("dude2");
            this.spriteFont = this.content.Load<SpriteFont>("gamefont");
        }

        public void UnloadContent()
        {
            this.content.Unload();
        }

        public void Update()
        {
            switch (this.currentGameState)
            {
                case (int)GameState.Init:

                    //this.player = new Player(dude, new Vector2(0, 0));
                    this.player.SpriteTexture = this.dude;
                    this.player.Position = new Vector2();

                    this.levelManager.CreateLevelMap(this.tileset);

                    this.currentGameState = (int)GameState.Running;

                break;

                case (int)GameState.Running:

                    this.UpdatePlayerCollisions();

                    if (this.playerInput.IsInputToProcess)
                    {        
                        if (this.playerInput.Up)
                        {
                            if (this.playerCollision.MoveUpPossible())
                            {
                                this.player.MoveUp();
                                this.UpdatePlayerCollisions();

                                this.player.MoveUp();
                                this.UpdatePlayerCollisions();
                            }
                        }

                        if (this.playerInput.Left)
                        {
                            if (this.playerCollision.MoveLeftPossible())
                            {
                                this.player.MoveLeft();
                                this.UpdatePlayerCollisions();
                            }
                        }

                        if (this.playerInput.Right)
                        {
                            if (this.playerCollision.MoveRightPossible())
                            {
                                this.player.MoveRight();
                                this.UpdatePlayerCollisions();
                            }
                        }                      
                    }

                    //Gravity
                    if (this.playerCollision.MoveDownPossible())
                    {
                        this.player.MoveDown();
                        this.UpdatePlayerCollisions();
                    }

                    this.playerInput.Reset();
                    this.playerCollision.Reset();

                break;
            }
        }

        public void Draw()
        {
            this.device.Clear(ClearOptions.Target, Color.DarkBlue, 0, 0);

            if (this.currentGameState == (int)GameState.Running)
            { 
                this.spriteBatch.Begin();           
                this.spriteBatch.Draw(this.levelManager.GetCreatedLevel().LevelMapTexture, new Vector2(0, 0), Color.White);
                this.spriteBatch.Draw(this.player.SpriteTexture, this.player.Position, Color.White);
                this.spriteBatch.DrawString(this.spriteFont, "Position Player X: " + this.player.Position.X.ToString() + " Y: " + this.player.Position.Y.ToString(), new Vector2(5, 5), Color.White);               
                this.spriteBatch.End();              
            }
        }

        public void HandleInput(KeyboardState keyboardState, GamePadState gamepadState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                this.playerInput.Left = true;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                this.playerInput.Right = true;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                this.playerInput.Up = true;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                this.playerInput.Down = true;
            }

            if (gamepadState.IsButtonDown(Buttons.Start))
            {
                // Do something fancy
            }
        }

        private void UpdatePlayerCollisions()
        {
            this.playerCollision = this.collisionManager.CheckCollisionPlayerWithTerrain(this.player, this.levelManager.GetCreatedLevel());
        }

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
                if (this.content != null)
                {
                    this.content.Dispose();
                    this.content = null;
                }

                if (this.levelManager != null)
                {
                    this.levelManager.Dispose();
                }
            }
        }

        #endregion
    }
}
