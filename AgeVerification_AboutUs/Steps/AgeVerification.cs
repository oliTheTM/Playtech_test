using AgeVerification_and_AboutUs.WebPages;
using AgeVerification_and_AboutUs.WebPages.Util;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace AgeVerification_AboutUs.Steps
{
    /**
    * ScenarioContext:
    * 
    *     home-page
    *        
    */
    [Binding]
    public sealed class AgeVerification
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;


        public AgeVerification(FeatureContext featureContext, ScenarioContext scenarioContext) {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario()]
        public void BeforeAVScenario() {
            //only for the age-gate UC scenario
            if (_scenarioContext.ScenarioInfo.Title.Equals("1 Verify Age-Gate")) {
                //Refresh for next test-case:
                if (User.WebBrowser != null) {
                    User.WebBrowser.Manage().Cookies.DeleteAllCookies();
                    User.WebBrowser.Navigate();
                }
            }
        }


        [Given("the User is on '(.*)'")]
        public void GivenUserUsing(string browser) =>
            User.ChangeBrowser(browser.ToBrowser());

        [When(@"the User navigates to '(.*)'")]
        public void WhenTheUserNavigatesTo(string webPage) {
            if (webPage.Equals("playtech home"))
                //PlayTech_Home() transitively navigates to home-page
                _scenarioContext.Add("home-page", (new PlayTech_Home(User.WebBrowser)));
            else
                throw (new SpecFlowException("Unknown web-page: "+webPage));
        }

        [Then(@"the User observes the '(.*)'")]
        public void ThenTheUserObservesThe(string webElement) {
            //safest way given you can't foresee combination of Gherkin statements:
            if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home)) {
                if (webElement.Equals("Age-Gate Modal")) 
                    home.AgeGateVisible().Should().BeTrue();   
                else if (webElement.Equals("Alert Message"))
                    home.AlertVisible().Should().BeTrue();
                else if (webElement.Equals("Modal is gone"))
                    home.AgeGateVisible().Should().BeFalse();
                else
                    throw (new SpecFlowException("Unknown web element: " + webElement));
            } else
                throw (new SpecFlowException("Scenario Context missing: home-page"));
        }

        [When(@"the User enters their birth date as (NONE|DATE), (NONE|MONTH) and (NONE|YEAR)")]
        public void WhenTheUserEntersTheirBirthDateAsDMY(string day, string month, string year) {
            Birthday entry = 0;
            entry = (Birthday)(
                ((day.Equals("DATE"))? (int)Birthday.Date : 0) +
                ((month.Equals("MONTH"))? (int)Birthday.Month : 0) +
                ((year.Equals("YEAR"))? (int)Birthday.Year : 0)
            );
            if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home)) {
                home.SelectDate(entry);
                home.ClickAgeGateSubmit();
            } else 
                throw (new SpecFlowException("Scenario context mssing: home-page"));
        }

        [When(@"the User clicks '(.*)'")]
        public void WhenTheUserClicks(string button)
        {
            if (button.Equals("Enter Site")) { 
                if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home))
                    home.ClickAgeGateSubmit();
                else
                    throw (new SpecFlowException("Scenario Context missing: age-gate"));
            }
        }
    }
}