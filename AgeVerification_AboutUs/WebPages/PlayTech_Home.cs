using AgeVerification_and_AboutUs.WebPages.Util;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using FluentAssertions;

namespace AgeVerification_and_AboutUs.WebPages 
{
    public sealed class PlayTech_Home : WebPage
    {
        private readonly int[] MONTHS_LESS_THAN_31 = { 2, 4, 6, 9, 11};

        /**
         * For generating random dates
         */
        private Random Xu;

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


        public PlayTech_Home(IWebDriver driver) : base(driver, "http://www.playtech.com/") {
            Xu = new Random(DateTime.Now.Millisecond);
        }


        /**
         * output: <day, month, year> indices
         */
        private int[] makeMature(bool isMature){
            int[] date = new int[3];
            if (isMature) {
                date[2] = Xu.Next(17, 100);
                if (date[2] == 17) {
                    date[1] = Xu.Next(1, DateTime.Now.Month);
                    if (date[1] == DateTime.Now.Month)
                        date[0] = Xu.Next(1, DateTime.Now.Day);
                }
                else 
                    date[1] = Xu.Next(1, 12);
                date[0] = Xu.Next(1, DateTime.DaysInMonth(
                    (DateTime.Now.Year - date[2]),
                    date[1]
                ));
                return date;
            }
            else {//83 = 100 -(18 - 1)
                date[2] = Xu.Next(1, 83);
                if (date[2] == 83) {
                    date[1] = Xu.Next(DateTime.Now.Month, 12);
                    if (date[1] == DateTime.Now.Month) {
                        if ((DateTime.Now.Day + 1) < DateTime.DaysInMonth(
                            (DateTime.Now.Year - date[2]), date[1]
                        ))
                            date[0] = Xu.Next(
                                (DateTime.Now.Day + 1),
                                DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1])
                            );
                        else
                            date[0] = DateTime.DaysInMonth(
                                (DateTime.Now.Year - date[2]),
                                date[1]
                            );
                    }
                    else
                        date[0] = Xu.Next(1, DateTime.DaysInMonth(
                            (DateTime.Now.Year - date[2]),
                            date[1]
                        ));
                    return date;
                }
                date[1] = Xu.Next(1, 12);
                date[2] = Xu.Next(1, DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1]));
                return date;
            }
        }
        private void makeInvalidMonth(ref int[] date) {
            int days = DateTime.DaysInMonth(
                (DateTime.Now.Year + date[2]),
                date[1]
            );
            if (days == 31)
                date[1] = MONTHS_LESS_THAN_31[Xu.Next(MONTHS_LESS_THAN_31.Length - 1)];
            days = DateTime.DaysInMonth((DateTime.Now.Year - date[2]), date[1]);
            date[0] = days + (date[0] % (31 - days - 1));
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

            //1.
            int[] generatedDate = makeMature(isMature);

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
                makeInvalidMonth(ref generatedDate);

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