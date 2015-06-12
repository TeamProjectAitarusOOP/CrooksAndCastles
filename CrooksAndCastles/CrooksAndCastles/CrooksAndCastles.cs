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
        private SpriteBatch spriteBatch;
        private Background background;
        private KeyboardState keyBoard;
        private bool startingCharacter = true;
        public const int WindowHeight = 576;
        public const int WindowWidth = 1024; 
        public const int MenuHeight = 50;
        public Enemy enemy;
        

        public CrooksAndCastles()
        {
            Content.RootDirectory = "Content";
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferHeight = WindowHeight;
            Graphics.PreferredBackBufferWidth = WindowWidth;
        }

        public static MainCharacter Hero { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new MainCharacter(Content, "CharacterBoyRight", 150f, 4, true);
            this.background = new Background(Content, "BackgroundIMG", new Rectangle(0, MenuHeight, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight));
            enemy = new Enemy(Content, "CharacterBoyRight", 150f, 4, true, 20, 40);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //CONTROLS MAIN CHARAPTER//
            this.keyBoard = Keyboard.GetState();
            enemy.playCharacterAnimation(gameTime);
            enemy.Awareness();
            MovePlayer(gameTime);
            if (this.startingCharacter == true)
            {
                Hero.ChangeAsset(Content, "CharacterBoyRight", 1);
                Hero.playCharacterAnimation(gameTime);
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            this.spriteBatch.Begin();
            this.background.Draw(spriteBatch);
            Hero.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /////////// External GAME XNA METHODS ///////////
        private void MovePlayer(GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Keys.Up))
            {
                Hero.MoveUp();
                Hero.ChangeAsset(Content, "CharacterBoyUp", 4);
                Hero.playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Right))
            {
                Hero.MoveRight();
                Hero.ChangeAsset(Content, "CharacterBoyRight", 4);
                Hero.playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Down))
            {
                Hero.MoveDown();
                Hero.ChangeAsset(Content, "CharacterBoyDown", 4);
                Hero.playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Left))
            {
                Hero.MoveLeft();
                Hero.ChangeAsset(Content, "CharacterBoyLeft", 4);
                Hero.playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
        }

    }
}
