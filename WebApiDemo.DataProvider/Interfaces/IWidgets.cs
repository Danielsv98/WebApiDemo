using System;
using System.Collections.Generic;
using System.Text;
using WebApiDemo.Models;

namespace WebApiDemo.DataProvider.Interfaces
{
    public interface IWidgets
    {
		string ConnectionString { get; set; }

		List<WidgetSettingModel> GetSettings(int widgetId);
		void DeleteSettings(int widgetId);
		void InsertSettings(string settingName, string settingValue, int widgetId);
		void UpdateSetting(string settingName, string settingValue, int widgetId);
	}
}
