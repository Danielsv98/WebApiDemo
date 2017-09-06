using System;
using System.Collections.Generic;

namespace WebApiDemo.Models
{
    public class WidgetModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public List<WidgetSettingModel> Settings { get; set; }
	}
}
