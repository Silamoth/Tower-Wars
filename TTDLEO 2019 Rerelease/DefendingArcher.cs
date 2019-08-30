// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.DefendingArcher
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class DefendingArcher
  {
    private bool isHit = false;
    private bool incrementHitTimer = false;
    private int hitTimer = 0;
    private int health = 5;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private RangedAnimatedSprite sprite;
    private KeyboardState oldKeyState;
    private Arrow arrow;
    private MouseState oldMouseState;
    private SpriteFont font;

    public DefendingArcher(ContentManager content)
    {
      this.LoadContent(content);
      this.arrow = new Arrow(content);
      this.position = new Vector2(280f, 240f);
    }

    public void Update(
      GameTime gameTime,
      ContentManager content,
      int screenWidth,
      int screenHeight,
      float scaleX,
      float scaleY)
    {
      KeyboardState state1 = Keyboard.GetState();
      MouseState state2 = Mouse.GetState();
      if (state1.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetLeft();
        if ((double) this.position.X - 4.0 > 0.0)
          this.position.X -= 4f;
      }
      else if (state1.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateLeft(gameTime);
        if ((double) this.position.X - 4.0 > 0.0)
          this.position.X -= 4f;
      }
      else if (state1.IsKeyDown(Keys.D) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetRight();
        if ((double) this.position.X + 4.0 < (double) screenWidth / (double) scaleX - (double) this.sprite.FrameWidth)
          this.position.X += 4f;
      }
      else if (state1.IsKeyDown(Keys.D) && this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateRight(gameTime);
        if ((double) this.position.X + 4.0 < (double) screenWidth / (double) scaleX - (double) this.sprite.FrameWidth)
          this.position.X += 4f;
      }
      else if (state1.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetUp();
        if ((double) this.position.Y - 4.0 > 0.0)
          this.position.Y -= 4f;
      }
      else if (state1.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateUp(gameTime);
        if ((double) this.position.Y - 4.0 > 0.0)
          this.position.Y -= 4f;
      }
      else if (state1.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetDown();
        if ((double) this.position.Y + 4.0 < (double) screenHeight / (double) scaleY - (double) this.sprite.FrameHeight)
          this.position.Y += 4f;
      }
      else if (state1.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateDown(gameTime);
        if ((double) this.position.Y + 4.0 < (double) screenHeight / (double) scaleY - (double) this.sprite.FrameHeight)
          this.position.Y += 4f;
      }
      if (this.sprite.CanChange)
      {
        if (state2.LeftButton == ButtonState.Pressed)
        {
          switch (this.sprite.State)
          {
            case AnimationState.UP:
              this.sprite.SetAttackingUp();
              break;
            case AnimationState.DOWN:
              this.sprite.SetAttackingDown();
              break;
            case AnimationState.LEFT:
              this.sprite.SetAttackingLeft();
              break;
            case AnimationState.RIGHT:
              this.sprite.SetAttackingRight();
              break;
            case AnimationState.ATTACKINGUP:
              this.sprite.UpdateUpAttack(gameTime);
              break;
            case AnimationState.ATTACKINGDOWN:
              this.sprite.UpdateDownAttack(gameTime);
              break;
            case AnimationState.ATTACKINGLEFT:
              if (this.sprite.CurrentFrame != 58)
              {
                this.sprite.UpdateLeftAttack(gameTime);
                break;
              }
              this.sprite.CanChange = false;
              break;
            case AnimationState.ATTACKINGRIGHT:
              if (this.sprite.CurrentFrame != 76)
              {
                this.sprite.UpdateRightAttack(gameTime);
                break;
              }
              this.sprite.CanChange = false;
              break;
          }
        }
        else if (state2.LeftButton == ButtonState.Released && this.oldMouseState.LeftButton == ButtonState.Pressed)
        {
          switch (this.sprite.State)
          {
            case AnimationState.ATTACKINGUP:
              this.sprite.SetUp();
              break;
            case AnimationState.ATTACKINGDOWN:
              this.sprite.SetDown();
              break;
            case AnimationState.ATTACKINGLEFT:
              this.sprite.SetLeft();
              break;
            case AnimationState.ATTACKINGRIGHT:
              this.sprite.SetRight();
              break;
          }
        }
      }
      else if (state2.LeftButton == ButtonState.Released && this.oldMouseState.LeftButton == ButtonState.Pressed && !this.arrow.IsActive)
      {
        switch (this.sprite.State)
        {
          case AnimationState.ATTACKINGUP:
            if ((double) state2.Y / (double) scaleY <= (double) this.position.Y)
            {
              Vector2 target = new Vector2((float) state2.X / scaleX, (float) state2.Y / scaleY);
              Vector2 position = new Vector2(this.position.X + (float) this.sprite.FrameWidth * 0.5f, this.position.Y);
              Vector2 vector2 = target - position;
              vector2.Normalize();
              if ((double) Math.Abs(MathHelper.ToDegrees((float) Math.Atan2((double) vector2.X, -(double) vector2.Y))) < 45.0)
                this.arrow.ActivateBullet(target, position);
            }
            this.sprite.SetUp();
            break;
          case AnimationState.ATTACKINGDOWN:
            if ((double) state2.Y / (double) scaleY >= (double) this.position.Y)
            {
              Vector2 target = new Vector2((float) state2.X / scaleX, (float) state2.Y / scaleY);
              Vector2 position = new Vector2(this.position.X + (float) this.sprite.FrameWidth * 0.5f, this.position.Y);
              Vector2 vector2 = target - position;
              vector2.Normalize();
              if ((double) Math.Abs(MathHelper.ToDegrees((float) Math.Atan2((double) vector2.X, -(double) vector2.Y))) > 135.0)
                this.arrow.ActivateBullet(target, position);
            }
            this.sprite.SetDown();
            break;
          case AnimationState.ATTACKINGLEFT:
            if ((double) state2.X / (double) scaleX <= (double) this.position.X)
            {
              Vector2 target = new Vector2((float) state2.X / scaleX, (float) state2.Y / scaleY);
              Vector2 position = new Vector2(this.position.X, this.position.Y + (float) this.sprite.FrameHeight * 0.5f);
              Vector2 vector2 = target - position;
              vector2.Normalize();
              float degrees = MathHelper.ToDegrees((float) Math.Atan2((double) vector2.X, -(double) vector2.Y));
              if ((double) degrees <= -45.0 && (double) degrees >= -135.0)
                this.arrow.ActivateBullet(target, position);
            }
            this.sprite.SetLeft();
            break;
          case AnimationState.ATTACKINGRIGHT:
            if ((double) state2.X / (double) scaleX >= (double) this.position.X)
            {
              Vector2 target = new Vector2((float) state2.X / scaleX, (float) state2.Y / scaleY);
              Vector2 position = new Vector2(this.position.X, this.position.Y + (float) this.sprite.FrameHeight * 0.5f);
              Vector2 vector2 = target - position;
              vector2.Normalize();
              float degrees = MathHelper.ToDegrees((float) Math.Atan2((double) vector2.X, -(double) vector2.Y));
              if ((double) degrees >= 45.0 && (double) degrees <= 135.0)
                this.arrow.ActivateBullet(target, position);
            }
            this.sprite.SetRight();
            break;
        }
        this.sprite.CanChange = true;
      }
      this.oldKeyState = state1;
      this.oldMouseState = state2;
      this.sprite.Update();
      if (this.arrow.IsActive)
        this.arrow.Update(gameTime, screenWidth, screenHeight);
      switch (this.sprite.State)
      {
        case AnimationState.UP:
        case AnimationState.DOWN:
        case AnimationState.LEFT:
          this.rectangle = new Rectangle((int) this.position.X + 30, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          break;
        case AnimationState.RIGHT:
          this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          break;
      }
      if (this.incrementHitTimer)
        ++this.hitTimer;
      if (this.hitTimer != 3)
        return;
      this.incrementHitTimer = false;
      this.hitTimer = 0;
      this.isHit = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (this.isHit)
        this.sprite.Draw(spriteBatch, this.position, Color.Red);
      else
        this.sprite.Draw(spriteBatch, this.position, Color.White);
      if (this.arrow.IsActive)
        this.arrow.Draw(spriteBatch);
      spriteBatch.DrawString(this.font, "Health: " + this.health.ToString(), new Vector2(this.position.X + 5f, this.position.Y - 10f), Color.Black);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("archerSpritesheet");
      this.sprite = new RangedAnimatedSprite(this.texture, 62, 55);
      this.font = content.Load<SpriteFont>("font");
    }

    public void Hit()
    {
      this.isHit = true;
      this.incrementHitTimer = true;
    }

    public Rectangle Position
    {
      get
      {
        return this.rectangle;
      }
    }

    public Arrow Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public int Health
    {
      get
      {
        return this.health;
      }
      set
      {
        this.health = value;
      }
    }
  }
}
