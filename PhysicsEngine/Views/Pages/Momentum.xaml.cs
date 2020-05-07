using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PhysicsEngine.Views.Pages
{
    /// <summary>
    /// Класс страницы демонстрации сохранения импульса
    /// </summary>
    public partial class Momentum : Page
    {
        // ПОЛЯ

        private System.Timers.Timer timer;          // таймер
        private List<Shape> shapes;                 // список форм объектов, находящихся на поле
        private PhysField field;                    // поле
        private float gravity;                      // ускорение свободного падения
        private int selected;                       // выбранный объект

        public string NameOfObj { get; set; }   // Имя последнего добавленного объекта


        // СВОЙСТВА

        /// <summary>
        /// Список форм объектов, содержащихся на поле
        /// </summary>
        public List<Shape> Shapes { get { return shapes; } set { shapes = value; } }

        /// <summary>
        /// Физическое поле
        /// </summary>
        public PhysField Field { get { return field; } set { field = value; } }

        /// <summary>
        /// Ускорение свободного падения
        /// </summary>
        public float Gravity { get { return gravity; } set { gravity = value; } }

        /// <summary>
        /// Выбранный объект
        /// </summary>
        public int Selected { get { return selected; } set { selected = value; } }

        
        public Momentum()
        {
            InitializeComponent();                  // Инициализируем компоненты интерфейса
            InitTimer();                            // Инициализируем таймер
            InitAll();                              // Инициализируем все оставшиеся копмоненты
        }

        // МЕТОДЫ ДЛЯ ИНИЦИАЛИЗАЦИИ

        // Инициализирет списко форм, ускорение свободного падения и поле
        private void InitAll()
        {
            shapes = new List<Shape>();             // Инициализируем список форм
            field = new PhysField();                // Создаем объект класса поля
            field.Height = 600;                     // Присваиваем высоту полю
            field.Width = 800;                      // Присваиваем ширину полю
            Gravity = 0.2f;                         // Устанавливаем ускорение свободного падения
        }

        // Инициализирует таймер
        private void InitTimer()
        {
            timer = new System.Timers.Timer(10);
            timer.AutoReset = true;
            timer.Elapsed += SimulationStep;
        }

        // ОСНОВНЫЕ МЕТОДЫ

        /// <summary>
        /// Запускает симуляцию
        /// </summary>
        internal void StartSimulation() => timer.Start();

        /// <summary>
        /// Останавливает симуляцию
        /// </summary>
        internal void StopSimulation() => timer.Stop();

        /// <summary>
        /// Добавляет физический объект на канвас
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="name">Имя объекта</param>
        internal void AddToCanvas(PhysEllipse obj)
        {
            obj.ApplyForce(new Vector2(0, Gravity * obj.Mass));                         // Добавляем силу гравитации объекту

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 87, 34));    // Создаем кисть цвета будущей формы объекта
            Ellipse ellipse = new Ellipse();                                            // Создаем форму объекта
            ellipse.Width = obj.Width;                                  // Задаем ширину форме объекта равную ширине объекта
            ellipse.Height = obj.Height;                                // Задаем высоту форме объекта равнеую высоте объекта
            ellipse.Fill = brush;                                       // Задаем цвет заливки формы объекта
            ellipse.Stroke = Brushes.Black;                             // Задаем цвет границы формы объекта
            Canvas.SetLeft(ellipse, obj.Position.X - obj.Width / 2);    // Устанавливаем координату по x форме объекта
            Canvas.SetTop(ellipse, obj.Position.Y - obj.Height / 2);    // Устанавливаем координату по y форме объекта
            canvas.Children.Add(ellipse);                               // Добавляем форму объекта на канвас
            
            shapes.Add(ellipse);                                        // Добавляем форму объекта в список форм
        }

        /// <summary>
        /// Устанавливает значение текста текущего состояния выбранного объекта
        /// </summary>
        /// <param name="state">Строка состояния</param>
        internal void SetStateString(string state)
        {
            stateLabel.Content = state;
        }

        // Обновляет текующее состояние всех форм на канвасе в соотвествии с состоянием объектов поля
        private void Update()
        {
            field.Width = (float)canvas.ActualWidth;            // Устанавливаем ширину поля по текущей ширине канваса
            field.Height = (float)canvas.ActualHeight;          // Устанавливаем высоту поля по текущей высоте канваса

            // Проходим циклом по всем объектам поля, чтобы обновить состояния их форм
            for (int i = 0; i < field.Objs.Count; i++)
            {
                // Получение доступа к формам из другуго потока.
                // В WPF формы являются элементами интерфейса, а меняем мы их состояния из фонового потока,
                // каждый тик таймера. Если бы мы просто попытались изменить их состояние, то было бы исключение,
                // так как мы не могли бы обратиться к элементам интерфейса из другого потока
                shapes[i].Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                {
                    Canvas.SetLeft(shapes[i], field.Objs[i].Position.X - field.Objs[i].Width / 2);
                    Canvas.SetTop(shapes[i], field.Objs[i].Position.Y - field.Objs[i].Height / 2);

                }));
            }

            // Получение доступа к лейблу из другого потока. Изменение текста состояния лейбла,
            // в соответствии с новым состоянием выбранного объекта
            stateLabel.Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                SetStateString(field.Objs[selected].StateString());
            }));
        }

        // Уменьшает прозрачность всех форм, кроме выделенной
        private void DecreaseBallsOpacity()
        {
            foreach (var shape in shapes)
                shape.Opacity = 0.5;

            shapes[selected].Opacity = 1;
        }

        // Увеличивает прозрачность всех форм
        private void IncreaseBallsOpacity()
        {
            foreach (var shape in shapes)
                shape.Opacity = 1;
        }


        // СОБЫТИЯ

        // Вызывается каждый тик таймера. Обновляет состояния всех объектов на поле
        private void SimulationStep(object sender, System.Timers.ElapsedEventArgs e)
        {
            field.Simualate();                  // Обновление состояний всех объектов на поле
            Update();                           // Обновление состояний всех форм на канвасе
        }

        // Событие, вызываемое при наведении мыши на лейбл, отоброжающий характеристики выбранного объекта
        private void stateLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            try { DecreaseBallsOpacity(); } catch { return; }
        }

        // Событие, вызываемое при уведении мыли с лейбла, отображающего характеристики выбранного объекта
        private void stateLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            try { IncreaseBallsOpacity(); } catch { return; }
        }
    }
}
