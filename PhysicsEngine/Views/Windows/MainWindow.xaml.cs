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
        private Momentum momentum;              // экземпляр класса страницы Momentum
        private Button[] menuButtons;           // массив кнопок главного меню
        private Button[] demoButtons;           // массив кнопок демонстрации

        private bool isPlaying;                 // true - если идет демонстрация, false - если не идет
        private bool isObjs;                    // true - если дерево объектов открыто, false - в противном случае


        // Конструктор класса главного окна программы
        public MainWindow()
        {
            InitializeComponent();          // Инициализируем компоненты интерфейса
            InitButtons();                  // Инициализируем массивы кнопок
        }

        // МЕТОДЫ ИНИЦИАЛИЗАЦИИ

        // Инициализирует массивы кнопок
        private void InitButtons() 
        {
            menuButtons = new Button[3]     // Инициализируем массив кнопок меню
            {
                momentumBtn,
                helpBtn,
                exitBtn
            };

            demoButtons = new Button[7]     // Инициализируем массив кнопок демонстрации
            {
                playBtn,
                stopBtn,
                settingsBtn,
                menuBtn,
                objsBtn,
                addBtn,
                hideBtn
            };
        }

        // МЕТОДЫ ВЫЗЫВАЕМЫЕ В ГЛАВНОМ МЕНЮ

        // Открывает страницу демонстрации "Сохранение импульса"
        private void OpenMomentum()
        {
            momentum = new Momentum();
            frame.Navigate(momentum);
            HideMenuButtons();              // Скрываем кнопки меню
            ShowDemoButtons();              // Показываем кнопки демонстрации

            this.listBox.Items.Clear();     // Очищаем список в листбоксе от возможных предыдущих значений

            playBtn.IsEnabled = false;      // Делаем кнопку "Старт" неактивной
            stopBtn.IsEnabled = false;      // Делаем кнопку "Стоп" неактивной
        }

        // Открывает окно помощи
        private void OpenHelp()
        {
            Help help = new Help();
            help.ShowDialog();
        }

        // МЕТОДЫ, ВЫЗЫВАЕМЫЕ ВО ВРЕМЯ ДЕМОНСТРАЦИИ

        // Скрывает кнопки меню
        private void HideMenuButtons()
        {
            foreach (var btn in menuButtons)
                btn.Visibility = Visibility.Hidden;
        }

        // Показывает кнопки меню
        private void ShowMenuButtons()
        {
            foreach (var btn in menuButtons)
                btn.Visibility = Visibility.Visible;
        }

        // Скрывает кнопки демонстрации
        private void HideDemoButtons()
        {
            foreach (var btn in demoButtons)
                btn.Visibility = Visibility.Hidden;

            for (int i = 4; i < demoButtons.Length; i++)
                demoButtons[i].SetValue(Grid.ColumnProperty, 5);

            listBox.Visibility = Visibility.Hidden;
        }

        // Скрывает кнопки демонстрации (во время демонстрации)
        private void HideButtons()
        {
            HideDemoButtons();
            hideBtn.Visibility = Visibility.Visible;
        }

        // Показывает кнопки во время демонстрации
        private void ShowDemoButtons()
        {
            foreach (var btn in demoButtons)
                btn.Visibility = Visibility.Visible;
        }

        // Открывает настройки
        private void OpenSettings()
        {
            Settings settings = new Settings(momentum);
            settings.ShowDialog();
        }

        // Запускает демонстрацию
        private void Play()
        {
            if (!isPlaying)
            {
                momentum.StartSimulation();                 
                isPlaying = true;
                playBtn.IsEnabled = false;
                stopBtn.IsEnabled = true;
            }
        }

        // Останавливает демонстрацию
        private void Stop()
        {
            if (isPlaying)
            {
                momentum.StopSimulation();
                isPlaying = false;
                playBtn.IsEnabled = true;
                stopBtn.IsEnabled = false;
            }
        }

        // Переходит в меню
        private void ToMenu()
        {
            frame.Navigate(new PhysicsEngine.Views.Pages.Menu());   // Меняет страницу в фрейме на предыдущую
            HideDemoButtons();                                      // Прячем кнопки демонстрации
            ShowMenuButtons();                                      // Показываем кнопки меню
        }

        // Показывает дерево объектов
        private void ShowObjs()
        {
            for (int i = 4; i < demoButtons.Length; i++)        // Сдвигаем кнопки демонстрации, чтобы открыть дерево объектов
                demoButtons[i].SetValue(Grid.ColumnProperty, 3);

            listBox.Visibility = Visibility.Visible;            // Делаем дерево объектов видимым
            isObjs = true;                                      // Устанавливаем переменную, отвечающую за состояние дерева объектов в true
        }

        // Скрывает дерево объектов
        private void HideObjs()
        {
            for (int i = 4; i < demoButtons.Length; i++)        // Сдвигаем кнопки демонстрации обратно в край
                demoButtons[i].SetValue(Grid.ColumnProperty, 5);

            listBox.Visibility = Visibility.Hidden;             // Делаем дерево объектов невидимым
            isObjs = false;                                     // Устанавливаем переменную, отвечающую за состояние дерева объектов в false
        }

        // Добавлает новый объект на поле и форму на канвас
        private void Add()
        {
            AddObject addObj = new AddObject(momentum);
            try { addObj.ShowDialog(); }
            catch { return; }
            if (addObj.isAdded)
            {
                momentum.AddToCanvas((PhysEllipse)momentum.Field.Objs[momentum.Field.Objs.Count - 1]);
                listBox.Items.Add(momentum.NameOfObj);
                listBox.SelectedIndex = listBox.Items.Count - 1;
                if (!isPlaying) playBtn.IsEnabled = true;
            }
        }

        // Выделяет выбранный объект красным цветом
        private void PaintBall()
        {
            foreach (var ball in momentum.Shapes)
                ball.Fill = new SolidColorBrush(Color.FromRgb(255, 87, 34));

            momentum.Shapes[momentum.Selected].Fill = new SolidColorBrush(Color.FromRgb(221, 44, 0));
        }




        // СОБЫТИЯ НАЖАТИЯ НА КНОПКИ МЕНЮ

        // Событие нажатия на кнопку "Сохранение импульса"
        private void momentumBtn_Click(object sender, RoutedEventArgs e) => OpenMomentum();

        // Событие нажатия на кпопку "Справка"
        private void helpBtn_Click(object sender, RoutedEventArgs e) => OpenHelp();

        // Событие нажатия на кнопку "Выход"
        private void exitBtn_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();


        // СОБЫТИЯ НАЖАТИЯ НА КНОПКИ ДЕМОНСТРАЦИИ

        // Событие нажатия на кнопку "Старт". Запускает демонстрацию
        private void playBtn_Click(object sender, RoutedEventArgs e) => Play();

        // Событие нажатия на кнопку "Стоп". Останавливает демонстрацию
        private void stopBtn_Click(object sender, RoutedEventArgs e) => Stop();

        // Событие нажатия на кнопку "Настройки". Открывает окно настроек
        private void settingsBtn_Click(object sender, RoutedEventArgs e) => OpenSettings();

        // Событие нажатия на кнопку "Скрыть"\"Показать". Скрывает или показывает кнопки демонстрации
        private void hideBtn_Click(object sender, RoutedEventArgs e)
        {
            // Проверяет, что написано на кнопке.
            // Если "Скрыть", то скрывает все кнопки демонстрации, кроме данной и меняет на ней надпись на "Показать"
            // Если "Показать", то показывает все кнопки демонстрации и меняет на данной надпись на "Скрыть"
            switch (hideBtn.Content)
            {
                case "Скрыть":
                    HideButtons();
                    hideBtn.Content = "Показать";
                    break;
                case "Показать":
                    ShowDemoButtons();
                    hideBtn.Content = "Скрыть";
                    break;
            }
        }

        // Событие нажатия на кнопку "Меню". Выходит в меню
        private void menuBtn_Click(object sender, RoutedEventArgs e) => ToMenu();

        // Событие нажатия на кнопку "Объекты". Открывает или скрывает дерево объектов
        private void objsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isObjs) HideObjs();             // Если дерево объектов открыто, то скрывает его
            else ShowObjs();                    // Если дерево объектов скрыто, то открывает его
        }

        // Событие нажатия на кнопку "Добавить". Открывает окно добавления объекта
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try { Add(); } catch { return; }
        }

        // ОСТАЛЬНЫЕ СОБЫТИЯ ДЕМОНСТРАЦИИ

        // Событие, вызывающееся при смене выбранного элемента в дереве объектов
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.Items.Count == 0) return;
            momentum.SetStateString(momentum.Field.Objs[listBox.SelectedIndex].StateString());  // Меняет текст характеристик на характеристики выбранного объекта
            momentum.Selected = listBox.SelectedIndex;                  

            PaintBall();            // Перекрашивает все шарики и выделяет красным цветом выделенный
        }
    }
}
