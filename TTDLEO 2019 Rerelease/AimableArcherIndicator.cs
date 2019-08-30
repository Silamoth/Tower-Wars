// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.AimableArcherIndicator
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class AimableArcherIndicator
  {
    private Texture2D texture;
    private Vector2 position;
    private bool isActive;

    public AimableArcherIndicator(ContentManager content, Vector2 archerPosition, int height)
    {
      this.texture = content.Load<Texture2D>("archerOpacityThing");
    }

    public void Update(Vector2 archerPosition)
    {
      if (!this.isActive)
        return;
      this.position.Y -= 2.01f;
      if ((double) this.position.Y <= (double) archerPosition.Y + 50.0)
        this.isActive = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      if (!this.isActive)
        return;
      spriteBatch.Draw(this.texture, this.position, Color.White);
    }

    public void SetActive(Vector2 archerPosition, int height)
    {
      this.isActive = true;
      this.position = new Vector2(archerPosition.X, (float) ((double) archerPosition.Y + (double) height - 220.0));
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
  }
}
