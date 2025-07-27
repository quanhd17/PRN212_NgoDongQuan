using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class BookingManagementWindow : Window
    {
        public BookingManagementWindow()
        {
            InitializeComponent();
            DataContext = new BookingManagementViewModel();
        }
    }
} 