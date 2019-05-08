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
    [Parallelizable(ParallelScope.Fixtures)]
    public class ADVISEURS_PLANNINGBLANC : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void ADVISEURS_PLANNING_BLANC()
        {
            //Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("ADVISEURS_PLANNING_BLANC").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                driver.Url = "http://acceptance.veneta.com/planning-dashboard/nl/page/1100/";
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnNewRequest");

                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtFirstname", "test");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtLastname", "test");
                commonFunctionsUtility.AddInput("txtPostcode", "9001AS");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtHouseNumber", "20");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtStreet", "test");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtStreet", "test");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtEmail", "test@test.com");
                commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtAppointmentDate", "02-05-2029");
                driver.FindElement(By.XPath("//div[@class='product-container chekcbox-filter']")).Click();

                //
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnScheduleAppointment");
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@class='custom_radio']/label[@id='cphContent_cphContentMain_ctl00_ctl00_lblPlannedWithCustomer']")).Click();
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_LinkButton1");

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnBack");
                commonFunctionsUtility.CheckIfLoaded("SSBListContainer");

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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen16.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath("D:\\Veneta\\Veneta\\NUnit.Tests\\Screenshot\\screen16.jpeg"));
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
