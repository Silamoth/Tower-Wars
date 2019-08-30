// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Hammer
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class Hammer
  {
    private static Random random = new Random();
    private int timer = 0;
    private float acceleration = 1f;
    private bool hasTouched = false;
    private int score = 0;
    private bool canClick = true;
    private bool incrementClickTimer = false;
    private int clickTimer = 0;
    private Vector2 position;
    private Rectangle rectangle;
    private Texture2D texture;
    private SpriteFont font;
    private MouseState oldState;

    public Hammer(ContentManager content)
    {
      this.LoadContent(content);
      this.position = new Vector2(375f, 30f);
    }

    public void Update(Rectangle anvilRectangle)
    {
      ++this.timer;
      MouseState state = Mouse.GetState();
      float num1 = 0.0f;
      if ((double) this.position.Y <= 13.0 && this.hasTouched)
      {
        this.timer = 0;
        this.hasTouched = false;
        switch (new Random().Next(0, 4))
        {
          case 0:
            num1 = 0.0f;
            this.acceleration = 2.5f;
            break;
          case 1:
            num1 = 0.5f;
            this.acceleration = 1f;
            break;
          case 2:
            num1 = 1f;
            this.acceleration = 1.5f;
            break;
          case 3:
            num1 = 1.5f;
            this.acceleration = 3.5f;
            break;
        }
      }
      this.position.Y += num1 + this.acceleration;
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, 500, 30);
      if (this.timer % 30 == 0)
        this.acceleration += this.acceleration;
      if (anvilRectangle.Intersects(this.rectangle))
      {
        if (!this.hasTouched && state.LeftButton == ButtonState.Pressed && this.canClick)
        {
          ++this.score;
          this.canClick = false;
          this.incrementClickTimer = true;
        }
        this.hasTouched = true;
        float num2 = num1 * -1f;
        this.acceleration *= -1f;
        for (int index = 0; index < 5; ++index)
          Main.particleManager.AddParticle(new Vector2(this.position.X + (float) Hammer.random.Next(30, 50), this.position.Y + 60f), Hammer.random.Next(0, 50), (float) Hammer.random.Next(-1, 2), -1f, Color.Orange, 1f, 150f);
      }
      if (this.incrementClickTimer && state.LeftButton != ButtonState.Pressed)
        ++this.clickTimer;
      if (this.clickTimer == 10)
      {
        this.canClick = true;
        this.incrementClickTimer = false;
        this.clickTimer = 0;
      }
      this.oldState = state;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 3f, SpriteEffects.None, 1f);
      spriteBatch.DrawString(this.font, "Score: " + this.score.ToString(), new Vector2(350f, 25f), Color.White);
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("hammer");
      this.font = content.Load<SpriteFont>("font");
    }

    public int Score
    {
      get
      {
        return this.score;
      }
      set
      {
        this.score = 0;
      }
    }
  }
}
