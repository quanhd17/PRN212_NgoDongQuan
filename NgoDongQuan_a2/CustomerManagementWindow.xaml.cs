using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class CustomerManagementWindow : Window
    {
        public CustomerManagementWindow()
        {
            InitializeComponent();
            DataContext = new CustomerManagementViewModel();
        }
    }
} 