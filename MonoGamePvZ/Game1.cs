using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace PvZ
{
    public class Game1 : Game
    {
        SpriteBatch spriteBatch;

        public static GraphicsDeviceManager graphics;
        public static Game1 ThisGame;
        public static string PlayerName;

        //..........................................
        KeyboardState LastKbState;
        KeyboardState CurrentKbState;
        //..........................................
        public GameMap GameMap;

        Texture2D MainMenuTexture;
        Texture2D WinTexture;
        Texture2D LoseTexture;

        EntryField EntryField;
        public Song MainMenuMusic;
        Song InGameMusic;

        public enum GameState
        {   // Игра находится в каком из состояний
            MainMenu,
            Pause,
            ResultsTable,
            Playing,
        }

        public enum WinOrDefeatGameState
        {
            GameInProgress,
            Win,
            Defeat,
        }

        public static WinOrDefeatGameState WinOrDefeatGameStateVar;

        public GameState GameStateVar;
        public PvZ.ResultsTable.ResultsTable ResultsTableVar;

        Buttons.Buttons ContinueButton;
        Buttons.Buttons ExitGameButton;
        Buttons.Buttons ResultTableButton;
        Buttons.Buttons StartGameButton;
        Buttons.Buttons ToMenuButton;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true; //включаем полноэкранный режим
            graphics.PreferredBackBufferWidth = 1200; //ширина экрана 
            graphics.PreferredBackBufferHeight = 730; //его высота
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ThisGame = this;
            CurrentKbState = Keyboard.GetState();
            LastKbState = CurrentKbState;
            GameStateVar = GameState.MainMenu;
            ResultsTableVar = new ResultsTable.ResultsTable(this); 
            ContinueButton = new Buttons.ContinueButton(this);
            ExitGameButton = new Buttons.ExitGameButton(this);
            ResultTableButton = new Buttons.ResultsTableButton(this);
            StartGameButton = new Buttons.StartGameButton(this);
            ToMenuButton = new Buttons.ExitGameButton(this, true);
            MainMenuMusic = Content.Load<Song>("MainMenuSong");
            InGameMusic = Content.Load<Song>("InGame");
            PlayerName = "";
            EntryField = new EntryField(this);
            MediaPlayer.Play(MainMenuMusic);
        }
        protected override void Initialize()
        {
            base.Initialize();
            GameMap = new GameMap(this);
            WinOrDefeatGameStateVar = WinOrDefeatGameState.GameInProgress;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainMenuTexture = Content.Load<Texture2D>("MainMenu");
            WinTexture = Content.Load<Texture2D>("Win");
            LoseTexture = Content.Load<Texture2D>("Lose");
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            base.Update(gameTime);
            if(GameStateVar == GameState.MainMenu)
            {
                StartGameButton.Update(gameTime);
                if (StartGameButton.GetPressedStatus())
                {
                    // Начинаем игру заново
                    Initialize();
                    GameStateVar = GameState.Playing;
                    MediaPlayer.Play(InGameMusic);
                }
                ExitGameButton.Update(gameTime);
                ResultTableButton.Update(gameTime);
                if (ResultTableButton.GetPressedStatus())
                    GameStateVar = GameState.ResultsTable;
                EntryField.Update(gameTime);
            }
            else if (GameStateVar == GameState.Pause)
            {
                CurrentKbState = Keyboard.GetState();
                if (CurrentKbState.IsKeyDown(Keys.Escape) && LastKbState.IsKeyUp(Keys.Escape))
                {
                    GameStateVar = GameState.Playing;
                    MediaPlayer.Resume();
                }
                ContinueButton.Update(gameTime);
                if(ContinueButton.GetPressedStatus())
                {
                    GameStateVar = GameState.Playing;
                }
                ExitGameButton.Update(gameTime);
                ToMenuButton.Update(gameTime);
                LastKbState = CurrentKbState;
            }
            else if (GameStateVar == GameState.ResultsTable)
            {
                ResultsTableVar.Update(gameTime);
                ExitGameButton.Update(gameTime);
            }
            else if (GameStateVar == GameState.Playing)
            {
                CurrentKbState = Keyboard.GetState();
                if (CurrentKbState.IsKeyDown(Keys.Escape) && LastKbState.IsKeyUp(Keys.Escape))
                {
                    GameStateVar = GameState.Pause;
                    MediaPlayer.Pause();
                }
                if (WinOrDefeatGameStateVar == WinOrDefeatGameState.GameInProgress)
                {
                    GameMap.Update(gameTime);
                }
                LastKbState = CurrentKbState;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (GameStateVar == GameState.MainMenu)
            {
                GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
                spriteBatch.Begin();
                spriteBatch.Draw(MainMenuTexture, new Rectangle(0, 0, (int)graphics.PreferredBackBufferWidth, (int)graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();

                StartGameButton.Draw(gameTime);
                ExitGameButton.Draw(gameTime);
                ResultTableButton.Draw(gameTime);
                EntryField.Draw(gameTime);
            }
            else if (GameStateVar == GameState.Pause)
            {
                ExitGameButton.Draw(gameTime);
                ContinueButton.Draw(gameTime);
                ToMenuButton.Draw(gameTime);
            }
            else if (GameStateVar == GameState.ResultsTable)
            {
                GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
                spriteBatch.Begin();
                spriteBatch.Draw(MainMenuTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                spriteBatch.End();
                ExitGameButton.Draw(gameTime);
                ResultsTableVar.Draw(gameTime);
            }
            else if (GameStateVar == GameState.Playing)
            {
                GameMap.Draw(gameTime);
                if(WinOrDefeatGameStateVar == WinOrDefeatGameState.Win)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(WinTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.End();
                }
                else if (WinOrDefeatGameStateVar == WinOrDefeatGameState.Defeat)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(LoseTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.End();
                }
            }            
        }

        public void SetCurrentGameState(GameState nGameState)
        {
            GameStateVar = nGameState;
        }
    }
}
