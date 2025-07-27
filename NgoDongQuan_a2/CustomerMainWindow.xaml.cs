using DataAccess.Models;
using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class CustomerMainWindow : Window
    {
        private Customer _customer;
        public CustomerMainWindow(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BookRoomDialog(_customer);
            dialog.Owner = this;
            dialog.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            App.Current.MainWindow = loginWindow;
            loginWindow.Show();
            this.Close();
        }
    }
} 