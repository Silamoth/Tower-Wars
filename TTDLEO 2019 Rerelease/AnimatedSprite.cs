// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedSprite
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedSprite : AnimatedThing
  {
    public AnimatedSprite(Texture2D texture, int frameWidth, int frameHeight)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth;
    }

    protected AnimatedSprite()
    {
    }

    public override void Update()
    {
      switch (this.CurrentFrame)
      {
        case 0:
          int frameWidth1 = this.FrameWidth;
          this.rectangleX = 0;
          int frameHeight1 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 1:
          this.rectangleX = this.FrameWidth;
          int frameHeight2 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 2:
          this.rectangleX = this.FrameWidth * 2;
          int frameHeight3 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 3:
          this.rectangleX = this.FrameWidth * 3;
          int frameHeight4 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 4:
          this.rectangleX = this.FrameWidth * 4;
          int frameHeight5 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 5:
          this.rectangleX = this.FrameWidth * 5;
          int frameHeight6 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 6:
          this.rectangleX = this.FrameWidth * 6;
          int frameHeight7 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 7:
          this.rectangleX = this.FrameWidth * 7;
          int frameHeight8 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 8:
          this.rectangleX = this.FrameWidth * 8;
          int frameHeight9 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.UP;
          break;
        case 9:
          int frameWidth2 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 10:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 11:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 12:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 13:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 14:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 15:
          this.rectangleX = this.FrameWidth * 6;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 16:
          this.rectangleX = this.FrameWidth * 7;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 17:
          this.rectangleX = this.FrameWidth * 8;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 18:
          int frameWidth3 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 19:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 20:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 21:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 22:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 23:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 24:
          this.rectangleX = this.FrameWidth * 6;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 25:
          this.rectangleX = this.FrameWidth * 7;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 26:
          this.rectangleX = this.FrameWidth * 8;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.DOWN;
          break;
        case 27:
          int frameWidth4 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 28:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 29:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 30:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 31:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 32:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 33:
          this.rectangleX = this.FrameWidth * 6;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 34:
          this.rectangleX = this.FrameWidth * 7;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 35:
          this.rectangleX = this.FrameWidth * 8;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.RIGHT;
          break;
        case 36:
          int frameWidth5 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
        case 37:
          this.rectangleX = this.FrameWidth;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
        case 38:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
        case 39:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
        case 40:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
        case 41:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleX = this.FrameHeight * 4;
          this.state = AnimationState.DYING;
          break;
      }
      this.AnimateAttacks();
    }

    public override void SetRight()
    {
      this.CurrentFrame = 27;
    }

    public void UpdateRight(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 27 || this.CurrentFrame > 35)
        this.CurrentFrame = 27;
      this.timer = 0.0f;
    }

    public override void SetLeft()
    {
      this.CurrentFrame = 9;
    }

    public void UpdateLeft(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 9 || this.CurrentFrame > 17)
        this.CurrentFrame = 9;
      this.timer = 0.0f;
    }

    public override void SetUp()
    {
      this.CurrentFrame = 0;
    }

    public void UpdateUp(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 0 || this.CurrentFrame > 8)
        this.CurrentFrame = 0;
      this.timer = 0.0f;
    }

    public override void SetDown()
    {
      this.CurrentFrame = 18;
    }

    public void UpdateDown(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 18 || this.CurrentFrame > 26)
        this.CurrentFrame = 18;
      this.timer = 0.0f;
    }

    protected virtual void AnimateAttacks()
    {
    }

    public virtual void SetAttackingRight()
    {
    }

    public virtual void SetAttackingLeft()
    {
    }

    public virtual void SetAttackingUp()
    {
    }

    public virtual void SetAttackingDown()
    {
    }

    public virtual void UpdateRightAttack(GameTime gameTime)
    {
    }

    public virtual void UpdateLeftAttack(GameTime gameTime)
    {
    }

    public virtual void UpdateUpAttack(GameTime gameTime)
    {
    }

    public virtual void UpdateDownAttack(GameTime gameTime)
    {
    }
  }
}
