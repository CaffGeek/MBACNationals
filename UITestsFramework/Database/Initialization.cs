using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UITestsFramework.Database
{
    public static class Initialization
    {
        public static void BuildTables()
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_Membership')) BEGIN DROP TABLE [webpages_Membership] END
                                                  IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_OAuthMembership')) BEGIN DROP TABLE [webpages_OAuthMembership] END
                                                  IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_UsersInRoles')) BEGIN DROP TABLE [webpages_UsersInRoles] END
                                                  IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'webpages_Roles')) BEGIN DROP TABLE [webpages_Roles] END
                                                  IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserProfile')) BEGIN DROP TABLE [UserProfile] END                                                  
                                                ", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }


                using (var cmd = new SqlCommand(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Events'))
                                                BEGIN DROP TABLE [Events] END
                                                CREATE TABLE [Events] (
                                                	AggregateId uniqueidentifier not null,
                                                	SequenceNumber int not null,
                                                	[Type] nvarchar(max) not null,
                                                	Body nvarchar(max) not null,
                                                	Timestamp datetime2 not null,
                                                	IsDeleted bit)", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new SqlCommand(@"IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Aggregates'))
                                                BEGIN DROP TABLE [Aggregates] END
                                                CREATE TABLE [Aggregates] (
                                                	Id uniqueidentifier not null,
                                                	[Type] nvarchar(max) not null)", con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
