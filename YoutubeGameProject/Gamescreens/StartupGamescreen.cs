using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace YoutubeGameProject {
    public class StartupGamescreen : Gamescreen {
        Texture2D spaceIslandTexture;
        Sprite tinyMaleSprite;
        Sprite evilTinyMaleSprite;
        Font smallFont;

        Texture2D backgroundTexture;
        Sprite backgroundTexture_TileOne;
        Sprite backgroungTexture_TileTwo;
        private bool isCollision = false;

        public StartupGamescreen() {
        }

        public override void Initialize() {
            base.Initialize();
        }

        public override void LoadContent(ContentManager pContentManager) {
            base.LoadContent(pContentManager);

            spaceIslandTexture = pContentManager.GetTexture("Content/Sprites/space-island.png");
            Texture2D tinyMaleTexture = pContentManager.GetTexture("Content/Sprites/tiny-male.png");
            tinyMaleSprite = new Sprite(tinyMaleTexture, new Vector2(100, 100), Color.White, 4f);
            Texture2D evilTinyMaleTexture = pContentManager.GetTexture("Content/Sprites/evil-tiny-male.png");

            evilTinyMaleSprite = new Sprite(evilTinyMaleTexture, new Vector2(300, 100), Color.White,4f);

            Texture2D fontSpriteTexture = pContentManager.GetTexture("Content/Fonts/small-font.png");
            FramedSprite fontSprite = new FramedSprite(8, 6, 0, fontSpriteTexture, Vector2.Zero, Color.White, 8f);

            var mapping = pContentManager.GetFontMapping("Content/Fonts/small-font.fontmapping");

            smallFont = new Font(fontSprite, mapping, 0, 1, Color.SeaGreen);

            //Scrolling Background - simple
            backgroundTexture = pContentManager.GetTexture("Content/Sprites/seamless_flame.jpg");
            backgroundTexture_TileOne = new Sprite(backgroundTexture, Vector2.Zero, Color.White, 1f);
            backgroungTexture_TileTwo = new Sprite(backgroundTexture, new Vector2(backgroundTexture.Width,0), Color.White,1f);

        }

        public override void Update(GameTime pGameTime) {
            base.Update(pGameTime);

            //Update scrolling background
            float pixelsPerSecond = 100;
            float pixelsPerFrame = pixelsPerSecond * pGameTime.ElapsedGameTime.Milliseconds / 1000f;
            var TileOnePrevPos = backgroundTexture_TileOne.Position;

            var TileTwoPrevPos = backgroungTexture_TileTwo.Position;
            if (TileOnePrevPos.X < -backgroundTexture.Width)
            {
                TileOnePrevPos.X = TileTwoPrevPos.X + backgroundTexture.Width;
            }
            if (TileTwoPrevPos.X < -backgroundTexture.Width)
            {
                TileTwoPrevPos.X = TileOnePrevPos.X + backgroundTexture.Width;
            }

            backgroundTexture_TileOne.SetPosition(TileOnePrevPos.X - pixelsPerFrame, TileOnePrevPos.Y);
            backgroungTexture_TileTwo.SetPosition(TileTwoPrevPos.X - pixelsPerFrame, TileTwoPrevPos.Y);
            // END scrolling background



            if (YoutubeGame.Instance.InputManager[Input.Back].IsPressed) {
                quit = true;
            }

            if (YoutubeGame.Instance.InputManager[Input.Up].IsHeld) {
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X, tinyMaleSprite.Position.Y - 200 * pGameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            if (YoutubeGame.Instance.InputManager[Input.Down].IsHeld) {
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X, tinyMaleSprite.Position.Y + 200 * pGameTime.ElapsedGameTime.Milliseconds / 1000f);
            }
            if (YoutubeGame.Instance.InputManager[Input.Left].IsHeld) {
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X - 200 * pGameTime.ElapsedGameTime.Milliseconds / 1000f, tinyMaleSprite.Position.Y);
            }
            if (YoutubeGame.Instance.InputManager[Input.Right].IsHeld) {
                tinyMaleSprite.SetPosition(tinyMaleSprite.Position.X + 200 * pGameTime.ElapsedGameTime.Milliseconds / 1000f, tinyMaleSprite.Position.Y);
            }

            tinyMaleSprite.Update(pGameTime);
            evilTinyMaleSprite.Update(pGameTime);



            //Check collisions - simple
            isCollision = false;
            if(tinyMaleSprite.CollisionBox.Contains(evilTinyMaleSprite.CollisionBox) == ContainmentType.Intersects)
            {
                isCollision = true;
            }

        }

        public override void Draw(SpriteBatch pSpriteBatch) {
            base.Draw(pSpriteBatch);

            if (initialized) {
                pSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap);

                //Scrolling Background
                backgroundTexture_TileOne.Draw(pSpriteBatch);
                backgroungTexture_TileTwo.Draw(pSpriteBatch);

                pSpriteBatch.Draw(spaceIslandTexture, Vector2.Zero, Color.White);
                tinyMaleSprite.Draw(pSpriteBatch, 4f);
                evilTinyMaleSprite.Draw(pSpriteBatch, 4f);
                //smallFont.DrawString(pSpriteBatch, "Hello World!", new Vector2(20, 20), 8f);
                smallFont.DrawString(pSpriteBatch, (isCollision == true) ? "Do not touch me" : "Hello world" , new Vector2(20, 20), 8f);

                pSpriteBatch.End();
            }
        }
    }
}
