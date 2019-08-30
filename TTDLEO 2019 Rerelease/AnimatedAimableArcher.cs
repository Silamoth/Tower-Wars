// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedAimableArcher
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedAimableArcher : AnimatedSprite
  {
    private bool canMoveMore = true;
    private Vector2 target;

    public AnimatedAimableArcher(Texture2D texture, int frameWidth, int frameHeight)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth;
      this.target = new Vector2(420f, 160f);
    }

    public override void Update()
    {
      switch (this.CurrentFrame)
      {
        case 0:
          int frameWidth = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 1:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 2:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 3:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 4:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 5:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 6:
          this.rectangleX = this.FrameWidth * 6;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 7:
          this.rectangleX = this.FrameWidth * 7;
          this.rectangleY = this.FrameHeight * 8;
          break;
        case 8:
          this.rectangleX = this.FrameWidth * 8;
          this.rectangleY = this.FrameHeight * 8;
          break;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 target)
    {
      Vector2 vector2_1 = target - position;
      vector2_1.Normalize();
      float num1 = (float) Math.Atan2((double) vector2_1.X, -(double) vector2_1.Y);
      if ((double) num1 >= 3.0 * Math.PI / 8.0 && (double) num1 <= 3.0 * Math.PI / 4.0)
        this.target = target;
      Vector2 vector2_2 = this.target - position;
      vector2_2.Normalize();
      float num2 = (float) Math.Atan2((double) vector2_2.X, -(double) vector2_2.Y);
      spriteBatch.Draw(this.texture, position, new Rectangle?(new Rectangle(this.rectangleX, this.rectangleY, this.FrameWidth, this.FrameHeight)), Color.White, num2 - 1.570796f, new Vector2(0.0f, (float) (this.FrameHeight / 2)), 1f, SpriteEffects.None, 1f);
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
      Vector2 vector2 = this.target - position;
      vector2.Normalize();
      float num = (float) Math.Atan2((double) vector2.X, -(double) vector2.Y);
      spriteBatch.Draw(this.texture, position, new Rectangle?(new Rectangle(this.rectangleX, this.rectangleY, this.FrameWidth, this.FrameHeight)), Color.White, num - 1.570796f, new Vector2(0.0f, (float) (this.FrameHeight / 2)), 1f, SpriteEffects.None, 1f);
    }

    public override void SetAttackingRight()
    {
      this.CurrentFrame = 0;
      this.canMoveMore = true;
    }

    public override void UpdateRightAttack(GameTime gameTime)
    {
      if (!this.canMoveMore)
        return;
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer > (double) this.interval)
      {
        ++this.CurrentFrame;
        if (this.CurrentFrame < 0 || this.CurrentFrame > 3)
          this.canMoveMore = false;
        this.timer = 0.0f;
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
