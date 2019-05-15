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
    [Order(2)]
    [NonParallelizable]
    class ListGist
    {
        private IWebDriver driver;
        private IWebElement signButton, login_field, password,
            commit, btn_new, repository_name, btn_submit;

        private string username, pass, reponame;

        [SetUp]
        public void startBrowser()
        {
            username = ConfigurationSettings.AppSettings["username"];
            pass = ConfigurationSettings.AppSettings["password"]; 

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://gist.github.com/discover";
        }

        [Test]
        public void ListGistTest()
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

            //click link all gist
            var linkAll = driver.FindElement(By.CssSelector("main#gist-pjax-container > div > div > div > ul > li > a"));
            linkAll.Click();

            //find gist in list 
            var reposList = driver.FindElements(By.CssSelector("div.gist-snippet-meta.d-inline-block.width-full > div.float-left > div.d-inline-block span.f6.text-gray"));
            List<string> dataGist = new List<string>();
            foreach (var li in reposList)
            {
                dataGist.Add(li.Text);
                Console.WriteLine(li.Text);
            }
            
            Assert.That(dataGist, Is.Not.Null.Or.Empty, $"{dataGist} data in list");
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
