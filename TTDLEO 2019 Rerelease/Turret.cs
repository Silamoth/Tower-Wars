// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Turret
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
  internal class Turret
  {
    private bool canAttack = true;
    private bool incrementAttackTimer = false;
    private int attackTimer = 0;
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;
    private Arrow arrow;
    private int damage;
    private SoundEffect effect;
    private SoundEffectInstance instance;

    public Turret(ContentManager content, Rectangle towerRectangle, int damage, String name)
    {
      this.LoadContent(content, name);
      this.damage = damage;
      this.position = new Vector2((float) (towerRectangle.Width - this.texture.Width + 35), (float) (towerRectangle.Y + this.texture.Height * 2 - 3));
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
      this.arrow = new Arrow(content);
    }

    public Turret(Rectangle towerRectangle, ContentManager content, int damage, String name)
    {
      this.LoadContent(content, name);
      this.damage = damage;
      this.position = new Vector2((float) (towerRectangle.Width - this.texture.Width - 10), (float) (towerRectangle.Y - this.texture.Height - 25));
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
      this.arrow = new Arrow(content);
    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
      if (this.incrementAttackTimer && !this.arrow.IsActive)
        ++this.attackTimer;
      if (this.attackTimer == 30)
      {
        this.canAttack = true;
        this.incrementAttackTimer = false;
        this.attackTimer = 0;
      }
      if (!this.arrow.IsActive)
        return;
      this.arrow.Update(gameTime, screenWidth, screenHeight);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 1.5f, SpriteEffects.None, 1f);
      if (!this.arrow.IsActive)
        return;
      this.arrow.Draw(spriteBatch);
    }

    public void Shoot(Vector2 target, ContentManager content)
    {
      if ((double) target.X - 75.0 > (double) this.position.X)
        this.arrow.ActivateBullet(target, new Vector2(this.position.X + 20f, this.position.Y + 53f), (float) this.texture.Width);
      this.canAttack = false;
      this.incrementAttackTimer = true;
      if (this.effect == null)
        this.effect = content.Load<SoundEffect>("arrowShoot");
      if (this.instance == null)
        this.instance = this.effect.CreateInstance();
      this.instance.Play();
    }

    private void LoadContent(ContentManager content, String name)
    {
      this.texture = content.Load<Texture2D>(name);
    }

    public void Pause()
    {
      if (this.instance == null)
        return;
      this.instance.Pause();
    }

    public void Resume()
    {
      if (this.instance == null)
        return;
      this.instance.Resume();
    }

    public bool CanAttack
    {
      get
      {
        return this.canAttack;
      }
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public Vector2 Position
    {
      get
      {
        return this.position;
      }
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
        return this.damage;
      }
    }
  }
}
