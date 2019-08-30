// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AimableArcher
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  internal class AimableArcher
  {
    private bool isActive = false;
    private bool canStartAgain = true;
    private bool incrementStartTimer = false;
    private int startTimer = 0;
    private Texture2D baseTexture;
    private Texture2D bowTexture;
    private Texture2D towerTexture;
    private Vector2 position;
    private Rectangle rectangle;
    private MouseState oldMouseState;
    private Arrow arrow;
    private AnimatedAimableArcher sprite;
    private RangedAnimatedSprite baseSprite;
    private AimableArcherIndicator indicator;

    public AimableArcher(ContentManager content)
    {
      this.arrow = new Arrow(content);
      this.position = new Vector2(150f, 265f);
      this.LoadContent(content);
      this.sprite.SetAttackingRight();
      this.baseSprite.SetAttackingRight();
      this.indicator = new AimableArcherIndicator(content, this.position, this.rectangle.Height);
    }

    public void Update(
      GameTime gameTime,
      ContentManager content,
      List<Soldier> enemies,
      List<RangedSoldier> rangedEnemies,
      int screenWidth,
      int screenHeight,
      float scaleX,
      float scaleY)
    {
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.baseTexture.Width, this.baseTexture.Height);
      MouseState state = Mouse.GetState();
      if (this.isActive)
      {
        if (this.baseSprite.CanChange && this.sprite.CanChange)
        {
          if (this.sprite.CurrentFrame == 0)
          {
            if (this.canStartAgain)
            {
              if (state.LeftButton == ButtonState.Pressed)
              {
                if (this.baseSprite.CanChange)
                  this.baseSprite.UpdateRightAttack(gameTime);
                if (this.sprite.CanChange)
                  this.sprite.UpdateRightAttack(gameTime);
              }
              else
              {
                this.sprite.SetAttackingRight();
                this.baseSprite.SetAttackingRight();
              }
            }
          }
          else if (state.LeftButton == ButtonState.Pressed)
          {
            if (this.baseSprite.CanChange)
              this.baseSprite.UpdateRightAttack(gameTime);
            if (this.sprite.CanChange)
              this.sprite.UpdateRightAttack(gameTime);
          }
          else
          {
            this.sprite.SetAttackingRight();
            this.baseSprite.SetAttackingRight();
          }
        }
        else
        {
          if (state.LeftButton == ButtonState.Released && this.oldMouseState.LeftButton == ButtonState.Pressed)
          {
            if (!this.arrow.IsActive)
            {
              if ((double) state.X / (double) scaleX >= (double) this.position.X)
              {
                Vector2 target = new Vector2((float) state.X / scaleX, (float) state.Y / scaleY);
                Vector2 position = new Vector2(this.position.X + 25f, this.position.Y + 20f);
                Vector2 vector2 = target - position;
                vector2.Normalize();
                float degrees = MathHelper.ToDegrees((float) Math.Atan2((double) vector2.X, -(double) vector2.Y));
                if ((double) degrees >= 45.0 && (double) degrees <= 150.0)
                {
                  this.arrow.ActivateBullet(target, position);
                }
                else
                {
                  this.sprite.SetAttackingRight();
                  this.baseSprite.SetAttackingRight();
                }
              }
              this.sprite.SetAttackingRight();
              this.baseSprite.SetAttackingRight();
            }
          }
          else if (this.oldMouseState.LeftButton == ButtonState.Released)
          {
            this.sprite.SetAttackingRight();
            this.baseSprite.SetAttackingRight();
          }
          this.canStartAgain = false;
          this.incrementStartTimer = true;
          this.indicator.SetActive(this.position, this.rectangle.Height);
        }
      }
      this.oldMouseState = state;
      this.sprite.Update();
      this.baseSprite.Update();
      if (this.arrow.IsActive)
      {
        this.arrow.Update(gameTime, screenWidth, screenHeight);
        foreach (Soldier enemy in enemies)
        {
          if (this.arrow.Rectangle.Intersects(new Rectangle(enemy.Rectangle.X, enemy.Rectangle.Y + 45, enemy.Rectangle.Width, enemy.Rectangle.Height)))
          {
            enemy.Health -= this.Damage;
            this.arrow.Kill();
            break;
          }
        }
        if (this.arrow.IsActive)
        {
          foreach (RangedSoldier rangedEnemy in rangedEnemies)
          {
            if (this.arrow.Rectangle.Intersects(new Rectangle(rangedEnemy.Rectangle.X, rangedEnemy.Rectangle.Y + 45, rangedEnemy.Rectangle.Width, rangedEnemy.Rectangle.Height)))
            {
              rangedEnemy.Health -= this.Damage;
              this.arrow.Kill();
              break;
            }
          }
        }
      }
      if (this.incrementStartTimer)
        ++this.startTimer;
      if (this.startTimer == 150)
      {
        this.canStartAgain = true;
        this.incrementStartTimer = false;
        this.startTimer = 0;
      }
      this.indicator.Update(this.position);
    }

    public void Draw(SpriteBatch spriteBatch, float scaleX, float scaleY)
    {
      MouseState state = Mouse.GetState();
      Vector2 target = new Vector2((float) state.X / scaleX, (float) state.Y / scaleY);
      spriteBatch.Draw(this.towerTexture, new Vector2(this.position.X, (float) ((double) this.position.Y + (double) this.baseSprite.FrameWidth - 10.0)), Color.White);
      this.baseSprite.Draw(spriteBatch, this.position, Color.White);
      if (this.isActive)
        this.sprite.Draw(spriteBatch, new Vector2(this.position.X + 25f, this.position.Y + 20f), target);
      else
        this.sprite.Draw(spriteBatch, new Vector2(this.position.X + 25f, this.position.Y + 20f));
      if (this.arrow.IsActive)
        this.arrow.Draw(spriteBatch);
      this.indicator.Draw(spriteBatch);
    }

    public void Activate()
    {
      this.isActive = !this.isActive;
    }

    private void LoadContent(ContentManager content)
    {
      this.baseTexture = content.Load<Texture2D>("aimableArcherBaseSpritesheet");
      this.bowTexture = content.Load<Texture2D>("aimableArcherBowSpritesheet");
      this.towerTexture = content.Load<Texture2D>("archerTower");
      this.sprite = new AnimatedAimableArcher(this.bowTexture, 62, 55);
      this.baseSprite = new RangedAnimatedSprite(this.baseTexture, 62, 55);
    }

    public Arrow Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    public int Damage
    {
      get
      {
        return 2;
      }
    }

    public bool IsActive
    {
      get
      {
        return this.isActive;
      }
    }
  }
}
