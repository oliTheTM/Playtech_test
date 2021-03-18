using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using FluentAssertions;

namespace AgeVerification_and_AboutUs.WebPages
{
    public sealed class PlayTech_AboutUs : WebPage 
    {
        //
        [FindsBy(How = How.XPath, Using = "//body[1]/div[1]/main[1]/section[1]/div[1]/div[1]/div[1]/h1[1]")]
        private IWebElement aboutUs_Title;
        [FindsBy(How = How.XPath, Using = "//body[1]/div[1]/main[1]/div[2]/div[1]")]
        private IWebElement aboutUs_4KIs;

       
        /**
         * Asserts that user navigation is correct.
         */
        public PlayTech_AboutUs(IWebDriver driver) : base(driver) {
            _driver.Url.Should().Be("https://www.playtech.com/about-us");
        }


        //Assertions:
        public bool TitleVisible() =>
            WaitUntilVisible(aboutUs_Title);

        //Actions:
    }
}