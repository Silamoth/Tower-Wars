// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Corpse
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class Corpse
  {
    private Texture2D texture;
    private Vector2 position;

    public Corpse(ContentManager content, Vector2 position)
    {
      this.LoadContent(content);
      this.position = position;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, Color.White);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("corpse");
    }

    public Vector2 Position
    {
      get
      {
        return this.position;
      }
    }
  }
}
