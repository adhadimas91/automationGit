using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace AdhaTest.PageObject
{
    class HomePage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "[data-qa-id='btn-login-google']")]
        private IWebElement GoogleBtnLogin;

        public void click()
        {
            GoogleBtnLogin.Click();
        }

        [FindsBy(How = How.CssSelector, Using = "[data-qa-id='username']")]
        private IWebElement Username;

        public string username() {  
        WebDriverWait waits = new WebDriverWait(driver, TimeSpan.FromSeconds(30000)); // 5 seconds timeout
        waits.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-qa-id='username']")));
   
            return Username.GetAttribute("title");
        }

        [FindsBy(How = How.CssSelector, Using = "div[data-qa-id='trending-story-item']:nth-child(1)")]
        private IWebElement _firstTending;

        public void FirstTendingClick()
        { 
            _firstTending.Click();
        }
    }
}
