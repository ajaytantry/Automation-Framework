using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Testproject
{
    public class BaseClass
    {
        private readonly IWebDriver driver;
        public WebDriverWait waitForElement;
        public BaseClass(IWebDriver _webDriver)
        {
            driver = _webDriver;
            waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public static string URL;        

        public void NavigatetoURL(String URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public void WaitForElement(IWebElement webElement)
        {            
            waitForElement.Until(ExpectedConditions.ElementToBeClickable(webElement));
        }

        public void EnterTextinTextField(IWebElement webElement, string texttobesearched)
        {
            webElement.SendKeys(texttobesearched);
        }
    }
}
