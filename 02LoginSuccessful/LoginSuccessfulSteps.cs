using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System.Threading;

namespace _02LoginSuccessful
{
    [Binding]
    public class LoginSuccessfulSteps
    {
        IWebDriver driver;

        [Given(@"I register at TapMango portal")]
        public void GivenIRegisterAtTapMangoPortal()
        {
            driver = new ChromeDriver();
            driver.Url = "https://customer.tapmango.com/";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[2]/div/div[2]/a")).SendKeys(OpenQA.Selenium.Keys.Return);
            
            //Generate random values for some open fields
            Random r = new Random();
            string Email = String.Format($"Email{r.Next(1000)}@tapmango.com");
            string FullName = String.Format($"FullName{r.Next(1000)}");
            string Phone = r.Next(1000000000).ToString().PadRight(10, '0');

            //Fill in the information into open fields
            driver.FindElement(By.Id("Email")).SendKeys(Email);
            driver.FindElement(By.Id("Email")).SendKeys(OpenQA.Selenium.Keys.Control + "a");
            driver.FindElement(By.Id("Email")).SendKeys(OpenQA.Selenium.Keys.Control + "c");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("FirstName")).SendKeys(FullName);
            driver.FindElement(By.Id("DOBDay")).SendKeys("30");
            driver.FindElement(By.Id("DOBMonth")).SendKeys("August");
            driver.FindElement(By.Id("Phone")).SendKeys(Phone);
            driver.FindElement(By.Id("Password")).SendKeys("QNKWMQFZ22");
            driver.FindElement(By.Id("PasswordConfirm")).SendKeys("QNKWMQFZ22");
            driver.FindElement(By.XPath("/html/body/div[1]/div/section[2]/div/div[2]/div/form/div/div[2]/div[8]/input")).Click();
            driver.FindElement(By.Id("login-btn")).Click();
            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("Sweet Success!"));

            driver.FindElement(By.XPath("/html/body/div[1]/header/div[1]/div/div[2]/a/span")).Click();
        }

        [Given(@"I navigate to Login page")]
        public void GivenINavigateToLoginPage()
        {
            driver.Navigate().GoToUrl("http://www.tapmango.com/");
            driver.Navigate().GoToUrl("https://customer.tapmango.com/");
            driver.FindElement(By.XPath("/html/body/div[1]/div/section[2]/div/div/div/div[1]/div/div[2]/a")).Click();

            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("CUSTOMER LOGIN"));
        }
        
        [When(@"I enter my new credentials")]
        public void WhenIEnterMyNewCredentials()
        {
            driver.FindElement(By.Id("Email")).SendKeys(OpenQA.Selenium.Keys.Control + "v");
            driver.FindElement(By.Id("Password")).SendKeys("QNKWMQFZ22");
            driver.FindElement(By.XPath("/html/body/div[1]/div/section[2]/div/div/div[1]/div/form/button")).Click();
        }

        [Then(@"I see Login Successfully screen")]
        public void ThenISeeLoginSuccessfullyScreen()
        {
            IWebElement body = driver.FindElement(By.TagName("body"));
            Assert.IsTrue(body.Text.Contains("WELCOME"));

            driver.FindElement(By.XPath("/html/body/div[1]/header/div[1]/div/div[2]/a/span")).Click();
            Assert.IsTrue(body.Text.Contains("CUSTOMER LOGIN"));

            driver.Quit();
        }
    }
}
