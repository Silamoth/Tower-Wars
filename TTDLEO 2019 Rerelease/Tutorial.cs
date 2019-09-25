using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace TTDLEO_2019_Rerelease
{
    enum TutorialState { FIRSTLEVEL, POSTFIRSTLEVEL, SECONDLEVEL, POSTSECONDLEVELONE, POSTSECONDLEVELTWO,
        THIRDLEVELSWORDSMAN, THIRDLEVELARCHER, THIRDLEVELMEDIC, THIRDLEVELGENERAL, POSTTHIRDLEVEL, MAINMENUONE, SHOP, MAINMENUTWO, LORE, UPGRADETEST }

    class Tutorial
    {
        BattleManager battleManager;
        SpriteFont font, mediumFont;
        Button tutorialProceedButton;
        Texture2D background, textBar, popUpTexture, buttonTexture, menuTexture, otherMenuBackground;

        Button speedUpButton, slowDownButton;
        Button replayButton, menuButton, nextButton;

        bool showPopUp, didWin;

        TutorialState tutorialState;

        int updatesPerFrame;
        double timer;
        int timerSeconds;

        Button mainArcheryButton;
        Button exitButton;
        Button upgradeButton;
        Button loreButton;
        Button menuOptionsButton;
        Button gambleMenuButton;
        List<Button> levelButtons;

        Shop shop;

        Button returnHomeButton;
        List<Button> lockedLoreButtons;

        static Random random = new Random();

        Button endButton;

        public Tutorial(ContentManager content, Texture2D buttonTexture, Texture2D background, Texture2D textBar, 
            Texture2D popUpTexture, Texture2D menuTexture)
        {
            battleManager = new BattleManager(content, buttonTexture, -1);  //TODO: Handle
            font = content.Load<SpriteFont>("font");
            mediumFont = content.Load<SpriteFont>("mediumFont");

            tutorialState = TutorialState.FIRSTLEVEL;

            tutorialProceedButton = new Button(new Rectangle(600, 427, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Proceed");
            this.background = background;

            speedUpButton = new Button(new Rectangle(680, 145, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Speed Up");
            slowDownButton = new Button(new Rectangle(680, 180, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Slow Down");

            replayButton = new Button(new Rectangle(210, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Replay Level");
            menuButton = new Button(new Rectangle(344, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Menu");
            nextButton = new Button(new Rectangle(477, 325, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Next Level");

            this.textBar = textBar;
            this.popUpTexture = popUpTexture;
            this.buttonTexture = buttonTexture;
            this.menuTexture = menuTexture;

            tutorialState = TutorialState.FIRSTLEVEL;

            updatesPerFrame = 5;
            timer = 0;
            timerSeconds = 0;
        }

        public bool Update(GameTime gameTime, ContentManager content, Rectangle mouseRectangle, float scaleX,
            float scaleY, int screenWidth, int screenHeight, float startTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds - (double)startTime;
            while (timer > 1.0)
            {
                timer--;
                timerSeconds++;
            }

            speedUpButton.Update(mouseRectangle, scaleX, scaleY);
            slowDownButton.Update(mouseRectangle, scaleX, scaleY);

            if (tutorialState < TutorialState.MAINMENUONE || tutorialState == TutorialState.UPGRADETEST)
            {
                CheckForEndGame(content);
                battleManager.Update(content, timerSeconds, gameTime, mouseRectangle, scaleX, scaleY, screenWidth, screenHeight,
                 updatesPerFrame, 0);

                if (speedUpButton.IsHovered)
                {
                    if (Main.canClick && speedUpButton.IsActivated)
                    {
                        updatesPerFrame++;
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Speed up the rate at";
                    Main.hoverTextLineTwo = "which times passes";
                    Main.hoverTextLineThree = "";
                }

                if (slowDownButton.IsHovered)
                {
                    if (Main.canClick && slowDownButton.IsActivated)
                    {
                        if (updatesPerFrame > 1)
                            updatesPerFrame--;
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Slow down the rate at";
                    Main.hoverTextLineTwo = "which times passes";
                    Main.hoverTextLineThree = "";
                }
            }

            nextButton.Update(mouseRectangle, scaleX, scaleY);
            menuButton.Update(mouseRectangle, scaleX, scaleY);
            replayButton.Update(mouseRectangle, scaleX, scaleY);
            tutorialProceedButton.Update(mouseRectangle, scaleX, scaleY);

            if (tutorialState == TutorialState.POSTFIRSTLEVEL)
            {
                if (nextButton.IsActivated)
                {
                    tutorialState = TutorialState.SECONDLEVEL;
                    showPopUp = false;
                    battleManager = new BattleManager(content, buttonTexture, -2);
                    timerSeconds = 0;
                    timer = 0;

                    battleManager.AddEnemy(new Enemy(1, 7));
                    battleManager.AddEnemy(new Enemy(1, 11));
                }
            }
            else if (tutorialState == TutorialState.POSTSECONDLEVELONE)
            {
                if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    tutorialState = TutorialState.POSTSECONDLEVELTWO;
            }
            else if (tutorialState == TutorialState.POSTSECONDLEVELTWO)
            {
                if (nextButton.IsActivated)
                {
                    tutorialState = TutorialState.THIRDLEVELSWORDSMAN;
                    showPopUp = false;
                    battleManager = new BattleManager(content, buttonTexture, -3);
                    timerSeconds = 0;
                    timer = 0;
                }
            }
            else if (tutorialState == TutorialState.POSTTHIRDLEVEL)
            {
                if (menuButton.IsActivated)
                {
                    tutorialState = TutorialState.MAINMENUONE;
                    showPopUp = false;
                }
            }
            else if (tutorialState == TutorialState.MAINMENUONE)
            {
                loreButton.Update(mouseRectangle, scaleX, scaleY);
                upgradeButton.Update(mouseRectangle, scaleX, scaleY);
                gambleMenuButton.Update(mouseRectangle, scaleX, scaleY);
                mainArcheryButton.Update(mouseRectangle, scaleX, scaleY);
                menuOptionsButton.Update(mouseRectangle, scaleX, scaleY);
                exitButton.Update(mouseRectangle, scaleX, scaleY);

                if (upgradeButton.IsActivated && Main.canClick)
                {
                    Main.Click();
                    tutorialState = TutorialState.SHOP;

                    otherMenuBackground = content.Load<Texture2D>("otherMenuBackground");
                    shop = new Shop(content, buttonTexture);

                    upgradeButton = new Button(new Rectangle(545, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Main Menu");
                }
            }
            else if (tutorialState == TutorialState.SHOP)
            {
                upgradeButton.Update(mouseRectangle, scaleX, scaleY);
                if (upgradeButton.IsActivated && Main.canClick)
                {
                    tutorialState = TutorialState.MAINMENUTWO;
                    upgradeButton = new Button(new Rectangle(545, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Upgrade");
                }
            }
            else if (tutorialState == TutorialState.MAINMENUTWO)
            {
                loreButton.Update(mouseRectangle, scaleX, scaleY);
                upgradeButton.Update(mouseRectangle, scaleX, scaleY);
                gambleMenuButton.Update(mouseRectangle, scaleX, scaleY);
                mainArcheryButton.Update(mouseRectangle, scaleX, scaleY);
                menuOptionsButton.Update(mouseRectangle, scaleX, scaleY);
                exitButton.Update(mouseRectangle, scaleX, scaleY);

                if (loreButton.IsActivated && Main.canClick)
                {
                    tutorialState = TutorialState.LORE;

                    Main.Click();

                    returnHomeButton = new Button(new Rectangle(25, 35, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Menu");
                    lockedLoreButtons = new List<Button>();
                    for (int y = 75; y < 195; y += 40)
                    {
                        for (int x = 25; x < 750; x += 125)
                            lockedLoreButtons.Add(new Button(new Rectangle(x, y, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Locked"));
                    }
                }
            }
            else if (tutorialState == TutorialState.LORE)
            {
                returnHomeButton.Update(mouseRectangle, scaleX, scaleY);

                if (returnHomeButton.IsActivated && Main.canClick)
                {
                    Main.Click();

                    tutorialState = TutorialState.UPGRADETEST;

                    battleManager = new BattleManager(content, buttonTexture, -7);
                    battleManager.BuyArcher(content);
                    battleManager.BuyTurretOne(content);
                    battleManager.BuyTurretTwo(content);
                    battleManager.BuyWizard(content);

                    endButton = new Button(new Rectangle(300, 50, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "End");
                }
            }
            else if (tutorialState == TutorialState.UPGRADETEST)
            {
                battleManager.RestoreHealth();
                battleManager.SetMana(500);

                if (random.Next(0, 100) == 0)
                    battleManager.AddEnemy(new Enemy(random.Next(0, 9), 1));

                endButton.Update(mouseRectangle, scaleX, scaleY);

                if (endButton.IsActivated)
                    return true;
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mouseRectangle, float scaleX, float scaleY)
        {
            spriteBatch.Draw(background, new Vector2(0, -20), Color.White);

            spriteBatch.DrawString(font, "Gold: " + Main.gold.ToString(), new Vector2(29f, 15f), Color.Black);

            if (tutorialState < TutorialState.MAINMENUONE || tutorialState == TutorialState.UPGRADETEST)
                battleManager.Draw(spriteBatch, mouseRectangle, scaleX, scaleY);

            speedUpButton.Draw(spriteBatch);
            slowDownButton.Draw(spriteBatch);

            switch (tutorialState)
            {
                case TutorialState.FIRSTLEVEL:
                    spriteBatch.DrawString(font, "Level: T1", new Vector2(105, 15), Color.Black);

                    spriteBatch.DrawString(font, "Welcome to Tower Conquest!  In this game, you send out units to defend your tower", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "and attack your enemy's tower.  To begin, click the commoner button to send out a", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "commoner, the most basic soldier in the game.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.POSTFIRSTLEVEL:
                    spriteBatch.DrawString(font, "Congratulations!  You've made it past the first tutorial level!  This screen is shown", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "after every level.  Here you can find stats regarding the level, replay the level, move", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "on to the next level, or return to the menu.  For now, click on the Next Level button to", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "proceed to the next level of the tutorial.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.SECONDLEVEL:
                    spriteBatch.DrawString(font, "Level: T2", new Vector2(105, 15), Color.Black);

                    spriteBatch.DrawString(font, "In all other levels, you will have enemies to fight.  Send out Commoners like you did", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "last level in order to fight these enemies and attack their tower.  Take note of the", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "Speed Up and Slow Down buttons on the right edge of the screen.  These can be", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "used to make time pass more quickly or slowly within a level.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.POSTSECONDLEVELONE:
                    spriteBatch.DrawString(font, "As you can see, you will have to spend gold in order to beat levels.  You must be", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "smart about how you spend your gold or else you will run out.  For example, don't", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "send out a very strong unit like a Swordsman against something weak like a.", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "Commoner.", new Vector2(135f, 425), Color.Black);

                    spriteBatch.DrawString(font, "Press any button to continue...", new Vector2(300f, 355f), Color.White);
                    break;
                case TutorialState.POSTSECONDLEVELTWO:
                    spriteBatch.DrawString(font, "Now that you've experienced the basics of combat, let's explore some more", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "advanced soldiers and their capabilities.  Click on the Next Level button to begin", new Vector2(135f, 395f), Color.Black);
                    //spriteBatch.DrawString(font, "begin.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.THIRDLEVELSWORDSMAN:
                    spriteBatch.DrawString(font, "There are several soldiers specializing in melee combat.  They effectively function", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "as stronger versions of the Commoner you used before.  These include the Tough", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "Guy, Brute, and Swordsman.  For now, click the Swordsman button to send out a", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "Swordsman.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.THIRDLEVELARCHER:
                    spriteBatch.DrawString(font, "Sometimes you may find you want a soldier that can attack from a distance.  The", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "Archer is perfect for this.  Try sending out an Archer to see how it works.  Note that", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "the Archer can attack from behind another soldier.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.THIRDLEVELMEDIC:
                    spriteBatch.DrawString(font, "Not all soldiers are around to fight.  Some provide support to other soldiers.  For", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "example, the Medic can heal soldiers in front of it.  Try sending out a Tough Guy", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "followed by a Medic to see this in action.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.THIRDLEVELGENERAL:
                    spriteBatch.DrawString(font, "Like the Medic, the General does not do any fighting; it instead supports other", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "soldiers.  It does so by boosting morale and increasing the damage the soldier in", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "front of it is able to deal.  Try sending a Tough Guy followed by a General to see this.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.POSTTHIRDLEVEL:
                    spriteBatch.DrawString(font, "Now that you've mastered combat with soldiers, it's time to see what else you need", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "to know in order to be successful.  Click the Menu Button to go to the game's main", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "menu.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.MAINMENUONE:
                    spriteBatch.Draw(menuTexture, new Rectangle(0, -20, (int)(menuTexture.Width), (int)(menuTexture.Height)), Color.White);
                    spriteBatch.DrawString(mediumFont, "TOWERS THAT DON'T LIKE EACH OTHER", new Vector2(260, 20), Color.White);
                    int num = 1;
                    foreach (Button levelButton in levelButtons)
                    {
                        levelButton.Draw(spriteBatch);
                        num++;
                    }

                    spriteBatch.DrawString(font, "Gold: " + Main.gold.ToString(), new Vector2(10f, 40f), Color.Black);
                    upgradeButton.Draw(spriteBatch);
                    loreButton.Draw(spriteBatch);
                    gambleMenuButton.Draw(spriteBatch);
                    mainArcheryButton.Draw(spriteBatch);
                    menuOptionsButton.Draw(spriteBatch);

                    exitButton.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Welcome to the Main Menu!  This screen serves as your base of operations for all actions", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "in the game.  From here, you can visit the shop, play different levels, exit the game,", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "access mini games, change the settings, and more.  To start out, click the Upgrade Button", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "to access the shop.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.SHOP:
                    spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);

                    spriteBatch.DrawString(mediumFont, "SHOP", new Vector2(310f, 10f), Color.Black);
                    upgradeButton.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Gold: " + Main.gold.ToString(), new Vector2(10f, 40f), Color.Black);
                    shop.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "In the shop, you can purchase a number of upgrades to aid you in your fight against", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "Lord Morgoroth.  Later in the tutorial, you'll have the opportunity to test out these", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "various upgrades.  For now, click the Main Menu button to return to the menu.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.MAINMENUTWO:
                    spriteBatch.Draw(menuTexture, new Rectangle(0, -20, (int)(menuTexture.Width), (int)(menuTexture.Height)), Color.White);
                    spriteBatch.DrawString(mediumFont, "TOWERS THAT DON'T LIKE EACH OTHER", new Vector2(260, 20), Color.White);
                    num = 1;
                    foreach (Button levelButton in levelButtons)
                    {
                        levelButton.Draw(spriteBatch);
                        num++;
                    }

                    spriteBatch.DrawString(font, "Gold: " + Main.gold.ToString(), new Vector2(10f, 40f), Color.Black);
                    upgradeButton.Draw(spriteBatch);
                    loreButton.Draw(spriteBatch);
                    gambleMenuButton.Draw(spriteBatch);
                    mainArcheryButton.Draw(spriteBatch);
                    menuOptionsButton.Draw(spriteBatch);

                    exitButton.Draw(spriteBatch);

                    spriteBatch.DrawString(font, "Now that you're back at the menu, click on the Lore button to explore thelore of the", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "land.", new Vector2(135f, 395f), Color.Black);
                    break;
                case TutorialState.LORE:
                    spriteBatch.Draw(otherMenuBackground, new Vector2(0.0f, 0.0f), Color.White);
                    returnHomeButton.Draw(spriteBatch);
                    spriteBatch.DrawString(mediumFont, "LORE", new Vector2(337f, 15f), Color.Black);

                    foreach (Button lockedLoreButton in lockedLoreButtons)
                    {
                        lockedLoreButton.Draw(spriteBatch);
                    }

                    spriteBatch.DrawString(font, "When you win a level, you may find that you have learned a new tidbit of information ", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "about this world and its history.  You can view all that information from here.  ", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "Everything is currently locked, but you will begin unlocking lore once you win some", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "levels.  For now, click the Menu button to proceed with the tutorial.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.UPGRADETEST:
                    spriteBatch.DrawString(font, "While the Menu button will normally bring you back to the menu, we've put you into", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "an environment to experiment.  We've unlocked all upgrades, and your tower will not", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "take any damage.  Random enemies will spawn for you to fight.  Use this as an", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "opportunity to experiment and get a feel for combat.  Click the End button to exit.", new Vector2(135f, 425), Color.Black);

                    endButton.Draw(spriteBatch);
                    break;
            }

            spriteBatch.Draw(textBar, new Vector2(125f, 375f), new Rectangle?(), Color.White * 0.6f, 0.0f, Vector2.Zero, 0.85f, SpriteEffects.None, 1f);

            if (showPopUp)
            {
                spriteBatch.Draw(popUpTexture, new Vector2(0, 0), Color.White);
                if (didWin)
                    spriteBatch.DrawString(mediumFont, "Congratulations!  You Won The Battle!", new Vector2(220, 125), Color.Black);
                else
                    spriteBatch.DrawString(font, "You Lost The Battle...", new Vector2(270, 125), Color.Black);
                spriteBatch.DrawString(font, "Items gained:", new Vector2(445f, 175f), Color.Black);
                spriteBatch.DrawString(font, "Gold gained: " + Main.goldGained.ToString() + " gold", new Vector2(215f, 173f), Color.Black);
                spriteBatch.DrawString(font, "Gold spent: " + Main.goldSpent.ToString() + " gold", new Vector2(215f, 198f), Color.Black);
                spriteBatch.DrawString(font, "Troops lost: " + Main.troopsLost.ToString(), new Vector2(215f, 225f), Color.Black);
                spriteBatch.DrawString(font, "Enemies killed: " + Main.enemiesKilled.ToString(), new Vector2(215f, 248f), Color.Black);

                replayButton.Draw(spriteBatch);
                menuButton.Draw(spriteBatch);
                if (didWin)
                    nextButton.Draw(spriteBatch);
            }
        }

        void CheckForEndGame(ContentManager content)
        {
            if (tutorialState == TutorialState.UPGRADETEST)
                return;

            ENDGAMERESULT result = battleManager.CheckForEndGame(content);

            switch (result)
            {
                case ENDGAMERESULT.WIN:
                    didWin = true;
                    showPopUp = true;
                    didWin = true;

                    Main.gold += 50;
                    Main.goldGained += 50;

                    updatesPerFrame = 1;

                    if (tutorialState == TutorialState.FIRSTLEVEL)
                        tutorialState = TutorialState.POSTFIRSTLEVEL;
                    else if (tutorialState == TutorialState.SECONDLEVEL)
                        tutorialState = TutorialState.POSTSECONDLEVELONE;
                    else if (tutorialState == TutorialState.THIRDLEVELSWORDSMAN)
                    {
                        tutorialState = TutorialState.THIRDLEVELARCHER;
                        battleManager = new BattleManager(content, buttonTexture, -4);
                        showPopUp = false;
                    }
                    else if (tutorialState == TutorialState.THIRDLEVELARCHER)
                    {
                        tutorialState = TutorialState.THIRDLEVELMEDIC;
                        battleManager = new BattleManager(content, buttonTexture, -5);
                        showPopUp = false;

                        timerSeconds = 0;
                        timer = 0;
                    }
                    else if (tutorialState == TutorialState.THIRDLEVELMEDIC)
                    {
                        tutorialState = TutorialState.THIRDLEVELGENERAL;
                        battleManager = new BattleManager(content, buttonTexture, -6);
                        showPopUp = false;

                        timerSeconds = 0;
                        timer = 0;
                    }
                    else if (tutorialState == TutorialState.THIRDLEVELGENERAL)
                    {
                        tutorialState = TutorialState.POSTTHIRDLEVEL;
                        levelButtons = new List<Button>();

                        loreButton = new Button(new Rectangle(285, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Lore");
                        upgradeButton = new Button(new Rectangle(545, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Upgrade");
                        gambleMenuButton = new Button(new Rectangle(420, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Gamble");
                        mainArcheryButton = new Button(new Rectangle(150, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Archery");
                        menuOptionsButton = new Button(new Rectangle(15, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Options");
                        exitButton = new Button(new Rectangle(680, 425, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Exit");
                    }
                    break;
                case ENDGAMERESULT.LOSS:
                    showPopUp = true;
                    didWin = false;

                    updatesPerFrame = 1;
                    break;
            }
        }
    }
}