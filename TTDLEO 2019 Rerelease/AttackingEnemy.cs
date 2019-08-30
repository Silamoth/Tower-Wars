// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AttackingEnemy
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class AttackingEnemy
  {
    private static Random random = new Random();
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private MeleeAnimatedSprite sprite;
    private float attackTimer;
    private bool incrementAttackTimer;
    private bool canAttack;
    private float attackTime;
    private float speed;

    public AttackingEnemy(ContentManager content, Vector2 position)
    {
      this.LoadContent(content);
      this.position = position;
      this.speed = 1f;
      this.attackTimer = 0.0f;
      this.incrementAttackTimer = true;
      this.canAttack = false;
      this.attackTime = 500f;
    }

    public AttackingEnemy(ContentManager content)
    {
      this.LoadContent(content);
      this.position = AttackingEnemy.random.Next(0, 2) != 0 ? new Vector2(790f, (float) AttackingEnemy.random.Next(10, 590)) : new Vector2(20f, (float) AttackingEnemy.random.Next(10, 590));
      this.speed = 1f;
      this.attackTimer = 0.0f;
      this.incrementAttackTimer = true;
      this.canAttack = false;
      this.attackTime = 500f;
    }

    public void Update(GameTime gameTime, Rectangle playerPosition)
    {
      if (!this.rectangle.Intersects(playerPosition))
      {
        if ((double) this.position.X != (double) playerPosition.X)
        {
          if ((double) this.position.X > (double) playerPosition.X)
          {
            this.position.X -= this.speed;
            if (this.sprite.State != AnimationState.LEFT)
            {
              if (this.sprite.State == AnimationState.RIGHT)
                this.position.X += 5f;
              this.sprite.SetLeft();
            }
            else
              this.sprite.UpdateLeft(gameTime);
          }
          else
          {
            this.position.X += this.speed;
            if (this.sprite.State != AnimationState.RIGHT)
            {
              if (this.sprite.State == AnimationState.LEFT)
                this.position.X -= 5f;
              this.sprite.SetRight();
            }
            else
              this.sprite.UpdateRight(gameTime);
          }
        }
        else if ((double) this.position.Y > (double) playerPosition.Y)
        {
          this.position.Y -= this.speed;
          if (this.sprite.State != AnimationState.UP)
            this.sprite.SetUp();
          else
            this.sprite.UpdateUp(gameTime);
        }
        else
        {
          this.position.Y += this.speed;
          if (this.sprite.State != AnimationState.DOWN)
            this.sprite.SetDown();
          else
            this.sprite.UpdateDown(gameTime);
        }
      }
      this.sprite.Update();
      switch (this.sprite.State)
      {
        case AnimationState.UP:
        case AnimationState.DOWN:
        case AnimationState.LEFT:
          this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth, this.sprite.FrameHeight);
          break;
        case AnimationState.RIGHT:
          this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          break;
      }
      if (this.incrementAttackTimer)
        this.attackTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      if ((double) this.attackTimer <= (double) this.attackTime)
        return;
      this.incrementAttackTimer = false;
      this.canAttack = true;
      this.attackTimer = 0.0f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.sprite.Draw(spriteBatch, this.position);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("skeletonSpritesheet");
      this.sprite = new MeleeAnimatedSprite(this.texture, 62, 55, this.attackTime);
    }

    public void Attack()
    {
      this.canAttack = false;
      this.incrementAttackTimer = true;
    }

    private void SetRightAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGRIGHT)
        this.sprite.SetAttackingRight();
      else
        this.sprite.UpdateRightAttack(gameTime);
    }

    private void SetLeftAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGLEFT)
        this.sprite.SetAttackingLeft();
      else
        this.sprite.UpdateLeftAttack(gameTime);
    }

    private void SetUpAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGUP)
        this.sprite.SetAttackingUp();
      else
        this.sprite.UpdateUpAttack(gameTime);
    }

    private void SetDownAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGDOWN)
        this.sprite.SetAttackingDown();
      else
        this.sprite.UpdateDownAttack(gameTime);
    }

    public void SetAttacking(GameTime gameTime)
    {
      switch (this.sprite.State)
      {
        case AnimationState.UP:
        case AnimationState.ATTACKINGUP:
          this.SetUpAttack(gameTime);
          break;
        case AnimationState.DOWN:
        case AnimationState.ATTACKINGDOWN:
          this.SetDownAttack(gameTime);
          break;
        case AnimationState.LEFT:
        case AnimationState.ATTACKINGLEFT:
          this.SetLeftAttack(gameTime);
          break;
        case AnimationState.RIGHT:
        case AnimationState.ATTACKINGRIGHT:
          this.SetRightAttack(gameTime);
          break;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public int Damage
    {
      get
      {
        return 1;
      }
    }

    public bool CanAttack
    {
      get
      {
        return this.canAttack;
      }
    }
  }
}
