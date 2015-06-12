using System;
using System.Collections.Generic;
using System.Linq;
using CrooksAndCastles.Characters;
using CrooksAndCastles.BackgroundObjects.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace CrooksAndCastles
{
    public class CrooksAndCastles : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private MainCharacter player;
        private Background background;
        private KeyboardState keyBoard;
        private bool startingCharacter = true;
        public const int WindowHeight = 576;
        public const int WindowWidth = 1024; 

        public CrooksAndCastles()
        {
            Content.RootDirectory = "Content";
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = WindowHeight;
            this.graphics.PreferredBackBufferWidth = WindowWidth;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.player = new MainCharacter(Content, "CharacterBoyRight", 150f, 4, true);
            this.background = new Background(Content, "BackgroundIMG", new Rectangle(0, 50, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //CONTROLS MAIN CHARAPTER//
            this.keyBoard = Keyboard.GetState();
            MovePlayer(gameTime);
            if (this.startingCharacter == true)
            {
                this.player.ChangeAsset(Content, "CharacterBoyRight", 1);
                this.player.playCharapterAnimation(gameTime);
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            this.spriteBatch.Begin();
            this.background.Draw(spriteBatch);
            this.player.Draw(spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /////////// External GAME XNA METHODS ///////////
        private void MovePlayer(GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Keys.Up))
            {
                this.player.MoveUp();
                this.player.ChangeAsset(Content, "CharacterBoyUp", 4);
                this.player.playCharapterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Right))
            {
                this.player.MoveRight();
                this.player.ChangeAsset(Content, "CharacterBoyRight", 4);
                this.player.playCharapterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Down))
            {
                this.player.MoveDown();
                this.player.ChangeAsset(Content, "CharacterBoyDown", 4);
                this.player.playCharapterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Left))
            {
                this.player.MoveLeft();
                this.player.ChangeAsset(Content, "CharacterBoyLeft", 4);
                this.player.playCharapterAnimation(gameTime);
                this.startingCharacter = false;
            }
        }

    }
}
