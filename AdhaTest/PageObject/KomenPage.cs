using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest.PageObject
{
    public class KomenPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public KomenPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[data-qa-id='content']")]
        private IList<IWebElement> LastKomen;

        public string lastKomen()
        {
            //wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-qa-id='content']div:first-child")));
            var list = LastKomen.ToList();

            var data = list.FirstOrDefault().GetAttribute("textContent");
             return data;
        }
    }
}