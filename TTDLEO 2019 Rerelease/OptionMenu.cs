using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
    class OptionMenu
    {
        Button increaseVolumeButton, decreaseVolumeButton, creditButton, fullScreenButton;
        Texture2D background;

        public OptionMenu(ContentManager content, Texture2D buttonTexture, Texture2D otherMenuBackground)
        {
            fullScreenButton = new Button(new Rectangle(25, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Full Screen");
            increaseVolumeButton = new Button(new Rectangle(150, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Volume +");
            decreaseVolumeButton = new Button(new Rectangle(275, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Volume -");
            creditButton = new Button(new Rectangle(400, 75, buttonTexture.Width, buttonTexture.Height), buttonTexture, content, "Credits");

            background = otherMenuBackground;
        }

        public bool[] Update(Rectangle mouseRectangle, bool canClick, bool canSwitchMenus, float scaleX, float scaleY)
        {
            //TODO: Get rid of scaleX and scaleY from Button
            fullScreenButton.Update(mouseRectangle, scaleX, scaleY);
            increaseVolumeButton.Update(mouseRectangle, scaleX, scaleY);
            decreaseVolumeButton.Update(mouseRectangle, scaleX, scaleY);
            creditButton.Update(mouseRectangle, scaleX, scaleY);

            bool[] output = new bool[4];

            if (fullScreenButton.IsHovered)
            {
                if (fullScreenButton.IsActivated && canClick)
                    output[0] = true;

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Toggle if the game runs";
                Main.hoverTextLineTwo = "in full screen or not";
                Main.hoverTextLineThree = "";
            }

            if (increaseVolumeButton.IsHovered)
            {
                if (increaseVolumeButton.IsActivated && canClick && (double)Main.volume < .9)
                    output[1] = true;

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Increase the overall";
                Main.hoverTextLineTwo = "volume of the game";
                Main.hoverTextLineThree = "";
            }

            if (decreaseVolumeButton.IsHovered)
            {
                if (decreaseVolumeButton.IsActivated && canClick && (double)Main.volume > 0.0)
                    output[2] = true;

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Decrease the overall";
                Main.hoverTextLineTwo = "volume of the game";
                Main.hoverTextLineThree = "";
            }

            if (creditButton.IsHovered)
            {
                if (creditButton.IsActivated && canSwitchMenus)
                    output[3] = true;

                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "See the game credits";
                Main.hoverTextLineTwo = "";
                Main.hoverTextLineThree = "";
            }

            return output;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            fullScreenButton.Draw(spriteBatch);
            increaseVolumeButton.Draw(spriteBatch);
            decreaseVolumeButton.Draw(spriteBatch);
            creditButton.Draw(spriteBatch);
        }
    }
}