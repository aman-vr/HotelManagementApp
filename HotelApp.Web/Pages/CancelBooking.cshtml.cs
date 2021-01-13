using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelApp.Web.Pages
{
    public class CancelBookingModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string MobileNo { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        public List<BookingFullModel> BookingsMade { get; set; }
        public IDatabaseData Db { get; }

        public CancelBookingModel(IDatabaseData Db)
        {
            this.Db = Db;
        }

        public void OnGet()
        {
            if (SearchEnabled == true)
            {
                BookingsMade = Db.SearchBookings(MobileNo);
            }
        }
        public IActionResult OnPost()
        {
            return RedirectToPage(new
            {
                SearchEnabled = true,
                MobileNo = MobileNo
            });
        }
    }
}
