// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AnimatedThing
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class AnimatedThing
  {
    protected float timer = 0.0f;
    protected float interval = 125f;
    private int currentFrame;
    private int totalFrames;
    protected Texture2D texture;
    protected int rectangleX;
    protected int rectangleY;
    private int frameWidth;
    private int frameHeight;
    protected AnimationState state;

    public virtual void Update()
    {
    }

    public virtual void Update(GameTime gameTime, Vector2 position)
    {
    }

    public virtual void Update(GameTime gameTime)
    {
    }

    public virtual void SetUp()
    {
    }

    public virtual void SetDown()
    {
    }

    public virtual void SetRight()
    {
    }

    public virtual void SetLeft()
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
      Rectangle rectangle = new Rectangle(this.rectangleX, this.rectangleY, this.frameWidth, this.frameHeight);
      spriteBatch.Draw(this.texture, position, new Rectangle?(rectangle), Color.White);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
      Rectangle rectangle = new Rectangle(this.rectangleX, this.rectangleY, this.frameWidth, this.frameHeight);
      spriteBatch.Draw(this.texture, position, new Rectangle?(rectangle), color);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 target)
    {
    }

    public virtual void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
    {
      Rectangle rectangle1 = new Rectangle(this.rectangleX, this.rectangleY, this.frameWidth, this.frameHeight);
      spriteBatch.Draw(this.texture, rectangle, new Rectangle?(rectangle1), Color.White);
    }

    public virtual void Draw(SpriteBatch spriteBatch, Vector2 position, float directionRadians)
    {
    }

    public int CurrentFrame
    {
      get
      {
        return this.currentFrame;
      }
      set
      {
        this.currentFrame = value;
      }
    }

    public int FrameWidth
    {
      get
      {
        return this.frameWidth;
      }
      set
      {
        this.frameWidth = value;
      }
    }

    public int FrameHeight
    {
      get
      {
        return this.frameHeight;
      }
      set
      {
        this.frameHeight = value;
      }
    }

    public int TotalFrames
    {
      get
      {
        return this.totalFrames;
      }
      set
      {
        this.totalFrames = value;
      }
    }

    public AnimationState State
    {
      get
      {
        return this.state;
      }
    }

    public float Timer
    {
      get
      {
        return this.timer;
      }
      set
      {
        this.timer = value;
      }
    }
  }
}
