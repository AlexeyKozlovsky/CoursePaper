using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhysicsEngine
{
    /// <summary>
    /// Базовый класс для описания физического объекта
    /// </summary>
    [Serializable]
    public class PhysObject
    {
        protected Vector2 position;         // Положение 
        protected Vector2 velocity;         // Скорость
        protected Vector2 acceleration;     // Ускорение
        protected float mass;               // Масса
        protected float width;              // Ширина прямоугольника, в который вписан объект
        protected float height;             // Высота прямоугольника, в который вписан объект

        /// <summary>
        /// Положение объекта
        /// </summary>
        public Vector2 Position { get { return position; } set { position = value; } }
        
        /// <summary>
        /// Скорость объекта
        /// </summary>
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        /// <summary>
        /// Масса объекта
        /// </summary>
        public float Mass { get { return mass; } }
        
        /// <summary>
        /// Ширина прямоугольника, в который вписан объект
        /// </summary>
        public float Width { get { return width; } }

        /// <summary>
        /// Высотра прямоугольника, в который вписан объект
        /// </summary>
        public float Height { get { return height; } }
        

        /// <summary>
        /// Добавляет силу, действующую на объект
        /// </summary>
        /// <param name="force">Сила, действующая на объект</param>
        public void ApplyForce(Vector2 force)
        {
            this.acceleration += force/this.mass;
        }


        /// <summary>
        /// Делает ускорение объекта равным нулю
        /// </summary>
        public void DeleteAcceleration()
        {
            this.acceleration = Vector2.Zero;
        }

        /// <summary>
        /// Обновляет состояние объекта
        /// </summary>
        public virtual void Update()
        {
            velocity += acceleration;           // Изменяем скорость в соответствие с ускорением
            position += velocity;               // Изменяем положение в соответствие со скоростью
        }


        /// <summary>
        /// Строка состояния объекта
        /// </summary>
        /// <returns>Возвращает строку состояния объекта</returns>
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
