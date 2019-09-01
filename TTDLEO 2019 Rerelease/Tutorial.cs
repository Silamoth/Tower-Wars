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
        THIRDLEVELSWORDSMAN, THIRDLEVELARCHER, THIRDLEVELMEDIC, THIRDLEVELGENERAL, MAINMENU, OPTIONS, SHOP, LORE }

    class Tutorial
    {
        BattleManager battleManager;
        SpriteFont font, mediumFont;
        Button tutorialProceedButton;
        Texture2D background, textBar, popUpTexture, buttonTexture;

        Button speedUpButton, slowDownButton;
        Button replayButton, menuButton, nextButton;

        bool showPopUp, didWin;

        TutorialState tutorialState;

        int updatesPerFrame;
        double timer;
        int timerSeconds;

        public Tutorial(ContentManager content, Texture2D buttonTexture, Texture2D background, Texture2D textBar, Texture2D popUpTexture)
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

            tutorialState = TutorialState.FIRSTLEVEL;

            updatesPerFrame = 5;
            timer = 0;
            timerSeconds = 0;
        }

        public void Update(GameTime gameTime, ContentManager content, Rectangle mouseRectangle, float scaleX,
            float scaleY, int screenWidth, int screenHeight, float startTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds - (double)startTime;
            while (timer > 1.0)
            {
                timer--;
                timerSeconds++;
            }

            if (tutorialState == TutorialState.FIRSTLEVEL || tutorialState == TutorialState.SECONDLEVEL || tutorialState == TutorialState.THIRDLEVELSWORDSMAN
                || tutorialState == TutorialState.THIRDLEVELARCHER)
            {
                CheckForEndGame(content);
                battleManager.Update(content, timerSeconds, gameTime, mouseRectangle, scaleX, scaleY, screenWidth, screenHeight,
                 updatesPerFrame, 0);
            }

            nextButton.Update(mouseRectangle, scaleX, scaleY);
            menuButton.Update(mouseRectangle, scaleX, scaleY);
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
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mouseRectangle, float scaleX, float scaleY)
        {
            spriteBatch.Draw(background, new Vector2(0, -20), Color.White);

            spriteBatch.DrawString(font, "Gold: " + Main.gold.ToString(), new Vector2(29f, 15f), Color.Black);

            battleManager.Draw(spriteBatch, mouseRectangle, scaleX, scaleY);

            switch (tutorialState)
            {
                case TutorialState.FIRSTLEVEL:
                    spriteBatch.DrawString(font, "Level: T1", new Vector2(105, 15), Color.Black);

                    spriteBatch.DrawString(font, "Welcome to Tower Conquest!  In this game, you send out units to defend your", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "tower and attack your enemy's tower.  To begin, click the commoner button to", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "send out a commoner, the most basic soldier in the game.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.POSTFIRSTLEVEL:
                    spriteBatch.DrawString(font, "Congratulations!  You've made it past the first tutorial level!  This screen is", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "shown after every level.  Here you can find stats regarding the level, replay the", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "level, move on to the next level, or return to the menu.  For now, click on the", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "Next Level button to proceed to the next level of the tutorial.", new Vector2(135f, 425), Color.Black);
                    break;
                case TutorialState.SECONDLEVEL:
                    spriteBatch.DrawString(font, "Level: T2", new Vector2(105, 15), Color.Black);

                    spriteBatch.DrawString(font, "In all other levels, you will have enemies to fight.  Send out Commoners like you", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "did last level in order to fight these enemies and attack their tower.", new Vector2(135f, 395f), Color.Black);
                    //spriteBatch.DrawString(font, "level, move on to the next level, or return to the menu.  For now, click on the", new Vector2(135f, 410f), Color.Black);
                    //spriteBatch.DrawString(font, "Next Level button to proceed to the next level of the tutorial.", new Vector2(135f, 425), Color.Black);

                    speedUpButton.Draw(spriteBatch);
                    slowDownButton.Draw(spriteBatch);
                    break;
                case TutorialState.POSTSECONDLEVELONE:
                    spriteBatch.DrawString(font, "As you can see, you will have to spend gold in order to beat levels.  You must", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "be smart about how you spend your gold or else you will run out.  For example, ", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "don't send out a very strong unit like a Swordsman against something weak like", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "a Commoner.", new Vector2(135f, 425), Color.Black);

                    spriteBatch.DrawString(font, "Press any button to continue...", new Vector2(300f, 355f), Color.White);
                    break;

                case TutorialState.POSTSECONDLEVELTWO:
                    spriteBatch.DrawString(font, "Now that you've experienced the basics of combat, let's explore some more", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "advanced soldiers and their capabilities.  Click on the Next Level button to", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "begin.", new Vector2(135f, 410f), Color.Black);
                    break;
                case TutorialState.THIRDLEVELSWORDSMAN:
                    spriteBatch.DrawString(font, "There are several soldiers specializing in melee combat.  They effectively", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "function as stronger versions of the Commoner you used before.  These include", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "the Tough Guy, Brute, and Swordsman.  For now, click the Swordsman button to", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "send out a Swordsman.", new Vector2(135f, 425), Color.Black);
                    break;
            }

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

            spriteBatch.Draw(textBar, new Vector2(125f, 375f), new Rectangle?(), Color.White * 0.6f, 0.0f, Vector2.Zero, 0.85f, SpriteEffects.None, 1f);
        }

        void CheckForEndGame(ContentManager content)
        {
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