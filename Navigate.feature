Feature: Navigate

@mytag
Scenario Outline: Navigate to Google
	Given User navigate to google
	When User enters search query and searches <Keyword>
	Then Results are loaded
Examples: 
| Keyword |
| program |
| PCS     |
| Code    |
