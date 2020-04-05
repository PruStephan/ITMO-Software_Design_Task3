using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using Npgsql;


namespace PPO2._3
{
    public static class DatabaseHandler
    {
        public static readonly NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Username=stephan;Password=;Database=fitness");

        public static int GetMaxEventId(int userId)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"Select last_event_id from maxvals where user_id = {userId}", conn);
            var result = Convert.ToInt32(cmd.ExecuteScalar()); 
            conn.Close();
            return result;
        }

        public static void UpdateMaxEventId(int userId, int newEventId)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"Update maxvals set last_event_id = {newEventId} where user_id = {userId}", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static DateTime GetExpireTime(int userId)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"SELECT subscription_expires_at " +
                                        $"FROM Users WHERE user_id = {userId}", DatabaseHandler.conn);

            var reader = cmd.ExecuteScalar();
            conn.Close();
            return Convert.ToDateTime(reader);   
        }

        public static void UpdateExpireTime(int userId, DateTime newDt)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"Update users set subscription_expires_at =" +
                                        $" '{newDt.ToString("yyyy-MM-dd HH:mm:ss")}'" +
                                        $" where user_id = {userId}", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static void InsertIvent(int userId, int eventId, bool entered, DateTime happenedAt)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"INSERT INTO events VALUES" +
                                        $" ({userId}, {eventId}, {entered}, '{DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss")}')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public static int GetUserNum()
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"SELECT user_id From Users", conn);
            var reader = cmd.ExecuteReader();
            int res = 0;
            while (reader.Read())
                res++;
            conn.Close();
            return res;
        }

        public static DateTime GetTimeInGym(int eventId)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"SELECT happened_at " +
                                        $"FROM Events WHERE event_id = {eventId}", DatabaseHandler.conn);

            var reader = cmd.ExecuteScalar();
            conn.Close();
            return Convert.ToDateTime(reader);  
        }

        public static KeyValuePair<String, DateTime> GetUserInfo(int userId)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"SELECT user_name " +
                                        $"FROM Users WHERE user_id = {userId}", DatabaseHandler.conn);
            
            var username = Convert.ToString(cmd.ExecuteScalar());
            cmd = new NpgsqlCommand($"SELECT subscription_expires_at " +
                                        $"FROM Users WHERE user_id = {userId}", DatabaseHandler.conn);
            
            var subscription = Convert.ToDateTime(cmd.ExecuteScalar());

            conn.Close();
            return new KeyValuePair<String, DateTime>(username, subscription);
        }

        public static void InsertUser(string username)
        {
            conn.Open();
            var cmd = new NpgsqlCommand($"INSERT INTO Users(user_name, subscription_expires_at)" +
                                        $" VALUES ('{username}', '{DateTime.Now.AddDays(31).ToString("yyyy-MM-dd HH:mm:ss")}')", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            cmd = new NpgsqlCommand($"SELECT user_id from Users where user_name = '{username}'", conn);
            var user_id = Convert.ToInt32(cmd.ExecuteScalar()); 
            
            cmd = new NpgsqlCommand($"INSERT INTO maxvals values ({user_id}, 0)", conn);
            cmd.ExecuteNonQuery();
            
            conn.Close();
        }
    }
    //INSERT INTO Users(user_name, subscription_expires_at) VALUES ({username}), '{DateTime.Now.AddDays(31)}'
    //INSERT INTO Users(user_name, subscription_expires_at) CALUES ()
}