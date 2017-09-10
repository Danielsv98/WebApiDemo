using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.DataProvider;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using WebApiDemo.Configuration;
using Microsoft.Extensions.Options;
using WebApiDemo.DataProvider.Interfaces;

namespace WebApiDemo.Controllers
{
	[Route("api/[controller]")]
    public class WidgetController : Controller
    {
		#region Members

		private WebApiSettings _settings;
		private IWidgets _widgets;

		#endregion

		#region Constructors

		public WidgetController(IOptions<WebApiSettings> settings, IWidgets widgets)
		{
			_settings = settings.Value;
			_widgets = widgets;
			_widgets.ConnectionString = _settings.ConnectionString;
		}

		#endregion

		[HttpGet]
        public string Get()
        {
            return "This is the Demo Web API";
        }

        [HttpGet("{id}/settings")]
        public IActionResult GetSettings(int id)
        {
			try
			{
				var settings = _widgets.GetSettings(id);

				return new JsonResult(settings);
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
        }

		// Add parameters to url when they are required. for example (?name=...&value=...)
		[HttpPost("{id}/settings")]
		public IActionResult InsertSettings(int id, [FromBody]WidgetSettingModel[] settings)
		{
			try
			{
				if (settings == null || settings.Length == 0) 
					return BadRequest();

				for (var i = 0; i < settings.Length; i++)
				{
					_widgets.InsertSettings(settings[i].SettingName, settings[i].SettingValue, id);
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}

		// Add parameters to url when they are required. for example (?name=...&value=...)
		[HttpPut("{id}/settings")]
		public IActionResult UpdateSettings(int id, [FromBody]WidgetSettingModel[] settings)
		{
			try
			{
				if (settings != null)
				{
					_widgets.DeleteSettings(id);

					//Transanctions

					for (var i = 0; i < settings.Length; i++)
					{
						_widgets.InsertSettings(settings[i].SettingName, settings[i].SettingValue, id);
					}
				}

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("{id}/setting")]
        public IActionResult Put(int id, [FromBody]WidgetSettingModel setting)
        {
			try
			{
				if (setting == null)
					return BadRequest();

				_widgets.UpdateSetting(setting.SettingName, setting.SettingValue, id);

				return Ok();
			}
			catch (Exception ex) {
				return StatusCode(500);
			}
		}

		[HttpDelete("{id}/settings")]
        public IActionResult DeleteSettings(int id)
        {
			try
			{
				_widgets.DeleteSettings(id);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}
    }
}
