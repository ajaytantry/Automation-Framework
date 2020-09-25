using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Testproject;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;
using BoDi;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;

namespace Baseclass
{
    [Binding]
    public class Hooks 
    {
        public TestContext _testcontext;
        private readonly IObjectContainer _objectContainer;
        private IWebDriver _driver;
        public static ExtentTest feature;
        public static ExtentTest scenario;
        public static ExtentReports extent;


        public Hooks(TestContext testContext,IObjectContainer objectContainer)
        { 
            _testcontext = testContext;
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTest()
        {
            var htmlreporter = new ExtentHtmlReporter(@"C:\TFS\FrameworkTest\extent.html");
            htmlreporter.Config.Theme = Theme.Dark;
            htmlreporter.Config.DocumentTitle = "FrameworkTest";

            extent = new ExtentReports();
            extent.AttachReporter(htmlreporter);

        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            feature = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title); 
        }

        [BeforeScenario]
        public void NaviagteToGoogle()
        {
            _driver = new ChromeDriver();
            BaseClass.URL = _testcontext.Properties["webAppUrl"].ToString();
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            scenario = feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterStep]
        public static void AfterStep()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
            }
        }

        [AfterScenario]
        public void AfterFeature()
        {
            _driver.Quit();
        }

        [AfterTestRun]
        public static void AfterTest()
        {
            extent.Flush();
        }
    }
}
