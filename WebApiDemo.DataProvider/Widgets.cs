using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiDemo.Models;

namespace WebApiDemo.DataProvider
{
	/// <summary>
	/// Use ADO.NET to retrieve data from database
	/// </summary>
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
						// Create command
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "sp_GetWidgetSettings";
						cmd.Parameters.Add(new SqlParameter("@WidgetID", widgetId));

						connection.Open();
						
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

		/// <summary>
		/// Delete widget settings
		/// </summary>
		/// <param name="widgetId"></param>
		/// <returns></returns>
		public static void DeleteSettings (int widgetId)
		{
			try
			{
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "sp_DeleteWidgetSettings";
						cmd.Parameters.Add(new SqlParameter("@WidgetID", widgetId));

						connection.Open();
						cmd.ExecuteNonQuery();
						connection.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Database Error", ex);
			}
		}

		/// <summary>
		/// Insert widget settings
		/// </summary>
		/// <param name="widgetId"></param>
		/// <returns></returns>
		public static void InsertSettings(string settingName, string settingValue, int widgetId)
		{

			try
			{
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "sp_InsertWidgetSetting";
						cmd.Parameters.Add(new SqlParameter("@SettingName", settingName));
						cmd.Parameters.Add(new SqlParameter("@SettingValue", settingValue));
						cmd.Parameters.Add(new SqlParameter("@WidgetID", widgetId));

						connection.Open();
						cmd.ExecuteNonQuery();
						connection.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Database Error", ex);
			}
		}

		/// <summary>
		/// Update widget settings
		/// </summary>
		/// <param name="widgetId"></param>
		/// <returns></returns>
		public static void UpdateSetting(string settingName, string settingValue, int widgetId)
		{
			try
			{
				using (var connection = new SqlConnection(ConnectionString))
				{
					using (SqlCommand cmd = connection.CreateCommand())
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.CommandText = "sp_UpdateWidgetSetting";
						cmd.Parameters.Add(new SqlParameter("@SettingName", settingName));
						cmd.Parameters.Add(new SqlParameter("@SettingValue", settingValue));
						cmd.Parameters.Add(new SqlParameter("@WidgetID", widgetId));

						connection.Open();
						cmd.ExecuteNonQuery();
						connection.Close();
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Database Error", ex);
			}
		}

	}
}
