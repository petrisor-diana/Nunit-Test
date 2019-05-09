using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using NUnit.Tests1;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class ProductOrder_Plissegordijnens : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("Product Order")]

        public void ProductOrder_Plissegordijnen()
        {
            //Assert.Ignore("");
            System.Threading.Thread.Sleep(5000);
            //launch browser
            OpenBrowser();

            //add data to exceldata file
           // ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput2.xlsx", 1);

            try
            {
                //initialize test report
                test = extent.CreateTest("ProductOrder_Plissegordijnen").Info("Test started");
                test.Log(Status.Info, "Browser is launched");
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver, extent, test, listOfErrors);

                //login
                commonFunctionsUtilities.Login();

                driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(2)")).Click();

                commonFunctionsUtilities.CheckIfLoadedXpath("//div[@class='middle_content']");
                driver.FindElement(By.CssSelector("p.buttons")).Click();

                //select a product to add in basket
                commonFunctionsUtilities.CheckIfLoadedXpath("//li[@class='group cf']");
                IList<IWebElement> products = driver.FindElements(By.XPath("//li[@class='group cf']/ul/li[@class='item grid_3 cf']"));
                //Random randomElem = new Random();
                //System.Threading.Thread.Sleep(5000);
                //int num = randomElem.Next(1, products.Count);
                products[1].Click();

                //product configuration
                commonFunctionsUtilities.GoThroughProductConfigurationSteps();

                //go through baset steps
                commonFunctionsUtilities.GoThroughBasketSteps();
                System.Threading.Thread.Sleep(5000);
                test.Log(Status.Pass, "Success");

                CloseBrowser();
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
