// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Archer
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TTDLEO_2019_Rerelease
{
  internal class Archer : RangedSoldier
  {
    public Archer(ContentManager content, Vector2 target, Vector2 position) : base(content)
    {
      this.Target = target;
      this.Position = position;
      this.LoadContent(content);
      this.Damage = 1;
      this.Health = 7;
      this.Reward = 25;
      this.Speed = 1f;
      this.AttackTime = 550;
      this.Name = nameof (Archer);
      this.LoadSprite(this.Texture, content);
    }

    public override void LoadContent(ContentManager content)
    {
      this.Texture = content.Load<Texture2D>("archerSpritesheet");
      this.effect = content.Load<SoundEffect>("arrowShoot");
    }
  }
}
