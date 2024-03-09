using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.UITests
{
	internal class NeonPulsePage
	{
		private readonly IWebDriver _Driver;
		public const string PAGE_URL = "https://anotherusercalledh.github.io/Demo-Website/page2.html";

		private IWebElement NavBar
		{
			get
			{
				return _Driver.FindElements(By.TagName("nav"))[0];
			}
		}

		private IWebElement MaximiseButton
		{
			get
			{
				return NavBar.FindElements(By.TagName("a"))[1];
			}
		}
		private IWebElement CloseButton
		{
			get
			{
				return NavBar.FindElements(By.TagName("a"))[2];
			}
		}

		private IWebElement SongFileInput
		{
			get
			{
				return _Driver.FindElement(By.Id("songFileInput"));
			}
		}

		private IWebElement PlayButton
		{
			get
			{
				return _Driver.FindElement(By.TagName("Button")).FindElement(By.XPath("//*[text()='Play']"));
			}
		}

		public IWebElement InfoMessage
		{
			get
			{
				return _Driver.FindElement(By.Id("info-message"));
			}
		}

		public NeonPulsePage(IWebDriver driver)
		{
			this._Driver = driver;
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

		public Size GetCanvasDimensions()
		{
			IWebElement body = _Driver.FindElements(By.TagName("body"))[0];

			WebDriverWait waitForFullScreen = new WebDriverWait(_Driver, TimeSpan.FromSeconds(2));
			waitForFullScreen.Until(driver => _Driver.FindElements(By.TagName("body"))[0].FindElements(By.TagName("canvas")).Count > 0);

			IWebElement canvas = body.FindElements(By.TagName("canvas"))[0];

			return canvas.Size;
		}

		public Size GetViewportDimensions()
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)_Driver;
			string script = "return document.documentElement.clientWidth;";
			int vpWidth = Convert.ToInt32(js.ExecuteScript(script));
			script = "return document.documentElement.clientHeight;";
			int vpHeight = Convert.ToInt32(js.ExecuteScript(script));

			//Driver.Manage().Window.Size; this size is not accurate

			return new Size(vpWidth, vpHeight);
		}

		public void ClickMaximiseButton()
		{
			MaximiseButton.Click();
		}

		public string ClickCloseButton()
		{
			CloseButton.Click();
			return _Driver.Url;
		}

		public void UploadSong(String filePath)
		{
			SongFileInput.SendKeys(filePath);
		}

		public void ClickPlayButton()
		{
			PlayButton.Click();
		}
		public bool CheckAudioElementExists()
		{
			WebDriverWait waitForAudio = new WebDriverWait(_Driver, TimeSpan.FromSeconds(2));
			bool audioFound = waitForAudio.Until(driver => driver.FindElements(By.TagName("Audio")).Count > 0);

			return audioFound;
		}

		public bool CheckErrorMessageShown()
		{
			WebDriverWait waitForError = new WebDriverWait(_Driver, TimeSpan.FromSeconds(2));
			bool errorMessageShown = waitForError.Until(driver => InfoMessage.Text.Contains("Error in beat detection"));
			return errorMessageShown;
		}
	}
}
