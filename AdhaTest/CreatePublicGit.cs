using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
namespace AdhaTest
{
    [Order(1)]
    [NonParallelizable]
    class CreatePublicGit
    {
        private IWebDriver driver;
        private IWebElement signButton, login_field, password,
            commit,btn_new,repository_name,btn_submit;

        private string username, pass, reponame;
          
        [SetUp]
        public void startBrowser()
        {
            username = ConfigurationSettings.AppSettings["username"];
            pass = ConfigurationSettings.AppSettings["password"];
            reponame = ConfigurationSettings.AppSettings["reponame"];

            driver = new ChromeDriver(); 
            driver.Manage().Window.Maximize();
            driver.Url = "https://github.com/";
        }

        [Test]
        public void CreatePublicGitTest()
        { 

            signButton = driver.FindElement(By.CssSelector("a.HeaderMenu-link.no-underline.mr-3"));
            if(signButton != null)
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

            btn_new = driver.FindElement(By.LinkText("New"));
            btn_new.Click();

            repository_name = driver.FindElement(By.Id("repository_name"));
            btn_submit = driver.FindElement(By.CssSelector("button.btn.btn-primary.first-in-line"));
            Assert.That(repository_name.Displayed,Is.True,"New Project page display");

            repository_name.SendKeys(reponame);
            btn_submit.Click();

            var succespage = driver.FindElement(By.CssSelector("span.input-group.width-full"));
            Assert.That(succespage.Displayed,Is.True,"new repo success created");
        } 
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
