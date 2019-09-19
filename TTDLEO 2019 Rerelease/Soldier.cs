// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Soldier
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace TTDLEO_2019_Rerelease
{
  internal abstract class Soldier : Queueable
  {
    private bool isMovingForward = true;
    private float attackTimer = 0.0f;
    private bool incrementAttackTimer = false;
    private bool canAttack = true;
    private bool isHit = false;
    private bool incrementHitTimer = false;
    private int hitTimer = 0;
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private MeleeAnimatedSprite sprite;
    private int health;
    private int damage;
    private float speed;
    private float attackTime;
    private Vector2 target;
    private SpriteFont font;
    private int reward;
    private String name;
    private SoundEffect swordSwingSound;

    public void Update(
      GameTime gameTime,
      List<Soldier> soldiers,
      List<RangedSoldier> rangedSoldiers,
      List<SupportSoldier> supportSoldiers)
    {
      List<Soldier> soldierList = soldiers;
      List<RangedSoldier> rangedSoldierList = rangedSoldiers;
      List<SupportSoldier> supportSoldierList = supportSoldiers;
      bool flag = true;
      if (this.isMovingForward)
      {
        if ((double) this.position.X < (double) this.target.X)
        {
          foreach (Soldier soldier in soldierList)
          {
            if (new Rectangle((int) ((double) this.position.X + (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(soldier.Rectangle) && soldier != this)
            {
              flag = false;
              break;
            }
          }
          foreach (RangedSoldier rangedSoldier in rangedSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X + (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(rangedSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          foreach (SupportSoldier supportSoldier in supportSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X + (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(supportSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          if (flag && (double) this.position.X + (double) this.speed <= (double) this.target.X)
          {
            this.position.X += this.speed;
            if (this.sprite.State != AnimationState.RIGHT)
              this.sprite.SetRight();
            else
              this.sprite.UpdateRight(gameTime);
            this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          }
        }
        else if ((double) this.position.X > (double) this.target.X)
        {
          foreach (Soldier soldier in soldierList)
          {
            if (new Rectangle((int) ((double) this.position.X - (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(soldier.rectangle) && soldier != this)
            {
              flag = false;
              break;
            }
          }
          foreach (RangedSoldier rangedSoldier in rangedSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X - (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(rangedSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          foreach (SupportSoldier supportSoldier in supportSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X - (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(supportSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          if (flag && (double) this.position.X - (double) this.speed >= (double) this.target.X)
          {
            this.position.X -= this.speed;
            if (this.sprite.State != AnimationState.LEFT)
              this.sprite.SetLeft();
            else
              this.sprite.UpdateLeft(gameTime);
            this.rectangle = new Rectangle((int) this.position.X + 30, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          }
        }
      }
      else if (flag)
      {
        if (this.incrementAttackTimer)
          this.attackTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        if ((double) this.attackTimer > (double) this.attackTime)
        {
          this.incrementAttackTimer = false;
          this.canAttack = true;
          this.attackTimer = 0.0f;
        }
      }
      if ((double) this.position.X == (double) this.target.X)
        this.isMovingForward = false;
      this.sprite.Update();
      if (this.incrementHitTimer)
        ++this.hitTimer;
      if (this.hitTimer != 3)
        return;
      this.incrementHitTimer = false;
      this.hitTimer = 0;
      this.isHit = false;
    }

    public void Update(
      GameTime gameTime,
      List<Soldier> soldiers,
      List<RangedSoldier> rangedSoldiers)
    {
      List<Soldier> soldierList = soldiers;
      List<RangedSoldier> rangedSoldierList = rangedSoldiers;
      bool flag = true;
      if (this.isMovingForward)
      {
        if ((double) this.position.X < (double) this.target.X)
        {
          foreach (Soldier soldier in soldierList)
          {
            if (new Rectangle((int) ((double) this.position.X + (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(soldier.Rectangle) && soldier != this)
            {
              flag = false;
              break;
            }
          }
          foreach (RangedSoldier rangedSoldier in rangedSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X + (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(rangedSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          if (flag && (double) this.position.X + (double) this.speed <= (double) this.target.X)
          {
            this.position.X += this.speed;
            if (this.sprite.State != AnimationState.RIGHT)
              this.sprite.SetRight();
            else
              this.sprite.UpdateRight(gameTime);
            this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          }
        }
        else if ((double) this.position.X > (double) this.target.X)
        {
          foreach (Soldier soldier in soldierList)
          {
            if (new Rectangle((int) ((double) this.position.X - (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(soldier.rectangle) && soldier != this)
            {
              flag = false;
              break;
            }
          }
          foreach (RangedSoldier rangedSoldier in rangedSoldierList)
          {
            if (new Rectangle((int) ((double) this.position.X - (double) this.speed), (int) this.position.Y, this.rectangle.Width, this.rectangle.Height).Intersects(rangedSoldier.Rectangle))
            {
              flag = false;
              break;
            }
          }
          if (flag && (double) this.position.X - (double) this.speed >= (double) this.target.X)
          {
            this.position.X -= this.speed;
            if (this.sprite.State != AnimationState.LEFT)
              this.sprite.SetLeft();
            else
              this.sprite.UpdateLeft(gameTime);
            this.rectangle = new Rectangle((int) this.position.X + 30, (int) this.position.Y, this.sprite.FrameWidth - 30, this.sprite.FrameHeight);
          }
        }
      }
      else
      {
        if (this.incrementAttackTimer)
          this.attackTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        if ((double) this.attackTimer > (double) this.attackTime)
        {
          this.incrementAttackTimer = false;
          this.canAttack = true;
          this.attackTimer = 0.0f;
        }
      }
      if ((double) this.position.X == (double) this.target.X)
        this.isMovingForward = false;
      this.sprite.Update();
      if (this.incrementHitTimer)
        ++this.hitTimer;
      if (this.hitTimer != 3)
        return;
      this.incrementHitTimer = false;
      this.hitTimer = 0;
      this.isHit = false;
    }

    public void Draw(SpriteBatch spriteBatch, bool isLeft, Color color)
    {
      if (this.isHit)
        this.sprite.Draw(spriteBatch, this.position, Color.Red);
      else
        this.sprite.Draw(spriteBatch, this.position, Color.White);
      Vector2 position = !isLeft ? new Vector2(this.position.X + 45f, this.position.Y - 50f) : new Vector2(this.position.X + 5f, this.position.Y - 50f);
      spriteBatch.DrawString(this.font, this.health.ToString(), position, color);
    }

    public void LoadSprite(Texture2D texture, ContentManager content)
    {
      this.sprite = new MeleeAnimatedSprite(texture, 62, 55, this.attackTime);
      this.font = content.Load<SpriteFont>("font");
    }

    public virtual void LoadContent(ContentManager content)
    {
    }

    public void Hit()
    {
      this.isHit = true;
      this.incrementHitTimer = true;
    }

    public void SetRightAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGRIGHT)
        this.sprite.SetAttackingRight();
      else
        this.sprite.UpdateRightAttack(gameTime);
    }

    public void SetLeftAttack(GameTime gameTime)
    {
      if (this.sprite.State != AnimationState.ATTACKINGLEFT)
        this.sprite.SetAttackingLeft();
      else
        this.sprite.UpdateLeftAttack(gameTime);
    }

    public void SoundSwing()
    {
    }

    public Vector2 Position
    {
      get
      {
        return this.position;
      }
      set
      {
        this.position = value;
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

    public int Damage
    {
      get
      {
        return this.damage;
      }
      set
      {
        this.damage = value;
      }
    }

    public float Speed
    {
      get
      {
        return this.speed;
      }
      set
      {
        this.speed = value;
      }
    }

    public float AttackTime
    {
      get
      {
        return this.attackTime;
      }
      set
      {
        this.attackTime = value;
      }
    }

    public bool IsMovingForward
    {
      get
      {
        return this.isMovingForward;
      }
      set
      {
        this.isMovingForward = value;
      }
    }

    public bool CanAttack
    {
      get
      {
        return this.canAttack;
      }
      set
      {
        this.canAttack = value;
      }
    }

    public bool IncrementAttackTimer
    {
      set
      {
        this.incrementAttackTimer = value;
      }
    }

    public Texture2D Texture
    {
      get
      {
        return this.texture;
      }
      set
      {
        this.texture = value;
      }
    }

    public Vector2 Target
    {
      get
      {
        return this.target;
      }
      set
      {
        this.target = value;
      }
    }

    public int Reward
    {
      get
      {
        return this.reward;
      }
      set
      {
        this.reward = value;
      }
    }

    public String Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public float AttackTimer
    {
        get { return attackTimer; }
      set
      {
        this.attackTimer = value;
      }
    }
  }
}
