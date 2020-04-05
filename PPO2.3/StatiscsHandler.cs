using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PPO2._3
{
    public static class StatiscsHandler
    {
        private static Dictionary<int, ArrayList> visitStatistics = new Dictionary<int, ArrayList>();

        public static void RebootStatistics()
        {
            int user_num = DatabaseHandler.GetUserNum();
            for (int i = 1; i <= user_num; i++)
            {
                CountUserStatistic(i);
            }
        }

        public static void CountUserStatistic(int userId)
        {
            var max_val = DatabaseHandler.GetMaxEventId(userId);
            max_val = max_val % 2 == 0 ? max_val : max_val - 1;
            visitStatistics[userId] = new ArrayList();
            for (int j = 1; j < max_val; j += 2)
            {
                DateTime event1 = DatabaseHandler.GetTimeInGym(j);
                DateTime event2 = DatabaseHandler.GetTimeInGym(j + 1);
                visitStatistics[userId].Add(event1.Subtract(event2).TotalMinutes);
            }
        }
        
        public static void OnExit(int userId, int eventId)
        {
            DateTime event1 = DatabaseHandler.GetTimeInGym(eventId);
            DateTime event2 = DatabaseHandler.GetTimeInGym(eventId - 1);
            visitStatistics[userId].Add(event1.Subtract(event2).TotalMinutes);
        }

        public static ArrayList DayStats(int userId)
        {
            return visitStatistics[userId];
        }

        public static double CalculateAverege(int userId)
        {
            double sum = 0;
            foreach (double time in visitStatistics[userId])
            {
                sum += time;
            }

            return sum / visitStatistics[userId].Count;
        }
    }
}