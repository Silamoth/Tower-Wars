// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Bunny
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class Bunny : Animal
  {
    public Bunny(ContentManager content)
    {
      this.LoadContent(content);
      this.Position = new Vector2((float) Animal.Random.Next(30, 700), (float) Animal.Random.Next(90, 325));
      this.Sprite = new AnimatedAnimal(this.Texture, 33, 28, (double) this.Position.Y >= 30.0 ? ((double) this.Position.Y <= 30.0 || (double) this.Position.Y >= 200.0 ? ((double) this.Position.Y <= 200.0 || (double) this.Position.Y >= 250.0 ? 0.95f : 0.85f) : 0.6f) : 0.55f);
      this.Reward = 3;
    }

    public override void Update(GameTime gameTime)
    {
      this.Rectangle = new Rectangle((int) this.Position.X, (int) this.Position.Y, this.Sprite.FrameWidth, this.Sprite.FrameHeight);
      this.Sprite.Update();
      if (Animal.Random.Next(0, 40) == 7)
      {
        switch (Animal.Random.Next(0, 2))
        {
          case 0:
            this.Sprite.SetMovingRight();
            break;
          case 1:
            this.Sprite.SetMovingLeft();
            break;
        }
      }
      if (this.Sprite.State == AnimationState.RIGHT && this.Sprite.IsMoving && (double) this.Position.X + 4.0 < 700.0)
      {
        this.Position = new Vector2(this.Position.X + 4f, this.Position.Y);
        this.Sprite.UpdateRight(gameTime);
      }
      if (this.Sprite.State != AnimationState.LEFT || !this.Sprite.IsMoving || (double) this.Position.X - 4.0 <= 30.0)
        return;
      this.Position = new Vector2(this.Position.X - 4f, this.Position.Y);
      this.Sprite.UpdateLeft(gameTime);
    }

    public override void LoadContent(ContentManager content)
    {
      this.Texture = content.Load<Texture2D>("bunnySpriteSheet");
    }
  }
}
