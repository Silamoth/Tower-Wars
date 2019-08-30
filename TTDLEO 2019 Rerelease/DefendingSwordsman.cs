// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.DefendingSwordsman
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTDLEO_2019_Rerelease
{
  internal class DefendingSwordsman
  {
    private bool isHit = false;
    private bool incrementHitTimer = false;
    private int hitTimer = 0;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private MeleeAnimatedSprite sprite;
    private KeyboardState oldKeyState;
    private int health;
    private SpriteFont font;
    private float attackInterval;
    private bool incrementAttackTimer;
    private float attackTimer;
    private bool canAttack;
    private bool isAttacking;

    public DefendingSwordsman(ContentManager content)
    {
      this.position = new Vector2(280f, 350f);
      this.attackInterval = 550f;
      this.LoadContent(content);
      this.health = 30;
      this.incrementAttackTimer = true;
      this.attackTimer = 0.0f;
      this.canAttack = false;
      this.isAttacking = false;
    }

    public void Update(GameTime gameTime, Rectangle enemyRectangle)
    {
      KeyboardState state = Keyboard.GetState();
      if (state.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetLeft();
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X - 16, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.X -= 6f;
      }
      else if (state.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateLeft(gameTime);
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X - 16, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.X -= 6f;
      }
      else if (state.IsKeyDown(Keys.D) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetRight();
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X + 20, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.X += 6f;
      }
      else if (state.IsKeyDown(Keys.D) && this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateRight(gameTime);
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X + 20, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.X += 6f;
      }
      else if (state.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetUp();
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.Y -= 6f;
      }
      else if (state.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateUp(gameTime);
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y, this.rectangle.Width, this.rectangle.Height)))
          this.position.Y -= 6f;
      }
      else if (state.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetDown();
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y + 4, this.rectangle.Width, this.rectangle.Height)))
          this.position.Y += 6f;
      }
      else if (state.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateDown(gameTime);
        if (!enemyRectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y + 4, this.rectangle.Width, this.rectangle.Height)))
          this.position.Y += 6f;
      }
      else if (state.IsKeyDown(Keys.Space) && this.canAttack)
      {
        switch (this.sprite.State)
        {
          case AnimationState.UP:
            this.sprite.SetAttackingUp();
            this.isAttacking = true;
            break;
          case AnimationState.DOWN:
            this.sprite.SetAttackingDown();
            this.isAttacking = true;
            break;
          case AnimationState.LEFT:
            this.sprite.SetAttackingLeft();
            this.isAttacking = true;
            break;
          case AnimationState.RIGHT:
            this.sprite.SetAttackingRight();
            this.isAttacking = true;
            break;
        }
      }
      this.oldKeyState = state;
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
      if (this.isAttacking)
      {
        switch (this.sprite.State)
        {
          case AnimationState.ATTACKINGUP:
            if (this.sprite.CanChange)
            {
              this.sprite.UpdateUpAttack(gameTime);
              break;
            }
            this.sprite.SetUp();
            this.sprite.CanChange = true;
            this.isAttacking = false;
            break;
          case AnimationState.ATTACKINGDOWN:
            if (this.sprite.CanChange)
            {
              this.sprite.UpdateDownAttack(gameTime);
              break;
            }
            this.sprite.SetDown();
            this.sprite.CanChange = true;
            this.isAttacking = false;
            break;
          case AnimationState.ATTACKINGLEFT:
            if (this.sprite.CanChange)
            {
              this.sprite.UpdateLeftAttack(gameTime);
              break;
            }
            this.sprite.SetLeft();
            this.sprite.CanChange = true;
            this.isAttacking = false;
            break;
          case AnimationState.ATTACKINGRIGHT:
            if (this.sprite.CanChange)
            {
              this.sprite.UpdateRightAttack(gameTime);
              break;
            }
            this.sprite.SetRight();
            this.sprite.CanChange = true;
            this.isAttacking = false;
            break;
        }
      }
      if (this.incrementHitTimer)
        ++this.hitTimer;
      if (this.hitTimer == 3)
      {
        this.incrementHitTimer = false;
        this.hitTimer = 0;
        this.isHit = false;
      }
      if (this.incrementAttackTimer)
        this.attackTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.attackTimer > (double) this.attackInterval)
      {
        this.incrementAttackTimer = false;
        this.attackTimer = 0.0f;
        this.canAttack = true;
      }
      this.sprite.Update();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (this.isHit)
        this.sprite.Draw(spriteBatch, this.position, Color.Red);
      else
        this.sprite.Draw(spriteBatch, this.position, Color.White);
      spriteBatch.DrawString(this.font, "Health: " + this.health.ToString(), new Vector2(this.position.X + 5f, this.position.Y - 10f), Color.Black);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("swordsmanSpritesheet");
      this.sprite = new MeleeAnimatedSprite(this.texture, 62, 55, this.attackInterval + 40f);
      this.font = content.Load<SpriteFont>("font");
    }

    public void Hit()
    {
      this.isHit = true;
      this.incrementHitTimer = true;
    }

    public void Attack()
    {
      this.canAttack = false;
      this.incrementAttackTimer = true;
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

    public AnimationState State
    {
      get
      {
        return this.sprite.State;
      }
    }

    public bool CanAttack
    {
      get
      {
        return this.canAttack && (this.sprite.CurrentFrame == 46 || this.sprite.CurrentFrame == 52 || this.sprite.CurrentFrame == 58 || this.sprite.CurrentFrame == 64);
      }
    }

    public int Damage
    {
      get
      {
        return 5;
      }
    }

    public bool IsAttacking
    {
      get
      {
        return this.isAttacking;
      }
    }
  }
}
