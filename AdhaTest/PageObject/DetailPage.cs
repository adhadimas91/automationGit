using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest.PageObject
{
    class DetailPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public DetailPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "span[data-qa-id='story-title']")]
        private IWebElement TitleNews;
         
        public string titleNews()
        {
            return TitleNews.Text;
        }
 
    }
}
