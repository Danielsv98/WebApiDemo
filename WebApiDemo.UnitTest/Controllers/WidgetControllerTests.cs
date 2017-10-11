using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Linq;
using System.Collections.Generic;
using WebApiDemo.Configuration;
using WebApiDemo.Controllers;
using WebApiDemo.DataProvider;
using WebApiDemo.DataProvider.Interfaces;
using WebApiDemo.Models;
using Xunit;

namespace WebApiDemo.UnitTest.Controllers
{
    public class WidgetControllerTests
    {
		const int WidgetID = 1;

		private Mock<IWidgets> widgetsDataProvider;
		private WidgetController controller;

		public WidgetControllerTests()
		{
			var settings = new Mock<IOptions<WebApiSettings>>();
			settings.Setup(x => x.Value).Returns(new WebApiSettings());

			widgetsDataProvider = new Mock<IWidgets>();
			controller = new WidgetController(settings.Object, widgetsDataProvider.Object);
		}

		[Fact]
        public void If_Get_Then_ReturnsMessage()
        {
			// Arrange

			// Act
			var result = controller.Get();

			// Assert
			Assert.Equal(result, "This is the Demo Web API");
        }

		[Fact]
		public void If_GetSettings_Then_ReturnsSettings()
		{
			// Arrange
			var widgetSettings = new List<WidgetSettingModel>() {
				new WidgetSettingModel { SettingName = "setting1", SettingValue = "value1"  },
				new WidgetSettingModel { SettingName = "setting2", SettingValue = "value2"  }
			};

			widgetsDataProvider.Setup(x => x.GetSettings(It.IsAny<int>())).Returns(widgetSettings);

			// Act
			var result = controller.GetSettings(WidgetID);

			// Assert
			Assert.NotNull(result);
			Assert.IsType(typeof(JsonResult), result);

			var jsonResult = result as JsonResult;
			Assert.NotNull(jsonResult.Value);
			Assert.IsType(typeof(List<WidgetSettingModel>), jsonResult.Value);

			var settingsResults = jsonResult.Value as List<WidgetSettingModel>;
			Assert.NotNull(settingsResults);
			Assert.Equal(settingsResults.Count, widgetSettings.Count);
		}

		[Fact]
		public void If_GetSettings_When_Error_Then_Returns500()
		{
			// Arrange
			widgetsDataProvider.Setup(x => x.GetSettings(It.IsAny<int>())).Throws(new Exception());

			// Act
			var result = controller.GetSettings(WidgetID);

			// Assert
			Assert.NotNull(result);
			Assert.IsType(typeof(StatusCodeResult), result);

			var statusResult = result as StatusCodeResult;
			Assert.Equal(statusResult.StatusCode, 500);
		}
	}
}
