using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.IO;

namespace TTDLEO_2019_Rerelease
{
    enum ENDGAMERESULT { WIN, LOSS, NONE }

    class BattleManager
    {
        BattleInputManager inputManager;
        
        Tower playerTower;
        Tower enemyTower;
        List<Enemy> enemies;
        
        WizardTower wizardTower;
        bool hasWizardTower;
        List<Corpse> corpses;
        List<Queueable> playerQueue;
        List<Queueable> enemyQueue;
        Queue queue;
        AimableArcher aimableArcher;
        bool hasArcher;

        Turret playerTurretOne;
        Turret playerTurretTwo;
        bool hasTurretOne;
        bool hasTurretTwo;

        SoldierManager soldierManager;

        SoundEffect chargeEffect;
        SoundEffectInstance chargeInstance;

        int currentLevel;

        public BattleManager(ContentManager content, Texture2D buttonTexture, int currentLevel)
        {
            NewLevel(currentLevel, content, buttonTexture);
        }

        public void Update(ContentManager content, int timerSeconds, GameTime gameTime, Rectangle mouseRectangle, float scaleX,
            float scaleY, int screenWidth, int screenHeight, int updatesPerFrame, float startTime)
        {
            SpawnEnemies(timerSeconds, content);
            UpdatePlayerControls(content, gameTime, mouseRectangle, scaleX, scaleY, screenWidth, screenHeight);
            UpdatePlayerTurrets(gameTime, content, screenWidth, screenHeight);

            soldierManager.Update(updatesPerFrame, gameTime, startTime, content, enemyTower, playerTower,
                screenWidth, screenHeight, corpses);

            if (playerQueue.Count > 0)
            {
                float distance = soldierManager.GetDistance(playerTower.Rectangle);

                if (distance > 80)
                {
                    soldierManager.AddSoldier(playerQueue[0]);

                    playerQueue.RemoveAt(0);
                    queue.RemoveMember();
                    if (Main.random.Next(0, 4) == 0)
                    {
                        if (chargeEffect == null)
                            chargeEffect = content.Load<SoundEffect>("chargeEffect");
                        if (chargeInstance == null)
                            chargeInstance = chargeEffect.CreateInstance();
                        chargeInstance.Volume = Main.volume;
                        chargeInstance.Play();
                    }
                }
            }

            //Update inputManager

            List<SoldierPurchases> purchases = inputManager.Update(mouseRectangle, Main.canClick, Main.gold, queue.Count, scaleX, scaleY);

            foreach (SoldierPurchases purchase in purchases)
            {
                if (currentLevel == -1)
                {
                    if (purchase == SoldierPurchases.COMMONER && (soldierManager.PlayerSoldierCount + playerQueue.Count) == 0)
                    {
                        playerQueue.Add((Queueable)new Commoner(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 10;
                        Main.goldSpent += 10;
                        queue.AddMember("Commoner");
                    }

                    return;
                }

                switch (purchase)
                {
                    case SoldierPurchases.COMMONER:
                        playerQueue.Add((Queueable)new Commoner(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 10;
                        Main.goldSpent += 10;
                        queue.AddMember("Commoner");
                        break;
                    case SoldierPurchases.TOUGHGUY:
                        playerQueue.Add((Queueable)new ToughGuy(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 20;
                        Main.goldSpent += 20;
                        queue.AddMember("Tough Guy");
                        break;
                    case SoldierPurchases.BRUTE:
                        playerQueue.Add((Queueable)new Brute(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 40;
                        Main.goldSpent += 40;
                        queue.AddMember("Brute");
                        break;
                    case SoldierPurchases.SWORDSMAN:
                        playerQueue.Add((Queueable)new Swordsman(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 50;
                        Main.goldSpent += 50;
                        queue.AddMember("Swordsman");
                        break;
                    case SoldierPurchases.ARCHER:
                        playerQueue.Add((Queueable)new Archer(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 30;
                        Main.goldSpent += 30;
                        queue.AddMember("Archer");
                        break;
                    case SoldierPurchases.MEDIC:
                        playerQueue.Add((Queueable)new Medic(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 50;
                        Main.goldSpent += 50;
                        queue.AddMember("Medic");
                        break;
                    case SoldierPurchases.GENERAL:
                        playerQueue.Add((Queueable)new General(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 60;
                        Main.goldSpent += 60;
                        queue.AddMember("General");
                        break;
                    case SoldierPurchases.THIEF:
                        playerQueue.Add((Queueable)new Thief(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 15;
                        Main.goldSpent += 15;
                        queue.AddMember("Thief");
                        break;
                    case SoldierPurchases.ROGUE:
                        playerQueue.Add((Queueable)new Rogue(content, enemyTower.Position, new Vector2((float)((double)playerTower.Position.X + (double)playerTower.Rectangle.Width - 30.0), (float)((double)playerTower.Position.Y + (double)playerTower.Rectangle.Height - 55.0))));
                        Main.canClick = false;
                        Main.incrementButtonTimer = true;
                        Main.gold -= 20;
                        Main.goldSpent += 20;
                        queue.AddMember("Rogue");
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle mouseRectangle, float scaleX, float scaleY)
        {
            inputManager.Draw(spriteBatch);
            soldierManager.Draw(spriteBatch);

            playerTower.Draw(spriteBatch);
            enemyTower.Draw(spriteBatch);

            if (hasWizardTower)
                wizardTower.Draw(spriteBatch, mouseRectangle, scaleX, scaleY);
            if (hasArcher)
                aimableArcher.Draw(spriteBatch, scaleX, scaleY);
            
            foreach (Corpse corpse in corpses)
                corpse.Draw(spriteBatch);

            queue.Draw(spriteBatch);

            if (hasTurretOne)
                playerTurretOne.Draw(spriteBatch);
            if (hasTurretTwo)
                playerTurretTwo.Draw(spriteBatch);
        }

        void SpawnEnemies(int timerSeconds, ContentManager content)
        {
            for (int index = 0; index < enemies.Count; ++index)
            {
                if (timerSeconds >= enemies[index].Time)
                {
                    switch (enemies[index].ID)
                    {
                        case 1:
                            enemyQueue.Add((Queueable)new Commoner(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 2:
                            enemyQueue.Add((Queueable)new ToughGuy(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 3:
                            enemyQueue.Add((Queueable)new Brute(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 4:
                            enemyQueue.Add((Queueable)new Swordsman(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 5:
                            enemyQueue.Add((Queueable)new Archer(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 6:
                            enemyQueue.Add((Queueable)new Thief(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 7:
                            enemyQueue.Add((Queueable)new Rogue(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                        case 8:
                            enemyQueue.Add((Queueable)new Guard(content, playerTower.Position, new Vector2((float)((double)enemyTower.Position.X - (double)enemyTower.Rectangle.Width + 20.0), (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0))));
                            break;
                    }
                    enemies.RemoveAt(index);
                }
            }
            if (enemyQueue.Count <= 0)
                return;
            float num = 1E+09f;
            foreach (Soldier enemyTroop in soldierManager.EnemyTroops)
            {
                if ((double)Vector2.Distance(new Vector2(enemyTower.Position.X, (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0)), enemyTroop.Position) < (double)num)
                    num = Vector2.Distance(new Vector2(enemyTower.Position.X, (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0)), enemyTroop.Position);
            }
            foreach (RangedSoldier enemyRangedTroop in soldierManager.EnemyRangedTroops)
            {
                if ((double)Vector2.Distance(new Vector2(enemyTower.Position.X, (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0)), enemyRangedTroop.Position) < (double)num)
                    num = Vector2.Distance(new Vector2(enemyTower.Position.X, (float)((double)enemyTower.Position.Y + (double)enemyTower.Rectangle.Height - 55.0)), enemyRangedTroop.Position);
            }
            if ((double)num > 80.0)
            {
                if (enemyQueue[0] is Soldier)
                    soldierManager.EnemyTroops.Add((Soldier)enemyQueue[0]);
                else
                    soldierManager.EnemyRangedTroops.Add((RangedSoldier)enemyQueue[0]);
                enemyQueue.RemoveAt(0);
            }
        }

        void UpdatePlayerControls(ContentManager content, GameTime gameTime, Rectangle mouseRectangle, float scaleX, float scaleY,
            int screenWidth, int screenHeight)
        {
            if (hasWizardTower)
            {
                if (hasArcher)
                    wizardTower.Update(content, gameTime, soldierManager.EnemyTroops, soldierManager.EnemyRangedTroops, enemyTower.Position, ref corpses, ref playerQueue, playerTower.Position, !aimableArcher.IsActive, mouseRectangle, scaleX, scaleY, ref queue);
                else
                    wizardTower.Update(content, gameTime, soldierManager.EnemyTroops, soldierManager.EnemyRangedTroops, enemyTower.Position, ref corpses, ref playerQueue, playerTower.Position, true, mouseRectangle, scaleX, scaleY, ref queue);
            }
            if (hasArcher)
            {
                aimableArcher.Update(gameTime, content, soldierManager.EnemyTroops, soldierManager.EnemyRangedTroops, screenWidth, screenHeight, scaleX, scaleY);
                if (Keyboard.GetState().IsKeyDown(Keys.F) /**&& Main.canClick**/ && (!hasWizardTower || wizardTower.Aiming == Aiming.NA))
                {
                    //Main.incrementButtonTimer = true;
                    //buttonTimer = 0;
                    //Main.canClick = false;
                    aimableArcher.Activate();
                }
            }
        }

        public void Pause()
        {
            if (wizardTower != null)
                wizardTower.Pause();
            if (playerTurretOne != null)
                playerTurretOne.Pause();
            if (playerTurretTwo != null)
                playerTurretTwo.Pause();
        }

        public void Resume()
        {
            if (wizardTower != null)
                wizardTower.Play();
            if (playerTurretOne != null)
                playerTurretOne.Resume();
            if (playerTurretTwo != null)
                playerTurretTwo.Resume();
        }

        private void UpdatePlayerTurrets(GameTime gameTime, ContentManager content, int screenWidth, int screenHeight)
        {
            if (hasTurretOne)
            {
                playerTurretOne.Update(gameTime, screenWidth, screenHeight);
                if (playerTurretOne.CanAttack)
                {
                    foreach (Soldier enemyTroop in soldierManager.EnemyTroops)
                    {
                        if ((double)Vector2.Distance(playerTurretOne.Position, enemyTroop.Position) < 400.0 && !playerTurretOne.Arrow.IsActive && (double)enemyTroop.Position.X > (double)playerTurretOne.Position.X)
                            playerTurretOne.Shoot(enemyTroop.Position, content);
                    }
                    foreach (RangedSoldier enemyRangedTroop in soldierManager.EnemyRangedTroops)
                    {
                        if ((double)Vector2.Distance(playerTurretOne.Position, enemyRangedTroop.Position) < 400.0 && !playerTurretOne.Arrow.IsActive && (double)enemyRangedTroop.Position.X > (double)playerTurretOne.Position.X)
                            playerTurretOne.Shoot(enemyRangedTroop.Position, content);
                    }
                }
                foreach (Soldier enemyTroop in soldierManager.EnemyTroops)
                {
                    if (playerTurretOne.Arrow.Rectangle.Intersects(enemyTroop.Rectangle) && playerTurretOne.Arrow.IsActive)
                    {
                        enemyTroop.Health -= playerTurretOne.Damage;
                        enemyTroop.Hit();
                        playerTurretOne.Arrow.Kill();
                    }
                }
                foreach (RangedSoldier enemyRangedTroop in soldierManager.EnemyRangedTroops)
                {
                    if (playerTurretOne.Arrow.Rectangle.Intersects(enemyRangedTroop.Rectangle) && playerTurretOne.Arrow.IsActive)
                    {
                        enemyRangedTroop.Health -= playerTurretOne.Damage;
                        enemyRangedTroop.Hit();
                        playerTurretOne.Arrow.Kill();
                    }
                }
            }
            if (!hasTurretTwo)
                return;

            playerTurretTwo.Update(gameTime, screenWidth, screenHeight);
            foreach (Soldier enemyTroop in soldierManager.EnemyTroops)
            {
                if ((double)Vector2.Distance(playerTurretTwo.Position, enemyTroop.Position) < 275.0 && playerTurretTwo.CanAttack && (!playerTurretTwo.Arrow.IsActive && (double)enemyTroop.Position.X > (double)playerTurretTwo.Position.X))
                    playerTurretTwo.Shoot(enemyTroop.Position, content);
            }
            foreach (RangedSoldier enemyRangedTroop in soldierManager.EnemyRangedTroops)
            {
                if ((double)Vector2.Distance(playerTurretTwo.Position, enemyRangedTroop.Position) < 275.0 && playerTurretTwo.CanAttack && (!playerTurretTwo.Arrow.IsActive && (double)enemyRangedTroop.Position.X > (double)playerTurretTwo.Position.X))
                    playerTurretTwo.Shoot(enemyRangedTroop.Position, content);
            }
            if (playerTurretTwo.Arrow.IsActive)
            {
                foreach (Soldier enemyTroop in soldierManager.EnemyTroops)
                {
                    if (playerTurretTwo.Arrow.Rectangle.Intersects(enemyTroop.Rectangle) && playerTurretTwo.Arrow.IsActive)
                    {
                        enemyTroop.Health -= playerTurretTwo.Damage;
                        enemyTroop.Hit();
                        playerTurretTwo.Arrow.Kill();
                    }
                }
                foreach (RangedSoldier enemyRangedTroop in soldierManager.EnemyRangedTroops)
                {
                    if (playerTurretTwo.Arrow.Rectangle.Intersects(enemyRangedTroop.Rectangle) && playerTurretTwo.Arrow.IsActive)
                    {
                        enemyRangedTroop.Health -= playerTurretTwo.Damage;
                        enemyRangedTroop.Hit();
                        playerTurretTwo.Arrow.Kill();
                    }
                }
            }
        }

        public List<String> GetSaveInfo()
        {
            List<String> output = new List<string>();
            output.Add(playerTower.Health.ToString());
            output.Add(hasTurretOne.ToString());
            output.Add(hasTurretTwo.ToString());
            output.Add(hasWizardTower.ToString());
            output.Add(hasArcher.ToString());

            return output;
        }

        public void SetSaveData(int playerTowerHealth, bool hasTurretOne, bool hasTurretTwo, bool hasWizardTower, bool hasArcher)
        {
            playerTower.Health = playerTowerHealth;
            this.hasTurretOne = hasTurretOne;
            this.hasTurretTwo = hasTurretTwo;
            this.hasWizardTower = hasWizardTower;
            this.hasArcher = hasArcher;
        }

        public bool CanBuy
        {
            get { return (!hasArcher || !aimableArcher.IsActive) && (!hasWizardTower || wizardTower.Aiming == Aiming.NA); }
        }

        public void RestoreHealth()
        {
            playerTower.Health = 100;
        }

        public void BuyTurretOne(ContentManager content)
        {
            playerTurretOne = new Turret(content, playerTower.Rectangle, 3, "turretRightStraight");
            hasTurretOne = true;
        }

        public void BuyTurretTwo(ContentManager content)
        {
            playerTurretTwo = new Turret(playerTower.Rectangle, content, 1, "turretRightDown");
            hasTurretTwo = true;
        }

        public void BuyArcher(ContentManager content)
        {
            hasArcher = true;
            aimableArcher = new AimableArcher(content);
        }

        public void BuyWizard(ContentManager content)
        {
            hasWizardTower = true;
            wizardTower = new WizardTower(content, new Vector2(playerTower.Position.X, playerTower.Position.Y));
        }

        public ENDGAMERESULT CheckForEndGame(ContentManager content)
        {
            ENDGAMERESULT result = ENDGAMERESULT.NONE;

            if (enemyTower.Health <= 0)
            {
                result = ENDGAMERESULT.WIN;

                playerQueue = new List<Queueable>();
                enemyQueue = new List<Queueable>();
                soldierManager = new SoldierManager();

                if (chargeInstance != null)
                    chargeInstance.Stop();
                queue.Clear();
                if (hasWizardTower)
                    wizardTower.Stop();

                corpses = new List<Corpse>();
                if (hasWizardTower)
                    wizardTower = new WizardTower(content, new Vector2(playerTower.Position.X, playerTower.Position.Y));

                if (hasArcher && aimableArcher.IsActive)
                    aimableArcher.Activate();
            }
            else if (playerTower.Health <= 0)
            {
                result = ENDGAMERESULT.LOSS;

                soldierManager = new SoldierManager();

                playerQueue = new List<Queueable>();
                enemyQueue = new List<Queueable>();
                corpses = new List<Corpse>();
                if (hasWizardTower)
                    wizardTower = new WizardTower(content, new Vector2(playerTower.Position.X, playerTower.Position.Y));
                playerTower.Health = 1;
                if (hasArcher && aimableArcher.IsActive)
                    aimableArcher.Activate();

                if (chargeInstance != null)
                    chargeInstance.Stop();
                queue.Clear();
                if (hasWizardTower)
                    wizardTower.Stop();
            }

            return result;
        }

        public void NewLevel(int currentLevel, ContentManager content, Texture2D buttonTexture)
        {
            this.currentLevel = currentLevel;

            enemies = new List<Enemy>();

            if (playerTower == null)
                playerTower = new Tower(content, new Vector2(0.0f, 280f), "playerTower");

            enemyTower = new Tower(content, new Vector2(730f, 280f), "enemyTower");
            corpses = new List<Corpse>();
            playerQueue = new List<Queueable>();
            enemyQueue = new List<Queueable>();
            queue = new Queue(content);

            inputManager = new BattleInputManager(buttonTexture, content);
            soldierManager = new SoldierManager();

            if (currentLevel == -1 || currentLevel == -2)
                return;

            StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/Level Data/level" + currentLevel.ToString() + ".txt");

            string[] strArray = reader.ReadToEnd().Split('\n');
            for (int index = 0; index < strArray.Length; index += 2)
                enemies.Add(new Enemy(Convert.ToInt32(strArray[index]), Convert.ToInt32(strArray[index + 1])));

            if (hasTurretOne)
                playerTurretOne = new Turret(content, playerTower.Rectangle, 3, "turretRightStraight");
            if (hasTurretTwo)
                playerTurretTwo = new Turret(playerTower.Rectangle, content, 1, "turretRightDown");
            if (hasWizardTower)
                wizardTower = new WizardTower(content, new Vector2(playerTower.Position.X, playerTower.Position.Y));
            if (hasArcher)
                aimableArcher = new AimableArcher(content);

        }
    }
}