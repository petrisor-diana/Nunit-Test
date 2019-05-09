﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ProductOrder_HoutenJaloezieens_r1 : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("")]
        public void ProductOrder_HoutenJaloezieen_r1()
        {

            //launch browser
            OpenBrowser();

            try
            {
                //initialize test report
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver);

                //login
                commonFunctionsUtilities.Login();

                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:first-child")).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.CssSelector("p.buttons")).Click();


                //select a product to add in basket
                commonFunctionsUtilities.CheckIfLoaded("ProductListerTop53");

                IList<IWebElement> products = driver.FindElements(By.XPath("//ul[@id='ProductListerTop53']/li/ul/li"));
                //Random randomElem = new Random();
                System.Threading.Thread.Sleep(2000);
                //int num = randomElem.Next(1, products.Count);
                products[1].Click();

                //product configuration
                commonFunctionsUtilities.GoThroughProductConfigurationSteps();

                //go through basket steps
                commonFunctionsUtilities.GoThroughBasketSteps();
                System.Threading.Thread.Sleep(5000);

                driver.Quit();
            }
            catch (Exception e)
            {
                      throw;
            }
            finally
            {
                CloseBrowser();
            }
        }
    }
}
