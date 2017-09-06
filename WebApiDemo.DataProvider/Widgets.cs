using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiDemo.Models;

namespace WebApiDemo.DataProvider
{
    public static class Widgets
    {
		public static string ConnectionString
		{
			get
			{
				return @"Server=DESKTOP-D2897OH\SQLEXPRESS;Database=Demo;Trusted_Connection=Yes;";
			}
		}

		/// <summary>
		/// Retrieve widget settings
		/// </summary>
		/// <param name="widgetId"></param>
		/// <returns></returns>
		public static List<WidgetSettingModel> GetSettings (int widgetId) {

			List<WidgetSettingModel> settings = null;

			try
			{
				// Connect to database
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						connection.Open();

						// Create command
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "sp_GetWidgetSettings";
						cmd.Parameters.Add(new SqlParameter("@WidgetID", widgetId));
						
						// Execute command
						var reader = cmd.ExecuteReader();
						if (reader.HasRows)
						{
							// Populate settings
							settings = new List<WidgetSettingModel>();
							while (reader.Read())
							{
								var setting = new WidgetSettingModel
								{
									SettingName = reader["SettingName"] as string,
									SettingValue = reader["SettingValue"] as string
								};

								settings.Add(setting);
							}
						}

						connection.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Database Error", ex);
			}

			return settings;
		}
	}
}
