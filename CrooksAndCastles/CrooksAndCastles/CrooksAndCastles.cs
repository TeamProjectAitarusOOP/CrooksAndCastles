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
        private bool startingCharapter = true;

        public CrooksAndCastles()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new MainCharacter(Content, "CharapterBoyRight", 150f, 4, true);
            background = new Background(Content, "BackgroundIMG", new Rectangle(0, 50, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //CONTROLS MAIN CHARAPTER//
            keyBoard = Keyboard.GetState();
            MovePlayer(gameTime);
            if (startingCharapter == true)
            {
                player.ChangeAsset(Content, "CharapterBoyRight", 1);
                player.playCharapterAnimation(gameTime);
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /////////// External GAME XNA METHODS ///////////
        private void MovePlayer(GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Keys.Up))
            {
                player.MoveUp();
                player.ChangeAsset(Content, "CharapterBoyUp", 4);
                player.playCharapterAnimation(gameTime);
                startingCharapter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Right))
            {
                player.MoveRight();
                player.ChangeAsset(Content, "CharapterBoyRight", 4);
                player.playCharapterAnimation(gameTime);
                startingCharapter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Down))
            {
                player.MoveDown();
                player.ChangeAsset(Content, "CharapterBoyDown", 4);
                player.playCharapterAnimation(gameTime);
                startingCharapter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Left))
            {
                player.MoveLeft();
                player.ChangeAsset(Content, "CharapterBoyLeft", 4);
                player.playCharapterAnimation(gameTime);
                startingCharapter = false;
            }
        }

    }
}
