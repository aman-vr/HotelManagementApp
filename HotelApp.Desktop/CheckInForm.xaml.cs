using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Interaction logic for CheckInForm.xaml
    /// </summary>
    public partial class CheckInForm : Window
    {
        private readonly IDatabaseData db;
        private BookingFullModel _data = null;
        public CheckInForm(IDatabaseData db)
        {
            InitializeComponent();
            this.db = db;
        }

        public void PopulateCheckInInfo(BookingFullModel data)
        {
            _data = data;
            firstNameText.Text = _data.FirstName;
            lastNameText.Text = _data.LastName;
            mobileNoText.Text = _data.MobileNo;
            titleText.Text = _data.Title;
            roomNumberText.Text = _data.RoomNumber;
            totalCostText.Text = String.Format("{0:c}",_data.TotalCost);
        }

        private void CheckInUser_Click(object sender, RoutedEventArgs e)
        {
            db.CheckInGuest(_data.RoomId, _data.StartDate);
            this.Close();
        }
    }
}
