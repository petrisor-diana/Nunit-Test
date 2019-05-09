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
            //Assert.Ignore("Not ready for testing");
            System.Threading.Thread.Sleep(4000);

            //launch browser
            OpenBrowser();

            //add data to exceldata file
         //   ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput.xlsx", 1);

            try
            {
                //initialize test report
                test = extent.CreateTest("ProductOrder_HoutenJaloezieen").Info("Test started");
                test.Log(Status.Info, "Browser is launched");
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver, extent, test, listOfErrors);

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
                test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                test.Log(Status.Fail, e.ToString());
           
                throw;
            }
            finally
            {
                CloseBrowser();
            }
        }
    }
}
