using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem
{
    public class BookingBLL
    {
        public static IEnumerable<DateTime> GetAllBookings(DateTime bookingDate)
        {
            return new BookingDAL().GetAvailableSlots(bookingDate);
        }

        public static BookingStatusModel MakeBooking(DateTime bookSlot)
        {
            return new BookingDAL().MakeBooking(bookSlot);
        }

        public static BookingStatusModel DeleteBooking(DateTime bookSlot)
        {
            return new BookingDAL().DeleteBooking(bookSlot);
        }

        public static BookingStatusModel KeepBooking(DateTime bookSlot)
        {
            throw new NotImplementedException();
        }
    }
}
