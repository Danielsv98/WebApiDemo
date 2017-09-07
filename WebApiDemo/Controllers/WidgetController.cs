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
        public HttpResponseMessage GetSettings(int id)
        {
			try
			{
				var settings = Widgets.GetSettings(id);
				var jsonResult = JsonConvert.SerializeObject(settings);

				var response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new StringContent(jsonResult, Encoding.UTF8, "application/json");

				return response;
			}
			catch (Exception ex)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
        }

		// Add parameters to url when they are required. for example (?name=...&value=...)
        [HttpPost("{id}/settings/{name}/{value}")]	
		public HttpResponseMessage InsertSettings(int id, string name, string value)
		{
			try
			{
				Widgets.InsertSettings(name, value, id);

				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}

		[HttpPut("{id}/settings")]
        public HttpResponseMessage Put(int id, [FromBody]WidgetSettingModel setting)
        {
			try
			{
				if (setting == null)
					return new HttpResponseMessage(HttpStatusCode.BadRequest);

				Widgets.UpdateSetting(setting.SettingName, setting.SettingValue, id);

				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			catch (Exception ex) {
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}

		[HttpDelete("{id}/settings")]
        public HttpResponseMessage DeleteSettings(int id)
        {
			try
			{
				Widgets.DeleteSettings(id);

				return new HttpResponseMessage(HttpStatusCode.OK);
			}
			catch (Exception ex)
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
        }
    }
}
