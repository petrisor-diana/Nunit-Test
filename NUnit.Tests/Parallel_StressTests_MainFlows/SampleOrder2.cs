using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;


namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleOrders : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("")]
        public void SampleOrder()
        {

            //launch browser
            OpenBrowser();
        
        
            try
            {
                //initialize test report

                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver);

                //login
                commonFunctionsUtilities.Login();

                System.Threading.Thread.Sleep(4000);
                commonFunctionsUtilities.Click("ctrlHeader2018_ctl00_link1Url");
                commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl02_rptSamplePreshopItems_samplePreshopMidItem_0");

                //select a sample to add in basket
                commonFunctionsUtilities.CheckIfLoadedXpath("//ul[@id='ProductListerTop34']");
                IList<IWebElement> list = driver.FindElements(By.XPath("//ul[@id='ProductListerTop34']/li"));
                list[1].Click();
                list[2].Click();

                commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl01_rptGroupListAndValues_btnNextTop_0");

                //fill in input
                System.Threading.Thread.Sleep(5000);
                // commonFunctionsUtilities.AddInputFromDataTable();
                commonFunctionsUtilities.AddInput("fc_nameSC", "firstname");
                commonFunctionsUtilities.AddInput("fc_lastnameSC", "lastname");
                commonFunctionsUtilities.AddInput("fc_emailSC", "email@address.com");
                commonFunctionsUtilities.AddInput("fc_zipSC", "9001AS");
                commonFunctionsUtilities.AddInput("fc_housenumberSC", "20");
                commonFunctionsUtilities.AddInput("fc_streetSC", "street name");
                commonFunctionsUtilities.AddInput("fc_citySC", "city name");

                    commonFunctionsUtilities.Click("ctl16_fieldsetDelivery");

                    driver.FindElement(By.XPath("//a[@onclick='ValidateFields(true); return false;']")).Click();
                    System.Threading.Thread.Sleep(1000);
                    driver.FindElement(By.XPath("//a[@onclick='ResetHF();' and @id='ctl16_homeLink']")).Click();
                    System.Threading.Thread.Sleep(1000);


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
