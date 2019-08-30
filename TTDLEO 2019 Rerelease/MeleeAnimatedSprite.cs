// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.MeleeAnimatedSprite
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class MeleeAnimatedSprite : AnimatedSprite
  {
    private bool canChange = true;
    private float attackInterval;
    private Rectangle swordRectangle;

    public MeleeAnimatedSprite(
      Texture2D texture,
      int frameWidth,
      int frameHeight,
      float attackInterval)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.attackInterval = attackInterval / 6f;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth;
    }

    protected override void AnimateAttacks()
    {
      switch (this.CurrentFrame)
      {
        case 42:
          int frameWidth1 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 43:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 44:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 45:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 46:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 47:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 5;
          this.state = AnimationState.ATTACKINGUP;
          break;
        case 48:
          int frameWidth2 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 49:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 50:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 51:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 52:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 53:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 6;
          this.state = AnimationState.ATTACKINGLEFT;
          break;
        case 54:
          int frameWidth3 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 55:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 56:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 57:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 58:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 59:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 7;
          this.state = AnimationState.ATTACKINGDOWN;
          break;
        case 60:
          int frameWidth4 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
        case 61:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
        case 62:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
        case 63:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
        case 64:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
        case 65:
          this.rectangleX = this.FrameWidth * 5;
          this.rectangleY = this.FrameHeight * 8;
          this.state = AnimationState.ATTACKINGRIGHT;
          break;
      }
    }

    public override void SetAttackingDown()
    {
      this.CurrentFrame = 54;
    }

    public override void SetAttackingUp()
    {
      this.CurrentFrame = 42;
    }

    public override void SetAttackingLeft()
    {
      this.CurrentFrame = 48;
    }

    public override void SetAttackingRight()
    {
      this.CurrentFrame = 60;
    }

    public override void UpdateDownAttack(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.attackInterval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 54 || this.CurrentFrame > 59)
      {
        this.CurrentFrame = 54;
        this.canChange = false;
      }
      this.timer = 0.0f;
    }

    public override void UpdateUpAttack(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.attackInterval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 42 || this.CurrentFrame > 47)
      {
        this.CurrentFrame = 42;
        this.canChange = false;
      }
      this.timer = 0.0f;
    }

    public override void UpdateLeftAttack(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer < (double) this.attackInterval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 48 || this.CurrentFrame > 53)
      {
        this.CurrentFrame = 48;
        this.canChange = false;
      }
      this.timer = 0.0f;
    }

    public override void UpdateRightAttack(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer < (double) this.attackInterval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame < 60 || this.CurrentFrame > 65)
      {
        this.CurrentFrame = 60;
        this.canChange = false;
      }
      this.timer = 0.0f;
    }

    public bool CanChange
    {
      get
      {
        return this.canChange;
      }
      set
      {
        this.canChange = value;
      }
    }

    public float AttackInterval
    {
      get
      {
        return this.attackInterval;
      }
      set
      {
        this.attackInterval = value;
      }
    }
  }
}
