// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.DefendingLocation
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class DefendingLocation
  {
    private Texture2D texture;
    private Rectangle rectangle;
    private int health;
    private SpriteFont font;
    private Vector2 position;

    public DefendingLocation(ContentManager content)
    {
      this.LoadContent(content);
      this.position = new Vector2(340f, 215f);
      this.rectangle = new Rectangle((int) ((double) this.position.X + 15.0), (int) ((double) this.position.Y + 8.0), this.texture.Width - 20, this.texture.Height - 17);
      this.health = 45;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, new Vector2(this.position.X, this.position.Y), Color.White);
      spriteBatch.DrawString(this.font, "Health: " + this.health.ToString(), new Vector2(this.position.X + 17f, this.position.Y - 30f), Color.Black);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("defenseLocation");
      this.font = content.Load<SpriteFont>("font");
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public int Health
    {
      get
      {
        return this.health;
      }
      set
      {
        this.health = value;
      }
    }
  }
}
