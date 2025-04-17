using Module2.Models;
using Module2.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Module2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BochagovaDemExamContext context = new BochagovaDemExamContext();
        public MainWindow()
        {
            InitializeComponent();
            LoadPartner();
        }

        private void LoadPartner()
        {
            var partners = context.Partners.ToList();
            listPartners.ItemsSource = partners;
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {            
            AddPartner newPartnerWindow = new AddPartner();
            newPartnerWindow.Show();
            Close();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Partner selectedPartner)
            {
                AddPartner editPartnerWindow = new AddPartner(selectedPartner);
                editPartnerWindow.Show();
                Close();
            }
        }
    }
}