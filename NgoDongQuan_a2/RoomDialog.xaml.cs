using DataAccess.Models;
using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class RoomDialog : Window
    {
        public RoomInformation Room { get; set; }

        public RoomDialog()
        {
            InitializeComponent();
            Room = new RoomInformation();
            DataContext = this;
        }

        public RoomDialog(RoomInformation room)
        {
            InitializeComponent();
            Room = new RoomInformation
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomDetailDescription = room.RoomDetailDescription,
                RoomMaxCapacity = room.RoomMaxCapacity,
                RoomTypeId = room.RoomTypeId,
                RoomStatus = room.RoomStatus,
                RoomPricePerDay = room.RoomPricePerDay
            };
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
} 