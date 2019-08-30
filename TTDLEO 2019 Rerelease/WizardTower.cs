// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.WizardTower
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  internal class WizardTower
  {
    private static Random random = new Random();
    private int mana = 100;
    private int manaTimer = 0;
    private Aiming aiming = Aiming.NA;
    private Texture2D texture;
    private Vector2 position;
    private SpriteFont font;
    private Texture2D meteorAimingTexture;
    private Meteor meteor;
    private WizardMenu menu;
    private Lightning lightning;
    private Texture2D lightningAimingTexture;
    private Dragon dragon;
    private List<Zombie> toBeAdded;
    private SoundEffect lightningEffect;
    private SoundEffectInstance instance;

    public WizardTower(ContentManager content, Vector2 position)
    {
      this.LoadContent(content);
      this.position = position;
      this.position.Y -= (float) ((double) this.texture.Height * 0.12 - 70.0);
      this.position.X += 25f;
      this.meteor = new Meteor(content);
      this.menu = new WizardMenu(content);
      this.lightning = new Lightning(content, new Vector2((float) ((double) position.X + (double) (this.texture.Width / 2) + 75.0), (float) ((double) position.Y + (double) (this.texture.Height / 2) - 15.0)));
      this.dragon = new Dragon(content);
      this.toBeAdded = new List<Zombie>();
    }

    public void Update(
      ContentManager content,
      GameTime gameTime,
      List<Soldier> enemySoldiers,
      List<RangedSoldier> rangedEnemySoldiers,
      Vector2 enemyTowerPosition,
      ref List<Corpse> corpses,
      ref List<Queueable> playerQueue,
      Vector2 playerTowerPosition,
      bool canClick,
      Rectangle mouseRectangle,
      float scaleX,
      float scaleY,
      ref Queue queue)
    {
      if (canClick)
      {
        this.menu.Update(mouseRectangle, scaleX, scaleY);
        switch (this.menu.Action)
        {
          case Action.METEOR:
            this.ShootMeteor();
            break;
          case Action.LIGHTNING:
            this.aiming = this.aiming != Aiming.LIGHTNING ? Aiming.LIGHTNING : Aiming.NA;
            break;
          case Action.REVIVE:
            if (this.mana - 40 >= 0)
            {
              this.ReviveCorpses(ref corpses, ref playerQueue, enemyTowerPosition, content, playerTowerPosition, ref queue);
              this.mana -= 40;
              this.menu.Action = Action.NA;
              break;
            }
            break;
          case Action.DRAGON:
            if (this.mana - 120 >= 0)
            {
              this.SummonDragon(content);
              this.mana -= 120;
              break;
            }
            break;
        }
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
          this.aiming = Aiming.NA;
      }
      if (this.meteor.IsActive)
        this.meteor.Update(gameTime);
      ++this.manaTimer;
      if (this.manaTimer == 250)
      {
        this.mana += 5;
        this.manaTimer = 0;
      }
      if (this.aiming == Aiming.METEOR)
      {
        if (Main.canClick && (Mouse.GetState().LeftButton == ButtonState.Pressed && this.menu.Action == Action.NA))
        {
          this.aiming = Aiming.NA;
          this.meteor.ActivateBullet(new Vector2((float) Mouse.GetState().X / scaleX, 63f / scaleY), new Vector2((float) Mouse.GetState().X / scaleX, 0.0f));
          this.mana -= 15;
        }
      }
      else if (this.aiming == Aiming.LIGHTNING)
      {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
          if (this.menu.Action == Action.NA)
          {
            ++this.lightning.ManaTimer;
            if (this.lightning.ManaTimer == 45)
            {
              if (this.mana - 3 >= 0)
              {
                this.mana -= 5;
              }
              else
              {
                this.mana -= 5;
                this.aiming = Aiming.NA;
              }
              this.lightning.ManaTimer = 0;
            }
          }
          if (this.lightningEffect == null)
            this.lightningEffect = content.Load<SoundEffect>("lightningEffect");
          if (this.instance == null)
          {
            this.instance = this.lightningEffect.CreateInstance();
            this.instance.IsLooped = true;
            this.instance.Volume = Main.volume;
          }
          this.instance.Play();
        }
        else
          this.instance.Stop();
        this.lightning.Update(gameTime, new Vector2((float) Mouse.GetState().X - 0.5f * (float) this.lightningAimingTexture.Width, 0.0f), scaleX, scaleY);
      }
      Vector2 target = new Vector2(-10000f, -10000f);
      foreach (Soldier enemySoldier in enemySoldiers)
      {
        if ((double) Vector2.Distance(this.position, enemySoldier.Position) < (double) Vector2.Distance(this.position, target))
          target = enemySoldier.Position;
      }
      foreach (RangedSoldier rangedEnemySoldier in rangedEnemySoldiers)
      {
        if ((double) Vector2.Distance(this.position, rangedEnemySoldier.Position) < (double) Vector2.Distance(this.position, target))
          target = rangedEnemySoldier.Position;
      }
      this.dragon.Update(gameTime, target, enemySoldiers, rangedEnemySoldiers, scaleX, scaleY, content);
      this.CheckCollisions(enemySoldiers, rangedEnemySoldiers);
    }

    public void Draw(
      SpriteBatch spriteBatch,
      Rectangle mouseRectangle,
      float scaleX,
      float scaleY)
    {
      this.menu.Draw(spriteBatch, scaleX, scaleY);
      spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.12f, SpriteEffects.None, 1f);
      spriteBatch.DrawString(this.font, "Mana: " + this.mana.ToString(), new Vector2(this.menu.Position.X + 16f, this.menu.Position.Y + 82f), Color.White);
      if (this.aiming == Aiming.METEOR)
        spriteBatch.Draw(this.meteorAimingTexture, new Vector2((float) (mouseRectangle.X - this.meteorAimingTexture.Width / 2) / 1, (float) mouseRectangle.Y / 1), Color.White);
      else if (this.aiming == Aiming.LIGHTNING)
      {
        SpriteBatch spriteBatch1 = spriteBatch;
        Texture2D lightningAimingTexture = this.lightningAimingTexture;
        MouseState state = Mouse.GetState();
        double num1 = (double) (state.X - this.lightningAimingTexture.Width / 2) / (double) scaleX;
        state = Mouse.GetState();
        double num2 = (double) state.Y / (double) scaleY;
        Vector2 position = new Vector2((float) num1, (float) num2);
        Color white = Color.White;
        spriteBatch1.Draw(lightningAimingTexture, position, white);
        if (this.menu.Action == Action.NA && Mouse.GetState().LeftButton == ButtonState.Pressed)
          this.lightning.Draw(spriteBatch);
      }
      if (this.meteor.IsActive)
        this.meteor.Draw(spriteBatch);
      this.dragon.Draw(spriteBatch);
    }

    private void ShootMeteor()
    {
      if (this.mana - 15 < 0)
        return;
      this.aiming = Aiming.METEOR;
    }

    private void ReviveCorpses(
      ref List<Corpse> corpses,
      ref List<Queueable> soldierQueue,
      Vector2 enemyTowerPosition,
      ContentManager content,
      Vector2 playerTowerPosition,
      ref Queue queue)
    {
      this.toBeAdded = new List<Zombie>();
      for (int index = 0; index < corpses.Count; ++index)
      {
        int num1 = WizardTower.random.Next(0, 8);
        int num2;
        switch (num1)
        {
          case 0:
          case 1:
            num2 = 0;
            break;
          default:
            num2 = num1 != 2 ? 1 : 0;
            break;
        }
        if (num2 == 0)
        {
          this.toBeAdded.Add(new Zombie(content, enemyTowerPosition, new Vector2(playerTowerPosition.X, playerTowerPosition.Y + 150f)));
          Console.WriteLine("Skeleton created.");
        }
      }
      if (this.toBeAdded.Count == 0)
        this.toBeAdded.Add(new Zombie(content, enemyTowerPosition, new Vector2(playerTowerPosition.X, playerTowerPosition.Y + 150f)));
      foreach (Zombie zombie in this.toBeAdded)
      {
        soldierQueue.Add((Queueable) zombie);
        queue.AddMember("Skeleton");
      }
      corpses = new List<Corpse>();
      this.toBeAdded = new List<Zombie>();
    }

    private void SummonDragon(ContentManager content)
    {
      if (this.mana - 120 < 0)
        return;
      this.dragon.Activate();
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("wizard");
      this.font = content.Load<SpriteFont>("font");
      this.meteorAimingTexture = content.Load<Texture2D>("meteorAiming");
      this.lightningAimingTexture = content.Load<Texture2D>("lightningAimingTexture");
    }

    private void CheckCollisions(
      List<Soldier> enemySoldiers,
      List<RangedSoldier> rangedEnemySoldiers)
    {
      foreach (Soldier enemySoldier in enemySoldiers)
      {
        Rectangle rectangle = this.meteor.Rectangle;
        if (rectangle.Intersects(enemySoldier.Rectangle))
        {
          this.meteor.Kill();
          enemySoldier.Health -= this.meteor.Damage;
        }
        if (this.aiming == Aiming.LIGHTNING && (Mouse.GetState().LeftButton == ButtonState.Pressed && this.lightning.CanDamage))
        {
          rectangle = this.lightning.Rectangle;
          if (rectangle.Intersects(enemySoldier.Rectangle))
          {
            enemySoldier.Health -= this.lightning.Damage;
            this.lightning.ResetDamage();
          }
        }
      }
      foreach (RangedSoldier rangedEnemySoldier in rangedEnemySoldiers)
      {
        Rectangle rectangle = this.meteor.Rectangle;
        if (rectangle.Intersects(rangedEnemySoldier.Rectangle))
        {
          this.meteor.Kill();
          rangedEnemySoldier.Health -= this.meteor.Damage;
        }
        if (this.aiming == Aiming.LIGHTNING && (Mouse.GetState().LeftButton == ButtonState.Pressed && this.lightning.CanDamage))
        {
          rectangle = this.lightning.Rectangle;
          if (rectangle.Intersects(rangedEnemySoldier.Rectangle))
          {
            rangedEnemySoldier.Health -= this.lightning.Damage;
            this.lightning.ResetDamage();
          }
        }
      }
    }

    public void Pause()
    {
      this.dragon.Pause();
      if (this.instance != null)
        this.instance.Pause();
      if (this.meteor == null)
        return;
      this.meteor.Pause();
    }

    public void Play()
    {
      this.dragon.Play();
      if (this.instance != null)
        this.instance.Resume();
      if (this.meteor == null)
        return;
      this.meteor.Play();
    }

    public void Stop()
    {
      this.dragon.Stop();
      if (this.instance != null)
        this.instance.Stop();
      if (this.meteor != null)
        this.meteor.Stop();
      this.aiming = Aiming.NA;
    }

    public int Height
    {
      get
      {
        return this.texture.Height;
      }
    }

    public Aiming Aiming
    {
      get
      {
        return this.aiming;
      }
    }
  }
}
