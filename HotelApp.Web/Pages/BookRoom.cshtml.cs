using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        private readonly IDatabaseData db;

        [BindProperty(SupportsGet = true)]
        public int RoomTypeId{ get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string MobileNo { get; set; }

        public RoomTypeModel RoomType { get; set; }
        public BookRoomModel(IDatabaseData db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            if (RoomTypeId > 0)
            {
                RoomType = db.GetRoomTypeById(RoomTypeId);  
            }
        }

        public IActionResult OnPost()
        {
            db.BookGuest(FirstName, LastName, MobileNo, StartDate, EndDate, RoomTypeId);
            return RedirectToPage("/ThankYou");
        }
    }
}
