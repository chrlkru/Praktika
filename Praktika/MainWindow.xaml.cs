using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Praktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationContext context;
        public MainWindow()
        {
            InitializeComponent();
            var users = new List<User>();
             context = new ApplicationContext();

            
                var userList = context.Users.ToList();
                ClientsListView.ItemsSource = userList;
            
        }
        // Обработчик события нажатия кнопки "Добавить клиента"
        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение данных о клиенте из элементов управления
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string fatherName = fatherNameTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;
            string email = emailTextBox.Text;
            DateTime birthDate = datePicker1.SelectedDate ?? DateTime.Now;
            // Создание нового объекта клиента
            User newUser = new User
            {
                Фио = $"{lastName} {firstName} {fatherName}",
                Телефон = phoneNumber,
                Email = email,
                ДатаРождения = birthDate
            };
            int a = 0;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                     a = 1;
                    // Добавление клиента в базу данных
                    context.Users.Add(newUser);
                    context.SaveChanges(); // Сохранение изменений клиента
                    a = 2;
                    // Получение ID нового клиента
                    int clientId = newUser.UsersId;
                    a = 3;
                    // Создание дисконтной карты для клиента
                    double discount = 5.0; // Здесь можно задать скидку по умолчанию или запросить её у пользователя
                    double orderSum = 0.0; // Здесь можно задать сумму заказа по умолчанию или запросить её у пользователя
                    a = 4;
                    DiscontCard newDiscountCard = new DiscontCard
                    {
                        UsersId = clientId,
                        Discont = discount,
                        OrderSum = orderSum
                    };
                    a = 5;

                    // Добавление дисконтной карты в базу данных
                    context.DiscontCard.Add(newDiscountCard);
                    context.SaveChanges(); // Сохранение изменений дисконтной карты
                    a = 6;
                    transaction.Commit(); // Подтверждение транзакции
                    System.Windows.MessageBox.Show("Клиент успешно добавлен и создана дисконтная карта.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    a = 7;
                    // Очистка полей формы после добавления клиента (если требуется)
                    ClearClientFormFields();
                 
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Откат транзакции при ошибке
                    System.Windows.MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}",a.ToString() , MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // Метод для очистки полей формы добавления клиента
        private void ClearClientFormFields()
        {
            lastNameTextBox.Text = "";
            firstNameTextBox.Text = "";
            fatherNameTextBox.Text = "";
            phoneNumberTextBox.Text = "";
            emailTextBox.Text = "";
            datePicker1.SelectedDate = null; // Очистка выбранной даты
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}