// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Lightning
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class Lightning
  {
    private int manaTimer = 0;
    private bool canDamage = true;
    private bool incrementDamageTimer = false;
    private int damageTimer = 0;
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;
    private AnimatedLightning sprite;

    public Lightning(ContentManager content, Vector2 position)
    {
      this.LoadContent(content);
      this.sprite = new AnimatedLightning(this.texture, 34, 600);
      this.position = position;
    }

    public void Update(GameTime gameTime, Vector2 position, float scaleX, float scaleY)
    {
      this.sprite.Update(gameTime);
      this.position = position;
      this.rectangle = new Rectangle((int) ((double) position.X / (double) scaleX), (int) ((double) position.Y / (double) scaleY), (int) ((double) this.sprite.FrameWidth * (double) scaleX), (int) ((double) this.sprite.FrameHeight * (double) scaleY));
      if (this.incrementDamageTimer)
        ++this.damageTimer;
      if (this.damageTimer != 60)
        return;
      this.incrementDamageTimer = false;
      this.canDamage = true;
      this.damageTimer = 0;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.sprite.Draw(spriteBatch, this.rectangle);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("lightningSpritesheet");
    }

    public void ResetDamage()
    {
      this.canDamage = false;
      this.incrementDamageTimer = true;
    }

    public int ManaTimer
    {
      get
      {
        return this.manaTimer;
      }
      set
      {
        this.manaTimer = value;
      }
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
        return 2;
      }
    }

    public bool CanDamage
    {
      get
      {
        return this.canDamage;
      }
    }
  }
}
