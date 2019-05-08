﻿using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_AdvisorFlows
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Order_ClaimGarantie : BaseClass
    {
        public static ExtentTest test = null;

        [Test, Order(0), Category("Advisor dashboard testing")]
        [Description("")]
        public void Order_Claim_Garantie()
        {
            //Assert.Ignore("not ready");
            try
            {
                //login to advisor 
                OpenBrowser();

                //initialize test report
                test = extent.CreateTest("Order_Claim_Garantie").Info("Test started");
                test.Log(Status.Info, "Browser is launched");

                //
                commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='active item']");
                commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='tab-pane fade in active']");

                //select a random order to perform claim
                //driver.FindElement(By.CssSelector("ul.nav.nav-pills.table-navigation.pull-left li:nth-child(3)")).Click();
                //commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='carousel-cell item active']");
                //commonFunctionsUtility.CheckIfLoadedXpath("//div[@id='menu2' and @class='tab-pane fade active in']");

                ////
                //IWebElement rows = driver.FindElement(By.XPath("//div[@class='row-expand clearfix']/div/div[@class='row-description']/div/div[@class='expand-row']"));
                ////Random randomElem = new Random();
                //System.Threading.Thread.Sleep(3000);
                ////int num = randomElem.Next(1, rows.Count);
                //rows.Click();
                //System.Threading.Thread.Sleep(3000);

                //commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='row-expand clearfix active']");
                //commonFunctionsUtility.CheckIfLoadedXpath("//div[@class=' col-xs-24 col-sm-8 col-md-8 btn-container']");
                //IList<IWebElement> btn = driver.FindElements(By.XPath("//a[@class='btn small-width secondary open_register']"));
                //System.Threading.Thread.Sleep(6000);
                //btn[2].Click();

                //go to claim link directly
                driver.Url = ConfigurationManager.AppSettings["Url"] + "claimtype/nl/page/1146/?orderNumber=W347559";

                driver.FindElement(By.XPath("//a[@class='faq-item' and @data-target='004']")).Click();
                System.Threading.Thread.Sleep(3000);
                IList<IWebElement> product = driver.FindElements(By.XPath("//a[@class='open_register' and @data-target='edit-product']"));
                product[0].Click();

                commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='slide-active']");
                System.Threading.Thread.Sleep(2000);
                commonFunctionsUtility.AddInput("ClaimDescription", "test");
                driver.FindElement(By.XPath("//div[@class='edit-product-options-canvas']/a[@class='btn primary large']")).Click();
                System.Threading.Thread.Sleep(2000);
                commonFunctionsUtility.Click("lnkOrderTop");

                System.Threading.Thread.Sleep(6000);
                commonFunctionsUtility.Click("lnkOrderTop");

                System.Threading.Thread.Sleep(6000);
                commonFunctionsUtility.CheckIfLoaded("cphContent_cphContentMain_ctl00_ctl00_hlPayment");
                commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_hlPayment");

                System.Threading.Thread.Sleep(3000);
                commonFunctionsUtility.CheckIfLoaded("cphContent_main");
                driver.Url = ConfigurationManager.AppSettings["Url"] + "/advisor-dashboard/nl/page/1135/";

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
                screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen21.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\screen21.jpeg"));
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
