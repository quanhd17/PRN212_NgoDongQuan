using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class RoomManagementWindow : Window
    {
        public RoomManagementWindow()
        {
            InitializeComponent();
            DataContext = new RoomManagementViewModel();
        }
    }
} 