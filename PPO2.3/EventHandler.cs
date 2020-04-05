using System;
using Npgsql;

namespace PPO2._3
{
    public static class EventHandler
    {
        public static void OnEnter(int userId)
        {
            var dt = DatabaseHandler.GetExpireTime(userId);     

            if (dt < DateTime.Now)
            {
                Console.WriteLine("Your pass has expired. Contact manager for further info");
                return;
            }

            Console.WriteLine("Access granted");
            var eventId = DatabaseHandler.GetMaxEventId(userId);
            DatabaseHandler.InsertIvent(userId, eventId + 1, true, DateTime.Now);
            DatabaseHandler.UpdateMaxEventId(userId, eventId + 1);
        }

        public static void onExit(int userId)
        {
            var eventId = DatabaseHandler.GetMaxEventId(userId);
            DatabaseHandler.InsertIvent(userId, eventId + 1, true, DateTime.Now.AddMinutes(30));
            DatabaseHandler.UpdateMaxEventId(userId, eventId + 1);
            Console.WriteLine("Good bye)");

        }
    }
    
    
}