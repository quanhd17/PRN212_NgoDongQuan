using BusinessLogic;
using DataAccess;
using DataAccess.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NgoDongQuan_a2
{
    public class CustomerManagementViewModel : ViewModelBase
    {
        private readonly CustomerService _customerService;

        public ObservableCollection<Customer> Customers { get; set; }
        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
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

        public CustomerManagementViewModel()
        {
            var context = new FuminiHotelManagementContext();
            var repo = new GenericRepository<Customer>(context);
            _customerService = new CustomerService(repo);

            Customers = new ObservableCollection<Customer>(_customerService.GetAll());

            AddCommand = new RelayCommand(_ => AddCustomer());
            EditCommand = new RelayCommand(_ => EditCustomer(), _ => SelectedCustomer != null);
            DeleteCommand = new RelayCommand(_ => DeleteCustomer(), _ => SelectedCustomer != null);
            SearchCommand = new RelayCommand(_ => SearchCustomer());
        }

        private void AddCustomer()
        {
            var dialog = new CustomerDialog();
            if (dialog.ShowDialog() == true)
            {
                _customerService.Insert(dialog.Customer);
                Customers.Add(dialog.Customer);
            }
        }

        private void EditCustomer()
        {
            var dialog = new CustomerDialog(SelectedCustomer);
            if (dialog.ShowDialog() == true)
            {
                // Update the tracked entity's properties
                SelectedCustomer.CustomerFullName = dialog.Customer.CustomerFullName;
                SelectedCustomer.Telephone = dialog.Customer.Telephone;
                SelectedCustomer.EmailAddress = dialog.Customer.EmailAddress;
                SelectedCustomer.CustomerBirthday = dialog.Customer.CustomerBirthday;
                SelectedCustomer.CustomerStatus = dialog.Customer.CustomerStatus;
                SelectedCustomer.Password = dialog.Customer.Password;

                _customerService.Update(SelectedCustomer);
                // No need to replace the item in the collection
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to delete this customer?", "Confirm", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    _customerService.Delete(SelectedCustomer.CustomerId);
                    Customers.Remove(SelectedCustomer);
                }
            }
        }

        private void SearchCustomer()
        {
            Customers.Clear();
            foreach (var c in _customerService.SearchByName(SearchText ?? ""))
                Customers.Add(c);
        }
    }
} 