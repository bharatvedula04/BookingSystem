// See https://aka.ms/new-console-template for more information
using BookingSystem;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Utils;

Console.WriteLine($"Choose your option{Environment.NewLine} 1: Make Booking - (Enter DD/MM HH:mm) {Environment.NewLine} 2: Delete Booking - (Enter DD/MM HH:mm) {Environment.NewLine} 3: Find Booking - (Enter DD/MM) {Environment.NewLine} 4: Keep Booking - (Enter DD/MM)");
while (true)
{
    int a;
    int.TryParse(Console.ReadLine(), out a);

    if (ValidateInput(a))
    {
        ProcessBooking(a);
    }
    else
    {
        Console.WriteLine("Invalid Input"); 

    }
}

void ProcessBooking(int a)
{
    try
    {
        switch (a)
        {
            case 1:
                Console.WriteLine("Enter DD/MM HH:mm");
                //string input;

                var input1 = Console.ReadLine();
                DateTime slotTime = DateTime.Now;
                slotTime = slotTime.GetFormattedTime(int.Parse(input1.Split('/')[1].Split(' ')[0]), int.Parse(input1.Split('/')[0]), int.Parse(input1.Split('/')[1].Split(' ')[1].Split(':')[0]), int.Parse(input1.Split('/')[1].Split(' ')[1].Split(':')[1]));
                Console.WriteLine(BookingBLL.MakeBooking(slotTime).Status);
                break;
            case 2:
                Console.WriteLine("Enter DD/MM HH:mm");
                //string input;

                var input2 = Console.ReadLine();
                DateTime slotTime1 = DateTime.Now;
                slotTime1 = slotTime1.GetFormattedTime(int.Parse(input2.Split('/')[1].Split(' ')[0]), int.Parse(input2.Split('/')[0]), int.Parse(input2.Split('/')[1].Split(' ')[1].Split(':')[0]), int.Parse(input2.Split('/')[1].Split(' ')[1].Split(':')[1]));
                Console.WriteLine(BookingBLL.DeleteBooking(slotTime1).Status);
                break;
            case 3:
                Console.WriteLine("Enter DD/MM");
                //string input;

                var input3 = Console.ReadLine();
                DateTime slotTime3 = DateTime.Now;
                slotTime3 = slotTime3.GetFormattedTime(int.Parse(input3.Split('/')[1]), int.Parse(input3.Split('/')[0]),0,0);
                BookingBLL.GetAllBookings(slotTime3).ToList().ForEach( item => Console.WriteLine(item));
                break;
            case 4:
                Console.WriteLine("Enter DD/MM");
                //string input;

                var input4 = Console.ReadLine();
                DateTime slotTime4 = DateTime.Now;
                slotTime3 = slotTime4.GetFormattedTime(int.Parse(input4.Split('/')[1]), int.Parse(input4.Split('/')[0]), 0, 0);
                Console.WriteLine(BookingBLL.KeepBooking(slotTime4));
                break;
        }
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}

bool ValidateInput(int a)
{
    return a < 4 && a != 0;   
}