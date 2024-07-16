using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MessageBox = System.Windows.MessageBox;

namespace Praktika
{
    public partial class MainWindow : Window
    {
        private ApplicationContext context;
        public ICollectionView FilteredClients { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            context = new ApplicationContext();
            RefreshClientList();
        }

        // Обработчик события нажатия кнопки "Добавить клиента"
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
        private void RefreshClientList()
        {
            var userList = context.Users.ToList();
            FilteredClients = CollectionViewSource.GetDefaultView(userList);
            FilteredClients.Filter = FilterClients;
            ClientsListView.ItemsSource = FilteredClients;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = lastNameTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string fatherName = fatherNameTextBox.Text;
            string phoneNumber = phoneNumberTextBox.Text;
            string email = emailTextBox.Text;
            DateTime birthDate = datePicker1.SelectedDate ?? DateTime.Now;

            User newUser = new User
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

                    int clientId = newUser.UsersId;

                    DiscontCard newDiscountCard = new DiscontCard
                    {
                        UsersId = clientId,
                        Discont = 5.0,
                        OrderSum = 0.0
                    };

                    context.DiscontCard.Add(newDiscountCard);
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
}
