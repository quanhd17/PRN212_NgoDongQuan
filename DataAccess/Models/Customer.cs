using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.Models;

public partial class Customer : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private int _customerId;
    public int CustomerId
    {
        get => _customerId;
        set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
    }

    private string _customerFullName;
    public string CustomerFullName
    {
        get => _customerFullName;
        set { _customerFullName = value; OnPropertyChanged(nameof(CustomerFullName)); }
    }

    private string _telephone;
    public string Telephone
    {
        get => _telephone;
        set { _telephone = value; OnPropertyChanged(nameof(Telephone)); }
    }

    private string _emailAddress;
    public string EmailAddress
    {
        get => _emailAddress;
        set { _emailAddress = value; OnPropertyChanged(nameof(EmailAddress)); }
    }

    private System.DateTime? _customerBirthday;
    public System.DateTime? CustomerBirthday
    {
        get => _customerBirthday;
        set { _customerBirthday = value; OnPropertyChanged(nameof(CustomerBirthday)); }
    }

    private byte? _customerStatus;
    public byte? CustomerStatus
    {
        get => _customerStatus;
        set { _customerStatus = value; OnPropertyChanged(nameof(CustomerStatus)); }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }

    public virtual ICollection<BookingReservation> BookingReservations { get; set; } = new List<BookingReservation>();
}
