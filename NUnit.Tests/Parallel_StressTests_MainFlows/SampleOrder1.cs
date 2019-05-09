using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleOrder_r2 : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("")]
        public void SampleOrders_r2()
        {
            //launch browser
            OpenBrowser();

            //add data to exceldata file
          //  ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\SampleOrderInput2.xlsx", 1);

            try
            {
                //initialize test report
                test = extent.CreateTest("Sample_Order").Info("Test started");
                test.Log(Status.Info, "Browser is launched");
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver, extent, test, listOfErrors);

                //login
                commonFunctionsUtilities.Login();

                System.Threading.Thread.Sleep(4000);
                commonFunctionsUtilities.Click("ctrlHeader2018_ctl00_link1Url");
                commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl02_rptSamplePreshopItems_samplePreshopMidItem_0");

                //select a sample to add in basket
                commonFunctionsUtilities.CheckIfLoadedXpath("//ul[@id='ProductListerTop34']");
                IList<IWebElement> list = driver.FindElements(By.XPath("//ul[@id='ProductListerTop34']/li"));
                list[0].Click();
                list[1].Click();

                commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl01_rptGroupListAndValues_btnNextTop_0");

                //fill in input
                System.Threading.Thread.Sleep(5000);
                //  commonFunctionsUtilities.AddInputFromDataTable();
                commonFunctionsUtilities.AddInput("fc_nameSC", "firstname");
                commonFunctionsUtilities.AddInput("fc_lastnameSC", "lastname");
                commonFunctionsUtilities.AddInput("fc_emailSC", "email@address.com");
                commonFunctionsUtilities.AddInput("fc_zipSC", "9001AS");
                commonFunctionsUtilities.AddInput("fc_housenumberSC", "20");
                commonFunctionsUtilities.AddInput("fc_streetSC", "street name");
                commonFunctionsUtilities.AddInput("fc_citySC", "city name");

                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_nameSC", "Invalid input data for firstname");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_lastnameSC", "Invalid input data for lastname");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_emailSC", "Invalid input data for Email address");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_zipSC", "Invalid input data for postcode number");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_housenumberSC", "Invalid input data for house number");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_streetSC", "Invalid input data for street name");
                commonFunctionsUtilities.ValidateInputForSampleOrder("fc_citySC", "Invalid input data for city name");

                if (listOfErrors != null && listOfErrors.Count > 0)
                {
                    test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                   
                    listOfErrors.Clear();

                    CloseBrowser();
                }
                else
                {
                    commonFunctionsUtilities.Click("ctl16_fieldsetDelivery");

                    driver.FindElement(By.XPath("//a[@onclick='ValidateFields(true); return false;']")).Click();
                    System.Threading.Thread.Sleep(1000);
                    driver.FindElement(By.XPath("//a[@onclick='ResetHF();' and @id='ctl16_homeLink']")).Click();
                    System.Threading.Thread.Sleep(1000);

                    test.Log(Status.Pass, "Success");
                    driver.Quit();
                }
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
