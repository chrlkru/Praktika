using Praktika.Models;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MessageBox = System.Windows.MessageBox;

namespace Praktika.Views;

public partial class MainWindow : Window
{
    private PraktikaContext context;

    public ICollectionView FilteredClients { get; set; }

    public MainWindow()
    {
        context = new PraktikaContext();

        InitializeComponent();
        RefreshClientList();
    }

    private void RefreshClientList()
    {
        var userList = context.Users.ToList();
        FilteredClients = CollectionViewSource.GetDefaultView(userList);
        FilteredClients.Filter = FilterClients;
        ClientsListView.ItemsSource = FilteredClients;
    }

    private void EditMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (ClientsListView.SelectedItem != null)
        {
            User selectedClient = (User)ClientsListView.SelectedItem;
            EditClientWindow editWindow = new EditClientWindow(selectedClient);
            editWindow.Closed += EditWindow_Closed;
            editWindow.ShowDialog();
        }
    }

    private void EditWindow_Closed(object sender, EventArgs e)
    {
        ((Window)sender).Closed -= EditWindow_Closed;
        RefreshClientList();
    }

    private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (ClientsListView.SelectedItem != null)
        {
            User selectedClient = (User)ClientsListView.SelectedItem;
            context.Users.Remove(selectedClient);
            context.SaveChanges();
            RefreshClientList();
        }
    }

    private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        PlaceholderTextBlock.Visibility = Visibility.Collapsed;
    }

    private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
    {
        var textBox = sender as System.Windows.Controls.TextBox;
        PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
    }

    private void AddClientButton_Click(object sender, RoutedEventArgs e)
    {
        string lastName = lastNameTextBox.Text;
        string firstName = firstNameTextBox.Text;
        string fatherName = fatherNameTextBox.Text;
        string phoneNumber = phoneNumberTextBox.Text;
        string email = emailTextBox.Text;
        DateTime birthDate = datePicker1.SelectedDate ?? DateTime.Now;

        if (!IsValidInput(lastName, firstName, fatherName, phoneNumber, email))
        {
            MessageBox.Show("Пожалуйста, исправьте ошибки ввода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        User newUser = new()
        {
            Фио = $"{lastName} {firstName} {fatherName}",
            Телефон = phoneNumber,
            Email = email,
            ДатаРождения = birthDate
        };

        using (var transaction = context.Database.BeginTransaction())
        {
            try
            {
                context.Users.Add(newUser);
                context.SaveChanges();

                DiscontCard newDiscountCard = new()
                {
                    UserId = newUser.Id,
                    Discont = 5.0
                };

                context.DiscontCards.Add(newDiscountCard);
                context.SaveChanges();

                transaction.Commit();

                MessageBox.Show("Клиент успешно добавлен и создана дисконтная карта.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearClientFormFields();
                RefreshClientList();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private bool IsValidInput(string lastName, string firstName, string fatherName, string phoneNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 3 || lastName.Length > 50)
        {
            MessageBox.Show("Фамилия должна быть от 3 до 50 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 3 || firstName.Length > 50)
        {
            MessageBox.Show("Имя должно быть от 3 до 50 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (string.IsNullOrWhiteSpace(fatherName) || fatherName.Length < 3 || fatherName.Length > 50)
        {
            MessageBox.Show("Отчество должно быть от 3 до 50 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (!Regex.IsMatch(phoneNumber, @"^\+?\d{10,15}$"))
        {
            MessageBox.Show("Некорректный номер телефона. Должен содержать от 10 до 15 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MessageBox.Show("Некорректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        return true;
    }

    // Пример обработчика события с учетом допустимости значений NULL
  

    private void ClearClientFormFields()
    {
        lastNameTextBox.Text = "";
        firstNameTextBox.Text = "";
        fatherNameTextBox.Text = "";
        phoneNumberTextBox.Text = "";
        emailTextBox.Text = "";
        datePicker1.SelectedDate = null;
    }

    private bool FilterClients(object item)
    {
        if (string.IsNullOrEmpty(FilterTextBox.Text))
            return true;

        var client = item as User;
        return client != null && client.Телефон.StartsWith(FilterTextBox.Text);
    }

    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (FilteredClients != null)
        {
            FilteredClients.Refresh();
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
    }

    private void ClientsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }
}
