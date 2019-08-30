// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Queue
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TTDLEO_2019_Rerelease
{
  internal class Queue
  {
    private Texture2D texture;
    private Vector2 position;
    private List<Texture2D> allHeads;
    private List<String> names;
    private List<String> members;

    public Queue(ContentManager content)
    {
      this.texture = content.Load<Texture2D>("queue");
      this.position = new Vector2(150f, 175f);
      this.allHeads = new List<Texture2D>();
      this.allHeads.Add(content.Load<Texture2D>("commonerHead"));
      this.allHeads.Add(content.Load<Texture2D>("toughGuyHead"));
      this.allHeads.Add(content.Load<Texture2D>("bruteHead"));
      this.allHeads.Add(content.Load<Texture2D>("swordsmanHead"));
      this.allHeads.Add(content.Load<Texture2D>("archerHead"));
      this.allHeads.Add(content.Load<Texture2D>("thiefHead"));
      this.allHeads.Add(content.Load<Texture2D>("rogueHead"));
      this.allHeads.Add(content.Load<Texture2D>("medicHead"));
      this.allHeads.Add(content.Load<Texture2D>("generalHead"));
      this.allHeads.Add(content.Load<Texture2D>("skeletonHead"));
      this.names = new List<String>();
      this.names.Add("Commoner");
      this.names.Add("Tough Guy");
      this.names.Add("Brute");
      this.names.Add("Swordsman");
      this.names.Add("Archer");
      this.names.Add("Thief");
      this.names.Add("Rogue");
      this.names.Add("Medic");
      this.names.Add("General");
      this.names.Add("Skeleton");
      this.members = new List<String>();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      spriteBatch.Draw(this.texture, this.position, Color.White);
      float y = this.position.Y + 10f;
      float x = this.position.X + 10f;
      for (int index = 0; index < this.members.Count; ++index)
      {
        spriteBatch.Draw(this.allHeads[this.names.IndexOf(this.members[index])], new Vector2(x, y), Color.White);
        x += 20f;
        if ((index + 1) % 8 == 0)
        {
          y += 20f;
          x = this.position.X + 10f;
        }
      }
    }

    public void AddMember(String name)
    {
      this.members.Add(name);
    }

    public void RemoveMember()
    {
      this.members.RemoveAt(0);
    }

    public void Clear()
    {
      this.members = new List<String>();
    }

    public int Count
    {
      get
      {
        return this.members.Count;
      }
    }
  }
}
