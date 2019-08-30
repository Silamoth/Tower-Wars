// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Dragon
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  internal class Dragon
  {
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;
    private AnimatedDragon sprite;
    private float directionRadians;
    private bool isActive;
    private bool canAttack;
    private bool incrementAttackTimer;
    private int attackTimer;
    private FireBreath flame;
    private SoundEffect wingEffect;
    private SoundEffect roarEffect;
    private SoundEffectInstance wingInstance;
    private SoundEffectInstance roarInstance;

    public Dragon(ContentManager content)
    {
      this.LoadContent(content);
      this.position = new Vector2(0.0f, 300f);
      this.isActive = false;
      this.canAttack = true;
      this.incrementAttackTimer = false;
      this.attackTimer = 0;
      this.sprite = new AnimatedDragon(this.texture, 150, 75);
      this.flame = new FireBreath(content, new Vector2((float) ((double) this.position.X + (double) this.sprite.FrameWidth - 12.0), this.position.Y + 55f));
    }

    public void Update(
      GameTime gameTime,
      Vector2 target,
      List<Soldier> enemySoldiers,
      List<RangedSoldier> enemyRangedSoldiers,
      float scaleX,
      float scaleY,
      ContentManager content)
    {
      if (this.isActive)
      {
        this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, (int) ((double) this.sprite.FrameWidth * 0.35), (int) ((double) this.sprite.FrameHeight * 0.35));
        this.position.X += 2.5f;
        if (this.canAttack)
        {
          foreach (Soldier enemySoldier in enemySoldiers)
          {
            if (enemySoldier.Rectangle.Intersects(this.flame.Rectangle))
            {
              enemySoldier.Health -= this.flame.Damage;
              this.canAttack = false;
              this.incrementAttackTimer = true;
              break;
            }
          }
          if (this.canAttack)
          {
            foreach (RangedSoldier enemyRangedSoldier in enemyRangedSoldiers)
            {
              if (enemyRangedSoldier.Rectangle.Intersects(this.flame.Rectangle))
              {
                enemyRangedSoldier.Health -= this.flame.Damage;
                this.canAttack = false;
                this.incrementAttackTimer = true;
                break;
              }
            }
          }
        }
        if (this.incrementAttackTimer)
        {
          ++this.attackTimer;
          if (this.attackTimer == 15)
          {
            this.canAttack = true;
            this.incrementAttackTimer = false;
            this.attackTimer = 0;
          }
        }
        if ((double) this.position.X > 800.0 * (double) scaleX)
          this.Kill();
        this.flame.Update();
        this.sprite.Update(gameTime);
        if (this.wingEffect == null)
          this.wingEffect = content.Load<SoundEffect>("dragonWings");
        if (this.wingInstance == null)
        {
          this.wingInstance = this.wingEffect.CreateInstance();
          this.wingInstance.IsLooped = true;
          this.wingInstance.Volume = Main.volume;
        }
        this.wingInstance.Play();
        if (this.roarEffect == null)
          this.roarEffect = content.Load<SoundEffect>("dragonRoar");
        if (this.roarInstance == null)
        {
          this.roarInstance = this.roarEffect.CreateInstance();
          this.roarInstance.IsLooped = true;
          this.roarInstance.Volume = Main.volume;
        }
        this.roarInstance.Play();
      }
      else
      {
        if (this.wingInstance != null)
          this.wingInstance.Stop();
        if (this.roarInstance != null)
          this.roarInstance.Stop();
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.isActive)
        return;
      this.sprite.Draw(spriteBatch, this.position);
      this.flame.Draw(spriteBatch);
    }

    public void Activate()
    {
      this.isActive = true;
      this.position = new Vector2(0.0f, 300f);
      this.flame.Position = new Vector2((float) ((double) this.position.X + (double) this.sprite.FrameWidth - 12.0), this.position.Y + 55f);
    }

    private void Kill()
    {
      this.isActive = false;
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("dragonSpritesheet");
    }

    public void Pause()
    {
      if (this.wingInstance != null)
        this.wingInstance.Pause();
      if (this.roarInstance == null)
        return;
      this.roarInstance.Pause();
    }

    public void Play()
    {
      if (this.wingInstance != null)
        this.wingInstance.Resume();
      if (this.roarInstance == null)
        return;
      this.roarInstance.Resume();
    }

    public void Stop()
    {
      if (this.wingInstance != null)
        this.wingInstance.Stop();
      if (this.roarInstance == null)
        return;
      this.roarInstance.Stop();
    }
  }
}
