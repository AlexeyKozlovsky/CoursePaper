using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            float dx = obj2.Position.X - obj1.Position.X;       //Считаем расстояние между центрами по OX       
            float dy = obj2.Position.Y - obj1.Position.Y;       //Считаем расстояние между центрами по OY
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);   //Считаем расстояние между центрами

            // Проверяем, столкнулись ли объекты
            if (dist < obj1.Width / 2 + obj2.Width / 2)
            {
                // Вычисляем угол отклонения вектора, соединяющего центры объектов от оси OX
                float angle = -(float)Math.Atan2(dy, dx);       
                float sin = (float)Math.Sin(angle);             //Считаем синус угла отклонения
                float cos = (float)Math.Cos(angle);             //Считаем косинус угла отклонения

                // Переходим в систему отсчёта связанную с первым шариком и повернутую на угол
                // отклонения, найденный ранее по часовой стрелке
                // Находим положения объектов в этой системе отсчёта
                Vector2 currentPosition1 = Vector2.Zero;
                Vector2 currentPosition2 = new Vector2(dx * cos - dy * sin, dx * sin + dy * cos);

                // Находим скорости объектов в этой системе отсчёта
                Vector2 currentVelocity1 = new Vector2(obj1.Velocity.X * cos - obj1.Velocity.Y * sin,
                    +obj1.Velocity.X * sin + obj1.Velocity.Y * cos);
                Vector2 currentVelocity2 = new Vector2(obj2.Velocity.X * cos - obj2.Velocity.Y * sin,
                    +obj2.Velocity.X * sin + obj2.Velocity.Y * cos);

                // Считаем финальные значения скоростей в новой системе отсчёта относительно оси OX'
                // Расчёт ведется по закону сохранения импульса и закона сохранения кинетической энергии
                float vx1Final = ((obj1.Mass - obj2.Mass) * currentVelocity1.X + 2 * obj2.Mass * currentVelocity2.X) / (obj1.Mass + obj2.Mass);
                float vx2Final = ((obj2.Mass - obj1.Mass) * currentVelocity2.X + 2 * obj1.Mass * currentVelocity1.X) / (obj1.Mass + obj2.Mass);
                currentVelocity1.X = vx1Final;
                currentVelocity2.X = vx2Final;

                // Решаем проблемы с перекрытием
                // Вычисляем длину перекрытия и сумму абсолютных значений скоростей объектов по оси OX'
                float absV = Math.Abs(currentVelocity1.X) + Math.Abs(currentVelocity2.X);
                float overlap = (obj1.Width / 2 + obj2.Width / 2) -
                    Math.Abs(currentPosition2.X - currentPosition1.X);

                // Меняем положения объектов по оси OX' так, чтобы перекрытия больше не было
                currentPosition1.X += overlap * currentVelocity1.X / absV;
                currentPosition2.X += overlap * currentVelocity2.X / absV;

                // Поворачиваем систему координат обратно
                // Считаем финальный значения скоростей в повернутой обратно системе отсчёта
                Vector2 finalPosition1 = new Vector2((currentPosition1.X)* cos + 
                    (currentPosition1.Y)* sin,
                -(currentPosition1.X) * sin + (currentPosition1.Y) * cos );
                Vector2 finalPosition2 = new Vector2((currentPosition2.X) * cos +
                    (currentPosition2.Y) * sin ,
                    -(currentPosition2.X) * sin + (currentPosition2.Y) * cos);

                // Присваиваем полученные положения положениям объектов
                obj2.Position = new Vector2(obj1.Position.X + finalPosition2.X, 
                    obj1.Position.Y + finalPosition2.Y);
                obj1.Position = new Vector2(obj1.Position.X + finalPosition1.X ,
                    obj1.Position.Y + finalPosition1.Y );

                // Считаем финальные значения скоростей в повернутой обратно системе отсчёта
                // Присваиваем посчитанные значения скоростей скоростям объектов
                obj1.Velocity = new Vector2(currentVelocity1.X * cos + currentVelocity1.Y * sin,
                    currentVelocity1.Y * cos - currentVelocity1.X * sin);
                obj2.Velocity = new Vector2(currentVelocity2.X * cos + currentVelocity2.Y * sin,
                    -currentVelocity2.X * sin + currentVelocity2.Y * cos);
            }
        }

        /// <summary>
        /// Проверяет сталкиваются ли объекты поля со стенаками поля или выходят за них
        /// </summary>
        private void Edges()
        {
            // Проходим циклом по всем объектам списка объектов на поле
            for (int i = 0; i < objs.Count; i++)
            {
                // Вычисляем соприкосновение или заход за левый или правый край поля
                if (objs[i].Position.X + objs[i].Velocity.X - objs[i].Width/2 <= 0 || 
                    objs[i].Position.X + objs[i].Velocity.X >= width - objs[i].Width/2)
                {
                    // Присваиваем новое значение скорости объекта
                    objs[i].Velocity = new Vector2(-objs[i].Velocity.X, objs[i].Velocity.Y);
                    
                    // Если объект вышел за переделы поля, присваиваем его положению крайнее возможное значение
                    if (objs[i].Position.X + objs[i].Velocity.X - objs[i].Width/2 <= 0) 
                        objs[i].Position = new Vector2(objs[i].Width/2, objs[i].Position.Y);
                    else if (objs[i].Position.X + objs[i].Velocity.X >= width - objs[i].Width/2)
                        objs[i].Position = new Vector2(width - objs[i].Width/2, objs[i].Position.Y);
                }
                
                //Вычисляем соприкосновение или заход за верхний или нижний край поля
                if (objs[i].Position.Y + objs[i].Velocity.Y - objs[i].Height/2 <= 0 || 
                    objs[i].Position.Y + objs[i].Velocity.Y >= height - objs[i].Height/2)
                {
                    //Присваиваем новое значение скорости объекта
                    objs[i].Velocity = new Vector2(objs[i].Velocity.X, -objs[i].Velocity.Y);

                    // Если объект вышел за пределы поля, присваиваем его положению крайнее возможное значение
                    if (objs[i].Position.Y + objs[i].Velocity.Y >= height - objs[i].Height/2)
                        objs[i].Position = new Vector2(objs[i].Position.X, height - objs[i].Height/2);
                    else if (objs[i].Position.Y + objs[i].Velocity.Y - objs[i].Height/2 <= 0) 
                        objs[i].Position = new Vector2(objs[i].Position.X, objs[i].Height/2);
                }
            }
        }

        /// <summary>
        /// Обновляет состояние всех объектов на поле
        /// </summary>
        public void Simualate()
        {
            int length = objs.Count;                    // Для удобства запишем длину списка объектов в переменную
            
            for (int i = 0; i < length; i++)            // Обновим состояние каждого объекта
                objs[i].Update();

            Edges();                                    // Проверим объекты на столкновения со стенками

            for (int i = 0; i < length - 1; i++)        // Проверим коллизии каждого объекта поля с каждым
                for (int j = i + 1; j < length; j++)
                        Collision(objs[i], objs[j]);

        }
    }
}
