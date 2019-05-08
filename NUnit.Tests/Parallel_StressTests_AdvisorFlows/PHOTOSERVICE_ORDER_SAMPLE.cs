using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable]
    public class PHOTOSERVICE_ORDERSAMPLE : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void PHOTOSERVICE_ORDER_SAMPLE()
        {
            Assert.Ignore("test not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("PHOTOSERVICE_ORDER_SAMPLE").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoadedXpath("//ul[@class='nav nav-pills  table-navigation pull-left']//li[@class=' carousel-cell item']");
                driver.FindElement(By.XPath("//ul[@class='nav nav-pills  table-navigation pull-left']//li[@class=' carousel-cell item']")).Click();
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='tab-pane fade active in']");

                IWebElement row = driver.FindElement(By.XPath("//div[@class='photoservice-progress-requests photoservice-grid']/table/tbody/tr/td/div/a"));
                row.Click();

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_lnkOrderColorSample");
                System.Threading.Thread.Sleep(6000);

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl02_rptSamplePreshopItems_samplePreshopMidItem_2");
                commonFunctionsUtility.CheckIfLoaded("ProductListerTop29");
                System.Threading.Thread.Sleep(6000);

                IList<IWebElement> list = driver.FindElements(By.XPath("//ul[@id='ProductListerTop29']/li"));
                list[2].Click();
                list[5].Click();

                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl01_rptGroupListAndValues_btnNextTop_4");
                commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='sampleList cookies slide-active']");
                System.Threading.Thread.Sleep(3000);

                driver.FindElement(By.XPath("//a[@class='btn primary  extra-large ']")).Click();
                System.Threading.Thread.Sleep(3000);

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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen15.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen15.jpeg"));
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
