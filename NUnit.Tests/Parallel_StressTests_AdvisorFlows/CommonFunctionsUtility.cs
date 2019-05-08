using System.Collections.Generic;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;
using System;


namespace NUnit.BaseTestss
{
    public class CommonFunctionsUtilities
    {
        public IWebDriver driver;
        public ExtentReports extent = null;
        //public ExtentTest test = null;
        //public List<string> listOfErrors = new List<string>();

        public CommonFunctionsUtilities(IWebDriver driver, ExtentReports extent)
        {
            this.driver = driver;
            this.extent = extent;
            //this.test = test;
            //this.listOfErrors = listOfErrors;
        }

        public void LoginAsAdvisor()
        {
            AddInput( "cphContent_cphContentMain_ctl00_ctl00_txtLogin", "mugurel.rata@duk-tech.com");
            AddInput( "cphContent_cphContentMain_ctl00_ctl00_txtAdvisorPassword", "mugurel");
            Click("cphContent_cphContentMain_ctl00_ctl00_lbLogin");
        }

        public void Login()
        {
            AddInput( "txtUserName", "Veneta");
            AddInput( "txtPassword", "supershutter");
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
    }
}
