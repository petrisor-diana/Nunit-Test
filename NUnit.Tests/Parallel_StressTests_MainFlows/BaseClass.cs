using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    public class BaseClass
    {
        public IWebDriver driver;
        public static ExtentReports extent = null;
        public static ExtentHtmlReporter htmlReport;
        public ExtentTest test = null;
        public List<string> listOfErrors = new List<string>();
        public CommonFunctionsUtility_StressTests commonFunctionsUtilities;

        [SetUp]
        public void ReportStart()
        {
            //create report 
            extent = new ExtentReports();
            htmlReport = new ExtentHtmlReporter(ConfigurationManager.AppSettings["ProjectPath"] + "\\ExtentReport\\MainFlows_Parallel\\reports.html");
            extent.AttachReporter(htmlReport);
        }

        public void OpenBrowser()
        {
            //launch browser

            //ChromeOptions options = new ChromeOptions();
            //options.AddArguments("no-sandbox");
            //options.AddArguments("disable-extensions");
            //options.PageLoadStrategy = PageLoadStrategy.Normal;
            //options.AddAdditionalCapability("useAutomationExtension", false);
            //    driver = new ChromeDriver(ConfigurationManager.AppSettings["ProjectPath"] + "\\bin\\Debug\\", options, TimeSpan.FromSeconds(90));
            driver = new ChromeDriver();
          driver.Manage().Window.Maximize();

            driver.Url =  ConfigurationManager.AppSettings["Url"];
        }

        public void CloseBrowser()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }

        [TearDown]
        public void ReportClose()
        {
            //Clear report
            extent.Flush();
        }
    }
}
