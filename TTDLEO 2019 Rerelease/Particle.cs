// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Particle
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class Particle
  {
    private Vector2 position;
    private float life;
    private int angle;
    private float xSpeed;
    private float ySpeed;
    private Color color;
    private float angleRadians;
    private Vector2 velocity;
    private float size;
    private float originalSize;
    private float originalLife;
    private Texture2D texture;

    public Particle(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("particle");
    }

    public void Update(GameTime gameTime)
    {
      float totalMilliseconds = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
      this.life -= totalMilliseconds;
      this.position.X += this.velocity.X * totalMilliseconds;
      this.position.Y += this.velocity.Y * totalMilliseconds;
      this.size = this.originalSize * (this.life / this.originalLife);
    }

    public void Activate(
      Vector2 position,
      int angle,
      float xSpeed,
      float ySpeed,
      Color color,
      float size,
      float life)
    {
      this.position = position;
      this.angle = angle;
      this.xSpeed = xSpeed;
      this.ySpeed = ySpeed;
      this.color = color;
      this.originalSize = size;
      this.size = this.originalSize;
      this.originalLife = life;
      this.life = this.originalLife;
      this.angleRadians = (float) ((double) angle * Math.PI / 180.0);
      this.velocity = new Vector2(xSpeed * (float) Math.Cos((double) this.angleRadians), ySpeed * (float) Math.Sin((double) this.angleRadians));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, new Rectangle?(), this.color, 0.0f, Vector2.Zero, this.size, SpriteEffects.None, 1f);
    }

    public float Life
    {
      get
      {
        return this.life;
      }
    }
  }
}
