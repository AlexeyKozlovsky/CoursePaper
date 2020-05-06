using System;
using System.Collections.Generic;
using System.Text;

namespace PhysicsEngine
{
    /// <summary>
    /// Класс, описывающий вектор в двумерном пространстве
    /// </summary>
    [Serializable]
    public class Vector2
    {
        private float x;        // Координата x
        private float y;        // Координата y

        /// <summary>
        /// Координата X
        /// </summary>
        public float X { get { return x; } set { x = value; } }

        /// <summary>
        /// Координата Y
        /// </summary>
        public float Y { get { return y; } set { y = value; } }

        /// <summary>
        /// Возвращает нулевой вектор
        /// </summary>
        public static Vector2 Zero { get { return new Vector2(); } }

        /// <summary>
        /// Возвращает единичный вектор в направлении по оси OX
        /// </summary>
        public static Vector2 UnitX { get { return new Vector2(1, 0); } }
        
        /// <summary>
        /// Возвращает единичный вектор в направлении по оси OY
        /// </summary>
        public static Vector2 UnitY { get { return new Vector2(0, 1); } }

        /// <summary>
        /// Создает вектор с координатами (0, 0)
        /// </summary>
        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        /// <summary>
        /// Создает вектор c указанными координатами
        /// </summary>
        /// <param name="x">Координата по оси OX</param>
        /// <param name="y">Координата по оси OY</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Сумма
        /// </summary>
        /// <param name="vec1">Первый вектор</param>
        /// <param name="vec2">Второй вектор</param>
        /// <returns>Возвращает сумму векторов</returns>
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            Vector2 resultVec = new Vector2(vec1.x + vec2.x, vec1.y + vec2.y);
            return resultVec;
        }

        /// <summary>
        /// Разность
        /// </summary>
        /// <param name="vec1">Первый вектор</param>
        /// <param name="vec2">Второй вектор</param>
        /// <returns>Возвращает разность векторов</returns>
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            Vector2 resultVec = new Vector2(vec1.x - vec2.x, vec1.y - vec2.y);
            return resultVec;
        }

        /// <summary>
        /// Умножение вектора на число
        /// </summary>
        /// <param name="vec">Вектор</param>
        /// <param name="digit">Число</param>
        /// <returns>Возвращает вектор, умноженный на число</returns>
        public static Vector2 operator *(Vector2 vec, float digit)
        {
            Vector2 resultVec = new Vector2(vec.x * digit, vec.y * digit);
            return resultVec;
        }

        /// <summary>
        /// Умножение вектора на число
        /// </summary>
        /// <param name="digit">Число</param>
        /// <param name="vec">Вектор</param>
        /// <returns>Возвращает вектор, умноженный на число</returns>
        public static Vector2 operator *(float digit, Vector2 vec)
        {
            return vec * digit;
        }

        /// <summary>
        /// Скалярное произведение векторов
        /// </summary>
        /// <param name="vector1">Первый вектор</param>
        /// <param name="vector2">Второй вектор</param>
        /// <returns>Возвращает результат скалярного произведение двух векторов</returns>
        public static double operator *(Vector2 vector1, Vector2 vector2)
        {
            return vector1.x * vector2.x + vector1.y * vector2.y;
        }

        /// <summary>
        /// Деление вектора на число
        /// </summary>
        /// <param name="vec">Вектор</param>
        /// <param name="digit">Число</param>
        /// <returns>Возвращает вектор, разделенный на число</returns>
        public static Vector2 operator /(Vector2 vec, float digit)
        {
            Vector2 resultVec = new Vector2(vec.x / digit, vec.y / digit);
            return resultVec;
        }

        /// <summary>
        /// Копия вектора
        /// </summary>
        /// <returns>Возращает копию вектора</returns>
        public Vector2 Copy()
        {
            return this;
        }

        /// <summary>
        /// Длина вектора
        /// </summary>
        /// <returns>Возвращает длину вектора</returns>
        public float Magnitude()
        {
            float result = (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2));
            return result;
        }

        /// <summary>
        /// Нормирование
        /// </summary>
        /// <returns>Возвращает вектора единичной длины в направлении исходного</returns>
        public Vector2 Normalize()
        {
            return this / this.Magnitude();
        }

        /// <summary>
        /// Угол отклонения
        /// </summary>
        /// <returns>Возвращает угол вектора к оси OX</returns>
        public float GetAngle()
        {
            return (float)Math.Atan2(this.y, this.x);
        }

        /// <summary>
        /// Поворот вектора
        /// </summary>
        /// <param name="angle">Угол поворота (рад.)</param>
        /// <returns>Возвращает вектор, получающийся поворотом данного на данный угол (рад.)</returns>
        public Vector2 Rotate(float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            Vector2 resultVector = new Vector2(cos * this.x + sin * this.y, -sin * this.x + cos * this.y);
            return resultVector;
        }
    }
}
