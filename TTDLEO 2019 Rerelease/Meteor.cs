// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Meteor
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class Meteor
  {
    private static Random random = new Random();
    private float moveSpeed = 700f;
    private int damage = 4;
    private Texture2D texture;
    private Vector2 target;
    public Vector2 position;
    private Vector2 velocity;
    private bool isActive;
    private Rectangle rectangle;
    private SoundEffect effect;
    private SoundEffectInstance instance;

    public Meteor(ContentManager content)
    {
        this.isActive = false;
        LoadContent(content);
    }

    public void ActivateBullet(Vector2 target, Vector2 position)
    {
      this.target = target;
      this.position = position;
      this.isActive = true;
      this.SetVelocity();
      for (int index = 0; index < 20; ++index)
        Main.particleManager.AddParticle(new Vector2(position.X - (float) ((double) this.texture.Width * 0.25 / 2.0) + (float) Meteor.random.Next(-20, 20), position.Y + (float) Meteor.random.Next(-70, 0)), Meteor.random.Next(340, 470), 0.0f, 0.5f, Color.Gray, (float) (2 + index / 15), 800f);
      this.instance = this.effect.CreateInstance();
      this.instance.Volume = Main.volume;
      this.instance.Play();
    }

    public void SetVelocity()
    {
      this.velocity = -(this.position - this.target);
      this.velocity.Normalize();
    }

    public void Update(GameTime gameTime)
    {
      this.position += this.velocity * this.moveSpeed * (float) gameTime.ElapsedGameTime.TotalSeconds;
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, (int) ((double) this.texture.Width * 0.5), (int) ((double) this.texture.Height * 0.5));
      if ((double) this.position.X > 810.0)
        this.Kill();
      else if ((double) this.position.X < 0.0)
        this.Kill();
      if ((double) this.position.Y < 0.0)
      {
        this.Kill();
      }
      else
      {
        if ((double) this.position.Y <= 610.0)
          return;
        this.Kill();
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      Vector2 vector2 = this.target - this.position;
      vector2.Normalize();
      float rotation = (float) Math.Atan2(-(double) vector2.X, (double) vector2.Y);
      spriteBatch.Draw(this.texture, this.rectangle, new Rectangle?(), Color.White, rotation, new Vector2(64f, 64f), SpriteEffects.None, 1f);
    }

    public void Kill()
    {
      this.isActive = false;
      this.position = Vector2.Zero;
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, (int) ((double) this.texture.Width * 0.5), (int) ((double) this.texture.Height * 0.5));
    }

    private void LoadContent(ContentManager content)
    {
      if (this.texture == null)
        this.texture = content.Load<Texture2D>("meteor");
      if (this.effect != null)
        return;
      this.effect = content.Load<SoundEffect>("meteorEffect");
    }

    public void Play()
    {
      if (this.instance == null)
        return;
      this.instance.Resume();
    }

    public void Pause()
    {
      if (this.instance == null)
        return;
      this.instance.Pause();
    }

    public void Stop()
    {
      if (this.instance == null)
        return;
      this.instance.Stop();
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
        return this.damage;
      }
      set
      {
        this.damage = value;
      }
    }

    public bool IsActive
    {
      get
      {
        return this.isActive;
      }
      set
      {
        this.isActive = value;
      }
    }

    public Vector2 Target
    {
      get
      {
        return this.target;
      }
    }

    public Vector2 Velocity
    {
      get
      {
        return this.velocity;
      }
      set
      {
        this.velocity = value;
      }
    }

    public float MoveSpeed
    {
      set
      {
        this.moveSpeed = value;
      }
    }
  }
}
