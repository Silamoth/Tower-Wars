// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.FinalMorgoroth
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  internal class FinalMorgoroth
  {
    private static Random random = new Random();
    private bool isHit = false;
    private bool incrementHitTimer = false;
    private int hitTimer = 0;
    private bool canShootFireball = false;
    private bool incrementFireballTimer = true;
    private int fireballTimer = -50;
    private bool canSummon = false;
    private bool incrementSummonTimer = true;
    private int summonTimer = 0;
    private bool isStunned = false;
    private bool incrementStunTimer = false;
    private int stunTimer = 0;
    private bool canTeleport = false;
    private bool incrementTeleportTimer = true;
    private bool isTeleporting = false;
    private int teleportTimer = 0;
    private bool canThrowBoulder = false;
    private bool incrementBoulderTimer = false;
    private int boulderTimer = 0;
    private bool canShootArrows = false;
    private bool incrementArrowTimer = true;
    private int arrowTimer = 0;
    private int arrowShootTimer = 0;
    private Texture2D texture;
    private MeleeAnimatedSprite sprite;
    private Vector2 position;
    private Rectangle rectangle;
    private int health;
    private SpriteFont font;
    private float attackInterval;
    private bool incrementAttackTimer;
    private float attackTimer;
    private bool canAttack;
    private bool isAttacking;
    private Meteor meteor;
    private List<AttackingEnemy> skeletons;
    private Vector2 oldPosition;
    private Vector2 teleportPosition;
    private int arrowTimeAtTeleport;
    private int meteorTimeAtTeleport;
    private Boulder boulder;
    private List<ArrowTrap> rightArrowTraps;
    private List<ArrowTrap> leftArrowTraps;
    private int shootCounter;

    public FinalMorgoroth(ContentManager content)
    {
      this.position = new Vector2(280f, 50f);
      this.attackInterval = 525f;
      this.health = 100;
      this.incrementAttackTimer = true;
      this.attackTimer = 0.0f;
      this.canAttack = false;
      this.isAttacking = false;
      this.sprite = new MeleeAnimatedSprite(content.Load<Texture2D>("morgorothSpritesheet"), 62, 55, this.attackInterval);
      this.font = content.Load<SpriteFont>(nameof (font));
      this.sprite.SetDown();
      this.meteor = new Meteor(content);
      this.meteor.MoveSpeed = 375f;
      this.skeletons = new List<AttackingEnemy>();
      this.boulder = new Boulder();
      this.rightArrowTraps = new List<ArrowTrap>();
      this.leftArrowTraps = new List<ArrowTrap>();
      this.rightArrowTraps.Add(new ArrowTrap(content, new Vector2(735f, 100f), true));
      this.rightArrowTraps.Add(new ArrowTrap(content, new Vector2(735f, 180f), true));
      this.rightArrowTraps.Add(new ArrowTrap(content, new Vector2(735f, 260f), true));
      this.rightArrowTraps.Add(new ArrowTrap(content, new Vector2(735f, 340f), true));
      this.rightArrowTraps.Add(new ArrowTrap(content, new Vector2(735f, 420f), true));
      this.leftArrowTraps.Add(new ArrowTrap(content, new Vector2(15f, 100f), false));
      this.leftArrowTraps.Add(new ArrowTrap(content, new Vector2(15f, 180f), false));
      this.leftArrowTraps.Add(new ArrowTrap(content, new Vector2(15f, 260f), false));
      this.leftArrowTraps.Add(new ArrowTrap(content, new Vector2(15f, 340f), false));
      this.leftArrowTraps.Add(new ArrowTrap(content, new Vector2(15f, 420f), false));
    }

    public void Update(
      GameTime gameTime,
      DefendingSwordsman player,
      int screenWidth,
      int screenHeight)
    {
      if (!this.isStunned)
      {
        if (this.canTeleport)
        {
          if (!this.boulder.IsActive && !this.meteor.IsActive && !this.canShootArrows)
          {
            this.oldPosition = this.position;
            this.position = new Vector2(this.teleportPosition.X, this.teleportPosition.Y + 45f);
            this.sprite.SetAttackingUp();
            this.isAttacking = true;
            this.canTeleport = false;
            this.isTeleporting = true;
          }
        }
        else if (this.rectangle.Intersects(player.Rectangle))
        {
          if (this.canAttack)
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
            this.canAttack = false;
            this.incrementAttackTimer = true;
          }
        }
        else if (this.canShootArrows)
        {
          ++this.arrowShootTimer;
          if (this.arrowShootTimer == 25)
          {
            bool flag = false;
            while (!flag)
            {
              if (FinalMorgoroth.random.Next(0, 2) == 0)
              {
                int index = FinalMorgoroth.random.Next(0, this.rightArrowTraps.Count);
                if (!this.rightArrowTraps[index].Arrow.IsActive)
                {
                  this.rightArrowTraps[index].Shoot(true);
                  flag = true;
                }
              }
              else
              {
                int index = FinalMorgoroth.random.Next(0, this.leftArrowTraps.Count);
                if (!this.leftArrowTraps[index].Arrow.IsActive)
                {
                  this.leftArrowTraps[index].Shoot(false);
                  flag = true;
                }
              }
            }
            this.arrowShootTimer = 0;
            ++this.shootCounter;
          }
          if (this.shootCounter == 5)
          {
            this.canShootArrows = false;
            this.incrementArrowTimer = true;
            this.fireballTimer -= 50;
            this.canShootFireball = false;
            this.incrementFireballTimer = true;
            this.teleportTimer -= 50;
            this.canTeleport = false;
            this.incrementTeleportTimer = true;
          }
        }
        else if (this.canShootFireball && this.teleportTimer <= 350)
        {
          if (!this.meteor.IsActive && (double) Vector2.Distance(this.position, player.Position) > 150.0 && !this.boulder.IsActive && !this.isTeleporting)
          {
            this.meteor.MoveSpeed = 500f;
            this.sprite.SetDown();
            this.meteor.ActivateBullet(new Vector2(this.position.X, (float) player.Rectangle.Y), new Vector2(this.position.X + (float) (this.sprite.FrameWidth / 2), this.position.Y + 30f));
            this.canShootFireball = false;
            this.incrementFireballTimer = true;
          }
        }
        else if (FinalMorgoroth.random.Next(0, 1000) == 0 && this.fireballTimer > 0 && (this.teleportTimer <= 350 && !this.meteor.IsActive) && (double) Vector2.Distance(this.position, player.Position) > 150.0 && !this.isTeleporting)
        {
          this.meteor.MoveSpeed = 500f;
          this.sprite.SetDown();
          this.meteor.ActivateBullet(new Vector2(this.position.X, (float) player.Rectangle.Y), new Vector2(this.position.X + (float) (this.sprite.FrameWidth / 2), this.position.Y + 30f));
          this.canShootFireball = false;
          this.incrementFireballTimer = true;
          this.fireballTimer -= 200;
        }
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
              if (this.isTeleporting)
              {
                this.isTeleporting = false;
                this.incrementTeleportTimer = true;
                this.position = this.oldPosition;
                this.sprite.SetDown();
                this.fireballTimer -= 45;
                this.canShootFireball = false;
                this.incrementFireballTimer = true;
                this.arrowTimer -= 120;
                this.canShootArrows = false;
                this.incrementArrowTimer = true;
              }
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
        this.sprite.Update();
      }
      if (this.incrementHitTimer)
        ++this.hitTimer;
      if (this.hitTimer == 3)
      {
        this.incrementHitTimer = false;
        this.hitTimer = 0;
        this.isHit = false;
      }
      if (this.incrementStunTimer)
        ++this.stunTimer;
      if (this.stunTimer == 180)
      {
        this.incrementStunTimer = false;
        this.stunTimer = 0;
        this.isStunned = false;
        this.fireballTimer = -55;
        this.canShootFireball = false;
        this.incrementFireballTimer = true;
        this.teleportTimer -= 80;
        this.canTeleport = false;
        this.incrementTeleportTimer = true;
        this.arrowTimer -= 100;
        this.canShootArrows = false;
        this.incrementArrowTimer = true;
      }
      if (!this.meteor.IsActive && !this.boulder.IsActive && !this.isTeleporting & !this.canShootArrows)
      {
        if (this.incrementAttackTimer)
          this.attackTimer += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
        if ((double) this.attackTimer > (double) this.attackInterval)
        {
          this.incrementAttackTimer = false;
          this.attackTimer = 0.0f;
          this.canAttack = true;
        }
        if (this.incrementFireballTimer)
          ++this.fireballTimer;
        if (this.fireballTimer == 180)
        {
          this.canShootFireball = true;
          this.incrementFireballTimer = false;
          this.fireballTimer = 0;
        }
        if (this.incrementTeleportTimer)
        {
          ++this.teleportTimer;
          if (this.teleportTimer == 280)
          {
            this.teleportPosition = player.Position;
            this.fireballTimer -= 50;
            this.arrowTimer -= 50;
          }
        }
        if (this.teleportTimer == 430)
        {
          this.canTeleport = true;
          this.incrementTeleportTimer = false;
          this.teleportTimer = 0;
        }
        if (this.incrementArrowTimer)
          ++this.arrowTimer;
        if (this.arrowTimer > 380 && this.teleportTimer <= 330)
        {
          this.canShootArrows = true;
          this.incrementArrowTimer = false;
          this.arrowTimer = 0;
          this.shootCounter = 0;
          this.teleportTimer -= 100;
          this.fireballTimer -= 50;
          this.boulderTimer -= 75;
          this.canTeleport = false;
          this.canShootFireball = false;
          this.incrementTeleportTimer = true;
          this.incrementFireballTimer = true;
        }
      }
      if (this.meteor.IsActive)
      {
        this.meteor.Update(gameTime);
        if (this.meteor.Rectangle.Intersects(new Rectangle(player.Rectangle.X - 20, player.Rectangle.Y + 45, player.Rectangle.Width + 10, player.Rectangle.Height)))
        {
          if (player.State == AnimationState.ATTACKINGUP)
          {
            this.meteor.Velocity *= -1f;
          }
          else
          {
            player.Health -= this.meteor.Damage;
            player.Hit();
            this.meteor.Kill();
          }
        }
        if (this.meteor.Rectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y + 100, this.rectangle.Width, this.rectangle.Height)) && !this.isStunned && (FinalMorgoroth.random.Next(0, 7) == 0 && this.canAttack))
        {
          this.sprite.SetAttackingDown();
          this.isAttacking = true;
          this.canAttack = false;
          this.incrementAttackTimer = true;
        }
        if (this.meteor.Rectangle.Intersects(new Rectangle(this.rectangle.X, this.rectangle.Y - 20, this.rectangle.Width, this.rectangle.Height)))
        {
          if (this.isAttacking)
          {
            if (!this.isStunned)
              this.meteor.Velocity *= -1f;
          }
          else
          {
            this.Stun();
            this.meteor.Kill();
          }
        }
      }
      if (this.boulder.IsActive)
      {
        this.boulder.Update(gameTime);
        if (this.boulder.Rectangle.Intersects(new Rectangle(player.Rectangle.X, player.Rectangle.Y + 45, player.Rectangle.Width, player.Rectangle.Height)))
        {
          player.Health -= this.meteor.Damage;
          player.Hit();
          this.boulder.Kill();
          this.position = this.oldPosition;
        }
        if ((double) this.boulder.Rectangle.Y > (double) player.Position.Y + 100.0)
        {
          this.boulder.Kill();
          this.position = this.oldPosition;
        }
      }
      for (int index = 0; index < this.skeletons.Count; ++index)
      {
        this.skeletons[index].Update(gameTime, player.Rectangle);
        if (this.skeletons[index].Rectangle.Intersects(player.Rectangle))
        {
          this.skeletons[index].SetAttacking(gameTime);
          if (this.skeletons[index].CanAttack)
          {
            player.Health -= this.skeletons[index].Damage;
            player.Hit();
            this.skeletons[index].Attack();
          }
          if (player.CanAttack)
          {
            switch (player.State)
            {
              case AnimationState.ATTACKINGUP:
                if (this.skeletons[index].Rectangle.Y < player.Rectangle.Y)
                {
                  this.skeletons.RemoveAt(index);
                  player.Attack();
                  break;
                }
                break;
              case AnimationState.ATTACKINGDOWN:
                if (this.skeletons[index].Rectangle.Y > player.Rectangle.Y)
                {
                  this.skeletons.RemoveAt(index);
                  player.Attack();
                  break;
                }
                break;
              case AnimationState.ATTACKINGLEFT:
                if (this.skeletons[index].Rectangle.X < player.Rectangle.X)
                {
                  this.skeletons.RemoveAt(index);
                  player.Attack();
                  break;
                }
                break;
              case AnimationState.ATTACKINGRIGHT:
                if (this.skeletons[index].Rectangle.X > player.Rectangle.X)
                {
                  this.skeletons.RemoveAt(index);
                  player.Attack();
                  break;
                }
                break;
            }
          }
        }
      }
      foreach (ArrowTrap rightArrowTrap in this.rightArrowTraps)
      {
        rightArrowTrap.Update(gameTime, screenWidth, screenHeight);
        if (rightArrowTrap.Arrow.IsActive && rightArrowTrap.Arrow.Rectangle.Intersects(player.Rectangle))
        {
          player.Health -= rightArrowTrap.Arrow.Damage;
          rightArrowTrap.Arrow.Kill();
        }
      }
      foreach (ArrowTrap leftArrowTrap in this.leftArrowTraps)
      {
        leftArrowTrap.Update(gameTime, screenWidth, screenHeight);
        if (leftArrowTrap.Arrow.IsActive && leftArrowTrap.Arrow.Rectangle.Intersects(player.Rectangle))
        {
          player.Health -= leftArrowTrap.Arrow.Damage;
          leftArrowTrap.Arrow.Kill();
        }
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (this.isHit)
        this.sprite.Draw(spriteBatch, this.position, Color.Red);
      else if (this.isStunned)
        this.sprite.Draw(spriteBatch, this.position, Color.PaleVioletRed);
      else if (this.teleportTimer > 350 && this.arrowTimer <= 300)
        this.sprite.Draw(spriteBatch, this.position, Color.LightBlue);
      else
        this.sprite.Draw(spriteBatch, this.position, Color.White);
      spriteBatch.DrawString(this.font, "Health: " + this.health.ToString(), new Vector2(this.position.X + 5f, this.position.Y - 10f), Color.Black);
      if (this.meteor.IsActive)
        this.meteor.Draw(spriteBatch);
      if (this.boulder.IsActive)
        this.boulder.Draw(spriteBatch);
      foreach (AttackingEnemy skeleton in this.skeletons)
        skeleton.Draw(spriteBatch);
      foreach (ArrowTrap rightArrowTrap in this.rightArrowTraps)
        rightArrowTrap.Draw(spriteBatch);
      foreach (ArrowTrap leftArrowTrap in this.leftArrowTraps)
        leftArrowTrap.Draw(spriteBatch);
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

    public void Stun()
    {
      this.isStunned = true;
      this.incrementStunTimer = true;
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

    public bool CanAttack
    {
      get
      {
        return this.canAttack;
      }
    }

    public AnimationState State
    {
      get
      {
        return this.sprite.State;
      }
    }

    public int Damage
    {
      get
      {
        return 5;
      }
    }
  }
}
