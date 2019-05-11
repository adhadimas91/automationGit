﻿using System;
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
    class DeleteGit
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
            reponame = ConfigurationSettings.AppSettings["reponame"];

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://github.com/";
        }

        [Test]
        public void DeleteGitTest()
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

            //find repo in list
            var repos = driver.FindElement(By.CssSelector("div:nth-of-type(4) > div > aside > div:nth-of-type(2) > div > div > ul"));
            List<IWebElement> reposList = repos.FindElements(By.TagName("li")).ToList();
            foreach (var li in reposList)
            {
                if (li.Text.Contains(reponame))
                {
                    li.Click();
                    break;
                }
            }

            var fieldclone = driver.FindElement(By.CssSelector("span.input-group.width-full"));
            Assert.That(fieldclone.Displayed, Is.True, "repo page info has display");

            var setting = driver.FindElement(By.CssSelector("svg.octicon.octicon-gear"));
            setting.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            var renamefield = wait.Until(driver => driver.FindElement(By.Name("new_name")));

            Assert.That(renamefield.Displayed, Is.True, "Repo setting has open");
            
            var btndelete = driver.FindElement(By.CssSelector("div#options_bucket > div:nth-of-type(9) > ul > li:nth-of-type(4) > details > summary"));
            btndelete.Click();

            var fieldelete = driver.FindElement(By.CssSelector("div#options_bucket > div:nth-of-type(9) > ul > li:nth-of-type(4) > details > details-dialog > div:nth-of-type(3) > form > p > input"));
            fieldelete.SendKeys($"{username}/{reponame}");

           var btndeleted = driver.FindElement(By.CssSelector(
                "div#options_bucket > div:nth-of-type(9) > ul > li:nth-of-type(4) > details > details-dialog > div:nth-of-type(3) > form > button"));
            btndeleted.Click();
        }

        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
