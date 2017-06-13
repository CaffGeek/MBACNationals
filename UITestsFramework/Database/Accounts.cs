using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UITestsFramework.Database
{

    public static class Accounts
    {
        public static void UpgradeUserToAdmin(string user)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                int userId;
                int adminRoleId;

                con.Open();
                using (var cmd = new SqlCommand("IF NOT EXISTS (SELECT 1 FROM webpages_Roles where RoleName = @Role) BEGIN INSERT INTO webpages_Roles (RoleName) VALUES (@Role) END", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Role", "Admin"));
                    cmd.ExecuteNonQuery();
                }
                using (var cmd = new SqlCommand("SELECT RoleId FROM webpages_Roles WHERE RoleName = @Role", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Role", "Admin"));
                    adminRoleId = (int)cmd.ExecuteScalar();
                }
                using (var cmd = new SqlCommand("SELECT UserId FROM UserProfile WHERE UserName = @User", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@User", user));
                    userId = (int)cmd.ExecuteScalar();
                }
                using (var cmd = new SqlCommand("INSERT INTO webpages_UsersInRoles (UserId, RoleId) VALUES (@UserId, @RoleId)", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@UserId", userId));
                    cmd.Parameters.Add(new SqlParameter("@RoleId", adminRoleId));
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
