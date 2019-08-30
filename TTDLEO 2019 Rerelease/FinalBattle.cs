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
    enum FinalBattleResult { WIN, LOSS, NONE }

    class FinalBattle
    {
        DefendingSwordsman finalHero;
        FinalMorgoroth morgoroth;
        Texture2D finalBattleBackground;

        public FinalBattle(ContentManager content)
        {
            finalHero = new DefendingSwordsman(content);
            morgoroth = new FinalMorgoroth(content);
            finalBattleBackground = content.Load<Texture2D>("finalBattleBackground");
        }

        public FinalBattleResult Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            finalHero.Update(gameTime, morgoroth.Rectangle);
            morgoroth.Update(gameTime, finalHero, screenWidth, screenHeight);

            if (finalHero.Rectangle.Intersects(morgoroth.Rectangle))
            {
                if (finalHero.CanAttack)
                {
                    switch (finalHero.State)
                    {
                        case AnimationState.ATTACKINGUP:
                            if (morgoroth.Rectangle.Y < finalHero.Rectangle.Y)
                            {
                                morgoroth.Health -= finalHero.Damage;
                                morgoroth.Hit();
                                finalHero.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGDOWN:
                            if (morgoroth.Rectangle.Y > finalHero.Rectangle.Y)
                            {
                                morgoroth.Health -= finalHero.Damage;
                                morgoroth.Hit();
                                finalHero.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGLEFT:
                            if (morgoroth.Rectangle.X < finalHero.Rectangle.X)
                            {
                                morgoroth.Health -= finalHero.Damage;
                                morgoroth.Hit();
                                finalHero.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGRIGHT:
                            if (morgoroth.Rectangle.X > finalHero.Rectangle.X)
                            {
                                morgoroth.Health -= finalHero.Damage;
                                morgoroth.Hit();
                                finalHero.Attack();
                                break;
                            }
                            break;
                    }
                }
                if (morgoroth.CanAttack)
                {
                    switch (morgoroth.State)
                    {
                        case AnimationState.ATTACKINGUP:
                            if (finalHero.Rectangle.Y < morgoroth.Rectangle.Y)
                            {
                                finalHero.Health -= morgoroth.Damage;
                                finalHero.Hit();
                                morgoroth.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGDOWN:
                            if (finalHero.Rectangle.Y > morgoroth.Rectangle.Y)
                            {
                                finalHero.Health -= morgoroth.Damage;
                                finalHero.Hit();
                                morgoroth.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGLEFT:
                            if (finalHero.Rectangle.X < morgoroth.Rectangle.X)
                            {
                                finalHero.Health -= morgoroth.Damage;
                                finalHero.Hit();
                                morgoroth.Attack();
                                break;
                            }
                            break;
                        case AnimationState.ATTACKINGRIGHT:
                            if (finalHero.Rectangle.X > morgoroth.Rectangle.X)
                            {
                                finalHero.Health -= morgoroth.Damage;
                                finalHero.Hit();
                                morgoroth.Attack();
                                break;
                            }
                            break;
                    }
                }
            }

            if (finalHero.Health <= 0)
                return FinalBattleResult.LOSS;

            if (morgoroth.Health <= 0)
                return FinalBattleResult.WIN;

            return FinalBattleResult.NONE;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(finalBattleBackground, Vector2.Zero, Color.White);
            finalHero.Draw(spriteBatch);
            morgoroth.Draw(spriteBatch);
        }
    }
}