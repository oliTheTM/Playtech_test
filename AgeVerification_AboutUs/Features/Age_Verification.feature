Feature: Age-Verification & About-Us
Verify that the functionality of said Features are correct.

# Browser is grouped because continuous reloading of browsers hinders execution-time.
# DATE, MONTH and YEAR are randomly sampled.

Scenario Outline: 1 Verify Age-Gate
	Given the User is on <Browser>
	When the User navigates to 'playtech home'
	Then the User observes the 'Age-Gate Modal'
	When the User enters their birth date as <Date>, <Month> and <Year>
	And the User clicks 'Enter Site'
	Then the User observes the <Effect>
Examples:
	|  Browser  | Date | Month | Year |     Effect     |
	| 'Firefox' | NONE | MONTH | YEAR |'Alert Message' |
	| 'Firefox' | DATE | NONE  | YEAR |'Alert Message' |
	| 'Firefox' | DATE | MONTH | NONE |'Alert Message' |
	| 'Firefox' | NONE | NONE  | NONE |'Alert Message' |
	| 'Firefox' | DATE | NONE  | NONE |'Alert Message' |
	| 'Firefox' | NONE | NONE  | YEAR |'Alert Message' |
	| 'Firefox' | NONE | MONTH | NONE |'Alert Message' |
	| 'Firefox' | DATE | MONTH | YEAR |'Modal is gone' |
	|   'IE'    | NONE | MONTH | YEAR |'Alert Message' |
	|   'IE'    | DATE | NONE  | YEAR |'Alert Message' |
	|   'IE'    | DATE | MONTH | NONE |'Alert Message' |
	|   'IE'    | NONE | NONE  | NONE |'Alert Message' |
	|   'IE'    | DATE | NONE  | NONE |'Alert Message' |
	|   'IE'    | NONE | NONE  | YEAR |'Alert Message' |
	|   'IE'    | NONE | MONTH | NONE |'Alert Message' |
	|   'IE'    | DATE | MONTH | YEAR |'Modal is gone' |
	|  'Edge'   | NONE | MONTH | YEAR |'Alert Message' |
	|  'Edge'   | DATE | NONE  | YEAR |'Alert Message' |
	|  'Edge'   | DATE | MONTH | NONE |'Alert Message' |
	|  'Edge'   | NONE | NONE  | NONE |'Alert Message' |
	|  'Edge'   | DATE | NONE  | NONE |'Alert Message' |
	|  'Edge'   | NONE | NONE  | YEAR |'Alert Message' |
	|  'Edge'   | NONE | MONTH | NONE |'Alert Message' |
	|  'Edge'   | DATE | MONTH | YEAR |'Modal is gone' |
	| 'Chrome'  | NONE | MONTH | YEAR |'Alert Message' |
	| 'Chrome'  | DATE | NONE  | YEAR |'Alert Message' |
	| 'Chrome'  | DATE | MONTH | NONE |'Alert Message' |
	| 'Chrome'  | NONE | NONE  | NONE |'Alert Message' |
	| 'Chrome'  | DATE | NONE  | NONE |'Alert Message' |
	| 'Chrome'  | NONE | NONE  | YEAR |'Alert Message' |
	| 'Chrome'  | NONE | MONTH | NONE |'Alert Message' |
	| 'Chrome'  | DATE | MONTH | YEAR |'Modal is gone' |

#After last test-case/\, browser won't refresh/clear-cookies

Scenario: 2 Validate About-Us
	When the User clicks 'menu open'
	And the User clicks 'About Us'
	Then the User observes the ''