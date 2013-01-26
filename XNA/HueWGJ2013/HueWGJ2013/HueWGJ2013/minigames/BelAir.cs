using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HueWGJ2013;


namespace HueWGJ2013.minigames
{
    class BelAir:AMinigame
    {
        Texture2D img_baller;
        Texture2D pixel1;

        Rectangle powerBg;
        Rectangle powerWin;
        Rectangle powerCur;

        Vector2 powerBgPos;
        Vector2 powerWinPos;
        Vector2 powerCurPos;
        Vector2 ballerPos;

        int barDir;
        bool throwing;

        public BelAir(ContentManager c)
            : base(c)
        {
            this.Content = c;
        }

        public override void load(SpriteFont font)
        {
            this.font  = font;
            img_baller = Content.Load<Texture2D>("minigames/BelAir/baller");
            pixel1     = Game1.hueGraphics.getSolidTexture();
        }

        public override void draw(SpriteBatch sb)
        {
            switch (state)
            {
                case State.PLAY:
                    sb.Draw(img_baller, ballerPos, Color.White);
                    sb.Draw(pixel1, powerBg, Color.Red);
                    sb.Draw(pixel1, powerWin, Color.Green);
                    sb.Draw(pixel1, powerCur, Color.Black);
                    break;
                case State.WIN:
                    sb.Draw(img_baller, ballerPos, Color.White);
                    sb.DrawString(font, "WIN!", ballerPos, Color.Green);
                    break;
            }
        }

        public override bool update(KeyboardState kb, MouseState ms)
        {
            switch(state)
            {
                case State.START:
                    powerBgPos = new Vector2(20,20);
                    powerWinPos = new Vector2(80,20);
                    powerCurPos = new Vector2(20,20);
                    ballerPos = new Vector2(20, 180);

                    powerBg = new Rectangle();
                    powerBg.Width = 100;
                    powerBg.Height = 20;
                    powerBg.Offset((int)powerBgPos.X, (int)powerBgPos.Y);

                    powerWin = new Rectangle();
                    powerWin.Width = 20;
                    powerWin.Height = 20;
                    powerWin.Offset((int)powerWinPos.X, (int)powerWinPos.Y);

                    powerCur = new Rectangle();
                    powerCur.Width = 2;
                    powerCur.Height = 20;
                    powerCur.Offset((int)powerCurPos.X, (int)powerCurPos.Y);

                    throwing = false;
                    barDir = 0;
                    //Ready to go!
                    state = State.PLAY;
                    break;
                case State.PLAY:
                    if (kb.IsKeyDown(Keys.Space) && throwing == false)
                    {
                        throwing = true;
                        barDir = 1;
                        powerCurPos.X += barDir;
                    }
                    if (kb.IsKeyDown(Keys.Space) && throwing == true)
                    {
                        if (powerCurPos.X >= (powerBgPos.X + powerBg.Width))
                        {
                            barDir = -1;
                        }
                        else
                        {
                            barDir = 1;
                        }
                        powerCurPos.X += barDir;
                    }
                    if (!kb.IsKeyDown(Keys.Space) && throwing == false)
                    {
                        if( (powerCurPos.X >= powerWinPos.X) && (powerCurPos.X <= (powerWinPos.X + powerWin.Width) ) )
                        {
                            state = State.WIN;
                        }
                        else
                        {
                            state = State.LOSE;
                        }
                    }
                    break;
                case State.EXIT:
                    // Last shit here
                    return false;
                default:
                    break;
            }
            return true;
        }
    }
}

