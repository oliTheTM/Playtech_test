using AgeVerification_and_AboutUs.WebPages.Util;
using TechTalk.SpecFlow;

namespace AgeVerification_and_AboutUs.GlobalHooks
{ 
	[Binding]
	public static class Global 
	{
		private static ITestRunner _testRunner;

		static Global() {
			_testRunner = TestRunnerManager.GetTestRunner();
		}

		/**
		 * Tear-Down procedure
		 */
		[AfterTestRun]
		public static void AfterTestRun() {	
			_testRunner.CollectScenarioErrors();
			User.TearDown();
		}
	}
}