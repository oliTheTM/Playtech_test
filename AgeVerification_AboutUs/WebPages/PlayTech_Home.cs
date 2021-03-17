using AgeVerification_and_AboutUs.WebPages.Util;
using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AgeVerification_and_AboutUs.WebPages { 
    public sealed class PlayTech_Home : WebPage
    {
        //To pick random dates
        private Random Xu;

        //Objects:
        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement ageGate_Day;
        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement ageGate_Month;
        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement ageGate_Year;
        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement ageGate_Alert;
        [FindsBy(How = How.XPath, Using = "")]
        private IWebElement ageGate_submit;


        public PlayTech_Home() : base("http://www.playtech.com/") {
            Xu = new Random(DateTime.Now.Millisecond);
        }

        
        //Assertions:
        public bool AgeGateVisible() =>
            WaitUntilVisible(ageGate_submit);

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
                ((IJavaScriptExecutor)User.WebBrowser).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Day, Xu.Next(1, 31));
            }
            if (Regex.IsMatch(birthday, @"Month"))
            {
                if (!WaitUntilVisible(ageGate_Month))
                    throw (new ElementNotVisibleException("ageGate_Month"));
                //pick a random month:
                ((IJavaScriptExecutor)User.WebBrowser).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Month, Xu.Next(1, 12));
            }
            if (Regex.IsMatch(birthday, @"Year"))
            {
                if (!WaitUntilVisible(ageGate_Year))
                    throw (new ElementNotVisibleException("ageGate_Year"));
                //pick a random year:
                ((IJavaScriptExecutor)User.WebBrowser).ExecuteScript(
                    "args[0][args[1]].selected = true;",
                ageGate_Year, Xu.Next(1921, 2021));
            }
        }

        public void ClickAgeGateSubmit() =>
            Click(ageGate_submit, "ageGate_submit");
    }
}