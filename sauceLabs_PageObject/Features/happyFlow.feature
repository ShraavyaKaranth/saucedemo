Feature: happyFlow

A short summary of the feature

@final
Scenario: execution of saucedemo
	Given User is on login page
	When User enters username and password and clicks on login button
	Then User is logged in
	When User views an item
	Then Item data is displayed
	When User adds item to cart and clicks on cart button
	Then Cart will be opened
	When User clicks on checkout button
	Then checkout page will be opened
	When User enters firstname, lastname and postalcode and clicks on continue button
	Then User is navigated to checkout overview page
	When User scrolls down and clicks on finish
	Then Confirmation page will be appeared
	When User checks for confirmation text and compares it with actual text
	Then If it matches, the test case passes. Else it fails.
