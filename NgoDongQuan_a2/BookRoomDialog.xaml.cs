using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NgoDongQuan_a2
{
    public partial class BookRoomDialog : Window
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ObservableCollection<RoomInformation> AvailableRooms { get; set; }
        public RoomInformation SelectedRoom { get; set; }
        private Customer _customer;

        public BookRoomDialog(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(1);
            AvailableRooms = new ObservableCollection<RoomInformation>();
            DataContext = this;
        }

        private void Book_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate == null || EndDate == null || SelectedRoom == null || StartDate >= EndDate)
            {
                MessageBox.Show("Please select valid dates and a room.", "Error");
                return;
            }

            // Check again for room availability (in case of race condition)
            var context = new FuminiHotelManagementContext();
            var bookingRepo = new GenericRepository<BookingReservation>(context);
            var detailRepo = new GenericRepository<BookingDetail>(context);
            var roomRepo = new GenericRepository<RoomInformation>(context);

            var start = DateOnly.FromDateTime(StartDate.Value);
            var end = DateOnly.FromDateTime(EndDate.Value);
            // Check if the room is available for the selected period
            var overlapping = context.BookingDetails.Any(d => d.RoomId == SelectedRoom.RoomId &&
                (start < d.EndDate && end > d.StartDate));
            if (overlapping)
            {
                MessageBox.Show("The selected room is not available for the chosen dates.", "Error");
                return;
            }

            // Generate new BookingReservationId
            var maxId = bookingRepo.GetAll().Any() ? bookingRepo.GetAll().Max(b => b.BookingReservationId) : 0;
            var booking = new BookingReservation
            {
                BookingReservationId = maxId + 1,
                BookingDate = DateTime.Now,
                CustomerId = _customer.CustomerId,
                BookingStatus = 1 // e.g., 1 = active
            };
            bookingRepo.Insert(booking);
            bookingRepo.Save();

            // Add BookingDetail
            var detail = new BookingDetail
            {
                BookingReservationId = booking.BookingReservationId,
                RoomId = SelectedRoom.RoomId,
                StartDate = DateOnly.FromDateTime(StartDate.Value),
                EndDate = DateOnly.FromDateTime(EndDate.Value),
                ActualPrice = SelectedRoom.RoomPricePerDay ?? 0
            };
            detailRepo.Insert(detail);
            detailRepo.Save();

            MessageBox.Show("Booking successful!", "Success");
            DialogResult = true;
        }

        // Load available rooms when dates change
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            LoadAvailableRooms();
        }

        private void LoadAvailableRooms()
        {
            if (StartDate == null || EndDate == null || StartDate >= EndDate)
            {
                AvailableRooms.Clear();
                return;
            }
            var start = DateOnly.FromDateTime(StartDate.Value);
            var end = DateOnly.FromDateTime(EndDate.Value);
            var context = new FuminiHotelManagementContext();
            var roomRepo = new GenericRepository<RoomInformation>(context);
            var allRooms = roomRepo.GetAll().ToList();
            var bookedRoomIds = context.BookingDetails
                .Where(d => (start < d.EndDate && end > d.StartDate))
                .Select(d => d.RoomId)
                .Distinct()
                .ToList();
            var available = allRooms.Where(r => !bookedRoomIds.Contains(r.RoomId)).ToList();
            AvailableRooms.Clear();
            foreach (var r in available)
                AvailableRooms.Add(r);
        }
    }
} 