Feature: Age-Verification & About-Us
Verify that the functionality of said Features are correct.

# Browser is grouped because continuous reloading of browsers hinders execution-time.
# DATE, MONTH and YEAR are randomly sampled.

Scenario Outline: 1 Verify Age-Gate
	Given the User is on <Browser>
	When the User navigates to 'playtech home'
	Then the User observes the 'Age-Gate Modal'
	When the <Maturity> User enters their birth date as <Date>, <Month> and <Year>
	And the User clicks 'Enter Site'
	Then the User observes the <Effect>
Examples:
	|  Maturity  |  Browser  | Date | Month | Year |     Effect      |
	|  'mature'  | 'Firefox' | NONE | MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE | MONTH | YEAR | 'Modal is gone' |
	|  'mature'  | 'IE'      | NONE | MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE | MONTH | YEAR | 'Modal is gone' |
	|  'mature'  | 'Edge'    | NONE | MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE | MONTH | YEAR | 'Modal is gone' |
	|  'mature'  | 'Chrome'  | NONE | MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE | NONE  | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE | NONE  | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE | MONTH | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE | MONTH | YEAR | 'Modal is gone' |
	| 'immature' | 'Firefox' | NONE | MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Firefox' | DATE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Firefox' | DATE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Firefox' | NONE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Firefox' | DATE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Firefox' | NONE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Firefox' | NONE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Firefox' | DATE | MONTH | YEAR | 'Modal is gone' |
	| 'immature' | 'IE'      | NONE | MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'IE'      | DATE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'IE'      | DATE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'IE'      | NONE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'IE'      | DATE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'IE'      | NONE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'IE'      | NONE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'IE'      | DATE | MONTH | YEAR | 'Modal is gone' |
	| 'immature' | 'Edge'    | NONE | MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Edge'    | DATE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Edge'    | DATE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Edge'    | NONE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Edge'    | DATE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Edge'    | NONE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Edge'    | NONE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Edge'    | DATE | MONTH | YEAR | 'Modal is gone' |
	| 'immature' | 'Chrome'  | NONE | MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Chrome'  | DATE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Chrome'  | DATE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Chrome'  | NONE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Chrome'  | DATE | NONE  | NONE | 'Alert Message' |
	| 'immature' | 'Chrome'  | NONE | NONE  | YEAR | 'Alert Message' |
	| 'immature' | 'Chrome'  | NONE | MONTH | NONE | 'Alert Message' |
	| 'immature' | 'Chrome'  | DATE | MONTH | YEAR | 'Modal is gone' |

#After last test-case/\, browser won't refresh/clear-cookies

Scenario: 2 Validate About-Us
	When the User clicks 'menu open'
	And the User clicks 'About Us'
	Then the User observes the ''