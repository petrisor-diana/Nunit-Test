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
    public class Order_Bevestiging_Versturen : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void Order_Bevestiging_versturen()
        {
            //Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("Order_Bevestiging_versturen").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                //
                commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='active item']");
                commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='tab-pane fade in active']");

                driver.FindElement(By.CssSelector("ul.nav.nav-pills.table-navigation.pull-left li:nth-child(3)")).Click();
                commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='carousel-cell item active']");
                commonFunctionsUtility.CheckIfLoadedXpath("//div[@id='menu2' and @class='tab-pane fade active in']");

                //
                IList<IWebElement> rows = driver.FindElements(By.XPath("//div[@class='row-expand clearfix']/div/div[@class='row-description']/div/div[@class='expand-row']"));
                //Random randomElem = new Random();
                System.Threading.Thread.Sleep(3000);
                //int num = randomElem.Next(1, rows.Count);
                rows[1].Click();
                System.Threading.Thread.Sleep(3000);

                commonFunctionsUtility.CheckIfLoadedXpath("//div[@class=' col-xs-24 col-sm-8 col-md-8 btn-container']");
                driver.FindElement(By.XPath("//a[@class='btn small-width secondary open_register' and @data-target='confirm-order']")).Click();
                System.Threading.Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@class='custom_checkbox']/label[@for='cbSendToAdvisor']")).Click();
                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.Click("btnAdvisorSendOrderConfirmationEmail");


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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen10.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen10.jpeg"));
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
