using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest
{
    [Order(4)]
    [NonParallelizable]
    class DeleteGist
    {
        private IWebDriver driver;
        private IWebElement signButton, login_field, password,
            commit, btn_new, repository_name, btn_submit;

        private string username, pass, gistname;

        [SetUp]
        public void startBrowser()
        {
            username = ConfigurationSettings.AppSettings["username"];
            pass = ConfigurationSettings.AppSettings["password"];
            gistname = "mygist";

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://gist.github.com/discover";
        }

        [Test]
        public void DeleteGistTest()
        {

            signButton = driver.FindElement(By.CssSelector("a.HeaderMenu-link.no-underline.mr-3"));
            if (signButton != null)
                signButton.Click();
            login_field = driver.FindElement(By.Id("login_field"));
            password = driver.FindElement(By.Id("password"));
            commit = driver.FindElement(By.Name("commit"));
            Assert.That(login_field, Is.Not.Null, "page login is display");

            login_field.SendKeys(username);
            password.SendKeys(pass);
            commit.Click();
            var iconBell = driver.FindElement(By.CssSelector("svg.octicon.octicon-bell > path"));
            Assert.That(iconBell.Displayed, Is.True, "dashboard is display");

            var add = driver.FindElement(By.CssSelector("svg.octicon.octicon-plus > path"));
            add.Click();

            //find gist in list
            var repos = driver.FindElement(By.CssSelector("main#gist-pjax-container > div > div > div > ul "));
            List<IWebElement> reposList = repos.FindElements(By.TagName("li")).ToList();
            foreach (var li in reposList)
            {
                if (li.Text.Contains(gistname))
                {
                    li.Click();
                    break;
                }
            }

            //click delete button
           var btndelete = driver.FindElement(By.CssSelector("button.btn.btn-sm.btn-danger"));
            btndelete.Click(); 
            //click ok alert
            driver.SwitchTo().Alert().Accept();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var deleteMassage = wait.Until(driver => driver.FindElement(By.CssSelector("div.flash.flash-full.flash-notice")));
            Assert.That(deleteMassage.Text,Does.Contain("Gist deleted successfully."),"delete gist success");

        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
