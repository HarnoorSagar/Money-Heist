using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Game_Part_3
{
    class GameItem
    {
        //declares properties for inheritance
        protected Color color;
        protected Rectangle rect;
        protected Texture2D sprite;
        protected SoundEffect sound;

        //constructs the Players object with set properties
        public GameItem(Rectangle rect2, Texture2D sprite2, Color color2, SoundEffect sound2)
        {
            //calls setrect and takes in rect2 as a parameter
            this.setRect(rect2);

            //calls setSprite and takes in sprite2 as a parameter
            this.setSprite(sprite2);

            //calls setcolor and takes in color2 as a parameter
            this.setColor(color2);

            //calls setsound and takes in sound2 as a parameter
            this.setSound(sound2);
        }

        //=----------------------------------SET METHODS------------------------------

        //creates the method setRect
        public void setRect(Rectangle rect3)
        {
            //sets "this" rect to the value of this parameter
            this.rect = rect3;
        }

        //creates the method setSprite
        public void setSprite(Texture2D sprite3)
        {
            //sets "this" sprite to the value of this parameter
            this.sprite = sprite3;
        }

        //creates the method setColor
        public void setColor(Color color3)
        {
            //sets "this" color to the value of this parameter
            this.color = color3;
        }

        //creates the method setSound
        public void setSound(SoundEffect sound3)
        {
            //sets "this" sound to the value of this parameter
            this.sound = sound3;
        }

        //------------------------------GET METHODS-----------------------------

        //creates the method getRect
        public Rectangle getRect()
        {
            //returns "this" rect
            return this.rect;
        }

        //creates the method getSprite
        public Texture2D getSprite()
        {
            //returns "this" sprite
            return this.sprite;
        }

        //creates the method getColor
        public Color getColor()
        {
            //returns "this" color
            return this.color;
        }

        //creates the method getSound
        public SoundEffect getSound()
        {
            //returns "this" sound
            return this.sound;
        }

        //------------------------------OTHER METHODS-----------------------------

        //creates the draw method
        public void drawSprite(SpriteBatch spriteBatch)
        {
            //draws the gameItem
            spriteBatch.Draw(this.sprite, this.rect, this.color);
        }

        //creates the HitTest method
        public bool hitTest(Rectangle otherRect)
        {
            //if this rect intersects with another rect
            if (this.rect.Intersects(otherRect))
            {
                //return true
                return true;
            }

            //return false if not hitting
            return false;
        }

        //creates the playSound method, taking in the volume, pitch and pan as parameters
        public void PlaySound(float volume, float pitch, float pan)
        {
            //plays the sound
            this.sound.Play(volume, pitch, pan);

        }
    }
}
