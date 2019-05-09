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
        public CommonFunctionsUtility_StressTests commonFunctionsUtilities;

        [SetUp]
        public void OpenBrowser()
        {
            //launch browser

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("no-sandbox");
            options.AddArguments("disable-extensions");
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            options.AddAdditionalCapability("useAutomationExtension", false);
            driver = new ChromeDriver(ConfigurationManager.AppSettings["ProjectPath"] + "\\bin\\Debug\\", options, TimeSpan.FromSeconds(90));

            driver.Manage().Window.Maximize();

            driver.Url =  ConfigurationManager.AppSettings["Url"];
        }

        [TearDown]
        public void CloseBrowser()
        {
            if (driver != null)
            {
      
                driver.Quit();
            }
        }
     
    }
}
