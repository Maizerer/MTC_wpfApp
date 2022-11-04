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
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Win32;
namespace MTC_wpfApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ClientsTable.xaml
    /// </summary>
    public partial class ClientsTable : Window
    {
        public ClientsTable()
        {
            InitializeComponent();
            updateDataGrid();
        }

        private void updateDataGrid()
        {
            clientGrid.ItemsSource = Models.MTCEntities.GetContext().Clients.ToList();
        }
        private void openClientwindowButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.CreateClient window = new Windows.CreateClient(null);
            window.Show();
            this.Hide();
        }

        private void deleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Client clientToDelete = (sender as Button).DataContext as Models.Client;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Вы уверены что хотите удалить клиента №{clientToDelete.Id} {clientToDelete.name} {clientToDelete.surname} {clientToDelete.patronymic} ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Models.MTCEntities.GetContext().Clients.Remove(clientToDelete);
                Models.MTCEntities.GetContext().SaveChanges();
                MessageBox.Show($"Клиент №{clientToDelete.Id} {clientToDelete.name} {clientToDelete.surname} {clientToDelete.patronymic} был успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                updateDataGrid();
            }
        }

        private void toHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void editClientButton_Click(object sender, RoutedEventArgs e)
        {
            Models.Client clientToEdit = (sender as Button).DataContext as Models.Client;
            Windows.CreateClient window = new Windows.CreateClient(clientToEdit);
            window.Show();
            this.Hide();
        }

        private void exportButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    List<Models.Client> allClients = Models.MTCEntities.GetContext().Clients.ToList();
                    var app = new Word.Application();
                    Word.Document document = app.Documents.Add();

                    Word.Paragraph tableParagraph = document.Paragraphs.Add();
                    Word.Range tableRange = tableParagraph.Range;
                    Word.Table clientsTable = document.Tables.Add(tableRange, allClients.Count + 1, 7);
                    clientsTable.Borders.InsideLineStyle = clientsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    clientsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    Word.Range cellRange;
                    cellRange = clientsTable.Cell(1, 1).Range;
                    cellRange.Text = "Id";
                    cellRange = clientsTable.Cell(1, 2).Range;
                    cellRange.Text = "Имя";
                    cellRange = clientsTable.Cell(1, 3).Range;
                    cellRange.Text = "Фамилия";
                    cellRange = clientsTable.Cell(1, 4).Range;
                    cellRange.Text = "Отчество";
                    cellRange = clientsTable.Cell(1, 5).Range;
                    cellRange.Text = "Номер телефона";
                    cellRange = clientsTable.Cell(1, 6).Range;
                    cellRange.Text = "Адрес";
                    cellRange = clientsTable.Cell(1, 7).Range;
                    cellRange.Text = "Дата регистрации";
                    clientsTable.Rows[1].Range.Bold = 1;
                    clientsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    int i = 1;
                    foreach (var currentClient in allClients)
                    {
                        cellRange = clientsTable.Cell(i + 1, 1).Range;
                        cellRange.Text = currentClient.Id.ToString();
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 2).Range;
                        cellRange.Text = currentClient.name;
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 3).Range;
                        cellRange.Text = currentClient.surname;
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 4).Range;
                        cellRange.Text = currentClient.patronymic.ToString();
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 5).Range;
                        cellRange.Text = currentClient.phone;
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 6).Range;
                        cellRange.Text = currentClient.adress;
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = clientsTable.Cell(i + 1, 7).Range;
                        cellRange.Text = currentClient.format_reg_date;
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        i++;
                    }

                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                    app.Visible = true;
                    document.SaveAs2(saveFileDialog.FileName, Word.WdExportFormat.wdExportFormatPDF);
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка. Попробуйте позже");
                }
            }
        }
    }
}
