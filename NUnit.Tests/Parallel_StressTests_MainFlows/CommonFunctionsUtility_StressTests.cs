using System.Collections.Generic;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;
using System;
using NUnit.Tests1;

namespace NUnit.Tests_Parallel
{
    public class CommonFunctionsUtility_StressTests
    {
        public IWebDriver driver;
        public ExtentReports extent = null;
        public ExtentTest test = null;
        public List<string> listOfErrors = new List<string>();

        public CommonFunctionsUtility_StressTests(IWebDriver driver, ExtentReports extent, ExtentTest test, List<string> listOfErrors)
        {
            this.driver = driver;
            this.extent = extent;
            this.test = test;
            this.listOfErrors = listOfErrors;
        }

        public void GoThroughBasketSteps()
        {
            CheckIfLoadedXpath("//div[@class='wholeBasket']");
            System.Threading.Thread.Sleep(2000);
            Click("cphContent_cphContentMain_ctl00_ctl00_lnkOrder");
            System.Threading.Thread.Sleep(2000);

            CheckIfLoadedXpath("//label[@for='fc_differs']");
            driver.FindElement(By.XPath("//label[@for='fc_differs']")).Click();
            System.Threading.Thread.Sleep(2000);

            //fill in input
            //AddInputFromDataTable();
            AddInput("first_name", "firstname");
            AddInput("last_name", "lastname");
            AddInput("Postcode", "9001AS");
            AddInput("house_nr", "20");
            AddInput("email", "email@address.com");
            AddInput("Telefoonnummer", "0632456987");
            AddInput("b_firstname", "Ifirstname");
            AddInput("b_lastname", "Ilastname");
            AddInput("b_postcode", "0623123654");
            AddInput("b_housenumber", "20");
            AddInput("b_street", "street name");
            AddInput("b_city", "city name");

            System.Threading.Thread.Sleep(2000);

            ValidateInputForBasket("first_name", "Invalid input data for firstname");
            ValidateInputForBasket("last_name", "Invalid input data for lastname");
            ValidateInputForBasket("Postcode", "Invalid input data for postcode number");
            ValidateInputForBasket("house_nr", "Invalid input data for house number");
            ValidateInputForBasket("email", "Invalid input data for Email address");
            ValidateInputForBasket("Telefoonnummer", "Invalid input data for telephone");
            ValidateInputForBasket("b_firstname", "Invalid input data for delivery - firstname");
            ValidateInputForBasket("b_lastname", "Invalid input data for delivery - lastname");
            ValidateInputForBasket("b_postcode", "Invalid input data for delivery - postcode");
            ValidateInputForBasket("b_housenumber", "Invalid input data for delivery - house number");
            ValidateInputForBasket("b_street", "Invalid input data for street name");
            ValidateInputForBasket("b_city", "Invalid input data for city name");

            if (listOfErrors != null && listOfErrors.Count > 0)
            {
                test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                ITakesScreenshot screenshot = driver as ITakesScreenshot;
                Screenshot screen = screenshot.GetScreenshot();
                screen.SaveAsFile("D:\\NUnit Unit Test\\NUnit.Tests1\\NUnit.Tests1\\Screenshot\\AdvisorRequest\\screen7.jpeg", ScreenshotImageFormat.Jpeg);
                test.Log(Status.Info, "Snapshot below:" + test.AddScreenCaptureFromPath("D:\\NUnit Unit Test\\NUnit.Tests1\\NUnit.Tests1\\Screenshot\\AdvisorRequest\\screen7.jpeg"));
                listOfErrors.Clear();

                driver.Quit();
            }
            else
            {
                CheckIfLoaded("cphContent_cphContentMain_ctl00_ctl00_hlPayment");
                Click("cphContent_cphContentMain_ctl00_ctl00_hlPayment");

                //select the payment list
                CheckIfLoadedXpath("//div[@class='payment-container clearfix']");
                System.Threading.Thread.Sleep(2000);
                Dropdown("iDeal-select", "0721");
                System.Threading.Thread.Sleep(3000);
                Click("cphContent_cphContentMain_ctl00_ctl00_hlThankYou");
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.ClassName("idealButton")).Click();
                driver.FindElement(By.ClassName("btnLink")).Click();
                System.Threading.Thread.Sleep(2000);

                CheckIfLoadedXpath("//div[@class='title-thank-you']");
                driver.FindElement(By.ClassName("title-thank-you"));

                test.Log(Status.Pass, "Success");
            }
        }

        public void GoThroughProductConfigurationSteps()
        {

            CheckIfLoaded("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtWidth");
            AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtWidth", "800");

            CheckIfLoaded("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtHeight");
            AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtHeight", "800");

            Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step1Option_rptOptionGroups_ctl00_rptOptionsRadio_ctl01_lblRadioOption");

            CheckIfLoadedXpath("//li[@id='liStep1' and @class='active']");
            System.Threading.Thread.Sleep(2000);
            Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext1");

            CheckIfLoadedXpath("//li[@id='liStep2' and @class='active']");
            Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext2");

            CheckIfLoadedXpath("//li[@id='liStep3' and @class='active']");
            AddInput("state", "Badkamer");
            Click("liStep3");
            System.Threading.Thread.Sleep(2000);
            Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext3");
            System.Threading.Thread.Sleep(2000);

            CheckIfLoadedCSS("#fancybox-overlay");
            CheckIfLoadedXpath("//div[@id='fancybox-outer']/div[@id='fancyActions']/a[@class='btn primary small']");
            driver.FindElement(By.XPath("//div[@id='fancybox-outer']/div[@id='fancyActions']/a[@class='btn primary small']")).Click();
        }
        public void LoginAsAdvisor()
        {
            AddInput("cphContent_cphContentMain_ctl00_ctl00_txtLogin", "mugurel.rata@duk-tech.com");
            AddInput("cphContent_cphContentMain_ctl00_ctl00_txtAdvisorPassword", "mugurel");
            Click("cphContent_cphContentMain_ctl00_ctl00_lbLogin");
        }
        public void Login()
        {
            AddInput("txtUserName", "Veneta");
            AddInput("txtPassword", "supershutter");
            Click("btnSubmit");
        }

        public void AddInput(string inputId, string input)
        {
            driver.FindElement(By.Id(inputId)).SendKeys(input);
        }
        public void Click(string elemId)
        {
            driver.FindElement(By.Id(elemId)).Click();
        }
        public void Dropdown(string dropdownId, string dropdownValue)
        {
            var typecode = driver.FindElement(By.Id(dropdownId));
            var selectElement = new SelectElement(typecode);
            selectElement.SelectByValue(dropdownValue);
        }

        public void ValidateInputForBasket(string inputId, string errorMessage)
        {
            var inputErrors = driver.FindElements(By.XPath("//input[@id='" + inputId + "']/parent::div[@class='form-group filled validation-error']"));

            if (inputErrors != null && inputErrors.Count > 0)
            {
                listOfErrors.Add(errorMessage);
                test.Log(Status.Info, errorMessage + " for " + inputId);
            }

            else
            {
                test.Log(Status.Info, "Valid input data for " + inputId);
            }
        }

        public void ValidateInputForSampleOrder(string inputId, string errorMessage)
        {
            var inputErrors = driver.FindElements(By.XPath("//input[@id='" + inputId + "']/preceding-sibling::div[@class='error-msg']"));

            if (inputErrors != null && inputErrors.Count > 0)
            {
                test.Log(Status.Info, errorMessage + " for " + inputId);
                listOfErrors.Add(errorMessage);
            }

            else

            {
                test.Log(Status.Info, "Valid input data for " + inputId);
            }
        }

        public void ValidateInputForAdvisor(string inputId, string errorMessage)
        {
            var inputErrors = driver.FindElements(By.XPath("//input[@id='" + inputId + "']/following-sibling::div[@class='client-error-message']"));

            if (inputErrors != null && inputErrors.Count > 0)
            {
                test.Log(Status.Info, errorMessage + " for " + inputId);
                listOfErrors.Add(errorMessage);
            }
            else

            {
                test.Log(Status.Info, "Valid input data for " + inputId);
            }
        }

        //public void CheckPageIsReadyJS()
        //{

        //    string bodyClassToFind = string.Empty;

        //    //swtich
        //    //case 1
        //    // case 2

        //    //....




        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    Boolean pageLoaded = js.ExecuteScript("return document.readyState").ToString().Equals("complete");

        //    //Check ready state of page.
        //    if (pageLoaded == true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Page Is loaded.");
        //        return;
        //    }
        //    else
        //    {
        //        do
        //        {
        //            System.Threading.Thread.Sleep(1000);

        //            pageLoaded = js.ExecuteScript("return document.readyState").ToString().Equals("complete");
        //        }
        //        while (pageLoaded != true);
        //    }
        //}

        public void CheckIfLoaded(string elemId)
        {
            //DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            //fluentWait.Timeout = TimeSpan.FromSeconds(10);
            //fluentWait.PollingInterval = TimeSpan.FromMilliseconds(500);
            //fluentWait.IgnoreExceptionTypes(typeof(ElementNotVisibleException), typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            //IWebElement searchResult = fluentWait.Until(x => x.FindElement(By.Id(elemId)));

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            //IWebElement myDynamicElement = wait.Until<IWebElement>(d => d.FindElement(By.Id(elemId)));

            WebDriverWait fluentWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            fluentWait.Until(driver =>
            {
                try
                {
                    driver.FindElement(By.Id(elemId));
                }
                catch (Exception ex)
                {
                    Type exType = ex.GetType();
                    if (exType == typeof(ElementNotVisibleException) ||
                        exType == typeof(NoSuchElementException) ||
                        exType == typeof(InvalidOperationException)||
                        exType == typeof(StaleElementReferenceException))
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                    else
                    {
                        throw; //Rethrow exception if it's not ignore type.
                    }
                }

                System.Diagnostics.Debug.WriteLine("Page Is loaded.");
                return true;
            });
        }

        public void CheckIfLoadedXpath(string elemXpath)
        {
            WebDriverWait fluentWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            fluentWait.Until(driver =>
            {
                try
                {
                    driver.FindElement(By.XPath(elemXpath));
                }
                catch (Exception ex)
                {
                    Type exType = ex.GetType();
                    if (exType == typeof(ElementNotVisibleException) ||
                        exType == typeof(NoSuchElementException) ||
                        exType == typeof(InvalidOperationException) ||
                        exType == typeof(StaleElementReferenceException))
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                    else
                    {
                        throw; //Rethrow exception if it's not ignore type.
                    }
                }

                System.Diagnostics.Debug.WriteLine("Page Is loaded.");
                return true;
            });
        }

        public void CheckIfLoadedCSS(string elemCss)
        {
            WebDriverWait fluentWait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            fluentWait.Until(driver =>
            {
                try
                {
                    driver.FindElement(By.CssSelector(elemCss));
                }
                catch (Exception ex)
                {
                    Type exType = ex.GetType();
                    if (exType == typeof(ElementNotVisibleException) ||
                        exType == typeof(NoSuchElementException) ||
                        exType == typeof(InvalidOperationException) ||
                        exType == typeof(StaleElementReferenceException))
                    {
                        return false; //By returning false, wait will still rerun the func.
                    }
                    else
                    {
                        throw; //Rethrow exception if it's not ignore type.
                    }
                }

                System.Diagnostics.Debug.WriteLine("Page Is loaded.");
                return true;
            });
        }

        public void AddInputFromDataTable()
        {
            foreach (var item in ExcelDataUtility.dataCol)
            {
                AddInput(item.colName, item.colValue);
            }
            ExcelDataUtility.dataCol.Clear();
        }
    }
}
