using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace AgeVerification_and_AboutUs.WebPages {
    public abstract class WebPage 
    {
        protected IWebDriver _driver;


        public WebPage(IWebDriver driver, string url) {
            _driver = driver;
            _driver.Url = url;
            _driver.Navigate();
            initWebElements();
        }
        
        /**
         * This constructor asserts that the User is traversing web-pages
         * at the time of instantiation
         */
        public WebPage(IWebDriver driver) {
            _driver = driver;
            if (!Uri.TryCreate(_driver.Url, UriKind.Absolute, out Uri uri))
                throw (new WebDriverException("Webdriver set to invalid URL: "+ _driver.Url));
            initWebElements();
        }


        /**
         * Triggers web-driver web-element discovery
         */
        private void initWebElements() =>
            PageFactory.InitElements(_driver, this);

        /**
         * This method waits until a select web-element appears
         * If it never does, instead of throwing an exception, 
         * it returns false.
         */
        public bool WaitUntilVisible(IWebElement elem) {
            try { 
                WebDriverWait wait = (new WebDriverWait(
                    _driver,
                    _driver.Manage().Timeouts().PageLoad
                ));
                wait.Until<bool>((w)=>{try{
                    return elem.Displayed; 
                } catch (Exception) {
                    return false;
                }});
                return true;
            } catch (Exception) {
                return false;
            }
        }
    
        public void Click(IWebElement elem, string name) {
            if (WaitUntilVisible(elem))
                elem.Click();
            else
                throw(new ElementNotVisibleException(name));
        }
    }
}