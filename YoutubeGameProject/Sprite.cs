using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrpgProject {
    public class Sprite {

        protected float scalingFactor = 1.0f;
        public float ScalingFactor
        {
            get
            {
                return scalingFactor;
            }
            set
            {
                if(scalingFactor <= 0f)
                {
                    scalingFactor = 1.0f;
                }
                else { scalingFactor = ScalingFactor; }
            }
        }


        protected Vector2 position;
        public Vector2 Position {
            get {
                return position;
            }
        }

        protected Texture2D texture;
        public Texture2D Texture {
            get {
                return texture;
            }
        }

        protected Color tint;
        public Color Tint {
            get {
                return tint;
            }
        }

        protected BoundingBox boundingBox;
        public BoundingBox CollisionBox
        {
            get
            {
                return boundingBox;
            }
        }



        public Sprite(Texture2D pTexture, Vector2 pPosition, Color pTint, float scale) {
            position = pPosition;
            texture = pTexture;
            tint = pTint;
            scalingFactor = scale;
            UpdateBoundingBox();

        }

        public void SetTint(Color pTint) {
            tint = pTint;
        }

        public void SetPosition(Vector2 pPosition) {
            position = pPosition;
        }

        public void SetPosition(float pX, float pY) {
            position = new Vector2(pX, pY);
        }

        public void Update(GameTime pGameTime) {
            UpdateBoundingBox();
        }


        private void UpdateBoundingBox()
        {
            //what about scaling???
            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + texture.Width*scalingFactor, position.Y+texture.Height*scalingFactor, 0 )) ;
        }


        public virtual void Draw(SpriteBatch pSpriteBatch, float pScale = 1f) {
            pSpriteBatch.Draw(texture, position, null, null, null, 0, new Vector2(scalingFactor, scalingFactor), tint, SpriteEffects.None, 0);
        }
    }
}
