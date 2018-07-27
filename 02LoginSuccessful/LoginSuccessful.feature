Feature: LoginSuccessful
	Here I want to register
	At Customer Portal website
	And Iwant lo Login with my credentoals
	I must be sure that my Login works 

@mytag
Scenario: Verify login successfull
	Given I register at TapMango portal
	And I navigate to Login page
	When I enter my new credentials
	Then I see Login Successfully screen
