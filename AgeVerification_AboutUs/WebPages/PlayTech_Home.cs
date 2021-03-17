using AgeVerification_and_AboutUs.WebPages.Util;
using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AgeVerification_and_AboutUs.WebPages 
{ 
    public sealed class PlayTech_Home : WebPage
    {
        /**
         * For generating random dates
         */
        private Random Xu;

        /**
         * Remark, that the risk of a truncated Xpath being non-unique exponentially decreases w/r it's length.
         * This is because the expected No. elements of a given context compounds this at each level; by Law of Product.
         */
        [FindsBy(How =How.XPath, Using = "//html[1]/body[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[1]/img[1]")]
        private IWebElement ageGate;

        [FindsBy(How = How.XPath, Using = "//div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[3]/div[1]/select[1]")]
        private IWebElement ageGate_Day;
        [FindsBy(How = How.XPath, Using = "//div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[3]/div[2]/select[1]")]
        private IWebElement ageGate_Month;
        [FindsBy(How = How.XPath, Using = "//div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[3]/div[3]/select[1]")]
        private IWebElement ageGate_Year;
        
        [FindsBy(How = How.XPath, Using = "//div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[1]")]
        private IWebElement ageGate_Alert;
        
        [FindsBy(How = How.XPath, Using = "//div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[4]/button[1]")]
        private IWebElement ageGate_submit;

        [FindsBy(How = How.Id, Using = "hamburger")]
        private IWebElement home_menuOpen;

        [FindsBy(How = How.XPath, Using = "//div[1]/main[1]/div[1]/nav[1]/div[1]/ul[1]/li[1]/a[1]")]
        private IWebElement aboutUs_link;


        public PlayTech_Home(IWebDriver driver) : base(driver, "http://www.playtech.com/") {
            Xu = new Random(DateTime.Now.Millisecond);
        }


        //Assertions:
        /**
         * Assumption: a web-page is loaded given that the test-object is visible.
         */
        public bool AgeGateVisible() =>
            WaitUntilVisible(ageGate);

        public bool AlertVisible() =>
            WaitUntilVisible(ageGate_Alert);

        //Actions:
        /**
         * JS injection to manipulate drop-downs
         */
        public void SelectDate(Birthday filter)
        {
            string birthday = ((Birthday)filter).ToString();
            if (Regex.IsMatch(birthday, @"Day"))
            {
                if (!WaitUntilVisible(ageGate_Day))
                    throw (new ElementNotVisibleException("ageGate_Day"));
                //pick a random day:
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Day, Xu.Next(1, 31));
            }
            if (Regex.IsMatch(birthday, @"Month"))
            {
                if (!WaitUntilVisible(ageGate_Month))
                    throw (new ElementNotVisibleException("ageGate_Month"));
                //pick a random month:
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Month, Xu.Next(1, 12));
            }
            if (Regex.IsMatch(birthday, @"Year"))
            {
                if (!WaitUntilVisible(ageGate_Year))
                    throw (new ElementNotVisibleException("ageGate_Year"));
                //pick a random year:
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Year, Xu.Next(1921, 2021));
            }
        }

        public void ClickAgeGateSubmit() =>
            Click(ageGate_submit, "ageGate_submit");
    
        public void OpenMenu() =>
            Click(home_menuOpen, "home_menuOpen");

        public void ClickAboutUsLink() =>
            Click(aboutUs_link, "aboutUs_link");
    }
}