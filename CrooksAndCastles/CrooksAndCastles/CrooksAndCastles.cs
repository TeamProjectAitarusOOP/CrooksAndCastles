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
        private KeyboardState keyBoard;
        private List<Character> units = new List<Character>();
        private bool startingCharacter = true;
        private const float SpawnDistanceBetweenUnits = Enemy.EnemyChaseDistance + 10;
        HealthBar healthBar = new HealthBar();
        public const int WindowHeight = 576;
        public const int WindowWidth = 1024; 
        public const int MenuHeight = 50;

        

        public CrooksAndCastles()
        {
            Content.RootDirectory = "Content";
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = WindowHeight;
            this.graphics.PreferredBackBufferWidth = WindowWidth;
        }
        
        ////////// PROPERTIS //////////
        public static MainCharacter Hero { get; set; }

        /////////// Internal GAME XNA METHODS ///////////
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
            units.Add(Hero);
            this.background = new Background(Content, "BackgroundIMG", new Rectangle(0, MenuHeight, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));
            this.gameOver = new GameOver(Content, "GameOver", new Rectangle(0, 0, this.graphics.PreferredBackBufferWidth, this.graphics.PreferredBackBufferHeight));

            //Control Handle XNA
            Control.FromHandle(Window.Handle).Controls.Add(healthBar.HealBar);
            
            //TODO ENEMY SPOWNS
            units.Add(new Enemy(Content, "EnemyOneRight", 150f, 4, true, RandomCoordinates()));
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            this.keyBoard = Keyboard.GetState();
            foreach (var unit in this.units)
            {
                if (unit is Enemy)
                {
                    Enemy enemy = unit as Enemy;
                    enemy.playCharacterAnimation(gameTime);
                    enemy.Awareness();
                }
            }
            //Player Movement
            MovePlayer(gameTime);
            if (this.startingCharacter == true)
            {
                Hero.ChangeAsset(Content, "CharacterBoyRight", 1);
                Hero.playCharacterAnimation(gameTime);
            }

            //health initialization and update
            healthBar.ChangeSize(Hero.Health);

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
                Hero.Draw(spriteBatch);
                for (int index = 1; index < units.Count; index++)
                {
                    units[index].Draw(this.spriteBatch);
                }
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
        private void SpawnEnemy()
        {
           //TODO ENEMY SPOWN
        }
        private Vector2 RandomCoordinates()
        {
            Random rand = new Random();
            float randomX = rand.Next(0, WindowWidth - MenuHeight);
            float randomY = rand.Next(MenuHeight, WindowHeight - MenuHeight);
            return new Vector2(randomX, randomY);
        }
    }
}
