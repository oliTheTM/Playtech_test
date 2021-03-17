using AgeVerification_and_AboutUs.WebPages.Util;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using FluentAssertions;

namespace AgeVerification_and_AboutUs.WebPages 
{ 
    public sealed class PlayTech_Home : WebPage
    {
        private readonly int[] DAYS_OF_MONTH = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

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
        [FindsBy(How = How.XPath, Using = "/html[1]/body[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[2]/div[1]")]
        private IWebElement ageGate_Warning;

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

        public bool AgeWarningVisible() =>
            WaitUntilVisible(ageGate_Warning);

        //Actions:
        /**
         * JS injection to manipulate drop-downs
         */
        public void SelectDate(Birthday combination, bool isMature) {
            //generates random date
            int day = Xu.Next(1, 31), month = Xu.Next(1, 12), year = Xu.Next(1, 100);

            //if day is NONE then the invalid-error will override the age-error:
            if (combination.HasFlag(Birthday.Day)) {
                //iff valid-month then day must be in domain of month: 
                if (combination.HasFlag(Birthday.Month)) {
                    
                    day = day % DAYS_OF_MONTH[month - 1];

                    WaitUntilVisible(ageGate_Day).Should().BeTrue();

                    ((IJavaScriptExecutor)_driver).ExecuteScript(
                        "arguments[0][arguments[1]].selected = true;",
                    ageGate_Day, day);

                    WaitUntilVisible(ageGate_Month).Should().BeTrue();

                    ((IJavaScriptExecutor)_driver).ExecuteScript(
                        "arguments[0][arguments[1]].selected = true;",
                    ageGate_Month, month);
                }
                else if (combination.HasFlag(Birthday.InvalidMonth)) {
                    ;
                }
                if (combination.HasFlag(Birthday.Year)) {
                    WaitUntilVisible(ageGate_Year).Should().BeTrue();

                    ((IJavaScriptExecutor)_driver).ExecuteScript(
                        "arguments[0][arguments[1]].selected = true;",
                    ageGate_Year, year);
                }
            }
            else {
                //what happens if day is NONE??
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