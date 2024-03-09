using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.UITests
{
	internal class HomePage
	{
		private readonly IWebDriver _Driver;
		public const string PAGE_URL = "https://anotherusercalledh.github.io/Demo-Website/index.html";

		public HomePage(IWebDriver driver)
		{
			this._Driver = driver;
		}

		private IWebElement OuterContainer
		{
			get
			{
				return _Driver.FindElements(By.ClassName("outer-container"))[0];
			}
		}

		private IWebElement NeonPulseAnchor
		{
			get
			{
				return OuterContainer.FindElements(By.TagName("a"))[0];
			}
		}

		

		public void LoadPage()
		{
			_Driver.Navigate().GoToUrl(PAGE_URL);

			bool pageHasLoaded = (_Driver.Url == PAGE_URL);
			if (!pageHasLoaded)
			{
				throw new Exception($"Failed to load page. Found page {_Driver.Url}");
			}
		}

		public string ClickNeonPulse()
		{
			NeonPulseAnchor.Click();
			return _Driver.Url;
		}
	}
}
