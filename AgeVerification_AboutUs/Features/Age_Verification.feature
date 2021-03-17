Feature: Age-Verification & About-Us
Verify that the functionality of said Features are correct.


# DATE, MONTH and YEAR are randomly generated.
Scenario Outline: 1 Verify Age-Gate
	Given the User is on <Browser>
	When the User navigates to 'playtech home'
	Then the User observes the 'Age-Gate Modal'
	When the <Maturity> User enters their birth date as <Date>, <Month> and <Year>
	And the User clicks 'Enter Site'
	Then the User observes the <Effect>
#Factorial-Design (40 = 2^3 + 2^5). Where there are 3 Fields that may be left Empty
#and there are 2 states for Validity of Month chosen or 2 states for Maturity; for
#each Browser.
Examples:
	|  Maturity  |  Browser  | Date |     Month     | Year |     Effect      |
	|  'mature'  | 'Firefox' | NONE |   MONTH       | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Firefox' | NONE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Firefox' | DATE |   MONTH       | YEAR | 'Modal is gone' |
	|  'mature'  | 'Firefox' | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Firefox' | DATE |   MONTH       | YEAR |  'Age Warning'  |
	| 'immature' | 'Firefox' | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE |   MONTH       | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | NONE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE |   MONTH       | YEAR | 'Modal is gone' |
	| 'immature' | 'IE'      | DATE |   MONTH       | YEAR |  'Age Warning'  |
	| 'immature' | 'IE'      | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'IE'      | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE |   MONTH       | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Edge'    | NONE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Edge'    | DATE |   MONTH       | YEAR | 'Modal is gone' |
	| 'immature' | 'Edge'    | DATE |   MONTH       | YEAR |  'Age Warning'  |
	|  'mature'  | 'Edge'    | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Edge'    | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE |   MONTH       | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE |   NONE        | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE |   NONE        | YEAR | 'Alert Message' |
	|  'mature'  | 'Chrome'  | NONE |   MONTH       | NONE | 'Alert Message' |
	|  'mature'  | 'Chrome'  | DATE |   MONTH       | YEAR | 'Modal is gone' |
	| 'immature' | 'Chrome'  | DATE |   MONTH       | YEAR |  'Age Warning'  |
	|  'mature'  | 'Chrome'  | DATE | INVALID_MONTH | YEAR | 'Alert Message' |
	| 'immature' | 'Chrome'  | DATE | INVALID_MONTH | YEAR | 'Alert Message' |


	


#After last Example, browser won't refresh/clear-cookies
Scenario: 2 Validate About-Us
	When the User clicks 'menu open'
	And the User clicks 'About Us'
	Then the User observes the ''