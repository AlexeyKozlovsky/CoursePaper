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
    /// Класс окна добавления объекта
    /// </summary>
    public partial class AddObject : Window
    {
        private Momentum momentum;                  // Страница демонстрации, на которую добавляется объект
        private PhysEllipse ellipse;                // Физических объект, который добавляется
        private float width;                        // Ширина поля, на которое надо добавить объект
        private float height;                       // Высота поля, на которое надо добавить объект

        public bool isAdded { get; set; }           // true - если объект успешно добавлен, false - в противном случае

        // Конструктор класса окна добавления объекта.
        // Принимает как аргумент окно демонстрации
        public AddObject(Momentum momentum)
        {
            InitializeComponent();
            this.momentum = momentum;
            InitCharacteristics();
            InitTextBoxes();
        }

        // МЕТОДЫ

        // МЕТОДЫ ИНИЦИАЛИЗАЦИИ

        // Инициализирует длину и ширину
        private void InitCharacteristics()
        {
            this.width = momentum.Field.Width;          // Устанавливаем ширину в соответствии с шириной поля на странице
            this.height = momentum.Field.Height;        // Устанавливаем высоту в соответствии с высотой поля на странице
        }

        // Инициализирует тексты в текстбоксах по умолчанию
        private void InitTextBoxes()
        {
            nameTextBox.Text = $"Объект {momentum.Shapes.Count + 1}";   // Устанавливаем название
            massTextBox.Text = "10";                                    // Устанавливаем значение массы
            radiusTextBox.Text = "75";                                  // Устанавливаем значение радиуса
            positionXTextBox.Text = $"{width / 2}";                     // Устанавливаем значение положения по OX
            positionYTextBox.Text = $"{height / 2}";                    // Устанавливаем значение положения по OY
            velocityXTextBox.Text = "0";                                // Устанавливаем значение скорости по OX
            velocityYTextBox.Text = "0";                                // Устанавливаем значение скорости по OY
        }

        // ОСНОВНЫЕ МЕТОДЫ

        // Добавляет объект
        private void AddEllipse()
        {
            float mass = float.Parse(massTextBox.Text);
            float radius = float.Parse(radiusTextBox.Text);
            Vector2 position = new Vector2(float.Parse(positionXTextBox.Text), float.Parse(positionYTextBox.Text));
            Vector2 velocity = new Vector2(float.Parse(velocityXTextBox.Text), float.Parse(velocityYTextBox.Text));
            ellipse = new PhysEllipse(mass, position, velocity, radius, radius);
            momentum.Field.Objs.Add(ellipse);
            momentum.NameOfObj = nameTextBox.Text;
        }

        // СОБЫТИЯ

        // События нажатия на кнопку "Добавить". Добавляет объект, если введенные данные корректны
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Пробуем вызвать метод добавления объекта
            // Если выходит исключение, кидаем MessageBox и выходим из события
            try
            {
                AddEllipse();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введенных вами данных");
                return;
            }
            isAdded = true;                     // Если все хорошо, устанавливаем isAdded в true
            this.Close();                       // Закрываем окно
        }

        // Событие нажатия на кнопку "Отмена". Закрывает окно и не добавляет объект
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            isAdded = false;
            this.Close();
        }
    }
}
