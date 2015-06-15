using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CrooksAndCastles.BackgroundObjects.HealthBar;
using CrooksAndCastles.Characters;
using CrooksAndCastles.BackgroundObjects.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Keys = Microsoft.Xna.Framework.Input.Keys;



namespace CrooksAndCastles
{
    public class CrooksAndCastles : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Background background;
        private GameOver gameOver;
        private bool startingCharacter;
        private HealthBar healthBar;
        public const int WindowHeight = 576;
        public const int WindowWidth = 1024; 
        public const int MenuHeight = 50;

        public CrooksAndCastles()
        {
            startingCharacter = true;
            healthBar = new HealthBar();
            Content.RootDirectory = "Content";
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = WindowHeight;
            this.graphics.PreferredBackBufferWidth = WindowWidth;
            Units = new List<Character>();
        }
        
        ////////// PROPERTIS //////////
        internal static List<Character> Units { get; set; }
        public static MainCharacter Hero { get; set; }
        internal static KeyboardState keyBoard { get; set; }
        

        /////////// Internal GAME XNA METHODS ///////////
        protected override void Initialize()
        {
            base.Initialize();
            this.IsMouseVisible = true;
        }
        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            Hero = new MainCharacter(Content, "HeroMoveLeft", "HeroMoveRight", "HeroHitLeft", "HeroHitRight", 150f, 4, true, 2, "HeroMoveDown", "HeroMoveUp");
            Units.Add(Hero);
            this.background = new Background(Content, "BackgroundIMG", new Rectangle(0, MenuHeight, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.gameOver = new GameOver(Content, "GameOver", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            while (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }
            //Control Handle XNA
            Control.FromHandle(Window.Handle).Controls.Add(healthBar.HealBar);
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            keyBoard = Keyboard.GetState();

            //Player Movement
            MovePlayer(gameTime);
            if (this.startingCharacter == true)
            {
                Hero.ChangeAsset(Content, "HeroMoveDown", 1);
                Hero.playCharacterAnimation(gameTime);
            }

            if (Units.Count < 6)
            {
                Enemy enemy = SpawnEnemy();
                Units.Add(enemy);
            }

            for (int index = 1; index < Units.Count; index++)
            {
                Units[index].playCharacterAnimation(gameTime);
            }
            for (int index = 0; index < Units.Count; index++)
            {
                Units[index].Awareness();
            }
            

            //health initialization and update
            healthBar.HealBar.Maximum = Hero.Level * 1000;
            healthBar.ChangeSize(Math.Max(Hero.Level*Hero.Health, 0));

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.GreenYellow);
            this.spriteBatch.Begin();
            if (Hero.IsAlive == false)
            {
                healthBar.HealBar.Visible = false;
                gameOver.Draw(spriteBatch);             
            }
            else
            {
                this.background.Draw(spriteBatch);
                for (int index = 0; index < Units.Count; index++)
                {
                    Units[index].Draw(this.spriteBatch);
                }
                Hero.Draw(spriteBatch);
            }
            
            this.spriteBatch.End();
            base.Draw(gameTime);
        }

        /////////// External GAME XNA METHODS ///////////
        private void MovePlayer(GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Keys.Up))
            {
                Hero.MoveUp();
                Hero.ChangeAsset(Content, "HeroMoveUp", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Right))
            {
                Hero.MoveRight();
                Hero.ChangeAsset(Content, "HeroMoveRight", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Down))
            {
                Hero.MoveDown();
                Hero.ChangeAsset(Content, "HeroMoveDown", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Left))
            {
                Hero.MoveLeft();
                Hero.ChangeAsset(Content, "HeroMoveLeft", 4);
                Units[0].playCharacterAnimation(gameTime);
                this.startingCharacter = false;
            }
            else if (keyBoard.IsKeyDown(Keys.Space))
            {
                Units[0].playCharacterAnimation(gameTime); 
            }
        }
        private Enemy SpawnEnemy()
        {
            return new Enemy(Content, "EnemyOneLeft", "EnemyOneRight", "EnemyOneHitLeft", "EnemyOneHitRight", 150f, 4, true, Enemy.GetRandomPosition(), Hero.Level);
        }
    }
}
