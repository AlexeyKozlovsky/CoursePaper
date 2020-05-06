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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhysicsEngine
{
    /// <summary>
    /// Класс стартового окна программы
    /// </summary>
    public partial class MainWindow : Window
    {
        // Конструктор класса стартового окна программы
        public MainWindow()
        {
            InitializeComponent();          // Инициализация компонентов
            InitText();                     // Инициализация текста
        }

        // Метод для инициализации текста титульной страницы
        private void InitText()
        {
            string text;
            text = $"МИНИСТЕРСТВО НАУКИ И ОБРАЗОВАНИЯ РОССИЙСКОЙ ФЕДЕРАЦИИ\n";
            text += $"ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ ДУБНА\n";
            text += $"Институт системного анализа и управления\n";
            text += $"Кафедра распределенных информационно-вычислительных систем\n";
            text += $"Курсовая работа\n";
            text += $"по дисциплине: ПЯВУ\n";
            text += $"на тему: \"Физический движок\"\n";
            text += $"Подготовил: студент 1-го курса, гр. 1253 Козловский А.А.\n";
            text += $"Проверил: доц. Мельникова О.И.\n";
            signLabel.Content = text;
        }

        // Событие нажатия на кнопку "Далее"
        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();           // Создаем экземпляр класса MainWindow
            this.Close();                                       // Закрываем текущее окно
            mainWindow.Show();                                  // Открываем главное окно программы
        }
    }
}
