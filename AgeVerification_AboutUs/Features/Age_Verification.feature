Feature: Age-Verification & About-Us
Verify that the functionality of said Features are correct.

# DATE, MONTH and YEAR are randomly generated indexes.
Scenario Outline: 1 Verify Age-Gate
	Given the User is on <Browser>
	When the User navigates to 'playtech home'
	Then the User observes the 'Age-Gate Modal'
	When the <Maturity> User enters their birth date as <Day>, <Month> and <Year>
	And the User clicks 'Enter Site'
	Then the User observes the <Effect>
#constrained Factorial-Design(30 = 3(1-(1/4)-(1/8))(2(2^3)):
Examples:
	|  Browser  | Day  |     Month     | Year |  Maturity  |     Effect      |
	| 'Firefox' | NONE | MONTH         | YEAR | 'immature' | 'Alert Message' |
	| 'Firefox' | DAY  | NONE          | YEAR | 'immature' | 'Alert Message' |
	| 'Firefox' | NONE | NONE          | YEAR | 'mature'   | 'Alert Message' |
	| 'Firefox' | NONE | NONE          | YEAR | 'immature' | 'Alert Message' |
	| 'Firefox' | NONE | MONTH         | NONE | 'mature'   | 'Alert Message' |
	| 'Firefox' | DAY  | NONE          | NONE | 'mature'   | 'Alert Message' |
	| 'Firefox' | DAY  | MONTH         | YEAR | 'mature'   | 'Modal is gone' |
	| 'Firefox' | DAY  | MONTH         | YEAR | 'immature' | 'Age Warning'   |
	| 'Firefox' | DAY  | INVALID_MONTH | YEAR | 'immature' | 'Alert Message' |	
	| 'Firefox' | NONE | INVALID_MONTH | YEAR | 'mature'   | 'Alert Message' |
	|  'Edge'   | NONE | NONE          | YEAR | 'immature' | 'Alert Message' |
	|  'Edge'   | DAY  | MONTH         | YEAR | 'immature' | 'Age Warning'   |
	|  'Edge'   | NONE | NONE          | YEAR | 'mature'   | 'Alert Message' |
	|  'Edge'   | DAY  | NONE          | YEAR | 'immature' | 'Alert Message' |
	|  'Edge'   | NONE | INVALID_MONTH | YEAR | 'mature'   | 'Alert Message' |
	|  'Edge'   | NONE | MONTH         | YEAR | 'immature' | 'Alert Message' |
	|  'Edge'   | DAY  | NONE          | NONE | 'mature'   | 'Alert Message' |
	|  'Edge'   | DAY  | MONTH         | YEAR | 'mature'   | 'Modal is gone' |
	|  'Edge'   | NONE | MONTH         | NONE | 'mature'   | 'Alert Message' |
	|  'Edge'   | DAY  | INVALID_MONTH | YEAR | 'immature' | 'Alert Message' |
	|  'Chrome' | NONE | NONE          | YEAR | 'immature' | 'Alert Message' |
	|  'Chrome' | NONE | MONTH         | YEAR | 'immature' | 'Alert Message' |
	|  'Chrome' | DAY  | NONE          | YEAR | 'immature' | 'Alert Message' |
	|  'Chrome' | DAY  | MONTH         | YEAR | 'immature' | 'Age Warning'   |
	|  'Chrome' | DAY  | INVALID_MONTH | YEAR | 'immature' | 'Alert Message' |
	|  'Chrome' | NONE | NONE          | YEAR | 'mature'   | 'Alert Message' |
	|  'Chrome' | NONE | MONTH         | NONE | 'mature'   | 'Alert Message' |
	|  'Chrome' | DAY  | NONE          | NONE | 'mature'   | 'Alert Message' |
	|  'Chrome' | DAY  | MONTH         | YEAR | 'mature'   | 'Modal is gone' |
	|  'Chrome' | NONE | INVALID_MONTH | YEAR | 'mature'   | 'Alert Message' |
	
#After last Example, current-browser won't refresh/clear-cookies
Scenario: 2 Validate About-Us
	When the User clicks 'menu open'
	And the User clicks 'About Us'
	Then the User observes the '4 KIs'