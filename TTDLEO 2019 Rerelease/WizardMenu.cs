// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.WizardMenu
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
    internal class WizardMenu
    {
        private Vector2 position;
        private Rectangle rectangle;
        private Texture2D texture;
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Button meteorButton;
        private Button lightningButton;
        private Button reviveButton;
        private Button summonDragonButton;
        private Action action;

        public WizardMenu(ContentManager content)
        {
            this.LoadContent(content);
            this.position = new Vector2(275f, 20f);

            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.texture.Width, this.texture.Height);
            this.meteorButton = new Button(new Rectangle((int)((double)this.position.X + 17.0), (int)((double)this.position.Y + 20.0), this.buttonTexture.Width, this.buttonTexture.Height), this.buttonTexture, content, "Meteor");
            this.lightningButton = new Button(new Rectangle((int)((double)this.position.X + 35.0 + (double)this.buttonTexture.Width), (int)((double)this.position.Y + 20.0), this.buttonTexture.Width, this.buttonTexture.Height), this.buttonTexture, content, "Lightning");
            this.reviveButton = new Button(new Rectangle((int)((double)this.position.X + 55.0 + (double)(this.buttonTexture.Width * 2)), (int)((double)this.position.Y + 20.0), this.buttonTexture.Width, this.buttonTexture.Height), this.buttonTexture, content, "Revive");
            this.summonDragonButton = new Button(new Rectangle((int)((double)this.position.X + 75.0 + (double)(this.buttonTexture.Width * 3)), (int)((double)this.position.Y + 20.0), this.buttonTexture.Width, this.buttonTexture.Height), this.buttonTexture, content, "Dragon");
        }

        public void Update(Rectangle mouseRectangle, float scaleX, float scaleY)
        {
            this.action = Action.NA;
            this.meteorButton.Update(mouseRectangle, scaleX, scaleY);
            this.lightningButton.Update(mouseRectangle, scaleX, scaleY);
            this.reviveButton.Update(mouseRectangle, scaleX, scaleY);
            this.summonDragonButton.Update(mouseRectangle, scaleX, scaleY);
            if (this.meteorButton.IsHovered)
            {
                if (this.meteorButton.IsActivated && Main.canClick)
                {
                    this.action = Action.METEOR;
                    Main.canClick = false;
                    Main.incrementButtonTimer = true;
                }
                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Unleash a meteor upon";
                Main.hoverTextLineTwo = "your enemies";
                Main.hoverTextLineThree = "Cost: 15 mana";
            }
            if (this.lightningButton.IsHovered)
            {
                if (this.lightningButton.IsActivated && Main.canClick)
                {
                    this.action = Action.LIGHTNING;
                    Main.canClick = false;
                    Main.incrementButtonTimer = true;
                }
                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Release a stream of";
                Main.hoverTextLineTwo = "lightning";
                Main.hoverTextLineThree = "Cost: 5 mana at intervals";
            }
            if (this.reviveButton.IsHovered)
            {
                if (this.reviveButton.IsActivated && Main.canClick)
                {
                    this.action = Action.REVIVE;
                    Main.canClick = false;
                    Main.incrementButtonTimer = true;
                }
                Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
                Main.hoverTextLineOne = "Revive fallen troops";
                Main.hoverTextLineTwo = "as skeletons";
                Main.hoverTextLineThree = "Cost: 40 mana";
            }
            if (!this.summonDragonButton.IsHovered)
                return;
            if (this.summonDragonButton.IsActivated && Main.canClick)
            {
                this.action = Action.DRAGON;
                Main.canClick = false;
                Main.incrementButtonTimer = true;
            }
            Main.hoverRectangle = new Rectangle((int)((double)mouseRectangle.X / (double)scaleX), (int)((double)(mouseRectangle.Y - 50) / (double)scaleY), 180, 75);
            Main.hoverTextLineOne = "Summon a dragon to";
            Main.hoverTextLineTwo = "help you fight";
            Main.hoverTextLineThree = "Cost: 120 mana";
        }

        public void Draw(SpriteBatch spriteBatch, float scaleX, float scaleY)
        {
            spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);

            this.meteorButton.Draw(spriteBatch);
            //spriteBatch.DrawString(this.font, "Meteor", new Vector2((float)(this.meteorButton.Rectangle.X + 22), (float)(this.meteorButton.Rectangle.Y + 5)), Color.White);

            this.lightningButton.Draw(spriteBatch);
            //spriteBatch.DrawString(this.font, "Lightning", new Vector2((float)(this.lightningButton.Rectangle.X + 20), (float)(this.lightningButton.Rectangle.Y + 5)), Color.White);

            this.reviveButton.Draw(spriteBatch);
            //spriteBatch.DrawString(this.font, "Revive", new Vector2((float)(this.reviveButton.Rectangle.X + 25), (float)(this.reviveButton.Rectangle.Y + 5)), Color.White);

            this.summonDragonButton.Draw(spriteBatch);
            //spriteBatch.DrawString(this.font, "Dragon", new Vector2((float)(this.summonDragonButton.Rectangle.X + 25), (float)(this.summonDragonButton.Rectangle.Y + 5)), Color.White);
        }

        private void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("wizardMenuStrip");
            this.buttonTexture = content.Load<Texture2D>("birchButton");
            this.font = content.Load<SpriteFont>("font");
        }

        public Action Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
        }
    }
}
