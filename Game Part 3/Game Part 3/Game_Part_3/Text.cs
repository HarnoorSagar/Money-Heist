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
    class Text: GameItem
    {
        //declares properties
        protected string text;

        //constructs the Text object with set properties
        public Text(string text2, Rectangle rect2, Texture2D sprite2, Color color2, SoundEffect sound2) : base(rect2, sprite2, color2, sound2)
        {
            //calls the settext method and takes in the value of the parameter
            this.setText(text2);
        }

        //creates the method setText with a aparameter text2
        public void setText(string text2)
        {
            //sets "this" text to the value of the parameter
            this.text = text2;
        }

        //creates the method gettext
        public String getText()
        {
            //returns "this" text
            return this.text;
        }

        //creates the drawText method with the parameters spritebatch, font and vector2
        public void DrawText(SpriteBatch spriteBatch, SpriteFont font, Vector2 textVector)
        {
            //draws the text
            spriteBatch.DrawString(font, this.text, textVector, this.color);
        }
    }
}
