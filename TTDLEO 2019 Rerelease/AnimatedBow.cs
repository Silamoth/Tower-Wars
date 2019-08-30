// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedBow
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedBow : AnimatedThing
  {
    private bool canMoveMore = true;

    public AnimatedBow(Texture2D texture, int frameWidth, int frameHeight)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.interval = 58f;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth * (texture.Height / frameHeight);
    }

    public override void Update(GameTime gameTime)
    {
      switch (this.CurrentFrame)
      {
        case 0:
          int frameWidth1 = this.FrameWidth;
          this.rectangleX = 0;
          int frameHeight1 = this.FrameHeight;
          this.rectangleY = 0;
          break;
        case 1:
          this.rectangleX = this.FrameWidth;
          int frameHeight2 = this.FrameHeight;
          this.rectangleY = 0;
          break;
        case 2:
          int frameWidth2 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight;
          break;
        case 3:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight;
          break;
        case 4:
          int frameWidth3 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 2;
          break;
        case 5:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 2;
          break;
        case 6:
          int frameWidth4 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 3;
          break;
        case 7:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 3;
          break;
        case 8:
          int frameWidth5 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 4;
          break;
        case 9:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 4;
          break;
        case 10:
          int frameWidth6 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 5;
          break;
        case 11:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 5;
          break;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 target)
    {
      Rectangle rectangle = new Rectangle(this.rectangleX, this.rectangleY, this.FrameWidth, this.FrameHeight);
      Vector2 vector2 = target - position;
      vector2.Normalize();
      float rotation = (float) Math.Atan2((double) vector2.X, -(double) vector2.Y);
      spriteBatch.Draw(this.texture, position, new Rectangle?(rectangle), Color.White, rotation, new Vector2(64f, 64f), 1f, SpriteEffects.None, 1f);
    }

    public void UpdateImage(GameTime gameTime)
    {
      if (!this.canMoveMore)
        return;
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer > (double) this.interval)
      {
        ++this.CurrentFrame;
        this.timer = 0.0f;
        if (this.CurrentFrame == this.TotalFrames)
          this.canMoveMore = false;
      }
    }

    public bool CanChange
    {
      get
      {
        return this.canMoveMore;
      }
      set
      {
        this.canMoveMore = value;
      }
    }
  }
}
