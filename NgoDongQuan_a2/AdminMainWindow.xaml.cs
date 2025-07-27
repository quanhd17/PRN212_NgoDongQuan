using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void ManageCustomers_Click(object sender, RoutedEventArgs e)
        {
            var window = new CustomerManagementWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void ManageRooms_Click(object sender, RoutedEventArgs e)
        {
            var window = new RoomManagementWindow();
            window.Owner = this;
            window.ShowDialog();
        }

        private void ManageBookings_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookingManagementWindow();
            window.Owner = this;
            window.ShowDialog();
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