using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    [Serializable]
    class PhysEllipse : PhysObject
    {

        public PhysEllipse()
        {
            this.mass = 7;
            this.position = new Vector2(widthOfPanel / 2, heightOfPanel / 2);
            this.velocity = new Vector2(0, 0);
            this.acceleration = new Vector2(0, 0);
            this.width = 10 * this.mass;
            this.height = 10 * this.mass;
        }
        public PhysEllipse(float mass, Vector2 position)
        {
            this.mass = mass;
            this.position = position;
            this.velocity = new Vector2(0, 0);
            this.acceleration = new Vector2(0, 0);
            this.width = 10*this.mass;
            this.height = 10*this.mass;
        }

        public PhysEllipse(float mass, Vector2 position, Vector2 velocity, float width, float height)
        {
            this.mass = mass;
            this.position = position;
            this.velocity = velocity;
            this.width = width;
            this.height = height;
            this.acceleration = Vector2.Zero;
        }

    }
}
