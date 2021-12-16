using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace Strategy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //.................Меню игры................
        public enum GameState
        {   // Игра находится в каком из состояний
            MainMenu,
            InsertPlayerName,
            Pause,
            ResultsTable,
            Playing,
        }
        GameState CurrentGameState = GameState.MainMenu;
        EntryField EntryField;
        StartTheGameButton StartTheGameButton; // Кнопка старта игры
        ExitGameButton ExitGameButton; // Кнопка выхода
        ResultsTable ResultsTableButton; // Кнопка таблицы результатов
        //..........................................
        KeyboardState LastKbState;
        KeyboardState CurrentKbState;
        //..........................................Пауза
        ContinueButton ContinueButton;
        ExitGameButton ToMenuGameButton;
        LevelConditions LevelConditions;
        Song MainMenuMusic;
        Song InGameMusic;
        SoundEffect WinEffect;
        SoundEffect LoseEffect;
        Texture2D MainMenuImage;
        Texture2D End;

        TheBestOf_Table Table;
        
        // Игровые параметры
        public static Game1 ThisGame;
        public static GraphicsDeviceManager graphics;
        public static LowLeftMenu LLMenu;
        public static MouseState mouse; // Состояние мыши
        public static int Sciense; // Накопленные за хода очки науки
        public static int Gold; // Накопленные за хода золото
        public static int Turn;
        public static string PlayerName;
        public static int Level;
        public static bool GameInProgress;

        SpriteBatch spriteBatch;
        
        TechnologyTree TechTree; // Дерево технологий
        public UpStatisticsMenu StatMenu;  // Статистическое меню        
        EndTurnButton EndTurnButton; // Кнопка конца хода

        // Карта
        Map GameMap; // Карта игры
        Int32 MapSizeX, MapSizeY;

        // Сетка
        //GridMapButton GridMapButton;
        //Texture2D GridTexture;

        public GameState GetCurrentGameState()
        {
            return CurrentGameState;
        }

        public GameState SetCurrentGameState(GameState nGameState)
        {
            return CurrentGameState = nGameState;
        }
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true; //включаем полноэкранный режим
            graphics.PreferredBackBufferWidth = 1200; //ширина экрана 
            graphics.PreferredBackBufferHeight = 730; //его высота
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            ThisGame = this;
            IsMouseVisible = true;
            GameInProgress = false;
            MapSizeX = 20;
            MapSizeY = 13;
            Level = 1;
            CurrentKbState = Keyboard.GetState();
            LastKbState = CurrentKbState;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            EndTurnButton = new EndTurnButton(this);
            //GridMapButton = new GridMapButton(this);
            ExitGameButton = new ExitGameButton(this);
            StartTheGameButton = new StartTheGameButton(this);
            ResultsTableButton = new ResultsTable(this);
            ContinueButton = new ContinueButton(this);
            ToMenuGameButton = new ExitGameButton(this, true);
            EntryField = new EntryField(this);
            Table = new TheBestOf_Table(this);
            MainMenuMusic = Content.Load<Song>("InMenu");
            InGameMusic = Content.Load<Song>("InGame");
            WinEffect = Content.Load<SoundEffect>("Win");
            LoseEffect = Content.Load<SoundEffect>("Lose");
            MainMenuImage = Content.Load<Texture2D>("Меню");
            MediaPlayer.Play(MainMenuMusic);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //GridTexture = Content.Load<Texture2D>("Сетка");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();
            switch(CurrentGameState)
            {
                case GameState.MainMenu:
                {
                    MediaPlayer.Volume = 0.5f;
                    StartTheGameButton.Update(gameTime);
                    if (StartTheGameButton.GetPressedStatus())
                    {
                        // Начинаем игру заново
                        //  Строим карту игры
                        GameMap = new Map(this, MapSizeX, MapSizeY);
                        GameMap.Initialize();
                        //  Строим дерево технологий
                        TechTree = new TechnologyTree(this);
                        //  Выводим верхнее статистическое меню
                        StatMenu = new UpStatisticsMenu(this);
                        LLMenu = new LowLeftMenu(this);
                        Sciense = 0;
                        Gold = 0;
                        Turn = 0;
                        Level = 1;
                        GameInProgress = true;
                        LevelConditions = new LevelConditions(this, Level, Turn);
                        CurrentGameState = GameState.Playing;
                        MediaPlayer.Play(InGameMusic);
                    }
                    ExitGameButton.Update(gameTime);
                    ResultsTableButton.Update(gameTime);
                    if (ResultsTableButton.GetPressedStatus())
                        CurrentGameState = GameState.ResultsTable;
                    EntryField.Update(gameTime);
                    break;
                }
                case GameState.Pause:
                {
                    
                    CurrentKbState = Keyboard.GetState();
                    if (CurrentKbState.IsKeyDown(Keys.Escape) && LastKbState.IsKeyUp(Keys.Escape))
                    {
                        CurrentGameState = GameState.Playing;
                        MediaPlayer.Resume();
                    }                        
                    ExitGameButton.Update(gameTime);
                    ContinueButton.Update(gameTime);
                    if (ContinueButton.GetPressedStatus())
                    {
                        CurrentGameState = GameState.Playing;
                        MediaPlayer.Resume();
                    }
                    ToMenuGameButton.Update(gameTime);
                    if (ToMenuGameButton.GetPressedStatus())
                    {
                        CurrentGameState = GameState.MainMenu;
                        MediaPlayer.Play(MainMenuMusic);
                    }
                    LastKbState = CurrentKbState;
                    break;
                }
                case GameState.ResultsTable:
                {
                    ExitGameButton.Update(gameTime);
                    Table.Update(gameTime);
                    break;
                }
                case GameState.Playing:
                {
                    // TODO: Add your update logic here
                    CurrentKbState = Keyboard.GetState();
                    if (CurrentKbState.IsKeyDown(Keys.Escape) && LastKbState.IsKeyUp(Keys.Escape))
                    {
                        CurrentGameState = GameState.Pause;
                        MediaPlayer.Pause();
                    }
                    GameMap.Update(gameTime);
                    TechTree.Update(gameTime);
                    StatMenu.Update(gameTime);
                    LLMenu.Update(gameTime);
                    EndTurnButton.Update(gameTime);
                    //GridMapButton.Update(gameTime);
                    LevelConditions.Update(gameTime);
                    if (GameInProgress && !LevelConditions.GetActiveState() && !LevelConditions.GetConditionState())
                    {
                        Level++;
                        if(Level == 3)
                        {
                            // Выигрыш
                            MediaPlayer.Stop();
                            WinEffect.Play();
                            String Time = StatMenu.GameTimer.GetText();
                            String[] Win = { PlayerName, "Победа", Time };
                            Table.AddNewRecord(Win);
                            GameInProgress = false;
                            End = Content.Load<Texture2D>("Победа");
                        }
                        else
                        {
                            // Заменяем всех рабочих на модернизированных
                            GameMap.ModifyAllWorkers();
                            GameMap.GenerateOil();
                            LevelConditions.GoToNewLevel(Level, Turn);
                        }                            
                    }
                    if (GameInProgress && !LevelConditions.GetActiveState() && LevelConditions.GetConditionState())
                    {
                        // Проигрыш
                        MediaPlayer.Stop();
                        LoseEffect.Play();
                        String Time = StatMenu.GameTimer.GetText();
                        String[] Lose = { PlayerName, "Поражение", Time };
                        Table.AddNewRecord(Lose);
                        GameInProgress = false;
                        End = Content.Load<Texture2D>("Поражение");
                    }
                    // Если нажата кнопка конца хода
                    if (EndTurnButton.GetPressedStatus())
                    {   // Начинаем (пока что) только пресчет ходов юнитов
                        Turn++;
                        GameMap.NewTurn();
                    }
                    LastKbState = CurrentKbState;
                    break;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {          
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                {
                    GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
                    spriteBatch.Begin();
                    spriteBatch.Draw(MainMenuImage, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.End();
                    StartTheGameButton.Draw(gameTime);
                    ExitGameButton.Draw(gameTime);
                    ResultsTableButton.Draw(gameTime);
                    EntryField.Draw(gameTime);
                    break;
                }
                case GameState.Pause:
                {
                    ExitGameButton.Draw(gameTime);
                    ContinueButton.Draw(gameTime);
                    ToMenuGameButton.Draw(gameTime);
                    break;
                }
                case GameState.ResultsTable:
                {
                    GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
                    spriteBatch.Begin();
                    spriteBatch.Draw(MainMenuImage, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
                    spriteBatch.End();
                    ExitGameButton.Draw(gameTime);
                    Table.Draw(gameTime);
                    break;
                }
                case GameState.Playing:
                {
                    GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
                    // TODO: Add your drawing code here
                    spriteBatch.Begin();
                    GameMap.Draw(gameTime);
                    StatMenu.Draw(gameTime);
                    LLMenu.Draw(gameTime);
                    EndTurnButton.Draw(gameTime);
                    //GridMapButton.Draw(gameTime);
                    // Если нужно отрисовывать сетку
                    //if (GridMapButton.GetPressedStatus())
                    //    for (int i = 0; i <= MapSizeX; i++)
                    //        for (int j = 0; j <= MapSizeY; j++)
                    //            spriteBatch.Draw(GridTexture, new Rectangle(60 * i, 60 * j, 60, 60),
                    //                new Rectangle((int)0, (int)0, 57, 57), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.99f);
                    if (!GameInProgress)
                        spriteBatch.Draw(End, new Rectangle(graphics.PreferredBackBufferWidth / 3, graphics.PreferredBackBufferHeight / 3, End.Width, End.Height), Color.White);

                    TechTree.Draw(gameTime);
                    spriteBatch.End();
                    break;
                }
            }
            base.Draw(gameTime);
        }

        public Map GetMap()
        {
            return GameMap;
        }

        public TechnologyTree GetTechTree()
        {
            return TechTree;
        }
    }
}
