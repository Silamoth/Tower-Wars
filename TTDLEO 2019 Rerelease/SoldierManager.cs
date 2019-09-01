using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TTDLEO_2019_Rerelease
{
    class SoldierManager
    {
        List<Soldier> playerTroops;
        List<Soldier> enemyTroops;
        List<RangedSoldier> enemyRangedTroops;
        List<RangedSoldier> playerRangedTroops;
        List<SupportSoldier> playerSupportTroops;
        List<TroopHealth> healths;

        int troopsLost, enemiesKilled;

        double timer;

        public SoldierManager()
        {
            timer = 0;

            playerTroops = new List<Soldier>();
            enemyTroops = new List<Soldier>();
            playerRangedTroops = new List<RangedSoldier>();
            enemyRangedTroops = new List<RangedSoldier>();
            playerSupportTroops = new List<SupportSoldier>();

            troopsLost = 0;
            enemiesKilled = 0;

        }

        public void Update(int updatesPerFrame, GameTime gameTime, float startTime, ContentManager content,
            Tower enemyTower, Tower playerTower, int screenWidth, int screenHeight, List<Corpse> corpses)
        {
            for (int i = 0; i < updatesPerFrame; i++)
            {
                timer += gameTime.ElapsedGameTime.TotalSeconds - (double)startTime;
                int timerSeconds = 0;
                while (timer > 1.0)
                {
                    --timer;
                    ++timerSeconds;
                }
                foreach (Soldier playerTroop in playerTroops)
                {
                    bool flag = true;
                    foreach (Soldier enemyTroop in enemyTroops)
                    {
                        if (playerTroop.Rectangle.Intersects(enemyTroop.Rectangle))
                        {
                            flag = false;
                            playerTroop.IsMovingForward = false;
                            enemyTroop.IsMovingForward = false;
                            playerTroop.SetRightAttack(gameTime);
                            enemyTroop.SetLeftAttack(gameTime);
                            if (playerTroop.CanAttack)
                            {
                                enemyTroop.Health -= playerTroop.Damage;
                                enemyTroop.Hit();
                                playerTroop.CanAttack = false;
                                playerTroop.IncrementAttackTimer = true;
                                playerTroop.SoundSwing();
                            }
                            if (enemyTroop.CanAttack)
                            {
                                playerTroop.Health -= enemyTroop.Damage;
                                playerTroop.Hit();
                                enemyTroop.CanAttack = false;
                                enemyTroop.IncrementAttackTimer = true;
                                enemyTroop.SoundSwing();
                            }
                        }
                    }
                    foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                    {
                        if (playerTroop.Rectangle.Intersects(enemyRangedTroop.Rectangle))
                        {
                            flag = false;
                            playerTroop.IsMovingForward = false;
                            if (playerTroop.CanAttack)
                            {
                                enemyRangedTroop.Health -= playerTroop.Damage;
                                enemyRangedTroop.Hit();
                                playerTroop.CanAttack = false;
                                playerTroop.IncrementAttackTimer = true;
                                playerTroop.SoundSwing();
                            }
                        }
                    }
                    if (flag)
                        playerTroop.IsMovingForward = true;
                }
                foreach (Soldier enemyTroop in enemyTroops)
                {
                    bool flag = true;
                    enemyTroop.IsMovingForward = false;
                    foreach (Soldier playerTroop in playerTroops)
                    {
                        if (flag && enemyTroop.Rectangle.Intersects(playerTroop.Rectangle))
                            flag = false;
                    }
                    foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
                    {
                        if (flag && enemyTroop.Rectangle.Intersects(playerRangedTroop.Rectangle))
                            flag = false;
                    }
                    if (flag)
                        enemyTroop.IsMovingForward = true;
                }
                foreach (Soldier playerTroop in playerTroops)
                {
                    if (playerTroop.Rectangle.Intersects(enemyTower.Rectangle))
                    {
                        playerTroop.IsMovingForward = false;
                        playerTroop.SetRightAttack(gameTime);
                        if (playerTroop.CanAttack)
                        {
                            enemyTower.Health -= playerTroop.Damage;
                            playerTroop.CanAttack = false;
                            playerTroop.IncrementAttackTimer = true;
                            playerTroop.SoundSwing();
                        }
                    }
                }
                foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
                {
                    bool flag = true;
                    playerRangedTroop.IsMovingForward = false;
                    foreach (Soldier enemyTroop in enemyTroops)
                    {
                        if ((double)Vector2.Distance(playerRangedTroop.Position, enemyTroop.Position) < 200.0)
                        {
                            flag = false;
                            playerRangedTroop.SetRightAttack(gameTime);
                            if (playerRangedTroop.CanAttack && !playerRangedTroop.CanChange && !playerRangedTroop.Arrow.IsActive)
                            {
                                playerRangedTroop.Shoot(enemyTroop.Position, content);
                                playerRangedTroop.CanAttack = false;
                                playerRangedTroop.IncrementAttackTimer = true;
                            }
                        }
                        if (playerRangedTroop.Arrow.IsActive && playerRangedTroop.Arrow.Rectangle.Intersects(enemyTroop.Rectangle))
                        {
                            playerRangedTroop.Arrow.Kill();
                            enemyTroop.Health -= playerRangedTroop.Damage;
                            enemyTroop.Hit();
                        }
                        if (enemyTroop.Rectangle.Intersects(playerRangedTroop.Rectangle))
                        {
                            enemyTroop.SetLeftAttack(gameTime);
                            if (enemyTroop.CanAttack)
                            {
                                playerRangedTroop.Health -= enemyTroop.Damage;
                                playerRangedTroop.Hit();
                                enemyTroop.CanAttack = false;
                                enemyTroop.IncrementAttackTimer = true;
                                enemyTroop.SoundSwing();
                            }
                        }
                    }
                    foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                    {
                        if ((double)Vector2.Distance(playerRangedTroop.Position, enemyRangedTroop.Position) < 200.0)
                        {
                            flag = false;
                            playerRangedTroop.SetRightAttack(gameTime);
                            if (playerRangedTroop.CanAttack && !playerRangedTroop.CanChange && !playerRangedTroop.Arrow.IsActive)
                            {
                                playerRangedTroop.Shoot(enemyRangedTroop.Position, content);
                                playerRangedTroop.CanAttack = false;
                                playerRangedTroop.IncrementAttackTimer = true;
                            }
                        }
                        if (playerRangedTroop.Arrow.IsActive && playerRangedTroop.Arrow.Rectangle.Intersects(enemyRangedTroop.Rectangle))
                        {
                            enemyRangedTroop.Health -= playerRangedTroop.Damage;
                            enemyRangedTroop.Hit();
                            playerRangedTroop.Arrow.Kill();
                        }
                    }
                    if (flag)
                        playerRangedTroop.IsMovingForward = true;
                    if (enemyTower.Rectangle.Intersects(new Rectangle(playerRangedTroop.Rectangle.X + 250, playerRangedTroop.Rectangle.Y, playerRangedTroop.Rectangle.Width, playerRangedTroop.Rectangle.Height)))
                    {
                        playerRangedTroop.IsMovingForward = false;
                        playerRangedTroop.SetRightAttack(gameTime);
                        if (playerRangedTroop.CanAttack)
                        {
                            playerRangedTroop.Shoot(enemyTower.Position, content);
                            playerRangedTroop.CanAttack = false;
                            playerRangedTroop.IncrementAttackTimer = true;
                        }
                    }
                    if (playerRangedTroop.Arrow.IsActive && playerRangedTroop.Arrow.Rectangle.Intersects(enemyTower.Rectangle))
                    {
                        enemyTower.Health -= playerRangedTroop.Damage;
                        playerRangedTroop.Arrow.Kill();
                    }
                }
                foreach (Soldier enemyTroop in enemyTroops)
                {
                    if (enemyTroop.Rectangle.Intersects(new Rectangle(playerTower.Rectangle.X - 20, playerTower.Rectangle.Y, playerTower.Rectangle.Width, playerTower.Rectangle.Height)))
                    {
                        enemyTroop.IsMovingForward = false;
                        enemyTroop.SetLeftAttack(gameTime);
                        if (enemyTroop.CanAttack)
                        {
                            playerTower.Health -= enemyTroop.Damage;
                            enemyTroop.CanAttack = false;
                            enemyTroop.IncrementAttackTimer = true;
                            enemyTroop.SoundSwing();
                        }
                    }
                }
                foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                {
                    bool flag = true;
                    enemyRangedTroop.IsMovingForward = false;
                    foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
                    {
                        if ((double)Vector2.Distance(enemyRangedTroop.Position, playerRangedTroop.Position) < 200.0)
                        {
                            flag = false;
                            enemyRangedTroop.SetLeftAttack(gameTime);
                            if (enemyRangedTroop.CanAttack && !enemyRangedTroop.CanChange && !enemyRangedTroop.Arrow.IsActive)
                            {
                                enemyRangedTroop.Shoot(playerRangedTroop.Position, content);
                                enemyRangedTroop.CanAttack = false;
                                enemyRangedTroop.IncrementAttackTimer = true;
                            }
                        }
                        if (enemyRangedTroop.Arrow.IsActive && enemyRangedTroop.Arrow.Rectangle.Intersects(playerRangedTroop.Rectangle))
                        {
                            playerRangedTroop.Health -= enemyRangedTroop.Damage;
                            playerRangedTroop.Hit();
                            enemyRangedTroop.Arrow.Kill();
                        }
                    }
                    foreach (Soldier playerTroop in playerTroops)
                    {
                        if ((double)Vector2.Distance(enemyRangedTroop.Position, playerTroop.Position) < 200.0)
                        {
                            flag = false;
                            enemyRangedTroop.SetLeftAttack(gameTime);
                            if (enemyRangedTroop.CanAttack && !enemyRangedTroop.CanChange && !enemyRangedTroop.Arrow.IsActive)
                            {
                                enemyRangedTroop.Shoot(playerTroop.Position, content);
                                enemyRangedTroop.CanAttack = false;
                                enemyRangedTroop.IncrementAttackTimer = true;
                            }
                        }
                        if (enemyRangedTroop.Arrow.IsActive && enemyRangedTroop.Arrow.Rectangle.Intersects(playerTroop.Rectangle))
                        {
                            playerTroop.Health -= enemyRangedTroop.Damage;
                            playerTroop.Hit();
                            enemyRangedTroop.Arrow.Kill();
                        }
                        if (enemyRangedTroop.Rectangle.Intersects(playerTroop.Rectangle))
                        {
                            playerTroop.SetRightAttack(gameTime);
                            if (playerTroop.CanAttack)
                            {
                                enemyRangedTroop.Health -= playerTroop.Damage;
                                enemyRangedTroop.Hit();
                                playerTroop.CanAttack = false;
                                playerTroop.IncrementAttackTimer = true;
                                playerTroop.SoundSwing();
                            }
                        }
                    }
                    foreach (SupportSoldier playerSupportTroop in playerSupportTroops)
                    {
                        if ((double)Vector2.Distance(enemyRangedTroop.Position, playerSupportTroop.Position) < 200.0)
                        {
                            flag = false;
                            enemyRangedTroop.SetLeftAttack(gameTime);
                            if (enemyRangedTroop.CanAttack && !enemyRangedTroop.CanChange && !enemyRangedTroop.Arrow.IsActive)
                            {
                                enemyRangedTroop.Shoot(playerSupportTroop.Position, content);
                                enemyRangedTroop.CanAttack = false;
                                enemyRangedTroop.IncrementAttackTimer = true;
                            }
                        }
                        if (enemyRangedTroop.Arrow.IsActive && enemyRangedTroop.Arrow.Rectangle.Intersects(playerSupportTroop.Rectangle))
                        {
                            playerSupportTroop.Health -= enemyRangedTroop.Damage;
                            playerSupportTroop.Hit();
                            enemyRangedTroop.Arrow.Kill();
                        }
                    }
                    if (flag)
                        enemyRangedTroop.IsMovingForward = true;
                    if (playerTower.Rectangle.Intersects(new Rectangle(enemyRangedTroop.Rectangle.X - 190, enemyRangedTroop.Rectangle.Y, enemyRangedTroop.Rectangle.Width, enemyRangedTroop.Rectangle.Height)))
                    {
                        enemyRangedTroop.IsMovingForward = false;
                        enemyRangedTroop.SetLeftAttack(gameTime);
                        if (enemyRangedTroop.CanAttack && !enemyRangedTroop.CanChange && !enemyRangedTroop.Arrow.IsActive)
                        {
                            enemyRangedTroop.Shoot(playerTower.Position, content);
                            enemyRangedTroop.CanAttack = false;
                            enemyRangedTroop.IncrementAttackTimer = true;
                        }
                    }
                    if (enemyRangedTroop.Arrow.IsActive && enemyRangedTroop.Arrow.Rectangle.Intersects(playerTower.Rectangle))
                    {
                        playerTower.Health -= enemyRangedTroop.Damage;
                        enemyRangedTroop.Arrow.Kill();
                    }
                }
                foreach (SupportSoldier playerSupportTroop in playerSupportTroops)
                {
                    bool flag = true;
                    playerSupportTroop.IsMovingForward = false;
                    foreach (Soldier enemyTroop in enemyTroops)
                    {
                        if (playerSupportTroop.Rectangle.Intersects(enemyTroop.Rectangle))
                        {
                            flag = false;
                            playerSupportTroop.IsMovingForward = false;
                            enemyTroop.IsMovingForward = false;
                            enemyTroop.SetLeftAttack(gameTime);
                            if (enemyTroop.CanAttack)
                            {
                                playerSupportTroop.Health -= enemyTroop.Damage;
                                playerSupportTroop.Hit();
                                enemyTroop.CanAttack = false;
                                enemyTroop.IncrementAttackTimer = true;
                            }
                        }
                    }
                    foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                    {
                        if (playerSupportTroop.Rectangle.Intersects(enemyRangedTroop.Rectangle))
                        {
                            flag = false;
                            playerSupportTroop.IsMovingForward = false;
                        }
                    }
                    if (flag)
                        playerSupportTroop.IsMovingForward = true;
                }

                UpdateSoldiers(gameTime, screenWidth, screenHeight, content, corpses);
            }

            healths = new List<TroopHealth>();
            foreach (Soldier playerTroop in playerTroops)
                healths.Add(new TroopHealth(playerTroop.Position, playerTroop.Health, playerTroop.Name, Color.Blue));
            foreach (Soldier enemyTroop in enemyTroops)
                healths.Add(new TroopHealth(enemyTroop.Position, enemyTroop.Health, enemyTroop.Name, Color.Red));
            foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
                healths.Add(new TroopHealth(playerRangedTroop.Position, playerRangedTroop.Health, playerRangedTroop.Name, Color.Blue));
            foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                healths.Add(new TroopHealth(enemyRangedTroop.Position, enemyRangedTroop.Health, enemyRangedTroop.Name, Color.Red));
            foreach (SupportSoldier playerSupportTroop in playerSupportTroops)
                healths.Add(new TroopHealth(playerSupportTroop.Position, playerSupportTroop.Health, playerSupportTroop.Name, Color.Blue));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Soldier playerTroop in playerTroops)
                playerTroop.Draw(spriteBatch, true, Color.Blue);
            foreach (Soldier enemyTroop in enemyTroops)
                enemyTroop.Draw(spriteBatch, false, Color.Red);
            foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
                playerRangedTroop.Draw(spriteBatch, true, Color.Blue);
            foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
                enemyRangedTroop.Draw(spriteBatch, false, Color.Red);
            foreach (SupportSoldier playerSupportTroop in playerSupportTroops)
                playerSupportTroop.Draw(spriteBatch, true, Color.Blue);
        }

        private void UpdateSoldiers(GameTime gameTime, int screenWidth, int screenHeight, ContentManager content, List<Corpse> corpses)
        {
            bool flag = false;
            for (int index = 0; index < playerTroops.Count; ++index)
            {
                playerTroops[index].Update(gameTime, playerTroops, playerRangedTroops, playerSupportTroops);
                if (playerTroops[index].Health <= 0)
                {
                    corpses.Add(new Corpse(content, new Vector2((float)playerTroops[index].Rectangle.X, (float)(playerTroops[index].Rectangle.Y + 23))));
                    playerTroops.RemoveAt(index);
                    ++troopsLost;
                    flag = true;
                }
            }
            for (int index = 0; index < enemyTroops.Count; ++index)
            {
                enemyTroops[index].Update(gameTime, enemyTroops, enemyRangedTroops);
                if (enemyTroops[index].Health <= 0)
                {
                    Main.gold += enemyTroops[index].Reward;
                    Main.goldGained += enemyTroops[index].Reward;
                    enemyTroops.RemoveAt(index);
                    ++enemiesKilled;
                    flag = true;
                }
            }
            for (int index = 0; index < playerRangedTroops.Count; ++index)
            {
                playerRangedTroops[index].Update(gameTime, playerTroops, playerRangedTroops, playerSupportTroops, screenWidth, screenHeight);
                if (playerRangedTroops[index].Health <= 0)
                {
                    corpses.Add(new Corpse(content, new Vector2(playerRangedTroops[index].Position.X, playerRangedTroops[index].Position.Y + 23f)));
                    playerRangedTroops.RemoveAt(index);
                    ++troopsLost;
                    flag = true;
                }
            }
            for (int index = 0; index < enemyRangedTroops.Count; ++index)
            {
                enemyRangedTroops[index].Update(gameTime, enemyTroops, enemyRangedTroops, screenWidth, screenHeight);
                if (enemyRangedTroops[index].Health <= 0)
                {
                    Main.gold += enemyRangedTroops[index].Reward;
                    Main.goldGained += enemyRangedTroops[index].Reward;
                    enemyRangedTroops.RemoveAt(index);
                    ++enemiesKilled;
                    flag = true;
                }
            }
            for (int index = 0; index < playerSupportTroops.Count; ++index)
            {
                playerSupportTroops[index].Update(gameTime, playerTroops, playerRangedTroops, playerSupportTroops);
                if (playerSupportTroops[index].Health <= 0)
                {
                    corpses.Add(new Corpse(content, new Vector2((float)playerSupportTroops[index].Rectangle.X, (float)(playerSupportTroops[index].Rectangle.Y + 23))));
                    playerSupportTroops.RemoveAt(index);
                    ++troopsLost;
                    flag = true;
                }
            }
            if (!flag)
                return;
            foreach (Soldier enemyTroop in enemyTroops)
            {
                enemyTroop.CanAttack = false;
                enemyTroop.IncrementAttackTimer = true;
                enemyTroop.AttackTimer = 0.0f;
            }
            foreach (RangedSoldier enemyRangedTroop in enemyRangedTroops)
            {
                enemyRangedTroop.CanAttack = false;
                enemyRangedTroop.IncrementAttackTimer = true;
                enemyRangedTroop.AttackTimer = 0.0f;
            }
            foreach (Soldier playerTroop in playerTroops)
            {
                playerTroop.CanAttack = false;
                playerTroop.IncrementAttackTimer = true;
                playerTroop.AttackTimer = 0.0f;
            }
            foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
            {
                playerRangedTroop.CanAttack = false;
                playerRangedTroop.IncrementAttackTimer = true;
                playerRangedTroop.AttackTimer = 0.0f;
            }
        }

        public List<Soldier> EnemyTroops
        {
            get { return enemyTroops; }
        }

        public List<RangedSoldier> EnemyRangedTroops
        {
            get { return enemyRangedTroops; }
        }

        public float GetDistance(Rectangle playerTowerRectangle)
        {
            float num = 1E+11f;
            foreach (Soldier playerTroop in playerTroops)
            {
                if ((double)Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerTroop.Position) < (double)num)
                    num = Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerTroop.Position);
            }
            foreach (RangedSoldier playerRangedTroop in playerRangedTroops)
            {
                if ((double)Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerRangedTroop.Position) < (double)num)
                    num = Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerRangedTroop.Position);
            }
            foreach (SupportSoldier playerSupportTroop in playerSupportTroops)
            {
                if ((double)Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerSupportTroop.Position) < (double)num)
                    num = Vector2.Distance(new Vector2(playerTowerRectangle.X, (float)((double)playerTowerRectangle.Y + (double)playerTowerRectangle.Height - 55.0)), playerSupportTroop.Position);
            }

            return num;
        }

        public void AddSoldier(Queueable soldier)
        {
            if (soldier is Soldier)
                playerTroops.Add((Soldier)soldier);
            else if (soldier is RangedSoldier)
                playerRangedTroops.Add((RangedSoldier)soldier);
            else
                playerSupportTroops.Add((SupportSoldier)soldier);
        }

        public int PlayerSoldierCount
        {
            get { return playerRangedTroops.Count + playerSupportTroops.Count + playerTroops.Count; }
        }
    }
}