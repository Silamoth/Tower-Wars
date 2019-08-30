// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedAnimal
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedAnimal : AnimatedThing
  {
    private bool isMoving = false;
    private float scale = 0.0f;

    public AnimatedAnimal(Texture2D texture, int frameWidth, int frameHeight, float scale)
    {
      this.texture = texture;
      this.FrameWidth = frameWidth;
      this.FrameHeight = frameHeight;
      this.scale = scale;
      this.rectangleX = 0;
      this.rectangleY = 0;
      this.CurrentFrame = 0;
      this.TotalFrames = texture.Width / frameWidth * (texture.Height / frameHeight);
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
          this.state = AnimationState.RIGHT;
          break;
        case 1:
          this.rectangleX = this.FrameWidth;
          int frameHeight2 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.RIGHT;
          break;
        case 2:
          this.rectangleX = this.FrameWidth * 2;
          int frameHeight3 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.RIGHT;
          break;
        case 3:
          this.rectangleX = this.FrameWidth * 3;
          int frameHeight4 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.RIGHT;
          break;
        case 4:
          this.rectangleX = this.FrameWidth * 4;
          int frameHeight5 = this.FrameHeight;
          this.rectangleY = 0;
          this.state = AnimationState.RIGHT;
          break;
        case 5:
          int frameWidth2 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 6:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 7:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 8:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 9:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight;
          this.state = AnimationState.LEFT;
          break;
        case 10:
          int frameWidth3 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.RIGHT;
          break;
        case 11:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.RIGHT;
          break;
        case 12:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.RIGHT;
          break;
        case 13:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.RIGHT;
          break;
        case 14:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 2;
          this.state = AnimationState.RIGHT;
          break;
        case 15:
          int frameWidth4 = this.FrameWidth;
          this.rectangleX = 0;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.LEFT;
          break;
        case 16:
          this.rectangleX = this.FrameWidth;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.LEFT;
          break;
        case 17:
          this.rectangleX = this.FrameWidth * 2;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.LEFT;
          break;
        case 18:
          this.rectangleX = this.FrameWidth * 3;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.LEFT;
          break;
        case 19:
          this.rectangleX = this.FrameWidth * 4;
          this.rectangleY = this.FrameHeight * 3;
          this.state = AnimationState.LEFT;
          break;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
      Rectangle rectangle = new Rectangle(this.rectangleX, this.rectangleY, this.FrameWidth, this.FrameHeight);
      spriteBatch.Draw(this.texture, position, new Rectangle?(rectangle), Color.White, 0.0f, Vector2.Zero, this.scale, SpriteEffects.None, 1f);
    }

    public override void SetRight()
    {
      this.CurrentFrame = 0;
    }

    public override void SetLeft()
    {
      this.CurrentFrame = 5;
    }

    public void SetMovingLeft()
    {
      this.CurrentFrame = 15;
      this.state = AnimationState.LEFT;
      this.isMoving = true;
    }

    public void SetMovingRight()
    {
      this.CurrentFrame = 3;
      this.state = AnimationState.RIGHT;
      this.isMoving = true;
    }

    public void UpdateRight(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame == 15)
      {
        this.CurrentFrame = 0;
        this.isMoving = false;
      }
      this.timer = 0.0f;
    }

    public void UpdateLeft(GameTime gameTime)
    {
      this.timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.timer <= (double) this.interval)
        return;
      ++this.CurrentFrame;
      if (this.CurrentFrame == 20)
      {
        this.CurrentFrame = 5;
        this.isMoving = false;
      }
      this.timer = 0.0f;
    }

    public bool IsMoving
    {
      get
      {
        return this.isMoving;
      }
    }
  }
}
