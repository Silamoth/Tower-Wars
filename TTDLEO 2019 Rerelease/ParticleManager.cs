// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.ParticleManager
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  public class ParticleManager
  {
    private List<Particle> particles;

    public ParticleManager(ContentManager content)
    {
      this.particles = new List<Particle>();
      for (int index = 0; index < 30; ++index)
        this.particles.Add(new Particle(content));
    }

    public void Update(GameTime gameTime)
    {
      foreach (Particle particle in this.particles)
      {
        if ((double) particle.Life > 0.0)
          particle.Update(gameTime);
      }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (Particle particle in this.particles)
      {
        if ((double) particle.Life > 0.0)
          particle.Draw(spriteBatch);
      }
    }

    public void AddParticle(
      Vector2 position,
      int angle,
      float xSpeed,
      float ySpeed,
      Color color,
      float size,
      float life)
    {
      foreach (Particle particle in this.particles)
      {
        if ((double) particle.Life <= 0.0)
        {
          particle.Activate(position, angle, xSpeed, ySpeed, color, size, life);
          break;
        }
      }
    }
  }
}
