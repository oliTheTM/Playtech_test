using System;
using AgeVerification_and_AboutUs.WebPages.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AgeVerification_and_AboutUs.WebPages {
    public abstract class WebPage 
    {
        public WebPage(string url) {
            User.WebBrowser.Url = url;
            User.WebBrowser.Navigate();
        }
        public WebPage() {
            if (!string.IsNullOrEmpty(User.WebBrowser.Url))
                User.WebBrowser.Navigate();
            else
                throw (new WebDriverException("URL not set when instantiating a WebPage"));
        }

        /**
         * This method waits until a select web-element appears
         * If it never does, instead of throwing an exception, 
         * it returns false.
         */
        public bool WaitUntilVisible(IWebElement elem) {
            try { 
                WebDriverWait wait = (new WebDriverWait(
                    User.WebBrowser,
                    User.WebBrowser.Manage().Timeouts().PageLoad
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