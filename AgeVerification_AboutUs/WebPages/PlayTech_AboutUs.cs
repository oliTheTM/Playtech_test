using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace AgeVerification_and_AboutUs.WebPages
{
    public sealed class PlayTech_AboutUs : WebPage 
    {
        //WebElem's

        public PlayTech_AboutUs(IWebDriver driver) : base(driver) {
            if (!_driver.Url.Contains("https://www.playtech.com/about-us"))
                throw new WebDriverException("The current URL is not expected: "+_driver.Url);
        }
    }
}