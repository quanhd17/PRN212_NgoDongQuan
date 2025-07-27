using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.Models;

public partial class BookingReservation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private int _bookingReservationId;
    public int BookingReservationId
    {
        get => _bookingReservationId;
        set { _bookingReservationId = value; OnPropertyChanged(nameof(BookingReservationId)); }
    }

    private System.DateTime? _bookingDate;
    public System.DateTime? BookingDate
    {
        get => _bookingDate;
        set { _bookingDate = value; OnPropertyChanged(nameof(BookingDate)); }
    }

    private decimal? _totalPrice;
    public decimal? TotalPrice
    {
        get => _totalPrice;
        set { _totalPrice = value; OnPropertyChanged(nameof(TotalPrice)); }
    }

    private int? _customerId;
    public int? CustomerId
    {
        get => _customerId;
        set { _customerId = value; OnPropertyChanged(nameof(CustomerId)); }
    }

    private byte? _bookingStatus;
    public byte? BookingStatus
    {
        get => _bookingStatus;
        set { _bookingStatus = value; OnPropertyChanged(nameof(BookingStatus)); }
    }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual Customer Customer { get; set; } = null!;
}
