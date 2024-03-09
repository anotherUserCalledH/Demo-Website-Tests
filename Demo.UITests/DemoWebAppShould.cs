

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Drawing;


namespace Demo.UITests
{
	internal class DemoWebAppShould
	{
		private static readonly string workingDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

		[Test]
		public void LoadHomePage()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				HomePage homePOM = new HomePage(driver);
				homePOM.LoadPage();
			}
		}

		[Test]
		public void NavigateFromHomeToNeonPage()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				HomePage homePOM = new HomePage(driver);
				homePOM.LoadPage();

				string newURL = homePOM.ClickNeonPulse();
				Assert.That(newURL, Is.EqualTo(NeonPulsePage.PAGE_URL));
			}
		}

		[Test]
		public void LoadNeonPage()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				NeonPulsePage neonPulsePOM = new NeonPulsePage(driver);
				neonPulsePOM.LoadPage();
			}
		}

		[Test]
		public void NavigateFromNeonToHomePage()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				NeonPulsePage neonPulsePOM = new NeonPulsePage(driver);
				neonPulsePOM.LoadPage();

				string newURL = neonPulsePOM.ClickCloseButton();
				Assert.That(newURL, Is.EqualTo(HomePage.PAGE_URL));
			}
		}

		[Test]
		public void GoFullScreen()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				NeonPulsePage neonPulsePOM = new NeonPulsePage(driver);
				neonPulsePOM.LoadPage();
				Thread.Sleep(100);
				neonPulsePOM.ClickMaximiseButton();

				Size canvasSize = neonPulsePOM.GetCanvasDimensions();
				Size viewportSize = neonPulsePOM.GetViewportDimensions();

				int dimensions = (viewportSize.Width < viewportSize.Height ) ? viewportSize.Width : viewportSize.Height;
				Assert.That(canvasSize.Width, Is.EqualTo(dimensions));
			}
		}

		[Test]
		public void UploadAndPlaySong()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				NeonPulsePage neonPulsePOM = new NeonPulsePage(driver);
				neonPulsePOM.LoadPage();

				string songFilePath = Path.Combine(workingDirectory, "Resources", "TestSongWBeat.wav");
				Console.WriteLine(songFilePath);

				neonPulsePOM.UploadSong(songFilePath);
				neonPulsePOM.ClickPlayButton();
				Assert.That(neonPulsePOM.CheckAudioElementExists(), Is.True);
			}
		}

		[Test]
		public void ShowErrorMessage()
		{
			using (IWebDriver driver = new EdgeDriver())
			{
				NeonPulsePage neonPulsePOM = new NeonPulsePage(driver);
				neonPulsePOM.LoadPage();
				string songFilePath = Path.Combine(workingDirectory, "Resources", "TestSongWOBeat.wav");

				neonPulsePOM.UploadSong(songFilePath);
				neonPulsePOM.ClickPlayButton();

				Assert.That(neonPulsePOM.CheckErrorMessageShown(), Is.True);
			}
		}
	}
}
