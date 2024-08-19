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
    class Players: MovingGameItem
    {
        //declares properties
        protected int maxUpper;
        protected double upper;
        protected double gravity = 5;
        protected bool jumping = false;
        protected bool falling = false;

        //constructs the Players object with set properties
        public Players(int maxUpper2, Direction direction2, int speed2, Rectangle rect2, Texture2D sprite2, Color color2, SoundEffect sound2) : base(direction2, speed2, rect2, sprite2, color2, sound2)
        {
            //calls the setMaxUpper method and sets to the value of the parameter
            this.setMaxUpper(maxUpper2);
        }
        //creates the setMaxUpper method
        public void setMaxUpper(int maxUpper3)
        {
            //sets "this" maxUpper to the value of this parameter
            this.maxUpper = maxUpper3;
        }
        //creates the getMaxUpper method
        public int getMaxUpper()
        {
            //return "this" maxUpper
            return this.maxUpper;
        }

        //creates the getJumping method
        public bool getJumping()
        {
            //return "this" maxUpper
            return this.jumping;
        }
        //creates the setJumping method
        public void setJumping(bool jumping2)
        {
            //sets "this" jumping to the value of this parameter
            this.jumping = jumping2;
        }
        //creates the getGravity method
        public double getGravity()
        {
            //return "this" maxUpper
            return this.gravity;
        }
        //creates the setGravity method
        public void setGravity(double gravity2)
        {
            //sets "this" jumping to the value of this parameter
            this.gravity = gravity2;
        }
        //creates the getFalling method
        public bool getFalling()
        {
            //return "this" maxUpper
            return this.falling;
        }
        //creates the setFalling method
        public void setFalling(bool falling2)
        {
            //sets "this" falling to the value of this parameter
            this.falling = falling2;
        }

        //--------------------------------OTHER METHODS---------------------------------
        public void Jump()
        {
            //if the character is not jumping or falling
            if (!jumping && !falling)
            {
                //plays the sound
                this.PlaySound((float) 0.7, 0, 0);

                //sets jumping to true
                jumping = true;

                //sets the upper value to the max upper value
                upper = maxUpper;
            }
        }

        //creates the Update method
        public void Update()
        {
            //if jumping is true
            if (jumping)
            {
                //runs the move method with direction as up and speed as upper
                this.Move(Direction.up, (int)upper);
                
                //decrement upper by 1
                upper -= 0.4;

                //if upper value is less than 1
                if (upper < 1)
                {
                    //set jumping to false
                    jumping = false;
                    //set falling to true
                    falling = true;
                }
            }
            //if falling is true
            if (falling)
            {
                //increments gravity by 1
                gravity += 0.4;

                //runs the move method with direction as down and speed as gravity
                this.Move(Direction.down, (int)gravity);
            }
        }
    }
}
