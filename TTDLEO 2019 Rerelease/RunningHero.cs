// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.RunningHero
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
  internal class RunningHero
  {
    private bool isJumping = false;
    private int yChange = 0;
    private bool canJump = true;
    private bool incrementJumpTimer = false;
    private int jumpTimer = 0;
    private bool isMoving = true;
    private int score = 0;
    private int scoreTimer = 0;
    private int jumpCount = 0;
    private int jumpCoolDown = 21;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private AnimatedSprite sprite;
    private Rectangle collisionRectangle;
    private SpriteFont font;
    private KeyboardState oldState;

    public RunningHero(ContentManager content)
    {
      this.LoadContent(content);
      this.sprite = new AnimatedSprite(this.texture, 32, 32);
      this.position = new Vector2(650f, (float) (400 - this.texture.Height));
      this.sprite.SetRight();
    }

    public void Update(GameTime gameTime, Rectangle floorRectangle)
    {
      this.sprite.Update();
      KeyboardState state = Keyboard.GetState();
      if (this.isMoving)
        this.position.X += 5f;
      this.sprite.UpdateRight(gameTime);
      if (!this.isJumping)
      {
        if (state.IsKeyDown(Keys.Space))
          this.Jump();
      }
      else
      {
        this.position.Y += (float) this.yChange;
        switch (this.yChange)
        {
          case -24:
            this.yChange = 4;
            break;
          case -22:
            this.yChange = -24;
            break;
          case -20:
            this.yChange = -22;
            break;
          case -18:
            this.yChange = -20;
            break;
          case -16:
            this.yChange = -18;
            break;
          case -14:
            this.yChange = -16;
            break;
          case -12:
            this.yChange = -14;
            break;
          case -8:
            this.yChange = -3;
            break;
          case -6:
            this.yChange = -8;
            break;
          case -4:
            this.yChange = -6;
            break;
          case -3:
            this.yChange = -12;
            break;
          case 3:
            this.yChange = 12;
            break;
          case 4:
            this.yChange = 6;
            break;
          case 6:
            this.yChange = 8;
            break;
          case 8:
            this.yChange = 3;
            break;
          case 12:
            this.yChange = 14;
            break;
          case 14:
            this.yChange = 16;
            break;
          case 16:
            this.yChange = 18;
            break;
          case 18:
            this.yChange = 20;
            break;
          case 20:
            this.yChange = 22;
            break;
          case 22:
            this.yChange = 24;
            break;
          case 24:
            this.isJumping = false;
            break;
        }
        if (state.IsKeyDown(Keys.Space) && this.oldState.IsKeyDown(Keys.Space))
        {
          ++this.jumpCount;
          if (this.jumpCount == 180)
          {
            this.isJumping = false;
            this.canJump = false;
            this.incrementJumpTimer = true;
            this.jumpCoolDown = 300;
            this.jumpCount = 0;
          }
          Console.WriteLine(this.jumpCount);
        }
        else
          this.jumpCount = 0;
      }
      if (this.incrementJumpTimer)
      {
        ++this.jumpTimer;
        if (this.jumpTimer == this.jumpCoolDown)
        {
          this.canJump = true;
          this.incrementJumpTimer = false;
          this.jumpTimer = 0;
          if (this.jumpCoolDown > 21)
            this.jumpCoolDown = 21;
        }
      }
      if (!this.isMoving)
        this.UpdateCollisions();
      ++this.scoreTimer;
      if (this.scoreTimer == 30)
      {
        this.scoreTimer = 0;
        ++this.score;
      }
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth, this.sprite.FrameHeight);
      this.oldState = state;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.sprite.Draw(spriteBatch, this.rectangle);
    }

    public void DrawScore(SpriteBatch spriteBatch)
    {
      spriteBatch.DrawString(this.font, "Score: " + this.score.ToString(), new Vector2(350f, 3f), Color.White);
    }

    public void Collide(Rectangle obstacleRectangle)
    {
      this.isMoving = false;
      this.collisionRectangle = obstacleRectangle;
      if (this.isJumping)
        return;
      this.position.Y = 368f;
    }

    private void Jump()
    {
      if (!this.canJump)
        return;
      this.yChange = -4;
      this.isJumping = true;
      this.canJump = false;
      this.incrementJumpTimer = true;
    }

    private void UpdateCollisions()
    {
      if (this.rectangle.Intersects(this.collisionRectangle))
        return;
      this.isMoving = true;
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("heroSpritesheet");
      this.font = content.Load<SpriteFont>("font");
    }

    public Vector2 Position
    {
      get
      {
        return this.position;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public bool IsJumping
    {
      get
      {
        return this.isJumping;
      }
    }

    public int Score
    {
      get
      {
        return this.score;
      }
    }
  }
}
