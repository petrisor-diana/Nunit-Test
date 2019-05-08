using NUnit.Framework;
using OpenQA.Selenium;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class MONTAGE__PLANNING : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void MONTAGE_PLANNING()
        {
            Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("MONTAGE_PLANNING").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                driver.Url = "http://acceptance.veneta.com/assembly-planning-dashboard/nl/page/1190/";

                //
                commonFunctionsUtility.AddInput("tbOrderNumber", "W391580");
                commonFunctionsUtility.Click("btnAssemblyReqSearch");
                commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='filterItem form-group filled']");

                System.Threading.Thread.Sleep(6000);
                driver.FindElement(By.XPath("//tr[@class='row-details']/td/div/a")).Click();
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnScheduleAppointment");
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@class='custom_radio']/label[@id='cphContent_cphContentMain_ctl00_ctl00_lblPlannedWithCustomer']")).Click();
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_LinkButton1");
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnNextAppointment");
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnBack");
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnDashboardNo");
                System.Threading.Thread.Sleep(3000);

                CloseBrowser();
                test.Log(Status.Pass, "Success");
            }
            catch (Exception e)
            {
                test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                test.Log(Status.Fail, e.ToString());
                ITakesScreenshot screenshot = driver as ITakesScreenshot;
                Screenshot screen = screenshot.GetScreenshot();
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen18.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen18.jpeg"));
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
