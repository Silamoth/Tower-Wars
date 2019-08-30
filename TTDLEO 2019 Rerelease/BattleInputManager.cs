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
    enum SoldierPurchases { COMMONER, TOUGHGUY, BRUTE, SWORDSMAN, ARCHER, MEDIC, GENERAL, THIEF, ROGUE }

    class BattleInputManager
    {
        Button commonerButton;
        Button toughGuyButton;
        Button bruteButton;
        Button swordsmanButton;
        Button archerButton;
        Button medicButton;
        Button generalButton;
        Button thiefButton;
        Button rogueButton;

        public BattleInputManager(Texture2D buttonTexture, ContentManager content)
        {
            commonerButton = new Button(new Rectangle(25, 35, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Commoner");
            toughGuyButton = new Button(new Rectangle(25, 70, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Tough Guy");
            bruteButton = new Button(new Rectangle(25, 105, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Brute");
            swordsmanButton = new Button(new Rectangle(25, 140, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Swordsman");
            archerButton = new Button(new Rectangle(25, 175, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Archer");
            medicButton = new Button(new Rectangle(150, 35, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Medic");
            generalButton = new Button(new Rectangle(150, 70, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "General");
            thiefButton = new Button(new Rectangle(150, 105, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Thief");
            rogueButton = new Button(new Rectangle(150, 140, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Rogue");
        }

        public List<SoldierPurchases> Update(Rectangle mouseRectangle, bool canClick, int gold, int queueCount, float scaleX, float scaleY)
        {
            commonerButton.Update(mouseRectangle, 1, 1);
            toughGuyButton.Update(mouseRectangle, 1, 1);
            bruteButton.Update(mouseRectangle, 1, 1);
            swordsmanButton.Update(mouseRectangle, 1, 1);
            archerButton.Update(mouseRectangle, 1, 1);
            medicButton.Update(mouseRectangle, 1, 1);
            generalButton.Update(mouseRectangle, 1, 1);
            thiefButton.Update(mouseRectangle, 1, 1);
            rogueButton.Update(mouseRectangle, 1, 1);

            List<SoldierPurchases> purchases = new List<SoldierPurchases>();

            if (commonerButton.IsHovered)
            {
                if (canClick && commonerButton.IsActivated && (gold - 10 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.COMMONER);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A common citizen who";
                Main.hoverTextLineTwo = "is willing to fight";
                Main.hoverTextLineThree = "Cost: 10 gold";
            }
            if (toughGuyButton.IsHovered)
            {
                if (canClick && toughGuyButton.IsActivated && (gold - 20 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.TOUGHGUY);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A strong citizen who is";
                Main.hoverTextLineTwo = "willing to fight";
                Main.hoverTextLineThree = "Cost: 20 gold";
            }
            if (bruteButton.IsHovered)
            {
                if (canClick && bruteButton.IsActivated && (gold - 40 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.BRUTE);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A savage that tears";
                Main.hoverTextLineTwo = "through enemies";
                Main.hoverTextLineThree = "Cost: 40 gold";
            }
            if (swordsmanButton.IsHovered)
            {
                if (canClick && swordsmanButton.IsActivated && (gold - 50 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.SWORDSMAN);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A strong and highly";
                Main.hoverTextLineTwo = "trained swordsman";
                Main.hoverTextLineThree = "Cost: 50 gold";
            }
            if (archerButton.IsHovered)
            {
                if (canClick && archerButton.IsActivated && (gold - 30 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.ARCHER);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "An archer with deadly";
                Main.hoverTextLineTwo = "accurate aim";
                Main.hoverTextLineThree = "Cost: 30 gold";
            }
            if (medicButton.IsHovered)
            {
                if (canClick && medicButton.IsActivated && (gold - 50 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.MEDIC);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A very skilled doctor";
                Main.hoverTextLineTwo = "who can heal soldiers";
                Main.hoverTextLineThree = "Cost: 50 gold";
            }
            if (generalButton.IsHovered)
            {
                if (canClick && generalButton.IsActivated && (gold - 60 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.GENERAL);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A skilled tactician who";
                Main.hoverTextLineTwo = "directs soldiers";
                Main.hoverTextLineThree = "Cost: 60 gold";
            }
            if (thiefButton.IsHovered)
            {
                if (canClick && thiefButton.IsActivated && (gold - 15 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.THIEF);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A former thief who can";
                Main.hoverTextLineTwo = "move quickly";
                Main.hoverTextLineThree = "Cost: 15 gold";
            }
            if (rogueButton.IsHovered)
            {
                if (canClick && rogueButton.IsActivated && (gold - 20 >= 0 && queueCount < 24))
                    purchases.Add(SoldierPurchases.ROGUE);

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "A rogue who can move";
                Main.hoverTextLineTwo = "very quickly";
                Main.hoverTextLineThree = "Cost: 20 gold";
            }

            return purchases;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            commonerButton.Draw(spriteBatch);
            toughGuyButton.Draw(spriteBatch);
            bruteButton.Draw(spriteBatch);
            swordsmanButton.Draw(spriteBatch);
            archerButton.Draw(spriteBatch);
            medicButton.Draw(spriteBatch);
            generalButton.Draw(spriteBatch);
            thiefButton.Draw(spriteBatch);
            rogueButton.Draw(spriteBatch);
        }
    }
}