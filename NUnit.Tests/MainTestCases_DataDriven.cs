using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using NUnit.Tests1;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using NUnit.Tests2;
using System.Configuration;

namespace NUnit.Tests_DataDriven
{
    [TestFixture]

    public class MainTestCases
    {
        public IWebDriver driver;
        public ExtentReports extent = null;
        public ExtentTest test = null;
        public List<string> listOfErrors = new List<string>();
        public CommonFunctionsUtility commonFunctionsUtility;

        [OneTimeSetUp]
        public void ReportStart()
        {
            Assert.Ignore("");
            //create report 
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(ConfigurationManager.AppSettings["ProjectPath"] + "\\ExtentReport\\DataDriven_MainFlows\\reports.html");
            extent.AttachReporter(htmlReporter);
        }

        [Test, Order(8), Category("FE testing datadriven")]
        [Description("Sample Order")] 
        public void SampleOrder()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //launch browser
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();          

                try
                {
                    //initialize test report
                    test = extent.CreateTest("Sample_Order").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\SampleOrderInput.xlsx", i);                  

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();  

                    commonFunctionsUtility.Click("cphContentHeaderBottom_cphContentHeaderBottom_ctl00_ctl00_lnkSidebarItem1");
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl02_rptSamplePreshopItems_lnkBtnPreshopSample_0");

                    //select a sample to add in basket
                    System.Threading.Thread.Sleep(3000);
                    commonFunctionsUtility.CheckIfLoadedXpath("//ul[@id='ProductListerTop34']");
                    IList<IWebElement> list = driver.FindElements(By.XPath("//ul[@id='ProductListerTop34']/li"));
                    //IList<IWebElement> products = list.FindElements(By.CssSelector("ul.clearfix li"));
                    //Random randomElem = new Random();
                    //System.Threading.Thread.Sleep(5000);
                    //int num = randomElem.Next(1, products.Count);
                    list[1].Click();
                    list[2].Click();

                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl01_rptGroupListAndValues_btnNextTop_1");

                    //fill in input
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.AddInputFromDataTable();
                    System.Threading.Thread.Sleep(2000);

                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_nameSC", "Invalid input data for firstname");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_lastnameSC", "Invalid input data for lastname");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_emailSC", "Invalid input data for Email address");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_zipSC", "Invalid input data for postcode number");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_housenumberSC", "Invalid input data for house number");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_streetSC", "Invalid input data for street name");
                    commonFunctionsUtility.ValidateInputForSampleOrder("fc_citySC", "Invalid input data for city name");

                    if (listOfErrors != null && listOfErrors.Count > 0)
                    {
                        test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                        ITakesScreenshot screenshot = driver as ITakesScreenshot;
                        Screenshot screen = screenshot.GetScreenshot();
                        screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen1.jpeg", ScreenshotImageFormat.Jpeg);
                        test.Log(Status.Info, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen1s.jpeg"));
                        listOfErrors.Clear();

                        driver.Quit();
                    }
                    else
                    {
                        commonFunctionsUtility.Click("ctl16_fieldsetDelivery");

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
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen1.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen1.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(1), Category("FE testing datadriven")]
        [Description("Product Order")]
        public void ProductOrder_HoutenJaloezieen()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //launch browser
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();

                try
                {
                    //initialize test report
                    test = extent.CreateTest("ProductOrder_HoutenJaloezieen").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");
                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput.xlsx", i);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:first-child")).Click();
                    driver.FindElement(By.CssSelector("p.buttons")).Click();


                    //select a product to add in basket
                    commonFunctionsUtility.CheckIfLoaded("ProductListerTop53");

                    IList<IWebElement> products = driver.FindElements(By.XPath("//ul[@id='ProductListerTop53']/li/ul/li"));
                    //Random randomElem = new Random();
                    System.Threading.Thread.Sleep(2000);
                    //int num = randomElem.Next(0, products.Count);
                    products[1].Click();

                    //product configuration
                    commonFunctionsUtility.GoThroughProductConfigurationSteps();

                    //go through basket steps
                    commonFunctionsUtility.GoThroughBasketSteps();
                    System.Threading.Thread.Sleep(5000);

                    driver.Quit();
                }
                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen2.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen2.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(2), Category("FE testing datadriven")]
        [Description("Product Order")]
        public void ProductOrder_Plissegordijnen()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //launch browser
                driver = new ChromeDriver();
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();          

                try
                {
                    //initialize test report
                    test = extent.CreateTest("ProductOrder_Plissegordijnen").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput.xlsx", i);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(2)")).Click();

                    commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='middle_content']");
                    driver.FindElement(By.CssSelector("p.buttons")).Click();

                    //select a product to add in basket
                    commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='group cf']");
                    IList<IWebElement> products = driver.FindElements(By.XPath("//li[@class='group cf']/ul/li[@class='item grid_3 cf']"));
                    //Random randomElem = new Random();
                    //System.Threading.Thread.Sleep(5000);
                    //int num = randomElem.Next(0, products.Count);
                    products[1].Click();

                    //product configuration
                    commonFunctionsUtility.GoThroughProductConfigurationSteps();

                    //go through baset steps
                    commonFunctionsUtility.GoThroughBasketSteps();
                    System.Threading.Thread.Sleep(5000);
                    test.Log(Status.Pass, "Success");

                    driver.Quit();
                }
                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen3.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen3.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(3), Category("FE testing datadriven")]
        [Description("Product Order")]
        public void ProductOrder_Skylight()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //Assert.Ignore("not ready");
                //launch browser
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
                

                try
                {
                    //initialize test report
                    test = extent.CreateTest("ProductOrder_Skylight").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput.xlsx", i);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(6)")).Click();
                    System.Threading.Thread.Sleep(5000);

                    //select a product to add in basket
                    commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='lister cf lis86 big']");
                    IList<IWebElement> products = driver.FindElements(By.XPath("//li[@class='group cf']/ul/li[@class='item grid_3 cf']"));
                    //Random randomElem = new Random();
                    //products[randomElem.Next(0, products.Count)].Click();
                    System.Threading.Thread.Sleep(3000);
                    products[1].Click();

                    //skylight configuration - select options
                    commonFunctionsUtility.CheckIfLoaded("cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_ddlOptions_0");
                    commonFunctionsUtility.Dropdown("cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_ddlOptions_0", "1RLOPPREFIXVXGGL");

                    //maatcode
                    commonFunctionsUtility.CheckIfLoaded("cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_pnlOptionContainer_1");
                    System.Threading.Thread.Sleep(4000);
                    driver.FindElement(By.XPath("//li[@data-optioncode='1RLOPCODEVXC01']")).Click();

                    //hardware color
                    System.Threading.Thread.Sleep(4000);
                    commonFunctionsUtility.CheckIfLoadedXpath("//div[@id='cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_rptTabOptions_2_pnlTabOption_0']");
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_rptTabOptions_2_pnlTabOption_0");

                    //bedieningstok
                    System.Threading.Thread.Sleep(4000);
                    commonFunctionsUtility.CheckIfLoadedXpath("//div[@id='cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_rptTabOptions_3_pnlTabOption_0']");
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_Skylight_ProductConfigurator_Skylight_ConfiguratorOptions_rptOptionGroups_rptTabOptions_3_pnlTabOption_0");

                    //
                    System.Threading.Thread.Sleep(4000);
                    commonFunctionsUtility.CheckIfLoaded("btnAddToBasket");
                    commonFunctionsUtility.Click("btnAddToBasket");

                    commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='productDetailWrapper_skylight modal-open']");
                    commonFunctionsUtility.Click("productAddedGoToBasket");
                    System.Threading.Thread.Sleep(4000);

                    //go through baset steps
                    commonFunctionsUtility.GoThroughBasketSteps();
                    System.Threading.Thread.Sleep(5000);
                    test.Log(Status.Pass, "Success");

                    driver.Quit();
                }
                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen3.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen3.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(4), Category("FE testing datadriven")]
        [Description("Product Order")]
        public void ProductOrder_Gordijnen()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //launch browser
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();

                try
                {
                    //initialize test report
                    test = extent.CreateTest("ProductOrder_Gordijnen").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\CustomerBasketInput.xlsx", i);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(8)")).Click();
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl02_rptContentItems_btnPreshopMiddle_0");
                    System.Threading.Thread.Sleep(5000);

                    //select a product to add in basket
                    commonFunctionsUtility.CheckIfLoadedXpath("//li[@class='group cf']");
                    IList<IWebElement> products = driver.FindElements(By.XPath("//li[@class='group cf']/ul/li[@class='item grid_3 cf']"));
                    System.Threading.Thread.Sleep(5000);
                    products[4].Click();

                    //gordijnen configuration - select options
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_lblmm");

                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtWidth", "8000");
                    commonFunctionsUtility.AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtHeight", "2000");
                    System.Threading.Thread.Sleep(5000);

                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext1");
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.AddInput("state", "Badkamer");
                    commonFunctionsUtility.Dropdown("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step3Option_rptOptionGroups_ctl01_ddlOptions", "863");
                    commonFunctionsUtility.Click("configure");
                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext3");
                    System.Threading.Thread.Sleep(1000);

                    commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='productDetailWrapper fancybox-active']");
                    System.Threading.Thread.Sleep(3000);
                    driver.FindElement(By.XPath("//div[@id='fancyActions']/a")).Click();
                    System.Threading.Thread.Sleep(5000);

                    //go through basket steps
                    commonFunctionsUtility.GoThroughBasketSteps();
                    System.Threading.Thread.Sleep(2000);
                    test.Log(Status.Pass, "Success");

                    driver.Quit();
                }
                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen4.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen4.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(5), Category("FE testing datadriven")]
        [Description("Product Order")]
        public void Request_ShuttersInfo()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                //launch browser
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
           

                try
                {
                    //initialize test report
                    test = extent.CreateTest("Request_ShuttersInfo").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    //add data to exceldata file
                    ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\RequestShutterInfoInput.xlsx", i);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.FindElement(By.CssSelector("ul.nav.navbar-nav li:nth-child(9)")).Click();
                    System.Threading.Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//p/a[@class='btn primary medium']")).Click();

                    //fill in input
                    commonFunctionsUtility.AddInputFromDataTable();
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Click("calD_1-1");

                    commonFunctionsUtility.ValidateInputForAdvisor("", "Invalid input data for firstname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl07_rptSubjectFields_txtAchternaam_1", "Invalid input data for lastname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl07_rptSubjectFields_txtTelefoonnummer_2", "Invalid input data for phone number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl07_rptSubjectFields_txtPostcode_3", "Invalid input data for postcode number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl07_rptSubjectFields_txtHuisnummer_4", "Invalid input data for house number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl07_txtEmailAddress", "Invalid email address");

                    if (listOfErrors != null && listOfErrors.Count > 0)
                    {
                        test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                        ITakesScreenshot screenshot = driver as ITakesScreenshot;
                        Screenshot screen = screenshot.GetScreenshot();
                        screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen5.jpeg", ScreenshotImageFormat.Jpeg);
                        test.Log(Status.Info, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen5.jpeg"));
                        listOfErrors.Clear();

                        driver.Quit();
                    }
                    else
                    {
                        commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl07_btnSend");

                        //go to thank you page
                        System.Threading.Thread.Sleep(5000);
                        driver.FindElement(By.XPath("//div[@class='adviesThankYouWrapper']/div/div[@class='title']"));
                        test.Log(Status.Pass, "Success");

                        driver.Quit();
                    }
                }
                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen6.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\Sample Order\\screen6.jpeg"));
                    throw;
                }
                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(0), Category("FE testing datadriven")]
        [Description("Create an Advisor Request")]
        public void AdvisorRequest()
        {
            System.Threading.Thread.Sleep(4000);
            for (int i = 1; i <= 2; i++)
            {
                FirefoxOptions options = new FirefoxOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;            
                driver = new FirefoxDriver(options);
                driver.Manage().Window.Maximize();
              
                //add data to exceldata file
                ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\AdvisorRequestInput.xlsx", i);

                try
                {
                    //initialize test report
                    test = extent.CreateTest("Advisor_Request").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    //go to advisor request
                    driver.Url = "http://acceptance.veneta.com/tips-tricks/afspraak-maken-raamdecoratie/nl/page/149/";

                    //fill in input
                    commonFunctionsUtility.AddInputFromDataTable();

                    commonFunctionsUtility.Click("calL_2-2");

                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtVoornaam_0", "Invalid input data for firstname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtAchternaam_1", "Invalid input data for lastname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtTelefoonnummer_2", "Invalid input data for phone number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtPostcode_3", "Invalid input data for postcode number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_rptSubjectFields_txtHuisnummer_4", "Invalid input data for house number");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl00_txtEmailAddress", "Invalid email address");

                    if (listOfErrors != null && listOfErrors.Count > 0)
                    {
                        test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                        ITakesScreenshot screenshot = driver as ITakesScreenshot;
                        Screenshot screen = screenshot.GetScreenshot();
                        screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen7.jpeg", ScreenshotImageFormat.Jpeg);
                        test.Log(Status.Info, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen7.jpeg"));
                        listOfErrors.Clear();

                        driver.Quit();
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(5000);
                        commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_btnSend");

                        //go to thank you page
                        driver.FindElement(By.ClassName("adviesThankYouWrapper"));
                        test.Log(Status.Info, "Thank you step");
                        test.Log(Status.Pass, "Success");
                        driver.Quit();
                    }
                }

                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen7.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen7.jpeg"));
                    throw;
                }

                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(7), Category("FE testing datadriven")]
        [Description("Complete a Contact Form")]
        public void ContactForm()
        {
            for (int i = 1; i <= 2; i++)
            {
                FirefoxOptions options = new FirefoxOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new FirefoxDriver(options);
                driver.Manage().Window.Maximize();
                

                //add data to exceldata file
                ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\ContactFormInput.xlsx", i);

                try
                {
                    //initialize test report
                    test = extent.CreateTest("Contact_Form").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");

                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    driver.Url = ConfigurationManager.AppSettings["Url"];
                    commonFunctionsUtility.Login();

                    driver.Url = "http://acceptance.veneta.com/klantenservice/contact/nl/page/68/";

                    //fill in input
                    commonFunctionsUtility.AddInputFromDataTable();

                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl02_txtFirstName", "Invalid input data for firstname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl02_txtLastName", "Invalid input data for lastname");
                    commonFunctionsUtility.ValidateInputForAdvisor("cphContent_cphContentMain_ctl00_ctl02_txtEmailAddress", "Invalid input data for address");
                    commonFunctionsUtility.ValidateInputForAdvisor("CCF0", "Invalid input data for telephone number");
                    commonFunctionsUtility.ValidateInputForAdvisor("CCF1", "Invalid input data for subject");
                    commonFunctionsUtility.ValidateInputForAdvisor("CCF2", "Invalid input data for commentary");

                    if (listOfErrors != null && listOfErrors.Count > 0)
                    {
                        test.Log(Status.Info, listOfErrors.Count + " Invalid data entries. Can not reach Thank you page!");

                        ITakesScreenshot screenshot = driver as ITakesScreenshot;
                        Screenshot screen = screenshot.GetScreenshot();
                        screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen8.jpeg", ScreenshotImageFormat.Jpeg);
                        test.Log(Status.Info, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen8.jpeg"));
                        listOfErrors.Clear();

                        driver.Quit();
                    }
                    else
                    {
                        commonFunctionsUtility.Click("btnSend");
                        commonFunctionsUtility.CheckIfLoadedXpath("//div[@class='info']/div[@class='text']/h1");
                        test.Log(Status.Info, "Thank you step");
                        test.Log(Status.Pass, "Success");
                        driver.Quit();
                    }
                }

                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen8.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen8.jpeg"));
                    throw;
                }

                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [Test, Order(10), Category("FE testing datadriven")]
        [Description("Shutter Order As Advisor")]
        public void ShutterOrder_AsAdvisor()
        {
            for (int i = 1; i <= 2; i++)
            {
                //Assert.Ignore("not ready");
                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();

                //add data to exceldata file
                ExcelDataUtility.PopulateInCollection(ConfigurationManager.AppSettings["ProjectPath"] + "\\Resources\\ShutterOrderAsAdvisorInput.xlsx", i);

                try
                {
                    //initialize test report
                    test = extent.CreateTest("ShutterOrder_AsAdvisor").Info("Test started");
                    test.Log(Status.Info, "Browser is launched");
                    commonFunctionsUtility = new CommonFunctionsUtility(driver, extent, test, listOfErrors);

                    if (i == 1) { test.Log(Status.Info, "Run with Correct set of data"); }     // no errors expected
                    if (i == 2) { test.Log(Status.Info, "Run with Incorrect set of data"); }

                    //login
                    System.Threading.Thread.Sleep(5000);
                    driver.Url = ConfigurationManager.AppSettings["LoginUrl"];
                    commonFunctionsUtility.Login();
                    commonFunctionsUtility.LoginAsAdvisor();

                    driver.Url = "http://acceptance.veneta.com/woodlore-plus-full-height-shutter-63mm-pure-white-wp001/nl/product/7706/";

                    //product configuration
                    commonFunctionsUtility.AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtWidth", "800");
                    commonFunctionsUtility.AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtHeight", "800");
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step1Option_rptOptionGroups_ctl00_rptOptionsRadio_ctl01_lblRadioOption");

                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext1");
                    System.Threading.Thread.Sleep(6000);

                    commonFunctionsUtility.Dropdown("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step2Option_rptOptionGroups_ctl00_ddlOptions", "635");
                    System.Threading.Thread.Sleep(6000);
                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext2");
                    System.Threading.Thread.Sleep(5000);

                    commonFunctionsUtility.AddInput("state", "Badkamer");
                    System.Threading.Thread.Sleep(2000);
                    commonFunctionsUtility.Click("configure");
                    commonFunctionsUtility.Dropdown("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step3Option_rptOptionGroups_ctl03_ddlOptions", "650");
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Dropdown("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step3Option_rptOptionGroups_ctl08_ddlOptions", "663");
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Dropdown("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_step3Option_rptOptionGroups_ctl05_ddlOptions", "857");
                    System.Threading.Thread.Sleep(5000);
                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext3");
                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.AddInput("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_txtInstallationHeight", "5000");
                    commonFunctionsUtility.Click("ctl00_ctl00_cphContent_cphContentMain_ctl00_ctl00_venetaDetailWithConfigurator_btnNext4");
                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='productDetailWrapper fancybox-active']");
                    driver.FindElement(By.XPath("//div[@id='fancybox-outer']/div[@id='fancyActions']/a")).Click();
                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.AddInput("cphContent_cphContentMain_ctl00_ctl00_venetaBasketTotals_txtComment", "test");
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_venetaBasketTotals_lbAdvisorFields");
                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='basket basketShopping fancybox-active']");

                    //find the frame
                    IWebElement detailFrame = driver.FindElement(By.XPath("//div[@id='fancybox-outer']/div[@id='fancybox-content']/iframe[@id='fancybox-frame']"));
                    driver.SwitchTo().Frame(detailFrame);

                    driver.FindElement(By.XPath("//label[@id='cphContent_lbOrderTypeNormal']")).Click();

                    System.Threading.Thread.Sleep(4000);

                    commonFunctionsUtility.Click("cphContent_lbOrderCompleteYes");
                    commonFunctionsUtility.Click("cphContent_lbNeighbourDeliveryYes");
                    commonFunctionsUtility.Click("cphContent_lbOwnLeadYes");
                    commonFunctionsUtility.Click("cphContent_lbAdvisorFields");

                    commonFunctionsUtility.CheckIfLoadedXpath("//body[@class='basket basketShopping']");
                    commonFunctionsUtility.Click("cphContent_cphContentMain_ctl00_ctl00_lnkOrderTop");
                    //switch back to main frame
                    driver.SwitchTo().DefaultContent();

                    commonFunctionsUtility.AddInputFromDataTable();

                    test.Log(Status.Pass, "Success");

                    driver.Quit();
                }

                catch (Exception e)
                {
                    test.Log(Status.Info, "Can not reach Thank you page. Check error bellow:");
                    test.Log(Status.Fail, e.ToString());
                    ITakesScreenshot screenshot = driver as ITakesScreenshot;
                    Screenshot screen = screenshot.GetScreenshot();
                    screen.SaveAsFile(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen9.jpeg", ScreenshotImageFormat.Jpeg);
                    test.Log(Status.Fail, "Snapshot below:" + test.AddScreenCaptureFromPath(ConfigurationManager.AppSettings["ProjectPath"] + "\\Screenshot\\AdvisorRequest\\screen9.jpeg"));
                    throw;
                }

                finally
                {
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }

        [OneTimeTearDown]
        public void ReportClose()
        {        
            //Clear report
            extent.Flush();
        }
    }
}

