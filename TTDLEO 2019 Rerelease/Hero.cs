// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Hero
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TTDLEO_2019_Rerelease
{
  internal class Hero
  {
    private Texture2D texture;
    private Vector2 position;
    private Rectangle rectangle;
    private AnimatedSprite sprite;
    private KeyboardState oldKeyState;

    public Hero(ContentManager content)
    {
      this.LoadContent(content);
      this.position = new Vector2(400f, 300f);
      this.sprite = new AnimatedSprite(this.texture, 32, 32);
    }

    public void Update(GameTime gameTime)
    {
      KeyboardState state = Keyboard.GetState();
      if (state.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetLeft();
        this.position.X -= 4f;
      }
      else if (state.IsKeyDown(Keys.A) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateLeft(gameTime);
        this.position.X -= 4f;
      }
      else if (state.IsKeyDown(Keys.D) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetRight();
        this.position.X += 4f;
      }
      else if (state.IsKeyDown(Keys.D) && this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateRight(gameTime);
        this.position.X += 4f;
      }
      else if (state.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetUp();
        this.position.Y -= 4f;
      }
      else if (state.IsKeyDown(Keys.W) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateUp(gameTime);
        this.position.Y -= 4f;
      }
      else if (state.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (!this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.SetDown();
        this.position.Y += 4f;
      }
      else if (state.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.D) && (this.oldKeyState.IsKeyDown(Keys.S) && !this.oldKeyState.IsKeyDown(Keys.W)) && !this.oldKeyState.IsKeyDown(Keys.A))
      {
        this.sprite.UpdateDown(gameTime);
        this.position.Y += 4f;
      }
      this.sprite.Update();
      this.oldKeyState = state;
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.sprite.FrameWidth, this.sprite.FrameHeight);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.sprite.Draw(spriteBatch, this.position);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("heroSpritesheet");
    }

    public Rectangle Rectangle
    {
      get
      {
        return this.rectangle;
      }
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
