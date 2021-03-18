using AgeVerification_and_AboutUs.WebPages.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using FluentAssertions;

namespace AgeVerification_and_AboutUs.WebPages 
{
    public sealed class PlayTech_Home : WebPage
    {
        /**
         * Remark, that the risk of a truncated Xpath being non-unique exponentially decreases w/r it's length.
         * This is because the expected No. elements of a given context compounds this at each level; by Law of Product.
         */
        [FindsBy(How = How.XPath, Using = "//html[1]/body[1]/div[1]/div[1]/section[1]/div[1]/div[1]/div[1]/div[1]/img[1]")]
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


        public PlayTech_Home(IWebDriver driver) : base(driver, "http://www.playtech.com/") {}


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

            //1.
            int[] generatedDate = BirthDateGenerator.MakeMature(isMature);

            //If the year isn't enough to decide if a birthday is mature/immature, then
            //missing fields will mean it is undecidable. To solve this - we reduce/raise
            //the year depending on which eqv.class.
            if (
                (generatedDate[2] == 17) &&
                (combination.HasFlag(Birthday.Day) || combination.HasFlag(Birthday.Month))
            ) {
                if (isMature)
                    --generatedDate[2];
                else
                    ++generatedDate[2];
            }
            
            //2.
            if (combination.HasFlag(Birthday.InvalidMonth))
                BirthDateGenerator.MakeInvalidMonth(ref generatedDate);

            //3.
            if (combination.HasFlag(Birthday.Day)) {
                WaitUntilVisible(ageGate_Day).Should().BeTrue();
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Day, generatedDate[0]);
            }
            if (combination.HasFlag(Birthday.Month)) {
                WaitUntilVisible(ageGate_Month).Should().BeTrue();
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Month, generatedDate[1]);
            }
            if (combination.HasFlag(Birthday.Year)){
                WaitUntilVisible(ageGate_Year).Should().BeTrue();
                ((IJavaScriptExecutor)_driver).ExecuteScript(
                    "arguments[0][arguments[1]].selected = true;",
                ageGate_Year, generatedDate[2]);
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