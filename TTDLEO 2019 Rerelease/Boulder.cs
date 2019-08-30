// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Boulder
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class Boulder
  {
    private static Random random = new Random();
    private int damage = 7;
    private Texture2D texture;
    private Vector2 target;
    public Vector2 position;
    private Vector2 velocity;
    private bool isActive;
    private float moveSpeed;
    private Rectangle rectangle;

    public Boulder()
    {
      this.isActive = false;
    }

    public void ActivateBullet(Vector2 target, Vector2 position, ContentManager content)
    {
      this.target = target;
      this.position = position;
      this.moveSpeed = 175f;
      this.LoadContent(content);
      this.isActive = true;
      this.SetVelocity();
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
      this.moveSpeed = 0.0f;
      this.position = Vector2.Zero;
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, (int) ((double) this.texture.Width * 0.5), (int) ((double) this.texture.Height * 0.5));
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("boulder");
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
  }
}
