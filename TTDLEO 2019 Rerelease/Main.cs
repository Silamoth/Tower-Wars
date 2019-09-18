// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Main
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TTDLEO_2019_Rerelease
{
    public class Main : Game
    {
        public static int buttonTimer = 0;
        public static bool incrementButtonTimer = false;
        public static bool canClick = true;
        public static Random random = new Random();
        public static float volume = 1f;
        public static int gold = 130;
        private bool canPause = true;
        private bool incrementPauseTimer = false;
        private int pauseTimer = 0;
        private int levelUnlocked = 1;
        private int currentLevel = 0;
        private bool canSwitchMenus = true;
        private bool incrementMenuTimer = false;
        private int menuTimer = 0;
        private int forwardTimer = 0;
        private bool canMoveForward = false;
        private bool incrementForwardTimer = true;
        private List<string> openingText = new List<string>();
        private bool showLore = false;
        private int gambleTimer = 0;
        private bool canGamble = true;
        private bool incrementGambleTimer = false;
        private Vector2 baseScreenSize = new Vector2(800f, 480f);
        private int updatesPerFrame = 1;
        private const int GamblingTime = 150;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private Texture2D buttonTexture;
        private Rectangle mouseRectangle;
        private SpriteFont font;
        private List<Button> levelButtons;
        private int startTime;
        private double timer;
        private Button upgradeButton;
        
        private bool hasSeenOpeningSequence;
        private Texture2D background;

        public static Rectangle hoverRectangle;
        public static string hoverTextLineOne;
        public static string hoverTextLineTwo;
        public static string hoverTextLineThree;

        private Texture2D hoverButton;
        private SpriteFont largeFont;
        private Button returnHomeButton;
        private States state;
        
        private Song song;
        private Texture2D helperTexture;
        private string helperText;
        private Texture2D textBar;
        private Button helpButton;
        private Texture2D menuTexture;
        private DefendingArcher archer;
        private List<AttackingEnemy> attackingEnemies;
        public static ParticleManager particleManager;
        private Texture2D defenseTexture;
        private bool showPopUp;
        private bool didWin;
        public static int goldSpent;
        public static int goldGained;
        public static int troopsLost;
        public static int enemiesKilled;
        private Texture2D popUpTexture;
        private Button replayButton;
        private Button menuButton;
        private Button nextButton;
        private SpriteFont mediumFont;
        private Button loreButton;
        private List<string> lorePieces;
        private List<Button> unlockedLoreButtons;
        private List<Button> lockedLoreButtons;
        private string currentLore;
        private Texture2D loreShower;
        private List<string> allLorePieces;
        private List<string> loreNames;
        private List<string> unlockedItems;
        
        private List<string> storyTexts;
        private string currentStoryText;
        
        private Button gambleMenuButton;
        private Button gambleButton;
        private Button rollButton;
        private int gamblingAmount;
        private double gambleChance;
        private Texture2D gambleTexture;
        private string gamblingResult;
        private float scaleX;
        private float scaleY;
        private Vector2 mousePosition;
        private Texture2D gamblingBackground;
        private Texture2D openingBackground;
        private Button mainArcheryButton;
        private Button exitButton;
        private bool hasDoneTutorial;
        private int tutorialCounter;
        private SpriteFont midLargeFont;
        private Button menuOptionsButton;
        private Button speedUpButton;
        private Button slowDownButton;
        
        private Texture2D otherMenuBackground;

        //New Stuff:
        OptionMenu optionMenu;
        FinalBattle finalBattle;
        BattleManager battleManager;
        Shop shop;
        Tutorial tutorial;

        int timerSeconds;

        public Main()
        {
            graphics = new GraphicsDeviceManager((Game)this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            Window.Title = "Towers That Don't Like Each Other";
            
            buttonTexture = Content.Load<Texture2D>("birchButton");
            levelButtons = new List<Button>();
            returnHomeButton = new Button(new Rectangle(25, 35, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
            
            helpButton = new Button(new Rectangle(13, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Help");
            MediaPlayer.IsRepeating = true;
            particleManager = new ParticleManager(Content);
            loreButton = new Button(new Rectangle(285, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Lore");
            lorePieces = new List<string>();
            unlockedLoreButtons = new List<Button>();
            lockedLoreButtons = new List<Button>();
            allLorePieces = new List<string>();
            loreNames = new List<string>();
            unlockedItems = new List<string>();
            upgradeButton = new Button(new Rectangle(545, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Upgrade");
            gambleMenuButton = new Button(new Rectangle(420, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Gamble");
            mainArcheryButton = new Button(new Rectangle(150, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Archery");
            menuOptionsButton = new Button(new Rectangle(15, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Options");
            exitButton = new Button(new Rectangle(680, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Exit");

            volume = MediaPlayer.Volume;

            battleManager = new BattleManager(Content, buttonTexture, 1);

            LoadData();
            if (!hasSeenOpeningSequence)
            {
                state = States.OPENING;
                LoadOpening();
            }
            else if (!hasDoneTutorial)
            {
                background = Content.Load<Texture2D>("background");
                
                battleManager = new BattleManager(Content, buttonTexture, -1);
                
                replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Replay Level");
                menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
                nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Next Level");
                state = States.TUTORIAL;
                PlayMainGameMusic();

                textBar = Content.Load<Texture2D>("textBar");
                popUpTexture = Content.Load<Texture2D>("popUp");

                tutorial = new Tutorial(Content, buttonTexture, background, textBar, popUpTexture);
            }
            else
                state = States.MENU;

            LoadRectangles();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadStuffForEverythingAndMenu();
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < updatesPerFrame; i++)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds - (double)startTime;
                while (timer > 1.0)
                {
                    --timer;
                    ++timerSeconds;
                }
            }

            if (!IsActive)
                return;
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) && ((this.state == States.TOWERS || this.state == States.PAUSED) && canPause))
            {
                if (this.state == States.PAUSED)
                {
                    this.state = States.TOWERS;
                    MediaPlayer.Resume();
                    
                }
                else if (this.state == States.TOWERS)
                {
                    this.state = States.PAUSED;
                    MediaPlayer.Pause();
                    
                }
                canPause = false;
                pauseTimer = 0;
                incrementPauseTimer = true;
            }
            if (incrementPauseTimer)
                ++pauseTimer;
            if (pauseTimer == 15)
            {
                canPause = true;
                incrementPauseTimer = false;
            }
            MouseState state = Mouse.GetState();
            double x = (double)state.X;
            state = Mouse.GetState();
            double y = (double)state.Y;
            mousePosition = new Vector2((float)x, (float)y);

            Matrix transform = Matrix.CreateScale((float)Window.ClientBounds.Width / baseScreenSize.X, (float)Window.ClientBounds.Height / baseScreenSize.Y, 1.0f);
            mousePosition = Vector2.Transform(mousePosition, Matrix.Invert(transform));

            mouseRectangle = new Rectangle((int)(mousePosition.X), (int)(mousePosition.Y), 15, 9);
            scaleX = (float)Window.ClientBounds.Width / baseScreenSize.X;
            scaleY = (float)Window.ClientBounds.Height / baseScreenSize.Y;

            hoverRectangle = Rectangle.Empty;
            if (this.state == States.TOWERS)
            {
                for (int index = 0; index < updatesPerFrame; ++index)
                {
                    battleManager.Update(Content, timerSeconds, gameTime, mouseRectangle,
                        scaleX, scaleY, Window.ClientBounds.Width, Window.ClientBounds.Height, updatesPerFrame, startTime);

                    if (!showPopUp)
                    {
                        if (battleManager.CanBuy)
                            CheckForBuying();
                        CheckForEndGame();
                    }
                    else
                        UpdatePopUp(mouseRectangle);
                    PlayMainGameMusic();
                }
            }
            else if (this.state == States.MENU)
                UpdateMenu();
            else if (this.state == States.UPGRADING)
                UpdateUpgrades();
            else if (this.state == States.OPENING)
                UpdateOpening(Keyboard.GetState());
            else if (this.state == States.ARCHERY)
                UpdateArchery(gameTime);
            else if (this.state == States.LORE)
                UpdateLore();
            else if (this.state == States.POSTLEVEL)
                UpdateStory(Keyboard.GetState());
            else if (this.state == States.GAMBLING)
                UpdateInvesting();
            else if (this.state == States.TUTORIAL)
                tutorial.Update(gameTime, Content, mouseRectangle, scaleX, scaleY,
                    Window.ClientBounds.Width, Window.ClientBounds.Height, startTime);
            else if (this.state == States.OPTIONS)
                UpdateOptions();
            else if (this.state == States.FINALBOSS)
                UpdateFinalBattle(gameTime);
            else if (this.state == States.CREDITS)
                UpdateCredits();
            if (this.state == States.MENU || this.state == States.UPGRADING || (this.state == States.HUNTING || this.state == States.MINIGAMEMENU) || (this.state == States.SMITHING || this.state == States.RUNNING || (this.state == States.LORE || this.state == States.GAMBLING)) || this.state == States.OPTIONS || this.state == States.CREDITS)
            {
                if (incrementMenuTimer)
                    ++menuTimer;
                if (menuTimer == 20)
                {
                    incrementMenuTimer = false;
                    canSwitchMenus = true;
                }
            }
            if (this.state == States.TOWERS || this.state == States.UPGRADING || this.state == States.OPTIONS || this.state == States.TUTORIAL)
            {
                if (incrementButtonTimer)
                    ++buttonTimer;
                if (buttonTimer == 40)
                {
                    incrementButtonTimer = false;
                    canClick = true;
                    buttonTimer = 0;
                }
            }
            
            particleManager.Update(gameTime);
            if (incrementGambleTimer)
            {
                ++gambleTimer;
                if (gambleTimer == 150)
                {
                    canGamble = true;
                    incrementGambleTimer = false;
                    gambleTimer = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Purple);

            Matrix transform = Matrix.CreateScale((float)Window.ClientBounds.Width / baseScreenSize.X, (float)Window.ClientBounds.Height / baseScreenSize.Y, 1.0f);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, transform);
            if (state == States.TOWERS || state == States.PAUSED)
            {
                spriteBatch.Draw(background, new Rectangle(0, -20, (int)(background.Width), (int)(background.Height)), Color.White);

                //int num1 = 0;
                //while (num1 < healths.Count)
                //    ++num1;

                battleManager.Draw(spriteBatch, mouseRectangle, scaleX, scaleY);

                spriteBatch.DrawString(font, "Gold: " + gold.ToString(), new Vector2(29f, 15f), Color.Black);
                spriteBatch.DrawString(font, "Level: " + currentLevel.ToString(), new Vector2(153f, 15f), Color.Black);
                speedUpButton.Draw(spriteBatch);
                slowDownButton.Draw(spriteBatch);
                if (showPopUp)
                {
                    spriteBatch.Draw(popUpTexture, new Vector2(0.0f, 0.0f), Color.White);
                    if (didWin)
                        spriteBatch.DrawString(mediumFont, "Congratulations!  You Won The Battle!", new Vector2(220f, 125f), Color.Black);
                    else
                        spriteBatch.DrawString(font, "You Lost The Battle...", new Vector2(270f, 125f), Color.Black);
                    spriteBatch.DrawString(font, "Items gained:", new Vector2(445f, 175f), Color.Black);
                    spriteBatch.DrawString(font, "Gold gained: " + goldGained.ToString() + " gold", new Vector2(215f, 173f), Color.Black);
                    spriteBatch.DrawString(font, "Gold spent: " + goldSpent.ToString() + " gold", new Vector2(215f, 198f), Color.Black);
                    spriteBatch.DrawString(font, "Troops lost: " + troopsLost.ToString(), new Vector2(215f, 225f), Color.Black);
                    spriteBatch.DrawString(font, "Enemies killed: " + enemiesKilled.ToString(), new Vector2(215f, 248f), Color.Black);
                    int num2 = 200;
                    foreach (string unlockedItem in unlockedItems)
                    {
                        spriteBatch.DrawString(font, "Lore: " + unlockedItem, new Vector2(405f, (float)num2), Color.Black);
                        num2 += 25;
                    }
                    replayButton.Draw(spriteBatch);
                    menuButton.Draw(spriteBatch);
                    if (didWin && currentLevel != 21)
                    {
                        nextButton.Draw(spriteBatch);
                    }
                }
            }
            else if (state == States.MENU || state == States.HELPMENU)
            {
                spriteBatch.Draw(menuTexture, new Rectangle(0, -20, (int)(menuTexture.Width), (int)(menuTexture.Height)), Color.White);
                spriteBatch.DrawString(midLargeFont, "TOWERS THAT DON'T LIKE EACH OTHER", new Vector2(260, 20), Color.White);
                int num = 1;
                foreach (Button levelButton in levelButtons)
                {
                    levelButton.Draw(spriteBatch);
                    num++;
                }

                spriteBatch.DrawString(font, "Gold: " + gold.ToString(), new Vector2(10f, 40f), Color.Black);
                upgradeButton.Draw(spriteBatch);
                loreButton.Draw(spriteBatch);
                gambleMenuButton.Draw(spriteBatch);
                mainArcheryButton.Draw(spriteBatch);
                menuOptionsButton.Draw(spriteBatch);

                exitButton.Draw(spriteBatch);
            }
            else if (state == States.UPGRADING)
            {
                spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);
                spriteBatch.DrawString(largeFont, "SHOP", new Vector2(310f, 10f), Color.Black);
                upgradeButton.Draw(spriteBatch);

                spriteBatch.DrawString(font, "Gold: " + gold.ToString(), new Vector2(10f, 40f), Color.Black);
                shop.Draw(spriteBatch);
            }
            else if (state == States.OPENING)
            {
                spriteBatch.Draw(openingBackground, new Vector2(0.0f, -20f), Color.White);
                spriteBatch.Draw(helperTexture, new Vector2(125f, (float)byte.MaxValue), Color.White);
                spriteBatch.Draw(textBar, new Vector2(50f, 380f), Color.White * 0.75f);
                spriteBatch.DrawString(font, "Press any button to continue...", new Vector2(300f, 355f), Color.White);
                if (helperText.Length > 95)
                {
                    int num = helperText.Length / 95 + 1;
                    for (int index = 0; index < num; ++index)
                        spriteBatch.DrawString(font, helperText.Substring(95 * index).Length <= 95 ? helperText.Substring(95 * index) : helperText.Substring(95 * index, 95), new Vector2(60f, (float)(390 + index * 25)), Color.Black);
                }
                else
                    spriteBatch.DrawString(font, helperText, new Vector2(60f, 390f), Color.Black);
            }
            else if (state == States.ARCHERY)
                DrawArchery(spriteBatch);
            else if (state == States.LORE)
                DrawLore(spriteBatch);
            else if (state == States.POSTLEVEL)
                DrawStory(spriteBatch);
            else if (state == States.GAMBLING)
                DrawInvesting(spriteBatch);
            else if (state == States.TUTORIAL)
                tutorial.Draw(spriteBatch, mouseRectangle, scaleX, scaleY);
            else if (state == States.OPTIONS)
                DrawOptions();
            else if (state == States.FINALBOSS)
                DrawFinalBattle();
            else if (state == States.CREDITS)
                DrawCredits();

            if (hoverRectangle != Rectangle.Empty)
            {
                if (hoverRectangle.X > 600 / scaleX)
                {
                    spriteBatch.Draw(hoverButton, new Rectangle((int)((hoverRectangle.X * scaleX - 90)), (int)(hoverRectangle.Y * scaleY), hoverRectangle.Width, hoverRectangle.Height), Color.White * 0.8f);
                    spriteBatch.DrawString(font, hoverTextLineOne, new Vector2((float)(mousePosition.X - 90 + 6), (float)(mousePosition.Y - hoverRectangle.Height + 30)), Color.Black);
                    spriteBatch.DrawString(font, hoverTextLineTwo, new Vector2((float)(mousePosition.X - 90 + 6), (float)(mousePosition.Y - hoverRectangle.Height + 50)), Color.Black);
                    spriteBatch.DrawString(font, hoverTextLineThree, new Vector2((float)(mousePosition.X - 90 + 6), (float)(mousePosition.Y - hoverRectangle.Height + 70)), Color.Black);
                }
                else
                {
                    spriteBatch.Draw(hoverButton, new Rectangle((int)(hoverRectangle.X * scaleX), (int)(hoverRectangle.Y * scaleY), hoverRectangle.Width, hoverRectangle.Height), Color.White * 0.8f);
                    spriteBatch.DrawString(font, hoverTextLineOne, new Vector2((float)(mousePosition.X + 6), (float)(mousePosition.Y - hoverRectangle.Height + 30)), Color.Black);
                    spriteBatch.DrawString(font, hoverTextLineTwo, new Vector2((float)(mousePosition.X + 6), (float)(mousePosition.Y - hoverRectangle.Height + 50)), Color.Black);
                    spriteBatch.DrawString(font, hoverTextLineThree, new Vector2((float)(mousePosition.X + 6), (float)(mousePosition.Y - hoverRectangle.Height + 70)), Color.Black);
                }
            }

            particleManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            if (state != States.TOWERS && state != States.PAUSED || currentLevel != levelUnlocked)
                ;

            List<String> otherData = battleManager.GetSaveInfo();

            StreamWriter streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\load.txt");
            streamWriter.WriteLine(hasSeenOpeningSequence.ToString());
            streamWriter.WriteLine(hasDoneTutorial.ToString());
            streamWriter.WriteLine(levelUnlocked.ToString());
            streamWriter.WriteLine(otherData[0]);
            streamWriter.WriteLine(gold.ToString());
            streamWriter.WriteLine(otherData[1]);
            streamWriter.WriteLine(otherData[2]);
            streamWriter.WriteLine(otherData[3]);
            streamWriter.WriteLine(otherData[4]);
            streamWriter.WriteLine(volume.ToString());
            streamWriter.WriteLine(graphics.IsFullScreen.ToString());
            streamWriter.Close();
            streamWriter = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\lore.txt");
            foreach (string lorePiece in lorePieces)
                streamWriter.WriteLine(lorePiece);
            streamWriter.Close();
        }

        private void LoadRectangles()
        {
            levelButtons = new List<Button>();
            int num = 0;
            for (int y = 75; y < 450; y += 50)
            {
                for (int x = 13; x < 750; x += 125)
                {
                    if (num < levelUnlocked)
                    {
                        String name = "Level " + (num + 1).ToString();

                        if (num == 20)
                        {
                            name = "Final Boss";
                        }

                        levelButtons.Add(new Button(new Rectangle(x, y, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, name));
                        num++;
                    }
                }
            }
        }

        private void LoadData()
        {
            StreamReader streamReader;

            string path1 = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\load.txt";
            if (File.Exists(path1))
            {
                streamReader = new StreamReader(path1);
                int playerTowerHealth;
                bool.TryParse(streamReader.ReadLine(), out hasSeenOpeningSequence);
                bool.TryParse(streamReader.ReadLine(), out hasDoneTutorial);
                int.TryParse(streamReader.ReadLine(), out levelUnlocked);
                int.TryParse(streamReader.ReadLine(), out playerTowerHealth);
                int.TryParse(streamReader.ReadLine(), out gold);

                bool hasTurretOne, hasTurretTwo, hasWizardTower, hasArcher;
                
                bool.TryParse(streamReader.ReadLine(), out hasTurretOne);
                bool.TryParse(streamReader.ReadLine(), out hasTurretTwo);
                bool.TryParse(streamReader.ReadLine(), out hasWizardTower);
                bool.TryParse(streamReader.ReadLine(), out hasArcher);
                float.TryParse(streamReader.ReadLine(), out volume);

                bool isFullScreen;
                bool.TryParse(streamReader.ReadLine(), out isFullScreen);
                graphics.IsFullScreen = isFullScreen;
                graphics.ApplyChanges();
                streamReader.Close();

                battleManager.SetSaveData(playerTowerHealth, hasTurretOne, hasTurretTwo, hasWizardTower, hasArcher);
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter(path1);
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("1");
                streamWriter.WriteLine("100");
                streamWriter.WriteLine("130");
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("False");
                streamWriter.WriteLine("1");
                streamWriter.WriteLine("False");
                streamWriter.Close();
            }

            streamReader = new StreamReader(Environment.CurrentDirectory + "/loreNames.txt");
            while (streamReader.Peek() != -1)
                loreNames.Add(streamReader.ReadLine());

            streamReader.Close();

            streamReader = new StreamReader(Environment.CurrentDirectory + "/allLore.txt");
            while (streamReader.Peek() != -1)
                allLorePieces.Add(streamReader.ReadLine());

            streamReader.Close();
        }

        private void UpdateMenu()
        {
            loreButton.Update(mouseRectangle, scaleX, scaleY);
            upgradeButton.Update(mouseRectangle, scaleX, scaleY);
            helpButton.Update(mouseRectangle, scaleX, scaleY);
            gambleMenuButton.Update(mouseRectangle, scaleX, scaleY);
            mainArcheryButton.Update(mouseRectangle, scaleX, scaleY);
            menuOptionsButton.Update(mouseRectangle, scaleX, scaleY);
            exitButton.Update(mouseRectangle, scaleX, scaleY);
            foreach (Button levelButton in levelButtons)
            {
                levelButton.Update(mouseRectangle, scaleX, scaleY);
                if (levelButton.IsHovered)
                {
                    if (levelButton.IsActivated)
                    {
                        canClick = false;
                        incrementButtonTimer = true;
                        buttonTimer = 0;
                        int num = levelButtons.IndexOf(levelButton) + 1;
                        if (num == 21)
                        {
                            LoadFinalBattle();
                            state = States.FINALBOSS;
                            currentLevel = num;
                            return;
                        }

                        state = States.TOWERS;
                        MediaPlayer.Stop();
                        PlayMainGameMusic();
                        background = Content.Load<Texture2D>("background");
                        speedUpButton = new Button(new Rectangle(680, 145, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Speed Up");
                        slowDownButton = new Button(new Rectangle(680, 180, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Slow Down");

                        timerSeconds = 0;
                        timer = 0.0;
                        currentLevel = levelButtons.IndexOf(levelButton) + 1;

                        battleManager.NewLevel(currentLevel, Content, buttonTexture);
                    }
                    hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    hoverTextLineOne = "Play Level " + (levelButtons.IndexOf(levelButton) + 1).ToString() + " of the";
                    hoverTextLineTwo = "game";
                    hoverTextLineThree = "";
                }
            }
            if (upgradeButton.IsHovered)
            {
                if (upgradeButton.IsActivated && canSwitchMenus)
                {
                    state = States.UPGRADING;
                    otherMenuBackground = Content.Load<Texture2D>("otherMenuBackground");

                    if (shop == null)
                        shop = new Shop(Content, buttonTexture);
                    
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayOtherMusic();

                    upgradeButton.Text = "Menu";
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Visit the shop to buy";
                hoverTextLineTwo = "upgrades";
                hoverTextLineThree = "";
            }
            if (loreButton.IsHovered)
            {
                if (loreButton.IsActivated && canSwitchMenus)
                {
                    LoadLore();
                    state = States.LORE;
                    loreShower = Content.Load<Texture2D>("loreWindow");
                    otherMenuBackground = Content.Load<Texture2D>("otherMenuBackground");
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayOtherMusic();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Read what you have ";
                hoverTextLineTwo = "found about the lore";
                hoverTextLineThree = "of the land";
            }
            if (gambleMenuButton.IsHovered)
            {
                if (gambleMenuButton.IsActivated && canSwitchMenus)
                {
                    LoadInvesting();
                    state = States.GAMBLING;
                    gamblingBackground = Content.Load<Texture2D>("gamblingBackground");
                    gambleTexture = Content.Load<Texture2D>("investmentBox");
                    gambleButton = new Button(new Rectangle(480, 400, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Gamble");
                    rollButton = new Button(new Rectangle(275, 400, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Roll");
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayOtherMusic();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Take a risk and";
                hoverTextLineTwo = "gamble your gold";
                hoverTextLineThree = "";
            }
            if (mainArcheryButton.IsHovered)
            {
                if (mainArcheryButton.IsActivated && canSwitchMenus)
                {
                    LoadArchery();
                    state = States.ARCHERY;
                    defenseTexture = Content.Load<Texture2D>("defenseBackground");
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayOtherMusic();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Experience life as an";
                hoverTextLineTwo = "archer on the battlefield";
                hoverTextLineThree = "and gain a little gold.";
            }
            if (menuOptionsButton.IsHovered)
            {
                if (menuOptionsButton.IsActivated && canSwitchMenus)
                {
                    //LoadArchery();
                    state = States.OPTIONS;
                    otherMenuBackground = Content.Load<Texture2D>("otherMenuBackground");

                    if (optionMenu == null)
                        optionMenu = new OptionMenu(Content, buttonTexture, otherMenuBackground);

                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayOtherMusic();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Visit the options menu";
                hoverTextLineTwo = "to customize the game";
                hoverTextLineThree = "to your liking.";
            }
            if (exitButton.IsHovered)
            {
                if (exitButton.IsActivated && canSwitchMenus)
                    Exit();
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Exit the game";
                hoverTextLineTwo = "";
                hoverTextLineThree = "";
            }
            PlayMenuMusic();
        }

        private void UpdateUpgrades()
        {
            upgradeButton.Update(mouseRectangle, scaleX, scaleY);
            
            if (upgradeButton.IsHovered)
            {
                if (upgradeButton.IsActivated && canSwitchMenus)
                {
                    state = States.MENU;
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();

                    upgradeButton.Text = "Upgrade";
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Return to the menu";
                hoverTextLineTwo = "";
                hoverTextLineThree = "";
            }

            shop.Update(mouseRectangle, scaleX, scaleY, canClick, gold, battleManager, Content);

            if (!canClick)
                incrementButtonTimer = true;
        }

        private void CheckForBuying()
        {
            
            speedUpButton.Update(mouseRectangle, scaleX, scaleY);
            slowDownButton.Update(mouseRectangle, scaleX, scaleY);
            if (speedUpButton.IsHovered)
            {
                if (canClick && speedUpButton.IsActivated)
                {
                    ++updatesPerFrame;
                    canClick = false;
                    incrementButtonTimer = true;
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Speed up the rate at";
                hoverTextLineTwo = "which times passes";
                hoverTextLineThree = "";
            }
            if (!slowDownButton.IsHovered)
                return;
            if (canClick && slowDownButton.IsActivated)
            {
                if (updatesPerFrame - 1 > 0)
                    --updatesPerFrame;
                canClick = false;
                incrementButtonTimer = true;
            }
            hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
            hoverTextLineOne = "Slow down the rate at";
            hoverTextLineTwo = "which times passes. Can";
            hoverTextLineThree = "not slow past default";
        }

        private void CheckForEndGame()
        {
            ENDGAMERESULT result = battleManager.CheckForEndGame(Content);

            switch (result)
            {
                case ENDGAMERESULT.WIN:
                    try
                    {
                        StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/Level Data/postLevel" + currentLevel.ToString() + ".txt");
                        string[] strArray = reader.ReadToEnd().Split('\n');
                        didWin = true;
                        state = States.POSTLEVEL;
                        openingBackground = Content.Load<Texture2D>("openingBackground");
                        replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Replay Level");
                        menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
                        nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Next Level");
                        storyTexts = new List<string>();
                        storyTexts = ((IEnumerable<string>)strArray).ToList<string>();
                        currentStoryText = storyTexts[0];
                        storyTexts.RemoveAt(0);
                        canMoveForward = false;
                        incrementForwardTimer = true;
                        forwardTimer = 0;
                    }
                    catch (Exception ex)
                    {
                        replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Replay Level");
                        menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
                        nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Next Level");
                        showPopUp = true;
                        didWin = true;
                        
                        int num1 = 0;
                        int num2 = currentLevel >= 3 ? (currentLevel >= 9 ? (currentLevel >= 15 ? num1 + currentLevel * 5 : num1 + currentLevel * 10) : num1 + currentLevel * 15) : num1 + (currentLevel * 25 + 20);
                        gold += num2;
                        goldGained += num2;
                        
                        if (currentLevel == levelUnlocked)
                            ++levelUnlocked;
                        LoadRectangles();

                        if (random.Next(0, 1) == 0)
                        {
                            string allLorePiece = allLorePieces[random.Next(0, allLorePieces.Count)];
                            if (!lorePieces.Contains(allLorePiece))
                                lorePieces.Add(allLorePiece);
                            unlockedItems.Add(loreNames[allLorePieces.IndexOf(allLorePiece)]);
                        }
                    }

                    updatesPerFrame = 1;
                    break;
                case ENDGAMERESULT.LOSS:
                    showPopUp = true;
                    didWin = false;

                    LoadRectangles();
                    updatesPerFrame = 1;
                    break;
            }
        }

        private void PlayMainGameMusic()
        {
            if (MediaPlayer.State != MediaState.Stopped)
                return;
            song = Content.Load<Song>("Track " + (object)random.Next(1, 4));
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = false;
        }

        private void PlayMenuMusic()
        {
            if (MediaPlayer.State != MediaState.Stopped)
                return;
            song = Content.Load<Song>("Main Menu " + (object)random.Next(1, 2));
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(song);
        }

        private void UpdateOpening(KeyboardState state)
        {
            if (canMoveForward && state.GetPressedKeys().Length > 0)
            {
                if (openingText.Count > 0)
                {
                    helperText = openingText[0];
                    openingText.RemoveAt(0);
                }
                else
                {
                    this.state = States.TUTORIAL;
                    tutorialCounter = 0;
                    hasSeenOpeningSequence = true;
                    background = Content.Load<Texture2D>("background");
                    replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Replay Level");
                    menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
                    nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Next Level");
                    PlayMainGameMusic();

                    tutorial = new Tutorial(Content, buttonTexture, background, textBar, popUpTexture);
                }
                canMoveForward = false;
                incrementForwardTimer = true;
            }
            if (!incrementForwardTimer)
                return;
            ++forwardTimer;
            if (forwardTimer == 20)
            {
                canMoveForward = true;
                incrementForwardTimer = false;
                forwardTimer = 0;
            }
        }

        private void LoadOpening()
        {
            openingBackground = Content.Load<Texture2D>("openingBackground");

            StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/opening.txt");

            openingText = reader.ReadToEnd().Split('\n').ToList<String>();
            helperText = openingText[0];
            openingText.RemoveAt(0);
        }

        private void PlayMiniGameMusic()
        {
            song = Content.Load<Song>("Track4");
            MediaPlayer.Volume = volume;
            MediaPlayer.Play(song);
        }

        private void UpdateArchery(GameTime gameTime)
        {
            archer.Update(gameTime, Content, Window.ClientBounds.Width, Window.ClientBounds.Height, scaleX, scaleY);
            for (int index = 0; index < attackingEnemies.Count; ++index)
            {
                attackingEnemies[index].Update(gameTime, archer.Position);
                Rectangle rectangle = archer.Arrow.Rectangle;
                if (rectangle.Intersects(attackingEnemies[index].Rectangle))
                {
                    archer.Arrow.Kill();
                    attackingEnemies.RemoveAt(index);
                    gold += 2;
                    break;
                }
                rectangle = attackingEnemies[index].Rectangle;
                if (rectangle.Intersects(archer.Rectangle))
                {
                    attackingEnemies[index].SetAttacking(gameTime);
                    if (attackingEnemies[index].CanAttack)
                    {
                        archer.Health -= attackingEnemies[index].Damage;
                        archer.Hit();
                        attackingEnemies[index].Attack();
                    }
                }
            }
            if (random.Next(0, 250) == 0)
                attackingEnemies.Add(new AttackingEnemy(Content));
            if (archer.Health <= 0)
            {
                state = States.MENU;
                LoadRectangles();
            }
            returnHomeButton.Update(mouseRectangle, scaleX, scaleY);
            if (!returnHomeButton.IsHovered)
                return;
            if (returnHomeButton.IsActivated)
            {
                state = States.MENU;
                LoadRectangles();
                canSwitchMenus = false;
                incrementMenuTimer = true;
                menuTimer = 0;
                MediaPlayer.Stop();
                PlayMenuMusic();
            }
            hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
            hoverTextLineOne = "Return home";
            hoverTextLineTwo = "";
            hoverTextLineThree = "";
        }

        private void DrawArchery(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(defenseTexture, new Vector2(0.0f, 0.0f), Color.White * 0.8f);
            archer.Draw(spriteBatch);
            foreach (AttackingEnemy attackingEnemy in attackingEnemies)
                attackingEnemy.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Gold: " + gold.ToString(), new Vector2(330f, 15f), Color.Black);
            returnHomeButton.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Menu", new Vector2((float)(returnHomeButton.Rectangle.X + 25), (float)returnHomeButton.Rectangle.Y), Color.Black);
        }

        private void LoadArchery()
        {
            state = States.ARCHERY;
            archer = new DefendingArcher(Content);
            attackingEnemies = new List<AttackingEnemy>();
            attackingEnemies.Add(new AttackingEnemy(Content));
        }

        private void UpdatePopUp(Rectangle mousePosition)
        {
            replayButton.Update(mouseRectangle, scaleX, scaleY);
            menuButton.Update(mouseRectangle, scaleX, scaleY);
            nextButton.Update(mouseRectangle, scaleX, scaleY);
            Mouse.GetState();
            if (replayButton.IsActivated)
            {
                battleManager.NewLevel(currentLevel, Content, buttonTexture);

                state = States.TOWERS;
                showPopUp = false;
                goldGained = 0;
                goldSpent = 0;
                troopsLost = 0;
                enemiesKilled = 0;
                unlockedItems = new List<string>();
                timer = 0.0;
                timerSeconds = 0;
                showPopUp = false;
            }
            if (menuButton.IsActivated)
            {
                state = States.MENU;
                MediaPlayer.Stop();
                showPopUp = false;
                goldGained = 0;
                goldSpent = 0;
                troopsLost = 0;
                enemiesKilled = 0;
                unlockedItems = new List<string>();
                timer = 0.0;
                timerSeconds = 0;
                showPopUp = false;
            }
            if (!nextButton.IsActivated || !didWin)
                return;
            ++currentLevel;
            canClick = false;
            incrementButtonTimer = true;
            if (currentLevel == 21)
            {
                LoadFinalBattle();
                state = States.FINALBOSS;
            }
            else
            {
                showPopUp = false;
                goldGained = 0;
                goldSpent = 0;
                troopsLost = 0;
                enemiesKilled = 0;
                unlockedItems = new List<string>();         

                state = States.TOWERS;
                showPopUp = false;
                goldGained = 0;
                goldSpent = 0;
                troopsLost = 0;
                enemiesKilled = 0;
                unlockedItems = new List<string>();
                timer = 0.0;
                timerSeconds = 0;

                battleManager.NewLevel(currentLevel, Content, buttonTexture);
            }
        }

        private void UpdateLore()
        {
            returnHomeButton.Update(mouseRectangle, scaleX, scaleY);
            Mouse.GetState();
            if (returnHomeButton.IsHovered)
            {
                if (returnHomeButton.IsActivated)
                {
                    state = States.MENU;
                    MediaPlayer.Stop();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Return to the main";
                hoverTextLineTwo = "menu";
                hoverTextLineThree = "";
            }
            foreach (Button unlockedLoreButton in unlockedLoreButtons)
            {
                unlockedLoreButton.Update(mouseRectangle, scaleX, scaleY);
                if (unlockedLoreButton.IsHovered && unlockedLoreButton.IsActivated)
                {
                    currentLore = lorePieces[unlockedLoreButtons.IndexOf(unlockedLoreButton)];
                    showLore = true;
                }
            }
            foreach (Button lockedLoreButton in lockedLoreButtons)
                lockedLoreButton.Update(mouseRectangle, scaleX, scaleY);
        }

        private void DrawLore(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);
            returnHomeButton.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Main Menu", new Vector2((float)(returnHomeButton.Rectangle.X + 8), (float)(returnHomeButton.Rectangle.Y + 2)), Color.Black);
            spriteBatch.DrawString(largeFont, "LORE", new Vector2(337f, 15f), Color.Black);
            for (int index = 0; index < unlockedLoreButtons.Count; ++index)
            {
                unlockedLoreButtons[index].Draw(spriteBatch);
                //spriteBatch.DrawString(font, loreNames[allLorePieces.IndexOf(lorePieces[index])], new Vector2((float)(unlockedLoreButtons[index].Rectangle.X + 3), (float)(unlockedLoreButtons[index].Rectangle.Y + 5)), Color.Black);
            }
            foreach (Button lockedLoreButton in lockedLoreButtons)
            {
                lockedLoreButton.Draw(spriteBatch);
                //spriteBatch.DrawString(font, "Locked", new Vector2((float)(lockedLoreButton.Rectangle.X + 24), (float)(lockedLoreButton.Rectangle.Y + 5)), Color.Black);
            }
            if (!showLore)
                return;
            spriteBatch.Draw(loreShower, new Vector2(180f, 200f), Color.White);
            spriteBatch.DrawString(mediumFont, currentLore, new Vector2(190f, 220f), Color.Black);
        }

        private void LoadLore()
        {
            lockedLoreButtons = new List<Button>();
            unlockedLoreButtons = new List<Button>();
            for (int y = 75; y < 195; y += 40)
            {
                for (int x = 25; x < 750; x += 125)
                    lockedLoreButtons.Add(new Button(new Rectangle(x, y, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Locked"));
            }
            int num = 0;
            for (int y = 75; y < 195; y += 40)
            {
                for (int x = 25; x < 750; x += 125)
                {
                    if (num < lorePieces.Count)
                    {
                        Button button = new Button(new Rectangle(x, y, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, loreNames[num]);
                        unlockedLoreButtons.Add(button);
                        foreach (Button lockedLoreButton in lockedLoreButtons)
                        {
                            if (lockedLoreButton.Rectangle.Intersects(button.Rectangle))
                            {
                                lockedLoreButtons.Remove(lockedLoreButton);
                                break;
                            }
                        }
                        ++num;
                    }
                }
            }
        }

        private void UpdateStory(KeyboardState state)
        {
            if (canMoveForward && state.GetPressedKeys().Length > 0)
            {
                if (storyTexts.Count > 0)
                {
                    currentStoryText = storyTexts[0];
                    storyTexts.RemoveAt(0);
                }
                else
                {
                    MediaPlayer.Stop();
                    PlayMainGameMusic();
                    if (currentLevel == 21)
                    {
                        this.state = States.MENU;
                        MediaPlayer.Stop();
                        PlayMenuMusic();
                        return;
                    }
                    this.state = States.TOWERS;
                    showPopUp = true;

                    int num1 = 0;
                    int num2 = currentLevel >= 3 ? (currentLevel >= 9 ? (currentLevel >= 15 ? num1 + currentLevel * 5 : num1 + currentLevel * 10) : num1 + currentLevel * 15) : num1 + (currentLevel * 25 + 20);
                    gold += num2;
                    goldGained += num2;

                    if (currentLevel == levelUnlocked && currentLevel != 20)
                    {
                        ++levelUnlocked;
                        LoadRectangles();
                    }
                    if (random.Next(0, 1) == 0)
                    {
                        string allLorePiece = allLorePieces[random.Next(0, allLorePieces.Count)];
                        if (!lorePieces.Contains(allLorePiece))
                            lorePieces.Add(allLorePiece);
                        unlockedItems.Add(loreNames[allLorePieces.IndexOf(allLorePiece)]);
                    }
                }
                canMoveForward = false;
                incrementForwardTimer = true;
            }
            if (!incrementForwardTimer)
                return;
            ++forwardTimer;
            if (forwardTimer == 20)
            {
                canMoveForward = true;
                incrementForwardTimer = false;
                forwardTimer = 0;
            }
        }

        private void DrawStory(SpriteBatch spriteBatch)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            spriteBatch.Draw(openingBackground, new Vector2(0.0f, -20f), Color.White);
            spriteBatch.Draw(helperTexture, new Vector2(125f, (float)byte.MaxValue), Color.White);
            spriteBatch.Draw(textBar, new Vector2(50f, 380f), Color.White * 0.75f);
            if (currentStoryText.Length > 95)
            {
                int num = currentStoryText.Length / 95 + 1;
                for (int index = 0; index < num; ++index)
                {
                    string text = currentStoryText.Substring(95 * index).Length <= 95 ? currentStoryText.Substring(95 * index) : currentStoryText.Substring(95 * index, 95);
                    spriteBatch.DrawString(font, text, new Vector2(60f, (float)(390 + index * 25)), Color.Black);
                }
            }
            else
                spriteBatch.DrawString(font, currentStoryText, new Vector2(60f, 390f), Color.Black);
        }

        private void UpdateInvesting()
        {
            returnHomeButton.Update(mouseRectangle, scaleX, scaleY);
            gambleButton.Update(mouseRectangle, scaleX, scaleY);
            rollButton.Update(mouseRectangle, scaleX, scaleY);
            if (returnHomeButton.IsHovered)
            {
                if (returnHomeButton.IsActivated && canSwitchMenus)
                {
                    state = States.MENU;
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Return to the menu";
                hoverTextLineTwo = "";
                hoverTextLineThree = "";
            }
            if (gambleButton.IsHovered)
            {
                if (canGamble)
                {
                    if (gold - gamblingAmount >= 0)
                    {
                        if (gambleButton.IsActivated)
                        {
                            canGamble = false;
                            incrementGambleTimer = true;
                            if (gambleChance == 0.5)
                            {
                                if (random.Next(0, 2) == 0)
                                {
                                    gold += gamblingAmount;
                                    gamblingResult = "Ye won!";
                                }
                                else
                                {
                                    gold -= gamblingAmount;
                                    gamblingResult = "Unfortunately, ye lost!";
                                }
                            }
                            else if (gambleChance == 0.33)
                            {
                                if (random.Next(0, 3) == 0)
                                {
                                    gold += gamblingAmount;
                                    gamblingResult = "Ye won!";
                                }
                                else
                                {
                                    gold -= gamblingAmount;
                                    gamblingResult = "Unfortunately, ye lost!";
                                }
                            }
                            else if (gambleChance == 0.25)
                            {
                                if (random.Next(0, 4) == 0)
                                {
                                    gold += gamblingAmount;
                                    gamblingResult = "Ye won!";
                                }
                                else
                                {
                                    gold -= gamblingAmount;
                                    gamblingResult = "Unfortunately, ye lost!";
                                }
                            }
                            else if (gambleChance == 0.2)
                            {
                                if (random.Next(0, 5) == 0)
                                {
                                    gold += gamblingAmount;
                                    gamblingResult = "Ye won!";
                                }
                                else
                                {
                                    gold -= gamblingAmount;
                                    gamblingResult = "Unfortunately, ye lost!";
                                }
                            }
                            else if (gambleChance == 0.125)
                            {
                                if (random.Next(0, 8) == 0)
                                {
                                    gold += gamblingAmount;
                                    gamblingResult = "Ye won!";
                                }
                                else
                                {
                                    gold -= gamblingAmount;
                                    gamblingResult = "Unfortunately, ye lost!";
                                }
                            }
                        }
                        hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                        hoverTextLineOne = "Risk your gold in";
                        hoverTextLineTwo = "an ol' fashioned";
                        hoverTextLineThree = "bet";
                    }
                    else
                    {
                        hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                        hoverTextLineOne = "Friend, ye don't";
                        hoverTextLineTwo = "have enough gold!";
                        hoverTextLineThree = "";
                    }
                }
                else
                {
                    hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    hoverTextLineOne = "Ye must wait to";
                    hoverTextLineTwo = "gamble again, friend!";
                    hoverTextLineThree = "";
                }
            }
            if (!rollButton.IsHovered)
                return;
            if (rollButton.IsActivated)
                LoadInvesting();
            hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
            hoverTextLineOne = "Roll the dice to";
            hoverTextLineTwo = "try for better odds";
            hoverTextLineThree = "";
        }

        private void LoadInvesting()
        {
            gamblingResult = "";
            gamblingAmount = random.Next(5, 301);
            if (gamblingAmount >= 5 && gamblingAmount < 50)
                gambleChance = 0.5;
            else if (gamblingAmount >= 50 && gamblingAmount < 100)
                gambleChance = 0.33;
            else if (gamblingAmount >= 100 && gamblingAmount < 150)
                gambleChance = 0.25;
            else if (gamblingAmount >= 150 && gamblingAmount < 200)
                gambleChance = 0.2;
            else
                gambleChance = 0.125;
        }

        private void DrawInvesting(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gamblingBackground, new Vector2(0.0f, -20f), Color.White);
            returnHomeButton.Draw(spriteBatch);

            gambleButton.Draw(spriteBatch);

            rollButton.Draw(spriteBatch);
            spriteBatch.Draw(gambleTexture, new Vector2(275f, 100f), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
            spriteBatch.DrawString(font, "Amount To Gamble: " + (object)gamblingAmount, new Vector2(320f, 138f), Color.Black);
            spriteBatch.DrawString(font, "Chance Of Winning: " + gambleChance.ToString("P2"), new Vector2(320f, 196f), Color.Black);
            spriteBatch.DrawString(font, gamblingResult, new Vector2(320f, 254f), Color.Black);
            spriteBatch.DrawString(font, "Gold: " + gold.ToString(), new Vector2(300f, 25f), Color.Black);
        }

        private void PlayOtherMusic()
        {
            MediaPlayer.Stop();
            if (MediaPlayer.State != MediaState.Stopped)
                return;
            song = Content.Load<Song>("Other Menus");
            MediaPlayer.Volume = 0.6f;
            MediaPlayer.Play(song);
        }

        private void UpdateOptions()
        {
            returnHomeButton.Update(mouseRectangle, scaleX, scaleY);

            if (returnHomeButton.IsHovered)
            {
                if (returnHomeButton.IsActivated && canSwitchMenus)
                {
                    state = States.MENU;
                    canSwitchMenus = false;
                    incrementMenuTimer = true;
                    menuTimer = 0;
                    MediaPlayer.Stop();
                    PlayMenuMusic();
                }
                hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                hoverTextLineOne = "Return to the menu";
                hoverTextLineTwo = "";
                hoverTextLineThree = "";
            }

            bool[] output = optionMenu.Update(mouseRectangle, canClick, canSwitchMenus, scaleX, scaleY);

            if (output[0])
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
                canClick = false;
                incrementButtonTimer = true;
            }

            if (output[1])
            {
                Main.volume += 0.1f;
                MediaPlayer.Volume = Main.volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                canClick = false;
                incrementButtonTimer = true;
            }

            if (output[2])
            {
                Main.volume -= 0.1f;
                MediaPlayer.Volume = Main.volume;
                MediaPlayer.Stop();
                MediaPlayer.Play(song);
                canClick = false;
                incrementButtonTimer = true;
            }

            if (output[3])
            {
                state = States.CREDITS;
                canSwitchMenus = false;
                incrementMenuTimer = true;
                menuTimer = 0;
            }
        }

        private void DrawOptions()
        {
            optionMenu.Draw(spriteBatch);
            returnHomeButton.Draw(spriteBatch);
        }

        private void UpdateFinalBattle(GameTime gameTime)
        {
            FinalBattleResult result = finalBattle.Update(gameTime, Window.ClientBounds.Width, Window.ClientBounds.Height);

            if (result == FinalBattleResult.LOSS)
            {
                replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Replay Level");
                menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Menu");
                nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, Content, "Next Level");
                string[] strArray = Resources.bossFailure.Split('\n');
                didWin = false;
                state = States.POSTLEVEL;
                currentStoryText = storyTexts[0];
                storyTexts.RemoveAt(0);
                canMoveForward = false;
                incrementForwardTimer = true;
                forwardTimer = 0;
            }

            else if (result == FinalBattleResult.WIN)
            {
                string[] strArray = Resources.bossWin.Split('\n');
                state = States.POSTLEVEL;
                currentStoryText = storyTexts[0];
                storyTexts.RemoveAt(0);
                canMoveForward = false;
                incrementForwardTimer = true;
                forwardTimer = 0;
                didWin = true;
            }
        }

        private void DrawFinalBattle()
        {
            finalBattle.Draw(spriteBatch);
            //graphics.GraphicsDevice.Clear(Color.White);
        }

        private void LoadFinalBattle()
        {
            finalBattle = new FinalBattle(Content);
            openingBackground = Content.Load<Texture2D>("openingBackground");
        }

        private void LoadStuffForEverythingAndMenu()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");
            hoverButton = Content.Load<Texture2D>("hoverButton");
            largeFont = Content.Load<SpriteFont>("largeFont");
            menuTexture = Content.Load<Texture2D>("menuBackground");
            helperTexture = Content.Load<Texture2D>("helper");
            textBar = Content.Load<Texture2D>("textBar");
            mediumFont = Content.Load<SpriteFont>("mediumFont");
            midLargeFont = Content.Load<SpriteFont>("midLargeFont");
            popUpTexture = Content.Load<Texture2D>("popUp");
        }

        private void UpdateCredits()
        {
            returnHomeButton.Update(mouseRectangle, scaleX, scaleY);
            if (!returnHomeButton.IsHovered)
                return;
            if (returnHomeButton.IsActivated && canSwitchMenus)
            {
                state = States.MENU;
                canSwitchMenus = false;
                incrementMenuTimer = true;
                menuTimer = 0;
                MediaPlayer.Stop();
                PlayMenuMusic();
            }
            hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
            hoverTextLineOne = "Return to the menu";
            hoverTextLineTwo = "";
            hoverTextLineThree = "";
        }

        private void DrawCredits()
        {
            spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);
            spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);
            returnHomeButton.Draw(spriteBatch);
            //spriteBatch.DrawString(font, "Main Menu", new Vector2((float)(returnHomeButton.Rectangle.X + 8), (float)(returnHomeButton.Rectangle.Y + 2)), Color.Black);
            spriteBatch.DrawString(largeFont, "CREDITS", new Vector2(300f, 20f), Color.White);
            spriteBatch.DrawString(font, "Music: http://www.bensound.com/royalty-free-music", new Vector2(15f, 100f), Color.White);
            spriteBatch.DrawString(font, "Dragon Effects: http://soundbible.com/tags-dragon.html and https://creativecommons.org/licenses/by/3.0/us/", new Vector2(15f, 125f), Color.White);
            spriteBatch.DrawString(font, "Charge Effect: http://www.flashkit.com/soundfx/People/Screams/scream_b-Sam_Love-8921/index.php", new Vector2(15f, 150f), Color.White);
            spriteBatch.DrawString(font, "Other Effects: https://www.freesoundeffects.com/licence.php", new Vector2(15f, 175f), Color.White);
            spriteBatch.DrawString(font, "Sprites: http://gaurav.munjal.us/Universal-LPC-Spritesheet-Character-Generator/ and", new Vector2(15f, 200f), Color.White);
            spriteBatch.DrawString(font, "http://gaurav.munjal.us/Universal-LPC-Spritesheet-Character-Generator/LICENSE", new Vector2(15f, 225f), Color.White);
        }

        public static void Click()
        {
            canClick = false;
            incrementButtonTimer = true;
        }
    }
}