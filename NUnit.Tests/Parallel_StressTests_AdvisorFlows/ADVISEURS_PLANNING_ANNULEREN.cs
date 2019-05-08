using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ANNULERENADVISEURS_PLANNING : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void ANNULEREN_ADVISEURS_PLANNING()
        {
            Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("ANNULEREN_ADVISEURS_PLANNING").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                //
               // driver.Url = "http://acceptance.veneta.com/planning-dashboard/nl/page/1100/";
                IList<IWebElement> rows = driver.FindElements(By.XPath("//tr[@class='row-details']/td/div/a"));
                rows[1].Click();

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnCancel");
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@class='custom_radio']/label[@id='cphContent_cphContentMain_ctl00_ctl00_lblCancelOrdersOnline']")).Click();
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnCancelYes");

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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen17.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen17.jpeg"));
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
