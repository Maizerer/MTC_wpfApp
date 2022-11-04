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

namespace MTC_wpfApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для CreateClient.xaml
    /// </summary>
    public partial class CreateClient : Window
    {
        private Models.Client _currentClient = new Models.Client();
        public CreateClient(Models.Client client)
        {
            InitializeComponent();
            if(client != null)
            {
                _currentClient = client;
            }
            DataContext = _currentClient;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                errors.AppendLine("Введите имя. Это обязательное поле");
            }
            if (string.IsNullOrEmpty(SurnameTextBox.Text))
            {
                errors.AppendLine("Введите фамилию. Это обязательное поле");
            }
            if (string.IsNullOrEmpty(PatronymicTextBox.Text))
            {
                errors.AppendLine("Введите отчество. Это обязательное поле");
            }
            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                errors.AppendLine("Введите номер телефона. Это обязательное поле");
            }
            if (string.IsNullOrEmpty(AdressTextBox.Text))
            {
                errors.AppendLine("Введите адрес. Это обязательное поле");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Некорректные данные", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(_currentClient.Id == 0)
            {
                _currentClient.reg_date = DateTime.Now;
                Models.MTCEntities.GetContext().Clients.Add(_currentClient);
            }

            try
            {
                Models.MTCEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Windows.ClientsTable clientsTable = new Windows.ClientsTable();
                clientsTable.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toClientTable_Click(object sender, RoutedEventArgs e)
        {
            Windows.ClientsTable clientsTable = new Windows.ClientsTable();
            clientsTable.Show();
            this.Hide();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }
    }
}
