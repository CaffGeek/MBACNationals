using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class HistoryController : Controller
    {
        public ActionResult Index(string year)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = @"
                        SELECT * 
                        FROM [dbo].[Events]
                        WHERE Year([Timestamp]) = @Year AND ISNULL([IsDeleted], 0) = 0
                        ORDER BY [Timestamp], [SequenceNumber]";
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("@Year", year));

                    using (var r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {

                        }
                    }
                }
            }

            // TODO: Get the history data...and return it to the view...
            ViewBag.Year = year;

            return View();
        }
    }
}