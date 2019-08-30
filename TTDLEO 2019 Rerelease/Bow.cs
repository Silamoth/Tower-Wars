// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Bow
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTDLEO_2019_Rerelease
{
  internal class Bow
  {
    private Texture2D texture;
    private Vector2 position;
    private AnimatedBow sprite;
    private Arrow arrow;
    private MouseState oldMouseButtonState;

    public Bow(Vector2 position, ContentManager content)
    {
      this.position = position;
      this.LoadContent(content);
      this.sprite = new AnimatedBow(this.texture, 140, 39);
      this.arrow = new Arrow(content);
    }

    public void Update(
      GameTime gameTime,
      ContentManager content,
      Vector2 target,
      int screenWidth,
      int screenHeight)
    {
      this.sprite.Update(gameTime);
      MouseState state = Mouse.GetState();
      if (this.sprite.CanChange)
      {
        if (state.LeftButton == ButtonState.Pressed)
          this.sprite.UpdateImage(gameTime);
        else if (state.LeftButton == ButtonState.Released && this.oldMouseButtonState.LeftButton == ButtonState.Pressed)
          this.sprite.CurrentFrame = 0;
      }
      else if (state.LeftButton == ButtonState.Released && this.oldMouseButtonState.LeftButton == ButtonState.Pressed && !this.arrow.IsActive)
        this.Shoot(content, target);
      this.oldMouseButtonState = state;
      if (!this.arrow.IsActive)
        return;
      this.arrow.Update(gameTime, screenWidth, screenHeight);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      AnimatedBow sprite = this.sprite;
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 position = this.position;
      MouseState state = Mouse.GetState();
      double x = (double) state.X;
      state = Mouse.GetState();
      double y = (double) state.Y;
      Vector2 target = new Vector2((float) x, (float) y);
      sprite.Draw(spriteBatch1, position, target);
      if (!this.arrow.IsActive)
        return;
      this.arrow.Draw(spriteBatch);
    }

    public void Shoot(ContentManager content, Vector2 target)
    {
      this.sprite.CurrentFrame = 0;
      this.sprite.CanChange = true;
      this.arrow.ActivateBullet(target, this.position);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("bow");
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
