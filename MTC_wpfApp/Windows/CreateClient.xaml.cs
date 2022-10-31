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
        public CreateClient()
        {
            InitializeComponent();
        }

        private void createClientButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Client client = new Models.Client();
            client.name = NameTextBox.Text;
            client.surname = SurnameTextBox.Text;
            client.patronymic = PatronymicTextBox.Text;
            client.phone = PhoneTextBox.Text;
            client.adress = AdressTextBox.Text;
            client.reg_date = DateTime.Now;
            Models.MTCEntities.GetContext().Clients.Add(client);
            Models.MTCEntities.GetContext().SaveChanges();

            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }
    }
}
