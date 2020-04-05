using System;

namespace PPO2._3
{
    public class Manager
    {
        public static void GetInfo(int userId)
        {
            var user = DatabaseHandler.GetUserInfo(userId);
            Console.WriteLine($"Name:                    {user.Key}\n" +
                              $"Id:                      {userId}\n" +
                              $"Subscription expires at: {user.Value.ToString("MM/dd/yyyy")}\n" +
                              $"Avrege visit number:     {StatiscsHandler.CalculateAverege(userId)}");
        }

        public static void Prolongate(int userId, int dayNum)
        {
            var curTime = DatabaseHandler.GetExpireTime(userId);
            DatabaseHandler.UpdateExpireTime(userId, curTime.AddDays(dayNum));
        }

        public static void AddUser(String username)
        {
            DatabaseHandler.InsertUser(username);
        }
    }
}