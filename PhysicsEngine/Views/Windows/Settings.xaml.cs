using PhysicsEngine.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhysicsEngine.Views.Windows
{
    /// <summary>
    /// Класс окна настроек
    /// </summary>
    public partial class Settings : Window
    {
        private Momentum momentum;      // Страница демонстрации момента импульса
        private float gravity;          // Ускорение свободного падения


        /// <summary>
        /// Создает окно настроек
        /// </summary>
        /// <param name="momentum">Страница демонстрации, на которой меняются настройки</param>
        public Settings(Momentum momentum)
        {
            InitializeComponent();              // Инициализируем компоненты интерфейса
            this.momentum = momentum;           // Устанавливаем страницу демонстрации
            gravityTextBox.Text = "1";          // Устанавливаем значение в текстбокс по умолчанию
        }

        // МЕТОДЫ

        // Метод для принятия новых настроек
        private void Accept()
        {
            // Пробуем пропарсить в float значение введенное пользователем
            // Если вызывается исключение, то кидаем MessageBox и выходим из метода
            try { gravity = float.Parse(gravityTextBox.Text); }
            catch
            {
                MessageBox.Show("Проверьте корректность введённых вами данных");
                return;
            }

            // Меняем характеристики каждого объекта на поле
            foreach (var obj in momentum.Field.Objs)
            {
                obj.DeleteAcceleration();                   // Обнуляем ускорение текущему объекту
                obj.ApplyForce(new Vector2(0, gravity));    // Задаем ускорение свободного падения текущему объекту
            }
        }

        // СОБЫТИЯ

        // Событие нажатия на кнопку "Принять". Принимает настройки
        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            Accept();
            this.Close();
        }
    }
}
