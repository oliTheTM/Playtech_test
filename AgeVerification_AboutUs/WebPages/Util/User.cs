using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace AgeVerification_and_AboutUs.WebPages.Util {

    /**
    * Represents the user
    */
    public static class User
    {
        private static Browser _currentBrowser;
        /**
         * This field is public because otherwise I'd have to anticipate it's future use
         */
        public static IWebDriver WebBrowser;


        static User(){
            _currentBrowser = 0;
            WebBrowser = null;
        }

        public static void ChangeRefreshBrowser(Browser browser){
            if ((browser > 0)&&(browser != _currentBrowser)) { 
                switch (browser) {
                    case Browser.Chrome:
                        WebBrowser = new ChromeDriver();
                        break;
                    case Browser.IE:
                        WebBrowser = new InternetExplorerDriver();
                        break;
                    case Browser.Edge:
                        WebBrowser = new EdgeDriver();
                        break;
                    case Browser.Firefox:
                        WebBrowser = new FirefoxDriver();
                        break;
                    default:
                        throw (new WebDriverException("Unrecognized browser: " + browser.ToString()));
                }
            }
            WebBrowser.Manage().Cookies.DeleteAllCookies();
            WebBrowser.Manage().Window.FullScreen();
        }
    
        public static void TearDown(){
            WebBrowser.Close();
            WebBrowser.Quit();
            WebBrowser.Dispose();
        }
    }
}