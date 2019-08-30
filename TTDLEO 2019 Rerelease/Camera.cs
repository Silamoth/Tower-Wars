// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Camera
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;

namespace TTDLEO_2019_Rerelease
{
  public class Camera
  {
    private Matrix viewMatrix;
    private Vector2 position;

    public Matrix ViewMatrix
    {
      get
      {
        return this.viewMatrix;
      }
    }

    public int ScreenWidth
    {
      get
      {
        return GraphicsDeviceManager.DefaultBackBufferWidth;
      }
    }

    public int ScreenHeight
    {
      get
      {
        return GraphicsDeviceManager.DefaultBackBufferHeight;
      }
    }

    public Vector2 Position
    {
      get
      {
        return this.position;
      }
    }

    public void Update(Vector2 playerPosition)
    {
      this.position.X = playerPosition.X - (float) (this.ScreenWidth / 2);
      this.position.Y = playerPosition.Y - (float) (this.ScreenHeight / 2);
      this.viewMatrix = Matrix.CreateTranslation(new Vector3(-this.position, 0.0f));
    }
  }
}
