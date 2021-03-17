using AgeVerification_and_AboutUs.WebPages.Util;
using TechTalk.SpecFlow;

namespace AgeVerification_and_AboutUs.GlobalHooks
{ 
	[Binding]
	public sealed class Global 
	{
		private ITestRunner _testRunner;

		public Global() {
			_testRunner = TestRunnerManager.GetTestRunner();
		}

		[AfterTestRun]
		public void AfterTestRun() {	
			_testRunner.CollectScenarioErrors();
			User.TearDown();
		}
	}
}