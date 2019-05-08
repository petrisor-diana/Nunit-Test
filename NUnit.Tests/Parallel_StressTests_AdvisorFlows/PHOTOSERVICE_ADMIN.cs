using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.BaseTestss;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable]
    public class PHOTOSERVICE_ADMINS : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void PHOTOSERVICE_ADMIN()
        {
            Assert.Ignore("test not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("PHOTOSERVICE_ADMIN").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                driver.Url = "http://acceptancepro.veneta.com/advisor-login-2018/nl/page/1136/?token=a46553a9ac1547e8af88174614149bf2&redirectTo=IPA";
                System.Threading.Thread.Sleep(3000);
                IWebElement rows = driver.FindElement(By.XPath("//tr[@class='row-details']/td/div/a"));
                System.Threading.Thread.Sleep(3000);
                rows.Click();

                commonFunctionsUtility.Click("btnPhsUnfinished");
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoadedXpath("//a[@id='btnPhsUnfinished' and @class='btn small-width secondary large btnMain open_register slide-active']");

                commonFunctionsUtility.Click("btnPhMeasurementRequest");
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoadedXpath("//a[@id='btnPhMeasurementRequest' and @class='btn primary extra-large btnMain open_register slide-active']");

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_lnkSendMeasurementRequest");

                //
                CloseBrowser();
                test.Log(Status.Pass, "Success");
            }
            catch (Exception e)
            {
                test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                test.Log(Status.Fail, e.ToString());
                ITakesScreenshot screenshot = driver as ITakesScreenshot;
                Screenshot screen = screenshot.GetScreenshot();
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen13.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen13.jpeg"));
                throw;
            }
            finally
            {
                if (driver != null)
                {
                    CloseBrowser();
                }
            }
        }
    }
}
