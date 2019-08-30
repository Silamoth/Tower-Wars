// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.RunningObstacle
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
  internal class RunningObstacle
  {
    public static Random Random = new Random();
    private Texture2D texture;
    private Rectangle rectangle;

    public RunningObstacle(ContentManager content, Vector2 playerPosition)
    {
      this.LoadContent(content);
      this.rectangle = new Rectangle(RunningObstacle.Random.Next((int) ((double) playerPosition.X + 390.0), (int) ((double) playerPosition.X + 460.0)), RunningObstacle.Random.Next(354, 369) + 18, (int) ((double) this.texture.Width * 0.45), (int) ((double) this.texture.Height * 0.45));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, new Vector2((float) this.rectangle.X, (float) this.rectangle.Y), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.45f, SpriteEffects.None, 1f);
    }

    private void LoadContent(ContentManager content)
    {
      switch (RunningObstacle.Random.Next(0, 5))
      {
        case 0:
          this.texture = content.Load<Texture2D>("obstacleOne");
          break;
        case 1:
          this.texture = content.Load<Texture2D>("obstacleTwo");
          break;
        case 2:
          this.texture = content.Load<Texture2D>("obstacleThree");
          break;
        case 3:
          this.texture = content.Load<Texture2D>("obstacleFour");
          break;
        case 4:
          this.texture = content.Load<Texture2D>("obstacleFive");
          break;
      }
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
