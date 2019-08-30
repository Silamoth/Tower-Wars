// Decompiled with JetBrains decompiler
// Type: TTDLEO_2019_Rerelease.Arrow
// Assembly: "Towers That Don't Like Each Other", Version=3.5.0.469, Culture=neutral, PublicKeyToken=null
// MVID: 6007DBA4-372B-4E68-8530-C9B5D90F7148
// Assembly location: C:\Users\micha\AppData\Local\Apps\2.0\MHA41BA0.5NL\11VZHGB6.95C\towe..tion_d45e21a605ba8ce1_0001.0000_f4ff248c09f115f0\Towers That Don't Like Each Other.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TTDLEO_2019_Rerelease
{
    internal class Arrow
    {
        private float moveSpeed = 650f;
        private int damage = 1;
        private Texture2D texture;
        private Vector2 target;
        public Vector2 position;
        private Vector2 velocity;
        private bool isActive;
        private Rectangle rectangle;

        public Arrow(ContentManager content)
        {
            this.isActive = false;
            LoadContent(content);
        }

        public void ActivateBullet(
          Vector2 target,
          Vector2 position,
          float frameWidth,
          float frameHeight)
        {
            this.target = target;
            this.position = position;
            this.isActive = true;
            position.X += frameWidth * 0.5f;
            position.Y += frameHeight * 0.5f;
            this.SetVelocity();
        }

        public void ActivateBullet(
          Vector2 target,
          Vector2 position,
          float frameWidth)
        {
            this.target = target;
            this.position = position;
            this.isActive = true;
            position.X += frameWidth * 0.5f;
            this.SetVelocity();
        }

        public void ActivateBullet(Vector2 target, Vector2 position)
        {
            this.target = target;
            this.position = position;
            this.isActive = true;
            this.SetVelocity();
        }

        public void SetVelocityOther()
        {
            if ((double)Vector2.Distance(this.position, this.target) > 100.0)
                this.velocity = -(this.position - this.target);
            else if ((double)Vector2.Distance(this.position, this.target) > 50.0)
            {
                if ((double)Math.Abs(this.position.X - this.target.X) > (double)Math.Abs(this.position.Y - this.target.Y))
                {
                    if ((double)this.position.X < (double)this.target.X)
                    {
                        this.velocity = new Vector2(100f, 0.0f);
                    }
                    else
                    {
                        this.velocity = new Vector2(-100f, 0.0f);
                        Console.WriteLine("Negatiive x...");
                    }
                }
                else
                    this.velocity = (double)this.position.Y >= (double)this.target.Y ? new Vector2(0.0f, -100f) : new Vector2(0.0f, 100f);
            }
            else
                this.velocity = Vector2.Zero;
            this.velocity.Normalize();
        }

        public void SetVelocity()
        {
            this.velocity = -(this.position - this.target);
            this.velocity.Normalize();
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            this.position += this.velocity * this.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, (int)((double)this.texture.Width * 0.119999997317791), (int)((double)this.texture.Height * 0.119999997317791));
            if ((double)this.position.X > (double)screenWidth)
                this.Kill();
            else if ((double)this.position.X < 0.0)
                this.Kill();
            if ((double)this.position.Y < 0.0)
            {
                this.Kill();
            }
            else
            {
                if ((double)this.position.Y <= (double)screenHeight)
                    return;
                this.Kill();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 vector2 = this.target - this.position;
            vector2.Normalize();
            float rotation = (float)Math.Atan2((double)vector2.X, -(double)vector2.Y);
            spriteBatch.Draw(this.texture, this.position, new Rectangle?(), Color.White, rotation, new Vector2(64f, 64f), 0.12f, SpriteEffects.None, 1f);
        }

        public void Kill()
        {
            this.isActive = false;
            this.position = Vector2.Zero;
            this.rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, (int)((double)this.texture.Width * 0.200000002980232), (int)((double)this.texture.Height * 0.200000002980232));
        }

        private void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("arrow");
        }

        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }
        }

        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        public Vector2 Target
        {
            get
            {
                return this.target;
            }
        }

        public float MoveSpeed
        {
            set
            {
                this.moveSpeed = value;
            }
        }
    }
}
