using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.BaseTestss;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;


namespace NUnit.Tests_AdvisorFlows
{
    public class BaseClass
    {
        public IWebDriver driver;
        public static ExtentReports extent = null;
        public static ExtentHtmlReporter htmlReport;

        public CommonFunctionsUtilities commonFunctionsUtility;

        [SetUp]
        public void ReportStart()
        {
            //create report 
            extent = new ExtentReports();
            htmlReport = new ExtentHtmlReporter(ConfigurationManager.AppSettings["ProjectPath"] + "\\ExtentReport\\AdvisorFlows\\reports.html");
            extent.AttachReporter(htmlReport);
        }

        public void OpenBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("no-sandbox");
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ProjectPath"] + "\\bin\\Debug\\", options, TimeSpan.FromSeconds(90));
            driver.Manage().Window.Maximize();

            commonFunctionsUtility = new CommonFunctionsUtilities(driver, extent);

            driver.Url = ConfigurationManager.AppSettings["LoginUrl"];
            commonFunctionsUtility.Login();
            commonFunctionsUtility.LoginAsAdvisor();
        }
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [TearDown]
        public void ReportClose()
        {
            //Clear report
            extent.Flush();
        }
    }
}
