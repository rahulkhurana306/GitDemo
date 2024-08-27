-------------------------------------------------------------
HOW TO TEST FRONTOFFICEMESSAGEPROCESSOR SERVICE USING MOCKS
-------------------------------------------------------------

For Testing FrontOfficeMessgaeProcessor Service through Mocks. You need to manually update the mock services against the scenarios you are running.
In order to test this you have to verify the acceptance criteria and update the mock configurations accordingly.

Please find below the combination of MockServices Update against the Scenarios mentioned on Fitnesse Pages:

Scenario	BatchSize	SOAP UI Customer Sequence No	Mock Response Selected
1		1		123456				Response 3 Null
2		1		123456				Response 2 False
3		1		123456				Response 1 True
4		2		123456				Response 1 True
-		-		123457				Response 1 True
5		2		123456				Response 1 True
-		-		123457				Response 2 False
6		3		123456				Response 1 True
-		-		123457				Response 2 False

Note that in Scenario 4,5,6 Mock Responses should be configured for both Customer Sequence Nos as in Scenario 4 and 5 we are sending messages for both Customer Sequence Nos in a Batch of Size 2

For Scenario 6 we still configured Mock Responses for both Sequence No's although we are sending Request Message for Three Customers as defined in a Batch of size 3. We are not defined Mock Response for third Customer because as per acceptance criteria Mock Response is only required for two Customer Sequence No not for thirdone.

In case you face any issue please contact me @aashish.juneja@soprasteria.com

