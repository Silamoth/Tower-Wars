// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Medic
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TTDLEO_2019_Rerelease
{
  internal class Medic : SupportSoldier
  {
    public Medic(ContentManager content, Vector2 target, Vector2 position)
    {
      this.Position = position;
      this.Target = target;
      this.LoadContent(content);
      this.Stat = 1;
      this.Speed = 1.1f;
      this.Health = 2;
      this.ActTime = 450;
      this.Reward = 3;
      this.Name = nameof (Medic);
      this.LoadSprite(this.Texture, content);
    }

    public override void Act(List<Soldier> allySoldiers, List<RangedSoldier> allyRangedSoldiers)
    {
      if (!this.CanAct)
        return;
      bool flag = true;
      this.IsMovingForward = false;
      foreach (Soldier allySoldier in allySoldiers)
      {
        if ((double) Vector2.Distance(this.Position, allySoldier.Position) < 75.0)
        {
          flag = false;
          if (this.CanAct)
          {
            allySoldier.Health += this.Stat;
            this.CanAct = false;
            this.IncrementActTimer = true;
            this.sprite.SetAttackingRight();
            flag = false;
          }
        }
      }
      if (flag)
        this.IsMovingForward = true;
    }

    public override void LoadContent(ContentManager content)
    {
      this.Texture = content.Load<Texture2D>("medicSpritesheet");
    }
  }
}
