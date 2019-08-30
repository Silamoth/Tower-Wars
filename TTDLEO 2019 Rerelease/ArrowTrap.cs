// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.ArrowTrap
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class ArrowTrap
  {
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;
    private Arrow arrow;

    public ArrowTrap(ContentManager content, Vector2 position, bool isRight)
    {
      this.position = position;
      this.arrow = new Arrow(content);
      this.arrow.Damage = 1;
      this.arrow.MoveSpeed = 400f;
      if (isRight)
        this.texture = content.Load<Texture2D>("arrowTrapRight");
      else
        this.texture = content.Load<Texture2D>("arrowTrapLeft");
    }

    public void Update(GameTime gameTime, int screenWidth, int screenHeight)
    {
      if (!this.arrow.IsActive)
        return;
      this.arrow.Update(gameTime, screenWidth, screenHeight);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, Color.White);
      if (!this.arrow.IsActive)
        return;
      this.arrow.Draw(spriteBatch);
    }

    public void Shoot(bool isRight)
    {
      if (isRight)
        this.arrow.ActivateBullet(new Vector2(-100f, this.position.Y + (float) this.texture.Height * 0.5f), new Vector2(this.position.X, this.position.Y + (float) this.texture.Height * 0.5f));
      else
        this.arrow.ActivateBullet(new Vector2(3000f, this.position.Y + (float) this.texture.Height * 0.5f), new Vector2(this.position.X + (float) this.texture.Width, this.position.Y + (float) this.texture.Height * 0.5f));
    }

    public Arrow Arrow
    {
      get
      {
        return this.arrow;
      }
    }
  }
}
