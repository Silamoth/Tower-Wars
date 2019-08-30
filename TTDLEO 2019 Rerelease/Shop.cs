using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
    class Shop
    {
        Button healthButton;
        Button turretOneButton;
        Button turretTwoButton;
        Button archerTowerButton;
        Button wizardButton;

        SoundEffect goldSound;

        public Shop(ContentManager content, Texture2D buttonTexture)
        {
            goldSound = content.Load<SoundEffect>("gold");

            healthButton = new Button(new Rectangle(25, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Health");
            turretOneButton = new Button(new Rectangle(175, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Turret One");
            turretTwoButton = new Button(new Rectangle(325, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Turret Two");
            archerTowerButton = new Button(new Rectangle(475, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Archer");
            wizardButton = new Button(new Rectangle(625, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Wizard");
        }

        public void Update(Rectangle mouseRectangle, float scaleX, float scaleY,
            bool canClick, int gold, BattleManager battleManager, ContentManager content)
        {
            archerTowerButton.Update(mouseRectangle, scaleX, scaleY);
            healthButton.Update(mouseRectangle, scaleX, scaleY);
            turretOneButton.Update(mouseRectangle, scaleX, scaleY);
            turretTwoButton.Update(mouseRectangle, scaleX, scaleY);
            wizardButton.Update(mouseRectangle, scaleX, scaleY);

            List<String> info = battleManager.GetSaveInfo();
            int playerTowerHealth;
            bool hasTurretOne, hasTurretTwo, hasWizardTower, hasArcher;

            int.TryParse(info[0], out playerTowerHealth);
            bool.TryParse(info[1], out hasTurretOne);
            bool.TryParse(info[2], out hasTurretTwo);
            bool.TryParse(info[3], out hasWizardTower);
            bool.TryParse(info[4], out hasArcher);

            if (healthButton.IsHovered)
            {
                if (playerTowerHealth < 100)
                {
                    if (healthButton.IsActivated && canClick && gold - 60 >= 0)
                    {
                        battleManager.RestoreHealth();
                        canClick = false;
                        gold -= 60;
                        goldSound.Play();
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Restore your tower to";
                    Main.hoverTextLineTwo = "maximum health";
                    Main.hoverTextLineThree = "Cost: 60 gold";
                }
                else
                {
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Restore your tower to";
                    Main.hoverTextLineTwo = "maximum health";
                    Main.hoverTextLineThree = "Not currently needed";
                }
            }
            if (turretOneButton.IsHovered)
            {
                if (!hasTurretOne)
                {
                    if (turretOneButton.IsActivated && canClick && gold - 100 >= 0)
                    {
                        canClick = false;
                        gold -= 100;
                        goldSound.Play();
                        battleManager.BuyTurretOne(content);
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Purchase a turret";
                    Main.hoverTextLineTwo = "to help fight off enemies";
                    Main.hoverTextLineThree = "Cost: 100 gold";
                }
                else
                {
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Turret 1:";
                    Main.hoverTextLineTwo = "Already purchased";
                    Main.hoverTextLineThree = "";
                }
            }
            if (turretTwoButton.IsHovered)
            {
                if (!hasTurretTwo)
                {
                    if (turretTwoButton.IsActivated && canClick && gold - 100 >= 0)
                    {
                        canClick = false;
                        gold -= 100;
                        goldSound.Play();
                        battleManager.BuyTurretTwo(content);

                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Purchase another";
                    Main.hoverTextLineTwo = "turret to help fight";
                    Main.hoverTextLineThree = "Cost: 100 gold";
                }
                else
                {
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Turret 2:";
                    Main.hoverTextLineTwo = "Already purchased";
                    Main.hoverTextLineThree = "";
                }
            }
            if (archerTowerButton.IsHovered)
            {
                if (!hasArcher)
                {
                    if (canClick && archerTowerButton.IsActivated && gold - 150 >= 0)
                    {
                        canClick = false;
                        gold -= 150;
                        goldSound.Play();
                        battleManager.BuyArcher(content);
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Purchase an archer";
                    Main.hoverTextLineTwo = "in a tower to fight";
                    Main.hoverTextLineThree = "Cost: 150 gold";
                }
                else
                {
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Archer Tower:";
                    Main.hoverTextLineTwo = "Already purchased";
                    Main.hoverTextLineThree = "Hit F to use";
                }
            }

            if (wizardButton.IsHovered)
            {
                if (!hasWizardTower)
                {
                    if (canClick && wizardButton.IsActivated && gold - 150 >= 0)
                    {
                        canClick = false;
                        gold -= 150;
                        goldSound.Play();
                        battleManager.BuyWizard(content);
                    }
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Purchase a wizard";
                    Main.hoverTextLineTwo = "in a tower to fight";
                    Main.hoverTextLineThree = "Cost: 150 gold";
                }
                else
                {
                    Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                    Main.hoverTextLineOne = "Wizard Tower:";
                    Main.hoverTextLineTwo = "Already purchased";
                    Main.hoverTextLineThree = "";
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            healthButton.Draw(spriteBatch);
            turretOneButton.Draw(spriteBatch);
            turretTwoButton.Draw(spriteBatch);
            archerTowerButton.Draw(spriteBatch);
            wizardButton.Draw(spriteBatch);
        }
    }
}