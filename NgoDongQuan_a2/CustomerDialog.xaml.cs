using DataAccess.Models;
using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; set; }

        public CustomerDialog()
        {
            InitializeComponent();
            Customer = new Customer();
            DataContext = this;
        }

        public CustomerDialog(Customer customer)
        {
            InitializeComponent();
            Customer = new Customer
            {
                CustomerId = customer.CustomerId,
                CustomerFullName = customer.CustomerFullName,
                Telephone = customer.Telephone,
                EmailAddress = customer.EmailAddress,
                CustomerBirthday = customer.CustomerBirthday,
                CustomerStatus = customer.CustomerStatus,
                Password = customer.Password
            };
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
} 