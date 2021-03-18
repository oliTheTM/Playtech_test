using AgeVerification_and_AboutUs.WebPages;
using AgeVerification_and_AboutUs.WebPages.Util;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace AgeVerification_AboutUs.Steps
{
    /**
    **ScenarioContext**
    * 
    *     home-page : PlayTech_Home
    *     about-us : PlayTech_AboutUs
    *        
    */
    [Binding]
    public sealed class Age_Verification
    {
        private readonly ScenarioContext _scenarioContext;


        public Age_Verification(ScenarioContext scenarioContext) {
            _scenarioContext = scenarioContext;
        }


        [Given("the User is on '(.*)'")]
        public void GivenUserUsing(string browser) =>
            User.ChangeRefreshBrowser(browser.ToBrowser());

        [When(@"the User navigates to '(.*)'")]
        public void WhenTheUserNavigatesTo(string webPage) {
            if (webPage.Equals("playtech home")) {
                //PlayTech_Home() transitively navigates to home-page:
                if (_scenarioContext.ContainsKey("home-page")) {
                    _scenarioContext["home-page"] = new PlayTech_Home(User.WebBrowser);
                }
                else
                    _scenarioContext.Add("home-page", (new PlayTech_Home(User.WebBrowser)));
            }
        }

        [When(
            @"the '(mature|immature)' User enters their birth date as (NONE|DATE), (NONE|MONTH) and (NONE|YEAR)"
        )]
        public void WhenTheUserEntersTheirBirthDateAsDMY(
            string maturity, string day,
            string month, string year
        ) {
            bool isMature = !maturity.Contains("im");
            Birthday entry = 0;
            //0 means none selected
            entry = (Birthday)(
                ((day.Equals("DAY"))? (int)Birthday.Day : 0) +
                ((month.Equals("MONTH"))? (int)Birthday.Month : 
                    ((month.Equals("INVALID_MONTH")) ? (int)Birthday.InvalidMonth : 0)
                ) +
                ((year.Equals("YEAR"))? (int)Birthday.Year : 0)
            );
            if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home)) {
                home.SelectDate(entry, isMature);
                home.ClickAgeGateSubmit();
            } else 
                throw (new SpecFlowException("Scenario context mssing: home-page"));
        }

        [When(@"the User clicks '(.*)'")]
        public void WhenTheUserClicks(string button)
        {
            if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home)) {
                if (button.Equals("Enter Site"))
                    home.ClickAgeGateSubmit();
                if (button.Equals("menu open"))
                    home.OpenMenu();        
                if (button.Equals("About Us"))
                    home.ClickAboutUsLink();
            } else
                throw (new SpecFlowException("Scenario Context missing: home-page"));
        }

        [Then(@"the User observes the '(.*)'")]
        public void ThenTheUserObservesThe(string webElement) {
            //safest way given you can't foresee combination of Gherkin statements:
            if (_scenarioContext.TryGetValue("home-page", out PlayTech_Home home)) {
                if (webElement.Equals("Age-Gate Modal"))
                    home.AgeGateVisible().Should().BeTrue();
                if (webElement.Equals("Alert Message"))
                    home.AlertVisible().Should().BeTrue();
                if (webElement.Equals("Modal is gone"))
                    home.AgeGateVisible().Should().BeFalse();
                if (webElement.Equals("Age Warning"))
                    home.AgeWarningVisible().Should().BeTrue();
                if (webElement.Equals("4 KIs")) {
                    _scenarioContext.Add("about-us", (new PlayTech_AboutUs(User.WebBrowser)));
                    ((PlayTech_AboutUs)_scenarioContext["about-us"]).TitleVisible().Should().BeTrue();
                }
            }
            else
                throw (new SpecFlowException("Scenario Context missing: home-page"));
        }
    }
}