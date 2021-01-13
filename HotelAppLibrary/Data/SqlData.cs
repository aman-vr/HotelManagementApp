using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelAppLibrary.Data
{
    public class SqlData : IDatabaseData
    {

        private readonly ISqlDataAccess db;
        private const string connectionStringName = "SqlDb";
        public SqlData(ISqlDataAccess db)
        {
            this.db = db;
        }
        public List<RoomTypeModel> GetAvailableRoomTypes(
            DateTime startDate,
            DateTime endDate)
        {
            return db.LoadData<RoomTypeModel, dynamic>("dbo.sp_RoomTypes_GetAvailableTypes",
                                                new { startDate, endDate },
                                                connectionStringName, true);
        }
        public void BookGuest(string firstName,
            string lastName,
            string mobileno,
            DateTime startDate,
            DateTime endDate,
            int roomTypeId)
        {
            GuestModel guest = db.LoadData<GuestModel, dynamic>("dbo.sp_Guests_Insert",
                                                                new { firstName, lastName, mobileno },
                                                                connectionStringName,
                                                                true).First();

            RoomTypeModel roomType = db.LoadData<RoomTypeModel, dynamic>(
                "select * from dbo.RoomTypes where Id = @roomTypeId",
                new { startDate, endDate, roomTypeId },
                connectionStringName, false).First();



            TimeSpan timeStaying = endDate.Subtract(startDate.Date);



            List<RoomModel> availableRooms = db.LoadData<RoomModel, dynamic>(
                "dbo.sp_Rooms_GetAvailableRooms",
                new { startDate, endDate, roomTypeId },
                connectionStringName,
                true);


            db.SaveData("dbo.sp_Bookings_Insert",
                new
                {
                    roomId = availableRooms.First().Id,
                    guestId = guest.Id,
                    startDate = startDate,
                    endDate = endDate,
                    totalCost = timeStaying.Days * roomType.Price
                },
                connectionStringName,
                true);
        }

        public RoomTypeModel GetRoomTypeById(int id)
        {
            return db.LoadData<RoomTypeModel, dynamic>("dbo.sp_RoomTypes_GetById",
                                                       new { id },
                                                       connectionStringName,
                                                       true).FirstOrDefault();
        }
        public List<BookingFullModel> SearchBookings(string mobileno)
        {
            return db.LoadData<BookingFullModel, dynamic>("sp_Bookings_Search",
                new
                {
                    mobileno,
                    startDate = DateTime.Now.Date
                },
                connectionStringName, true);
        }
        public void CheckInGuest(int roomId, DateTime startDate)
        {
            db.SaveData("dbo.sp_Bookings_CheckIn", new { roomId, startDate }, connectionStringName, true);
        }
    }
}
