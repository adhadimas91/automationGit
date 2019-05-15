using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest
{
    [Order(1)]
    [NonParallelizable]
    class CreateGist
    {
        private IWebDriver driver;
        private IWebElement signButton, login_field, password,
            commit,btn_new,repository_name,btn_submit;

        private string username, pass, gistdesc, gistfile;
        
          
        [SetUp]
        public void startBrowser()
        {
            username = ConfigurationSettings.AppSettings["username"];
            pass = ConfigurationSettings.AppSettings["password"];
            gistdesc = ConfigurationSettings.AppSettings["gistdesc"];
            gistfile = ConfigurationSettings.AppSettings["gistfile"];
            driver = new ChromeDriver(); 
            driver.Manage().Window.Maximize();
            driver.Url = "https://gist.github.com/discover";
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
            Assert.That(iconBell.Displayed, Is.True, "gist home is display");

            //step click add
            var add = driver.FindElement(By.CssSelector("svg.octicon.octicon-plus > path"));
            add.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var desc = wait.Until(driver => driver.FindElement(By.CssSelector("input.form-control.input-block.input-contrast"))); 
            desc.SendKeys(gistdesc);
            var filename = driver.FindElement(By.CssSelector("div#gists > div:nth-of-type(2) > div > div > div > input:nth-of-type(2)"));
            filename.SendKeys(gistfile);
             
            //todo still cannot fill the content when creating gist
            var ccc = driver.FindElement(By.CssSelector("div#gists > div:nth-of-type(2) > div > div:nth-of-type(2) > div > div:nth-of-type(5) > div > div > div > div > div:nth-of-type(5) > div > pre"));
            ccc.SendKeys("ddd");

        } 
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
