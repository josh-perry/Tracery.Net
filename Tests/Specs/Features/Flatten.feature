Feature: Flatten
	When some text is flattened it should return an appropriate string.

Scenario Outline: Flatten a single string.
	Given I have a string to be flattened: "<string>".
	When I flatten it.
	Then It should return "<string>".

	Examples:
		| string																						|
		| hello																							|
		| disarm                                                                                        |  
		| sky                                                                                           |  
		| ragged                                                                                        |  
		| sisters                                                                                       |  
		| hunt                                                                                          |  
		| Abstraction is often one floor above you.                                                     |  
		| How was the math test?                                                                        |  
		| Cats are good pets, for they are clean and are not noisy.                                     |  
		| I want to buy a onesie… but know it won’t suit me.                                            |  
		| The shooter says goodbye to his love.                                                         |  
		| There was no ice cream in the freezer, nor did they have money to go to the store.            |  
		| This is a Japanese doll.                                                                      |  
		| He said he was not there yesterday; however, many people saw him there.                       |  
		| The river stole the gods.                                                                     |  
		| She was too short to see over the fence.                                                      |  
		| Wow, does that work?                                                                          |  
		| She only paints with bold colors; she does not like pastels.                                  |  
		| Let me help you with your baggage.                                                            |  
		| A purple pig and a green donkey flew a kite in the middle of the night and ended up sunburnt. |  
