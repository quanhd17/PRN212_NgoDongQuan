using DataAccess.Models;
using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class BookingDialog : Window
    {
        public BookingReservation Booking { get; set; }

        public BookingDialog()
        {
            InitializeComponent();
            Booking = new BookingReservation();
            DataContext = this;
        }

        public BookingDialog(BookingReservation booking)
        {
            InitializeComponent();
            Booking = new BookingReservation
            {
                BookingReservationId = booking.BookingReservationId,
                BookingDate = booking.BookingDate,
                TotalPrice = booking.TotalPrice,
                CustomerId = booking.CustomerId,
                BookingStatus = booking.BookingStatus
            };
            DataContext = this;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
} 