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
    /// Класс главного окна программы
    /// </summary>
    public partial class MainWindow : Window
    {
        private Momentum momentum;          // экземпляр класса страницы Momentum

        // Конструктор класса главного окна программы
        public MainWindow()
        {
            InitializeComponent();          // Инициализируем компоненты интерфейса
        }

        // МЕТОДЫ ВЫЗЫВАЕМЫЕ В ГЛАВНОМ МЕНЮ
        private void OpenMomentum()
        {
            momentum = new Momentum();
            frame.Navigate(momentum);
        }

        private void OpenHelp()
        {
            Help help = new Help();
            help.ShowDialog();
        }

        // МЕТОДЫ, ВЫЗЫВАЕМЫЕ ВО ВРЕМЯ ДЕМОНСТРАЦИИ

        // СОБЫТИЯ НАЖАТИЯ НА КНОПКИ ГЛАВНОГО МЕНЮ

        // Событие нажатия на кнопку "Сохранение импульса"
        private void momentumBtn_Click(object sender, RoutedEventArgs e) => OpenMomentum();

        // Событие нажатия на кпопку "Справка"
        private void helpBtn_Click(object sender, RoutedEventArgs e) => OpenHelp();

        // Событие нажатия на кнопку "Выход"
        private void exitBtn_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();


        // СОБЫТИЯ НАЖАТИЯ НА КНОПКИ ДЕМОНСТРАЦИИ
        private void playBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hideBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void objsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
