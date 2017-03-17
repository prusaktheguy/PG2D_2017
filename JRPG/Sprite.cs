using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrpgProject {
    public class Sprite {



        protected Vector2 origin;
        protected Vector2 center;


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

        protected Boolean isRotated = false;
        public Boolean IsRotated
        {
            get
            {
                return isRotated;
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
        protected float radius;
        public float Radius
        {
            get
            {
                return radius;
            }
        }

        protected BoundingSphere boundingsphere;

        public BoundingSphere CollisionShpere
        {
            get
            {
                return boundingsphere;
            }
        }



        public Sprite(Texture2D pTexture, Vector2 pPosition, Color pTint, float scale) {
            position = pPosition;
            texture = pTexture;
            tint = pTint;
            scalingFactor = scale;
            UpdateBoundingBox();
            UpdateBoundingSphere();

        }


        public void SetTint(Color pTint) {
            tint = pTint;
        }



        public void SetPosition(Vector2 pPosition) {
            position = pPosition;
        }

        public void SetPosition(float pX, float pY)
        {
            position = new Vector2(pX, pY);
        }

        public void Update(GameTime pGameTime)
        {


            if (isRotated)
            {
                UpdateBoundingSphere();

            }
            else
            {
                UpdateBoundingBox();
            }
        }


        private void UpdateBoundingSphere()
        {
            center = new Vector2(((position.X + (texture.Width * scalingFactor))) / 2, (position.Y + (texture.Height * scalingFactor)) / 2);

            //polowa przekatnej
            radius = Convert.ToSingle(Math.Sqrt(Math.Pow(texture.Height*scalingFactor,2) + Math.Pow(texture.Width * scalingFactor, 2)))/2;

            boundingsphere = new BoundingSphere(new Vector3(center, 0), radius);
        }
        private void UpdateBoundingBox()
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            boundingBox = new BoundingBox(new Vector3(position, 0), new Vector3(position.X + texture.Width*scalingFactor, position.Y+texture.Height*scalingFactor, 0 )) ;

        }

        public virtual void Rotate(SpriteBatch pSpriteBatch,float rotation,float pScale = 1f)
        {
            isRotated = true;
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            pSpriteBatch.Draw(texture, position, null, null, origin, rotation, new Vector2(scalingFactor, scalingFactor), tint, SpriteEffects.None, 0);

        }



        public virtual void Draw(SpriteBatch pSpriteBatch, float pScale = 1f) {
            isRotated = false;

            pSpriteBatch.Draw(texture, position, null, null, null, 0, new Vector2(scalingFactor, scalingFactor), tint, SpriteEffects.None, 0);
        }
    }
}
