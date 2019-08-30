// Decompiled with JetBrains decompiler
// Type: TroopHealth
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal struct TroopHealth
{
  private Vector2 position;
  private int health;
  private String name;
  private Color color;

  public TroopHealth(Vector2 position, int health, String name, Color color)
  {
    this.position = position;
    this.health = health;
    this.name = name;
    this.color = color;
  }

  public Vector2 Position
  {
    get
    {
      return this.position;
    }
  }

  public int Health
  {
    get
    {
      return this.health;
    }
  }

  public String Name
  {
    get
    {
      return this.name;
    }
  }

  public Color Color
  {
    get
    {
      return this.color;
    }
  }
}
