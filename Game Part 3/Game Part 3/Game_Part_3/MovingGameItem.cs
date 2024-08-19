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
    class MovingGameItem: GameItem
    {
        //declares properties
        protected int speed;
        protected Direction direction;

        //creates the direction enum, with the left and right directions
        public enum Direction
        {
            up, down, left, right
        }

        //constructs the MovingGameItem object with set properties
        public MovingGameItem(Direction direction2, int speed2, Rectangle rect2, Texture2D sprite2, Color color2, SoundEffect sound2): base(rect2, sprite2, color2, sound2)
        {
            //calls the setSpeed method and sets to the value of the parameter
            this.setSpeed(speed2);

            //calls the setSpeed method and sets to the value of the parameter
            this.setDirection(direction2);
        }

        //--------------------------------SET METHODS---------------------------------

        //creates the setSpeed method
        public void setSpeed(int speed3)
        {
            //sets "this" speed to the value of this parameter
            this.speed = speed3;
        }

        //creates the setDirection method
        public void setDirection(Direction direction3)
        {
            //sets "this" direction to the value of this parameter
            this.direction = direction3;
        }

        //--------------------------------GET METHODS---------------------------------

        //creates the getSpeed method
        public int getSpeed()
        {
            //returns "this" speed
            return this.speed;
        }

        //creates the getDirection method
        public Direction getDirection()
        {
            //returns "this" direction
            return this.direction;
        }

        //--------------------------------MOVE METHODS---------------------------------

        //creates the move method with direction as a parameter
        public void Move(Direction direction4)
        {
            //calls the other move method with an extra parameter
            this.Move(direction4, this.speed);
        }

        //creates another move method with an additional speed parameter
        public void Move(Direction direction5, int speed)
        {
            //if the direction is left
            if (direction5 == Direction.left)
            {
                //move the rectangle left
                this.rect.X -= speed;
            }
            //otherwise, if direction is right
            else if (direction5 == Direction.right)
            {
                //move the rectangle right
                this.rect.X += speed;
            }
            //if the direction is up
            else if (direction5 == Direction.up)
            {
                //move the rectangle up
                this.rect.Y -= speed;
            }
            //otherwise, if direction is down
            else if (direction5 == Direction.down)
            {
                //move the rectangle down
                this.rect.Y += speed;
            }
        }
    }
}
