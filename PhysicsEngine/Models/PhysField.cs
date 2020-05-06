using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhysicsEngine
{
    /// <summary>
    /// Класс, описывающий физическое поле
    /// </summary>
    [Serializable]
    class PhysField
    {
        private float height;               // Высота поля
        private float width;                // Ширина поля
        private List<PhysObject> objs;      // Список физических объектов на поле

        /// <summary>
        /// Высота поля
        /// </summary>
        public float Height { get { return height; } set { height = value; } }

        /// <summary>
        /// Ширина поля
        /// </summary>
        public float Width { get { return width; } set { width = value; } }

        /// <summary>
        /// Список физических объектов, находящихся на поле
        /// </summary>
        public List<PhysObject> Objs { get { return objs; } }

        /// <summary>
        /// Добавляет объект на поле
        /// </summary>
        /// <param name="obj">Объект</param>
        public void Add(PhysObject obj) => objs.Add(obj);

        /// <summary>
        /// Определяет коллизию двух объектов
        /// </summary>
        /// <param name="obj1">Первый объект</param>
        /// <param name="obj2">Второй объект</param>
        private void Collision(PhysObject obj1, PhysObject obj2)
        {
            float dx = obj2.Position.X - obj1.Position.X;
            float dy = obj2.Position.Y - obj1.Position.Y;
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            if (dist < obj1.Width / 2 + obj2.Width / 2)
            {
                //Resolve(obj1, obj2);
                float angle = -(float)Math.Atan2(dy, dx);
                float sin = (float)Math.Sin(angle);
                float cos = (float)Math.Cos(angle);

                Vector2 currentPosition1 = new Vector2(0, 0);
                Vector2 currentPosition2 = new Vector2(dx * cos - dy * sin, dx * sin + dy * cos);

                Vector2 currentVelocity1 = new Vector2(obj1.Velocity.X * cos - obj1.Velocity.Y * sin,
                    +obj1.Velocity.X * sin + obj1.Velocity.Y * cos);
                Vector2 currentVelocity2 = new Vector2(obj2.Velocity.X * cos - obj2.Velocity.Y * sin,
                    +obj2.Velocity.X * sin + obj2.Velocity.Y * cos);

                float vx1Final = ((obj1.Mass - obj2.Mass) * currentVelocity1.X + 2 * obj2.Mass * currentVelocity2.X) / (obj1.Mass + obj2.Mass);
                float vx2Final = ((obj2.Mass - obj1.Mass) * currentVelocity2.X + 2 * obj1.Mass * currentVelocity1.X) / (obj1.Mass + obj2.Mass);

                currentVelocity1.X = vx1Final;
                currentVelocity2.X = vx2Final;

                float absV = Math.Abs(currentVelocity1.X) + Math.Abs(currentVelocity2.X);
                float overlap = (obj1.Width / 2 + obj2.Width / 2) -
                    Math.Abs(currentPosition2.X - currentPosition1.X);

                currentPosition1.X += overlap * currentVelocity1.X / absV;
                currentPosition2.X += overlap * currentVelocity2.X / absV;


                Vector2 finalPosition1 = new Vector2((currentPosition1.X)* cos + 
                    (currentPosition1.Y)* sin,
                -(currentPosition1.X) * sin + (currentPosition1.Y) * cos );
                Vector2 finalPosition2 = new Vector2((currentPosition2.X) * cos +
                    (currentPosition2.Y) * sin ,
                    -(currentPosition2.X) * sin + (currentPosition2.Y) * cos);


                obj2.Position = new Vector2(obj1.Position.X + finalPosition2.X, 
                    obj1.Position.Y + finalPosition2.Y);
                obj1.Position = new Vector2(obj1.Position.X + finalPosition1.X ,
                    obj1.Position.Y + finalPosition1.Y );


                obj1.Velocity = new Vector2(currentVelocity1.X * cos + currentVelocity1.Y * sin,
                    currentVelocity1.Y * cos - currentVelocity1.X * sin);
                obj2.Velocity = new Vector2(currentVelocity2.X * cos + currentVelocity2.Y * sin,
                    -currentVelocity2.X * sin + currentVelocity2.Y * cos);
            }
        }

        private void Edges()
        {
            for (int i = 0; i < objs.Count; i++)
            {

                if (objs[i].Position.X + objs[i].Velocity.X - objs[i].Width/2 <= 0 || 
                    objs[i].Position.X + objs[i].Velocity.X >= width - objs[i].Width/2)
                {
                    objs[i].Velocity = new Vector2(-objs[i].Velocity.X, objs[i].Velocity.Y);
                    if (objs[i].Position.X + objs[i].Velocity.X - objs[i].Width/2 <= 0) 
                        objs[i].Position = new Vector2(objs[i].Width/2, objs[i].Position.Y);
                    else if (objs[i].Position.X + objs[i].Velocity.X >= width - objs[i].Width/2)
                        objs[i].Position = new Vector2(width - objs[i].Width/2, objs[i].Position.Y);
                }
                if (objs[i].Position.Y + objs[i].Velocity.Y - objs[i].Height/2 <= 0 || 
                    objs[i].Position.Y + objs[i].Velocity.Y >= height - objs[i].Height/2)
                {
                    if (objs[i].Position.Y + objs[i].Velocity.Y >= height - objs[i].Height/2)
                    {
                        objs[i].Position = new Vector2(objs[i].Position.X, height - objs[i].Height/2);
                    }
                    else if (objs[i].Position.Y + objs[i].Velocity.Y - objs[i].Height/2 <= 0) 
                        objs[i].Position = new Vector2(objs[i].Position.X, objs[i].Height/2);
                    objs[i].Velocity = new Vector2(objs[i].Velocity.X, -objs[i].Velocity.Y);
                }
            }
        }
        public void Simualate()
        {
            int length = objs.Count;
            for (int i = 0; i < objs.Count; i++)
                objs[i].Update();

            Edges();

            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (objs[i] is PhysEllipse)
                        Collision(objs[i], objs[j]);
                }
            }

            
        }
    }
}
