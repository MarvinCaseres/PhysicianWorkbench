using System;
using MySql.Data.MySqlClient;

namespace PhysicianWorkbench
{
    internal static class Class1
    {
        public static string LoggedInUser;
        public static string LoggedInUserID;
        public static string LoggedInUserPasword;

        public static string connectionString = "server=localhost;database=opddeptdatabase;uid=root;pwd=;";

        public static string GetUserID(string username)
        {
            string userId = "";
            userId = LoggedInUserID;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT id FROM staffaccounts WHERE username=@username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    userId = result.ToString();
                }
            }

            return userId;
        }
    }
}