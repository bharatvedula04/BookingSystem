using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem
{
    public class BookingDAL
    {
        public IEnumerable<DateTime> GetAvailableSlots(DateTime bookSlot)
        {
            using (var context = new BookingSystemContext())
            {
                context.Database.EnsureCreated();
                var result= context.Booking.Where( x => x.BookingSlots.Date == bookSlot.Date)
                    .ToList();
                return result.Select(x => x.BookingSlots);
               
                
            }
        }

        public BookingStatusModel MakeBooking(DateTime bookSlot)
        {
            try
            {
                if (ImposedRulesCheck(bookSlot) && IsNotSecondDayOfThirdWeekInMonth(bookSlot))
                {
                    using (var context = new BookingSystemContext())
                    {
                        context.Database.EnsureCreated();

                        var model =
                    new BookingSystemModel
                    {
                        //Id = new Guid(),
                        BookingSlots = bookSlot

                    };
                        context.Booking.Add(model);
                        context.SaveChanges();
                    }
                    return new BookingStatusModel() { Status = "Booking is Successful" };
                }
                else
                {
                    return new BookingStatusModel() { Status = $"Booking is Unsuccessful" };
                }
            }
            catch (Exception ex) 
            {
                return new BookingStatusModel() { Status = $"Booking is UnSuccessful-{ex.Message}" };
            }
        }

        public BookingStatusModel DeleteBooking(DateTime bookSlot)
        {
            try
            {
                using (var context = new BookingSystemContext())
                {
                    context.Database.EnsureCreated();
                    var itemToRemove = context.Booking.SingleOrDefault(x => x.BookingSlots == bookSlot); //returns a single item.

                    if (itemToRemove != null)
                    {
                        context.Booking.Remove(itemToRemove);
                        context.SaveChanges();
                    }
                }
                return new BookingStatusModel() { Status = "Booking is Deleted" };
            }
            catch (Exception ex)
            {
                return new BookingStatusModel() { Status = $"Booking deletion is  UnSuccessful-{ex.Message}" };
            }
        }
        private bool ImposedRulesCheck(DateTime bookSlot)
        {
            if (bookSlot.Hour < 9 || bookSlot.Hour >= 17 || (bookSlot.Hour > 16 && bookSlot.Minute > 30))
                return false;
            else if (bookSlot.Minute == 30 || bookSlot.Minute == 0)
                return true;
            return false;
              
        }

        private static bool IsNotSecondDayOfThirdWeekInMonth(DateTime bookSlot)
        {
            DateTime res = new DateTime(bookSlot.Year, bookSlot.Month, 3);
            int offset = -(res.DayOfWeek - bookSlot.DayOfWeek);

            if (offset < 0)
                offset += 7;

            res = res.AddDays(offset);

            if (res.DayOfWeek == DayOfWeek.Tuesday && ((bookSlot.Hour >= 17) || (bookSlot.Hour >= 16 && bookSlot.Minute >= 30)))
                return false;
            else return true;
        }
        private static IEnumerable<DateTime> GetWorkingHourIntervals(DateTime clockIn, DateTime clockOut)
        {
            yield return clockIn;

            DateTime d = new DateTime(clockIn.Year, clockIn.Month, clockIn.Day, clockIn.Hour, 0, 0, clockIn.Kind).AddHours(1);

            while (d < clockOut)
            {
                yield return d;
                d = d.AddHours(1);
            }

            yield return clockOut;
        }

        
    }
}