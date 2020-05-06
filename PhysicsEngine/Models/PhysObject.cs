using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhysicsEngine
{
    [Serializable]
    public class PhysObject
    {
        protected Vector2 position;
        public Vector2 Position { get { return position; } set { position = value; } }
        protected Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }
        protected Vector2 acceleration;
        protected float mass;
        public float Mass { get { return mass; } }
        protected float width;
        public float Width { get { return width; } }
        public float Height { get { return height; } }
        protected float height;
        protected int widthOfPanel;
        protected int heightOfPanel;

        public int WidthOfPanel { set { widthOfPanel = value; } }
        public int HeightOfPanel { set { heightOfPanel = value; } }

        public void ApplyForce(Vector2 force)
        {
            this.acceleration += force/this.mass;
        }

        public void DeleteAcceleration()
        {
            this.acceleration = Vector2.Zero;
        }

        public virtual void Update()
        {
            velocity += acceleration;
            position += velocity;
        }

        public virtual string StateString()
        {
            string result = "";
            result += $"Mass = {mass} units\n";
            result += $"Radius = {width} pixels\n";
            result += $"PositionX = {Math.Round(position.X, 2)} pixels\n";
            result += $"PositionY = {Math.Round(position.Y, 2)} pixels\n";
            result += $"VelocityX = {Math.Round(velocity.X*100, 2)} pixels/second\n";
            result += $"VelocityX = {Math.Round(velocity.Y*100, 2)} pixels/seconds\n";
            result += $"AccelerationX = {Math.Round(acceleration.X*100, 2)} pixels/seconds^2\n";
            result += $"AccelerationY = {Math.Round(acceleration.Y*100, 2)} pixels/seconds^2";

            return result;
        }

    }
}
