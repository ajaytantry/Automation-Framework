using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testproject
{
    public class Pageobject : BaseClass
    {
        private readonly IWebDriver Driver;

        public Pageobject(IWebDriver webDriver) : base(webDriver)
        {
            Driver = webDriver;
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How=How.Name,Using = "q")]
        public IWebElement GoogleSearchBox {get;set;}

        [FindsBy(How = How.XPath, Using = "//div[@id='result-stats']")]
        public IWebElement ResultStats { get; set; }

        public void NavigateToGoogle()
        {
            NavigatetoURL(URL);
            WaitForElement(GoogleSearchBox);
            
        }

        public void SearchForKeyword(string keyword)
        {
            EnterTextinTextField(GoogleSearchBox,keyword);
            GoogleSearchBox.SendKeys(Keys.Enter);
        }        

        public void VerifyResults()
        {
            WaitForElement(ResultStats);
        }
    }
}
