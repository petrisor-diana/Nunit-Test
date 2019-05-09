using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;


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

            System.Threading.Thread.Sleep(5000);
            //launch browser
            OpenBrowser();

        
            try
            {
                //initialize test report
              
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver);

                //login
                commonFunctionsUtilities.Login();

                driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(2)")).Click();

                commonFunctionsUtilities.CheckIfLoadedXpath("//div[@class='middle_content']");
                driver.FindElement(By.CssSelector("p.buttons")).Click();

                //select a product to add in basket
                commonFunctionsUtilities.CheckIfLoadedXpath("//li[@class='group cf']");
                IList<IWebElement> products = driver.FindElements(By.XPath("//li[@class='group cf']/ul/li[@class='item grid_3 cf']"));

                products[1].Click();

                //product configuration
                commonFunctionsUtilities.GoThroughProductConfigurationSteps();

                //go through baset steps
                commonFunctionsUtilities.GoThroughBasketSteps();
                System.Threading.Thread.Sleep(5000);

                CloseBrowser();
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
