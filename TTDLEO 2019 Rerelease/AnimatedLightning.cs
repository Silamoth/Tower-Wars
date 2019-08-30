// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedLightning
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedLightning : AnimatedThing
  {
    public AnimatedLightning(Texture2D texture, int frameWidth, int frameHeight)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth;
      this.interval = 60f;
    }

    public override void Update(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer > (double) this.interval)
      {
        ++this.CurrentFrame;
        if (this.CurrentFrame > this.TotalFrames)
          this.CurrentFrame = 0;
        this.timer = 0.0f;
      }
      switch (this.CurrentFrame)
      {
        case 0:
          this.rectangleX = 0;
          break;
        case 1:
          this.rectangleX = this.FrameWidth;
          break;
        case 2:
          this.rectangleX = this.FrameWidth * 2;
          break;
        case 3:
          this.rectangleX = this.FrameWidth * 3;
          break;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
    {
      Rectangle rectangle1 = new Rectangle(this.rectangleX, this.rectangleY, this.FrameWidth, this.FrameHeight);
      spriteBatch.Draw(this.texture, rectangle, new Rectangle?(rectangle1), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1f);
    }
  }
}
