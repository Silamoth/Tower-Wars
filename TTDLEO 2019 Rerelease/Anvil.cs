// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Anvil
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class Anvil
  {
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;

    public Anvil(ContentManager content)
    {
      this.LoadContent(content);
      this.position = new Vector2(350f, 400f);
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.texture.Width, this.texture.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, Color.White);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("anvil");
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }
  }
}
