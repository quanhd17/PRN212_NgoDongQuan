using DataAccess;
using DataAccess.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NgoDongQuan_a2
{
    public class BookingManagementViewModel : ViewModelBase
    {
        private readonly GenericRepository<BookingReservation> _bookingRepo;

        public ObservableCollection<BookingReservation> Bookings { get; set; }
        private BookingReservation _selectedBooking;
        public BookingReservation SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); }
        }

        public BookingManagementViewModel()
        {
            var context = new FuminiHotelManagementContext();
            _bookingRepo = new GenericRepository<BookingReservation>(context);

            Bookings = new ObservableCollection<BookingReservation>(_bookingRepo.GetAll());

            AddCommand = new RelayCommand(_ => AddBooking());
            EditCommand = new RelayCommand(_ => EditBooking(), _ => SelectedBooking != null);
            DeleteCommand = new RelayCommand(_ => DeleteBooking(), _ => SelectedBooking != null);
            SearchCommand = new RelayCommand(_ => SearchBooking());
        }

        private void AddBooking()
        {
            var dialog = new BookingDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Generate a new unique BookingReservationId
                    var maxId = _bookingRepo.GetAll().Any() ? _bookingRepo.GetAll().Max(b => b.BookingReservationId) : 0;
                    dialog.Booking.BookingReservationId = maxId + 1;

                    _bookingRepo.Insert(dialog.Booking);
                    _bookingRepo.Save();
                    Bookings.Add(dialog.Booking);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    {
                        System.Windows.MessageBox.Show("The selected customer does not exist in the database.", "Error");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("An error occurred while saving the booking: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void EditBooking()
        {
            var dialog = new BookingDialog(SelectedBooking);
            if (dialog.ShowDialog() == true)
            {
                // Update the tracked entity's properties
                SelectedBooking.BookingDate = dialog.Booking.BookingDate;
                SelectedBooking.TotalPrice = dialog.Booking.TotalPrice;
                SelectedBooking.CustomerId = dialog.Booking.CustomerId;
                SelectedBooking.BookingStatus = dialog.Booking.BookingStatus;

                try
                {
                    _bookingRepo.Update(SelectedBooking);
                    _bookingRepo.Save();
                    // No need to replace the item in the collection
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("FOREIGN KEY"))
                    {
                        System.Windows.MessageBox.Show("The selected customer does not exist in the database.", "Error");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("An error occurred while saving the booking: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void DeleteBooking()
        {
            if (SelectedBooking != null)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to delete this booking?", "Confirm", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    _bookingRepo.Delete(SelectedBooking.BookingReservationId);
                    _bookingRepo.Save();
                    Bookings.Remove(SelectedBooking);
                }
            }
        }

        private void SearchBooking()
        {
            Bookings.Clear();
            foreach (var b in _bookingRepo.Find(b => b.BookingReservationId.ToString().Contains(SearchText ?? "")))
                Bookings.Add(b);
        }
    }
} 