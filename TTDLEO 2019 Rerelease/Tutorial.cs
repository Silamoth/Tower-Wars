using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TTDLEO_2019_Rerelease
{
    enum TutorialState { INTRO, FIRSTLEVEL, SECONDLEVEL, MAINMENU, OPTIONS, SHOP, LORE }

    class Tutorial
    {
        BattleManager battleManager;
        SpriteFont font, mediumFont;
        Button tutorialProceedButton;
        Texture2D background, textBar, popUpTexture;

        Button speedUpButton, slowDownButton;
        Button replayButton, menuButton, nextButton;

        bool showPopUp, didWin;

        TutorialState tutorialState;

        int updatesPerFrame;

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

            tutorialState = TutorialState.INTRO;

            updatesPerFrame = 5;
        }

        public void Update(GameTime gameTime, ContentManager content, int timerSeconds, Rectangle mouseRectangle, float scaleX,
            float scaleY, int screenWidth, int screenHeight)
        {
            battleManager.Update(content, timerSeconds, gameTime, mouseRectangle, scaleX, scaleY, screenWidth, screenHeight,
                 updatesPerFrame, 0);

            

            nextButton.Update(mouseRectangle, scaleX, scaleY);
            menuButton.Update(mouseRectangle, scaleX, scaleY);
            tutorialProceedButton.Update(mouseRectangle, scaleX, scaleY);
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
                    break;
                case TutorialState.SECONDLEVEL:
                    spriteBatch.DrawString(font, "Level: T2", new Vector2(105, 15), Color.Black);
                    speedUpButton.Draw(spriteBatch);
                    slowDownButton.Draw(spriteBatch);
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
            switch (tutorialState)
            {
                case TutorialState.INTRO:
                    spriteBatch.DrawString(font, "Welcome to Tower Conquest!  In this game, you send out units to defend your", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "tower and attack your enemy's tower.  To begin, click the commoner button to", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "send out a commoner, the most basic soldier in the game.", new Vector2(135f, 410f), Color.Black);
                    break;
                /**case 1:
                    spriteBatch.DrawString(font, "Now watch as the swordsman proceeds to attack the enemy's tower.  It won't", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "take long for the swordsman to defeat the enemy tower because it is one of the", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "strongest units in the game.", new Vector2(135f, 410f), Color.Black);
                    break;
                case 2:
                    spriteBatch.DrawString(font, "Now that the swordsman has defeated the enemy's tower, you have won this", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "level.  Because of this, you have earned some gold, which will be added to your", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "running total that carries over throughout the game.  Gold can be used to buy", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "soldiers and eventually to buy upgrades.", new Vector2(135f, 425f), Color.Black);
                    tutorialProceedButton.Draw(spriteBatch);
                    spriteBatch.DrawString(font, "Proceed", new Vector2((float)(tutorialProceedButton.Rectangle.X + 7), (float)tutorialProceedButton.Rectangle.Y), Color.Black);
                    break;
                case 3:
                    spriteBatch.DrawString(font, "This screen is shown whenever you finish a level.  From here, you can easily return", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "to the menu, play the current level again, or move on to the next level if you", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "won.  You can also see statistics related about the battle you just fought.  Click", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "on the Next Level button to continue the tutorial.", new Vector2(135f, 425f), Color.Black);
                    spriteBatch.DrawString(font, "", new Vector2(135f, 440f), Color.Black);
                    break;
                case 4:
                    spriteBatch.DrawString(font, "In real levels, enemies will appear to fight your soldiers and try to attack", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "your tower.  For this level, though, they will not hurt your tower.  You must", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "be ready for them.  Using your current gold, send out some soldiers and try to ", new Vector2(135f, 410f), Color.Black);
                    spriteBatch.DrawString(font, "win this level on your own. ", new Vector2(135f, 425f), Color.Black);
                    break;
                case 5:
                    spriteBatch.DrawString(font, "Congratulations!  You beat the level on your own!  Now you're ready for the", new Vector2(135f, 380f), Color.Black);
                    spriteBatch.DrawString(font, "real game.  Click the Menu button to go to the main menu and begin the fight", new Vector2(135f, 395f), Color.Black);
                    spriteBatch.DrawString(font, "against Lord Morgoroth!.", new Vector2(135f, 410f), Color.Black);
                    break;**/
            }
        }
    }
}