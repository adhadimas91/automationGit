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
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30000));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "span[data-qa-id='story-title']")]
        private IWebElement TitleNews;
         
        public string titleNews()
        { 
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span[data-qa-id='story-title']"))); 
            return TitleNews.Text;
        }

        [FindsBy(How = How.CssSelector, Using = "div[data-qa-id='input-comment'] div > div")]
        private IWebElement Comment;

        public void clickcomment()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[data-qa-id='input-comment'] div > div")));  
            Comment.Click();
        }
        public void Writecomment(string data)
        {
            Comment.SendKeys(data);
        }
         
        [FindsBy(How = How.CssSelector, Using = "button[data-qa-id='input-comment']")]
        private IWebElement BtnComment;

        public void btnComment()
        { 
            BtnComment.Click();
        }
    }
}
