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
        public void SelectDate(Birthday combination, bool isMature) {

            int d = Xu.Next(1, 31), m = Xu.Next(1, 12), yr = Xu.Next(1, 100);
            //you are immature if you are younger than 18years:
            if (isMature) {
                if (yr < 17)
                    yr = 17 + Xu.Next(0, 82);
                if (yr == 17) {
                    d = (d < DateTime.Now.Day)? (DateTime.Now.Day - d) : d;
                    m = (m < DateTime.Now.Month) ? (DateTime.Now.Month - m) : m;
                }
            } 
            else {
                if (yr > 17)
                    yr = yr % 17;
                yr = (yr == 0)? 17 : yr;
                d = d % DateTime.Now.Day;
                d = (d == 0)? DateTime.Now.Day : d;
                m = m % DateTime.Now.Month;
                m = (m == 0)? DateTime.Now.Month : m;
            }

            if (combination.HasFlag(Birthday.Date)) {
                if (!WaitUntilVisible(ageGate_Day))
                    throw (new ElementNotVisibleException("ageGate_Day"));
                //pick a random day:
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Day, d);
            }
            if (combination.HasFlag(Birthday.Month)) {
                if (!WaitUntilVisible(ageGate_Month))
                    throw (new ElementNotVisibleException("ageGate_Month"));
                //pick a random month:
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Month, m);
            }
            if (combination.HasFlag(Birthday.Year)) {
                if (!WaitUntilVisible(ageGate_Year))
                    throw (new ElementNotVisibleException("ageGate_Year"));        
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Year, yr);
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