using Baseclass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace Testproject
{
    [Binding]
    public class NavigateSteps
    {
        private readonly IWebDriver _driver;
        private readonly Pageobject pageobject;
        public NavigateSteps(IWebDriver webDriver)
        {
            _driver = webDriver;
            pageobject = new Pageobject(_driver);
        }
        

        [Given(@"User navigate to google")]
        public void GivenUserNavigateToGoogle()
        {
            pageobject.NavigateToGoogle();
        }
        
        [When(@"User enters search query and searches (.*)")]
        public void WhenUserEntersSearchQueryAndSearches(string keyword)
        {
            pageobject.SearchForKeyword(keyword);
        }
        
        [Then(@"Results are loaded")]
        public void ThenResultsAreLoaded()
        {
            pageobject.VerifyResults();
        }
    }
}
