// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.FireBreath
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class FireBreath
  {
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;

    public FireBreath(ContentManager content, Vector2 position)
    {
      this.position = position;
      this.LoadContent(content);
    }

    public void Update()
    {
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, (int) (3.75 * (double) this.texture.Width), (int) (3.75 * (double) this.texture.Height));
      this.position.X += 2.5f;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 3.75f, SpriteEffects.None, 1f);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("flame");
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
        return 5;
      }
    }

    public Vector2 Position
    {
      set
      {
        this.position = value;
      }
    }
  }
}
