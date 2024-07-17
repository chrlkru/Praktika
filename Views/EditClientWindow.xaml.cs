using Praktika.Models;
using System.Windows;

namespace Praktika.Views;

public partial class EditClientWindow : Window
{
    private User _user;

    public EditClientWindow(User user)
    {
        InitializeComponent();
        _user = user;
        DataContext = _user;
        var FIO = user.Фио.Split().ToArray();
        lastNameTextBox.Text = FIO[0];
        firstNameTextBox.Text = FIO[1];
        fatherNameTextBox.Text = FIO[2];
        phoneNumberTextBox.Text = user.Телефон.ToString();
        emailTextBox.Text = user.Email;
        datePicker1.Text = user.ДатаРождения.ToString();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        _user.Фио = $"{lastNameTextBox.Text} {firstNameTextBox.Text} {fatherNameTextBox.Text}";
        _user.Телефон = phoneNumberTextBox.Text;
        _user.Email = emailTextBox.Text;
        _user.ДатаРождения = datePicker1.SelectedDate ?? _user.ДатаРождения;

        // Сохранение изменений в базе данных
        using (var context = new PraktikaContext())
        {
            context.Users.Update(_user);
            context.SaveChanges();
        }

        DialogResult = true;
        Close();
    }
}
