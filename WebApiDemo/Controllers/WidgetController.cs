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

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    public class WidgetController : Controller
    {
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
				var settings = Widgets.GetSettings(id);
				return new JsonResult(settings);
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
        }

		// Add parameters to url when they are required. for example (?name=...&value=...)
        [HttpPost("{id}/settings/{name}/{value}")]	
		public IActionResult InsertSettings(int id, string name, string value)
		{
			try
			{
				Widgets.InsertSettings(name, value, id);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("{id}/settings")]
        public IActionResult Put(int id, [FromBody]WidgetSettingModel setting)
        {
			try
			{
				if (setting == null)
					return BadRequest();

				Widgets.UpdateSetting(setting.SettingName, setting.SettingValue, id);

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
				Widgets.DeleteSettings(id);

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500);
			}
		}
    }
}
