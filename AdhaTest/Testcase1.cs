﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Threading;
using AdhaTest.PageObject;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest
{
    
    [Order(1)]
    [NonParallelizable]
    public class Testcase1
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
            driver = new ChromeDriver(); 
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30000);
            driver.Url = "https://kumparan.com";
        }

        [Test]
        public void TestCase()
        {
            Console.WriteLine("1. tekan tombol login google");
            HomePage home = new HomePage(driver);
            home.click(); 
            var parentWindow = driver.CurrentWindowHandle;
            var googletab = driver.WindowHandles;
            foreach (var next_tab in googletab)
            {
                if (next_tab != parentWindow)
                {
                    Console.WriteLine("2. Isi email dan password");
                    driver.SwitchTo().Window(next_tab);
                    WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
                    wait2.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("input[name='identifier'][type='email']"))).SendKeys("dimasadha91@gmail.com");
                    var next = driver.FindElement(By.Id("identifierNext"));
                    next.Click();
                     
                    var pass = new WebDriverWait(driver, TimeSpan.FromSeconds(30000)).Until(ExpectedConditions.ElementToBeClickable((By.CssSelector("input[name='password'][type='password']"))));
                    pass.SendKeys("7471063kartika");
                    next = driver.FindElement(By.Id("passwordNext"));
                    next.Click(); 
                }
            }


            driver.SwitchTo().Window(parentWindow);
            home = new HomePage(driver);
            var username = home.username();
            Assert.That(username,Is.Not.Empty,"user berhasil login");

            Console.WriteLine("3. Klik satu berita trending");
            home.FirstTendingClick();

            DetailPage detail = new DetailPage(driver);
            var title = detail.titleNews();
            Assert.That(title, Is.Not.Empty, "halaman detail berita berhasil terbuka");

            Console.WriteLine("4. Isi kolum komentar ");
            Console.WriteLine("5. Tekan tombol submit ");
            detail.clickcomment(); 
            detail.Writecomment("test automation");
            detail.btnComment();
            KomenPage komen = new KomenPage(driver);
            var komenNews = komen.lastKomen();
            Assert.That(komenNews, Does.Contain("test automation"), "5. komen berhasil");

        } 
        [TearDown]
        public void closeBrowser()
        {
            driver.Close();
        }
    }
}
