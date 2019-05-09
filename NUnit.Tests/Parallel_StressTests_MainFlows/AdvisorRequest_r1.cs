using System;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AdvisorRequests_r1 : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("Create an Advisor Request")]
        public void AdvisorRequest_r1()
        {

            OpenBrowser();

            try
            {
                //initialize test report		
           
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver);

                //login
                commonFunctionsUtilities.Login();

                //go to advisor request
                driver.Url = ConfigurationManager.AppSettings["Url"] + "tips-tricks/afspraak-maken-raamdecoratie/nl/page/149/";

                //fill in input
                //commonFunctionsUtilities.AddInputFromDataTable();
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtVoornaam_0", "firstname");
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtAchternaam_1", "lastname");
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtTelefoonnummer_2", "0632252512");
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtPostcode_3", "9001AS");
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtHuisnummer_4", "20");
                commonFunctionsUtilities.AddInput("cphContent_cphContentMain_ctl00_ctl00_txtEmailAddress", "email@address.com");

                commonFunctionsUtilities.Click("calL_2-2");

                    System.Threading.Thread.Sleep(1000);
                    commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl00_btnSend");

                    //go to thank you page
                    IWebElement thankYou = driver.FindElement(By.ClassName("adviesThankYouWrapper"));
        
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
