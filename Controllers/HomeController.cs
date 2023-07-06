using diplaydatafromDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace diplaydatafromDB.Controllers
{
	public class HomeController : Controller
	{
		SqlCommand com = new SqlCommand();
		SqlDataReader dr;
		SqlConnection con = new SqlConnection();
		List<Address> Addresses = new List<Address>();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			con.ConnectionString = diplaydatafromDB.Properties.Resources.ConnectionString;

        }

		public IActionResult Index()
		{
			FetchData();

			return View(Addresses);
		}
		private void FetchData()
		{
			if(Addresses.Count > 0)
			{
				Addresses.Clear();
			}
			try
			{
				con.Open();
				com.Connection = con;
				com.CommandText = "SELECT TOP (1000) [ID],[Name] FROM [srikanth].[dbo].[NameID]";
				dr = com.ExecuteReader();
				while (dr.Read())
				{
					Addresses.Add(new Address()
					{
						ID = dr["ID"].ToString(),
						Name = dr["Name"].ToString()
					}) ;
				}
				while(dr.Read())
				con.Close();
            }
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}