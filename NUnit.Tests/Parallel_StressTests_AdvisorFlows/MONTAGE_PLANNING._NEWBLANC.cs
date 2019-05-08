using NUnit.Framework;
using OpenQA.Selenium;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class NEWBLANC_MONTAGE__PLANNING : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void MONTAGE_PLANNING_BLANC()
        {
            Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("MONTAGE_PLANNING_BLANC").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                driver.Url = "http://acceptance.veneta.com/assembly-planning-dashboard/nl/page/1190/";

                //
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_hyNewRequest");
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtFirstname", "test-user");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtLastname", "test-user");
                commonFunctionsUtility.AddInput("txtPostcode", "9001AS");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtHouseNumber", "20");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtEmail", "test@test.com");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtPhone", "0662403123");
                commonFunctionsUtility.Dropdown("cphContent_cphContentMain_ctl00_ctl00_ddlAdvisor", "104");

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnScheduleAppointment");
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@class='custom_radio']/label[@id='cphContent_cphContentMain_ctl00_ctl00_lblPlannedWithCustomer']")).Click();
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_LinkButton1");

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnBack");
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoaded("cphContent_cphContentMain_ctl00_ctl00_hyNewRequest");

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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen19.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen19.jpeg"));
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
