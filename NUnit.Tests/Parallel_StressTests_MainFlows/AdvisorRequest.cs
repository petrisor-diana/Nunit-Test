using System;
using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using System.Configuration;

namespace NUnit.Tests_Parallel
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AdvisorRequests : BaseClass
    {
        [Test, Category("FE testing parallelized")]
        [Description("Create an Advisor Request")]
        public void AdvisorRequest()
        {
            //Assert.Ignore("Not ready for testing");
            //login to advisor 
            OpenBrowser();

            //add data to exceldata file
            //ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\AdvisorRequestInput.xlsx", 1);

            try
            {
                //initialize test report
                test = extent.CreateTest("Advisor_Request").Info("Test started");
                test.Log(Status.Info, "Browser is launched");
                commonFunctionsUtilities = new CommonFunctionsUtility_StressTests(driver, extent, test, listOfErrors);

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

                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtVoornaam_0", "Invalid input data for firstname");
                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtAchternaam_1", "Invalid input data for lastname");
                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtTelefoonnummer_2", "Invalid input data for phone number");
                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtPostcode_3", "Invalid input data for postcode number");
                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtHuisnummer_4", "Invalid input data for house number");
                commonFunctionsUtilities.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_txtEmailAddress", "Invalid email address");

                if (listOfErrors != null && listOfErrors.Count > 0)
                {
                    test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");
                    listOfErrors.Clear();

                    driver.Quit();
                }
                else
                {
                    commonFunctionsUtilities.Click("cphContent_cphContentMain_ctl00_ctl00_btnSend");

                    //go to thank you page
                    IWebElement thankYou = driver.FindElement(By.ClassName("adviesThankYouWrapper"));
                    System.Threading.Thread.Sleep(4000);
                    test.Log(Status.Info, "Thank you step");
                    test.Log(Status.Pass, "Success");

                    CloseBrowser();
                }
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
