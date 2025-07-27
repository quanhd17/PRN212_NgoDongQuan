
using BusinessLogic;
using DataAccess;
using DataAccess.Models;
using System.Windows.Input;

namespace NgoDongQuan_a2
{
    public class LoginViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object parameter)
        {
            // Admin check
            if (Email == AppConfiguration.GetAdminEmail() && Password == AppConfiguration.GetAdminPassword())
            {
                // Open Admin Main Window
                App.Current.Dispatcher.Invoke(() =>
                {
                    var adminWindow = new AdminMainWindow();
                    adminWindow.Show();
                    App.Current.MainWindow.Close();
                });
                return;
            }

            // Customer check
            var context = new FuminiHotelManagementContext();
            var customerRepo = new GenericRepository<Customer>(context);
            var customerService = new CustomerService(customerRepo);
            var customer = customerService.Authenticate(Email, Password);

            if (customer != null)
            {
                // Open Customer Main Window
                App.Current.Dispatcher.Invoke(() =>
                {
                    var customerWindow = new CustomerMainWindow(customer);
                    customerWindow.Show();
                    App.Current.MainWindow.Close();
                });
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
            }
        }
    }
} 