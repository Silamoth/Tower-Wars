// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.RunningEnemy
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class RunningEnemy
  {
    private int yChange = 0;
    private bool isJumping = false;
    private Vector2 position;
    private Texture2D texture;
    private Rectangle rectangle;
    private AnimatedSprite spriteOne;
    private AnimatedSprite spriteTwo;
    private AnimatedSprite spriteThree;

    public RunningEnemy(ContentManager content)
    {
      this.LoadContent(content);
      this.spriteOne = new AnimatedSprite(this.texture, 32, 32);
      this.spriteTwo = new AnimatedSprite(this.texture, 32, 32);
      this.spriteThree = new AnimatedSprite(this.texture, 32, 32);
      this.position = new Vector2(34f, (float) (400 - this.texture.Height));
      this.spriteOne.SetRight();
      this.spriteTwo.SetRight();
      this.spriteThree.SetRight();
    }

    public void Update(GameTime gameTime, Vector2 obstaclePosition)
    {
      this.rectangle = new Rectangle((int) this.position.X, (int) this.position.Y, this.spriteOne.FrameWidth * 3 + 3, this.spriteOne.FrameHeight);
      this.spriteOne.Update();
      this.spriteTwo.Update();
      this.spriteThree.Update();
      this.position.X += 5f;
      this.spriteOne.UpdateRight(gameTime);
      this.spriteTwo.UpdateRight(gameTime);
      this.spriteThree.UpdateRight(gameTime);
      if ((double) Vector2.Distance(new Vector2((float) ((double) this.position.X + (double) (2 * this.spriteOne.FrameWidth) + 3.0), this.position.Y), obstaclePosition) < 35.0 && !this.isJumping && (double) obstaclePosition.Y != -1.0)
        this.Jump();
      if (!this.isJumping)
        return;
      this.position.Y += (float) this.yChange;
      switch (this.yChange)
      {
        case -24:
          this.yChange = 4;
          break;
        case -22:
          this.yChange = -24;
          break;
        case -20:
          this.yChange = -22;
          break;
        case -18:
          this.yChange = -20;
          break;
        case -16:
          this.yChange = -18;
          break;
        case -14:
          this.yChange = -16;
          break;
        case -12:
          this.yChange = -14;
          break;
        case -8:
          this.yChange = -3;
          break;
        case -6:
          this.yChange = -8;
          break;
        case -4:
          this.yChange = -6;
          break;
        case -3:
          this.yChange = -12;
          break;
        case 3:
          this.yChange = 12;
          break;
        case 4:
          this.yChange = 6;
          break;
        case 6:
          this.yChange = 8;
          break;
        case 8:
          this.yChange = 3;
          break;
        case 12:
          this.yChange = 14;
          break;
        case 14:
          this.yChange = 16;
          break;
        case 16:
          this.yChange = 18;
          break;
        case 18:
          this.yChange = 20;
          break;
        case 20:
          this.yChange = 22;
          break;
        case 22:
          this.yChange = 24;
          break;
        case 24:
          this.isJumping = false;
          break;
      }
    }

    private void Jump()
    {
      this.yChange = -4;
      this.isJumping = true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this.spriteOne.Draw(spriteBatch, this.position);
      this.spriteTwo.Draw(spriteBatch, new Vector2((float) ((double) this.position.X + (double) this.spriteOne.FrameWidth + 5.0), this.position.Y));
      this.spriteThree.Draw(spriteBatch, new Vector2((float) ((double) this.position.X + (double) this.spriteOne.FrameWidth + (double) this.spriteTwo.FrameWidth + 3.0), this.position.Y));
    }

    private void LoadContent(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("skeletonSpritesheet");
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
