// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Button
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TTDLEO_2019_Rerelease
{
    internal class Button
    {
        private bool canSound = true;
        private bool incrementSoundTimer = false;
        private int soundTimer = 0;
        private Rectangle originalRectangle;
        private Rectangle rectangle;
        private Texture2D texture;
        private bool isHovered;
        private bool isActivated;
        private SoundEffect click;

        String text;
        SpriteFont font;

        public Button(Rectangle rectangle, Texture2D texture, ContentManager content, String text)
        {
            this.originalRectangle = rectangle;
            this.texture = texture;
            this.isHovered = false;
            this.isActivated = false;
            this.click = content.Load<SoundEffect>("buttonClick");

            this.text = text;

            font = content.Load<SpriteFont>("font");
            //TODO: Make assets static and only load once
        }

        public void Update(Rectangle mouseRectangle, float scaleX, float scaleY)
        {
            this.rectangle = new Rectangle((int)(this.originalRectangle.X), (int)(this.originalRectangle.Y), (int)(this.originalRectangle.Width * 1), (int)(this.originalRectangle.Height * 1));
            this.isHovered = false;
            this.isActivated = false;
            MouseState state = Mouse.GetState();
            if (mouseRectangle.Intersects(this.rectangle))
                this.isHovered = true;
            if (this.isHovered && state.LeftButton == ButtonState.Pressed)
            {
                this.isActivated = true;
                if (this.canSound)
                {
                    this.click.Play();
                    this.canSound = false;
                    this.incrementSoundTimer = true;
                }
            }
            if (!this.incrementSoundTimer)
                return;
            ++this.soundTimer;
            if (this.soundTimer == 50)
            {
                this.incrementSoundTimer = false;
                this.soundTimer = 0;
                this.canSound = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.isHovered)
                spriteBatch.Draw(this.texture, new Rectangle((int)(originalRectangle.X), (int)(originalRectangle.Y), (int)(originalRectangle.Width), (int)(originalRectangle.Height)), Color.White);
            else
                spriteBatch.Draw(this.texture, new Rectangle((int)(originalRectangle.X), (int)(originalRectangle.Y), (int)(originalRectangle.Width), (int)(originalRectangle.Height)), new Color(175, 175, 175, (int)byte.MaxValue));

            //Vector2 textSize = font.MeasureString(text);

            spriteBatch.DrawString(font, text, new Vector2(originalRectangle.X + 10, originalRectangle.Y + 5), Color.Black);
        }

        public Rectangle Rectangle
        {
            get { return originalRectangle; }
        }

        public bool IsHovered
        {
            get { return isHovered; }
        }

        public bool IsActivated
        {
            get { return isActivated; }
        }

        public String Text
        {
            set { text = value; }
        }
    }
}
