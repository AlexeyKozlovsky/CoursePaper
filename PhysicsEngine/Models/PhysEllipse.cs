using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    /// <summary>
    /// Класс, описывающий физический объект в форме эллипса
    /// </summary>
    [Serializable]
    public class PhysEllipse : PhysObject
    {
        /// <summary>
        /// Создает физический объект в форме эллписа в нулевым ускорением, скоростью и позицией.
        /// </summary>
        public PhysEllipse()
        {
            this.mass = 7;
            this.position = Vector2.Zero;
            this.velocity = Vector2.Zero;
            this.acceleration = Vector2.Zero;
            this.width = 10 * this.mass;
            this.height = 10 * this.mass;
        }

        /// <summary>
        /// Создает физический объект в форме эллипса с заданной массой и положением
        /// </summary>
        /// <param name="mass">Масса</param>
        /// <param name="position">Положение</param>
        public PhysEllipse(float mass, Vector2 position)
        {
            this.mass = mass;
            this.position = position;
            this.velocity = new Vector2(0, 0);
            this.acceleration = new Vector2(0, 0);
            this.width = 10*this.mass;
            this.height = 10*this.mass;
        }

        /// <summary>
        /// Создает физический объект в форме эллипса с заданной массой, положением, скоростью, шириной и высотой
        /// </summary>
        /// <param name="mass">Масса</param>
        /// <param name="position">Положение</param>
        /// <param name="velocity">Скорость</param>
        /// <param name="width">Ширина (длина большей полуоси)</param>
        /// <param name="height">Высота (длина меньшей полуоси)</param>
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
