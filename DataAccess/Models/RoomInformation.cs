using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.Models;

public partial class RoomInformation : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private int _roomId;
    public int RoomId
    {
        get => _roomId;
        set { _roomId = value; OnPropertyChanged(nameof(RoomId)); }
    }

    private string _roomNumber;
    public string RoomNumber
    {
        get => _roomNumber;
        set { _roomNumber = value; OnPropertyChanged(nameof(RoomNumber)); }
    }

    private string _roomDetailDescription;
    public string RoomDetailDescription
    {
        get => _roomDetailDescription;
        set { _roomDetailDescription = value; OnPropertyChanged(nameof(RoomDetailDescription)); }
    }

    private int? _roomMaxCapacity;
    public int? RoomMaxCapacity
    {
        get => _roomMaxCapacity;
        set { _roomMaxCapacity = value; OnPropertyChanged(nameof(RoomMaxCapacity)); }
    }

    private int? _roomTypeId;
    public int? RoomTypeId
    {
        get => _roomTypeId;
        set { _roomTypeId = value; OnPropertyChanged(nameof(RoomTypeId)); }
    }

    private byte? _roomStatus;
    public byte? RoomStatus
    {
        get => _roomStatus;
        set { _roomStatus = value; OnPropertyChanged(nameof(RoomStatus)); }
    }

    private decimal? _roomPricePerDay;
    public decimal? RoomPricePerDay
    {
        get => _roomPricePerDay;
        set { _roomPricePerDay = value; OnPropertyChanged(nameof(RoomPricePerDay)); }
    }

    public virtual ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}
