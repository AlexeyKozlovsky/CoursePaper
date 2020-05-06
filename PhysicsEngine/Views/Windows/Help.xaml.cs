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
    /// Класс окна помощи
    /// </summary>
    public partial class Help : Window
    {
        // Конструктор класса окна помощи
        public Help()
        {
            InitializeComponent();      // Инициализируем компоненты
            InitHelpString();           // Инициализируем текст помощи
        }


        // Инициализирует строку помощи
        private void InitHelpString()
        {
            helpString.Content = $"В данной программе вы можете запускать демонстрацию\n" +
                $"закона сохранения импульса\n" +
                $"Выберите \"Сохранение импульса\" в главном меню, чтобы перейти на страницу\n" +
                $"демонстрации.\n" +
                $"Нажмите \"Добавить\", чтобы добавить шарик на поле и введите его характеристики\n" +
                $"в появившемся окне, затем нажмите \"Окей\".\n" +
                $"Нажмите \"Старт\", чтобы запустить демонстрацию, нажмите \"Стоп\", чтобы\n остановить\n" +
                $"Нажмите \"Скрыть\", чтобы скрыть кнопки во время демонстрации\n" +
                $"Нажмите \"Показать\", чтобы показать кнопки, если вы их скрыли ранее\n" +
                $"Нажмите \"Объекты\", чтобы открыть дерево объектов, находящихся на поле\n" +
                $"Выделите объект в дереве объектов и наблюдайте за его характеристиками,\n" +
                $"Наведите мышку на текст характеристик и он станет менее прозрачным,\n" +
                $"Помните, выделенный вами объект имеет красный цвет.\n" +
                $"Удачи в использовании программы!";
        }

        // События нажатия на кнопку "Окей"
        private void okButton_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
