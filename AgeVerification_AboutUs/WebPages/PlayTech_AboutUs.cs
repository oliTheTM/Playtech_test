using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using FluentAssertions;

namespace AgeVerification_and_AboutUs.WebPages
{
    public sealed class PlayTech_AboutUs : WebPage 
    {
        //WebElem's:


        /**
         * Asserts that user navigation is correct.
         */
        public PlayTech_AboutUs(IWebDriver driver) : base(driver) {
            _driver.Url.Should().Be("https://www.playtech.com/about-us");
        }
    }
}