using DataAccess;
using DataAccess.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NgoDongQuan_a2
{
    public class RoomManagementViewModel : ViewModelBase
    {
        private readonly GenericRepository<RoomInformation> _roomRepo;

        public ObservableCollection<RoomInformation> Rooms { get; set; }
        private RoomInformation _selectedRoom;
        public RoomInformation SelectedRoom
        {
            get => _selectedRoom;
            set { _selectedRoom = value; OnPropertyChanged(); }
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

        public RoomManagementViewModel()
        {
            var context = new FuminiHotelManagementContext();
            _roomRepo = new GenericRepository<RoomInformation>(context);

            Rooms = new ObservableCollection<RoomInformation>(_roomRepo.GetAll());

            AddCommand = new RelayCommand(_ => AddRoom());
            EditCommand = new RelayCommand(_ => EditRoom(), _ => SelectedRoom != null);
            DeleteCommand = new RelayCommand(_ => DeleteRoom(), _ => SelectedRoom != null);
            SearchCommand = new RelayCommand(_ => SearchRoom());
        }

        private void AddRoom()
        {
            var dialog = new RoomDialog();
            if (dialog.ShowDialog() == true)
            {
                _roomRepo.Insert(dialog.Room);
                _roomRepo.Save();
                Rooms.Add(dialog.Room);
            }
        }

        private void EditRoom()
        {
            var dialog = new RoomDialog(SelectedRoom);
            if (dialog.ShowDialog() == true)
            {
                // Update the tracked entity's properties
                SelectedRoom.RoomNumber = dialog.Room.RoomNumber;
                SelectedRoom.RoomDetailDescription = dialog.Room.RoomDetailDescription;
                SelectedRoom.RoomMaxCapacity = dialog.Room.RoomMaxCapacity;
                SelectedRoom.RoomTypeId = dialog.Room.RoomTypeId;
                SelectedRoom.RoomStatus = dialog.Room.RoomStatus;
                SelectedRoom.RoomPricePerDay = dialog.Room.RoomPricePerDay;

                _roomRepo.Update(SelectedRoom);
                _roomRepo.Save();
                // No need to replace the item in the collection
            }
        }

        private void DeleteRoom()
        {
            if (SelectedRoom != null)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to delete this room?", "Confirm", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    _roomRepo.Delete(SelectedRoom.RoomId);
                    _roomRepo.Save();
                    Rooms.Remove(SelectedRoom);
                }
            }
        }

        private void SearchRoom()
        {
            Rooms.Clear();
            foreach (var r in _roomRepo.Find(r => r.RoomNumber.Contains(SearchText ?? "")))
                Rooms.Add(r);
        }
    }
} 