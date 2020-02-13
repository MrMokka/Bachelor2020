using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeQuestions : MonoBehaviour {

	private DataClasses dc;

	/*
	void Awake() {
		dc = new DataClasses();
		Minigame m = dc.CreateNewMinigame("DnD", "Fill");
		m.AddQuestions("Calculate",
					new List<string> {
						"1 + 1 = {0}",
						"25 * 2 = {0}",
						"x = 5, y = 10",
						"x * 2 + y = {0}",
						"x + y * 1.5 = {0}"
					},
					new List<string> { "11", "2", "32", "46", "50", "62", "79", "80", "94", "20" },
					new List<int> { 1, 4, 9, 9 }
		);
		m.AddQuestions("Calculate",
					new List<string> { "10 + 10 = {0}" },
					new List<string> { "56", "105", "20", "0" },
					new List<int> { 2 }
		);
		m.AddQuestions("How many items will this loop return?",
					new List<string> {
						"Answer: {0}",
						"for(int i = 0; i < 10; i++) {",
						"	if(i % 2 == 0) Console.WriteLine(i);",
						"}"
					},
					new List<string> { "3", "4", "5", "6", "7" },
					new List<int> { 2 }
		);
		m.AddQuestions("What is the range of this loop?",
					new List<string> {
						"Answer: {0}",
						"for(int i = 2; i < 200; i++) {"
					},
					new List<string> { "0-200", "1-200", "2-200", "0-199", "1-199", "2-199" },
					new List<int> { 5 }
		);
		m.AddQuestions("Int x = 5, int y = 17, int z = 3",
					new List<string> {
						"Answer: {0}",
						"x * z - y  = ?"
					},
					new List<string> { "70", "2", "-36", "-3", "-2", "-1" },
					new List<int> { 4 }
		);
		m.AddQuestions("A rope.. ",
					new List<string> {
						"Answer: {0}",
						".. stretches 3 more meters",
						"eatch time you pull it,",
						"but goes back 2 meters when released.",
						"From 1 meter start, how many stretches",
						"to reach 10 meters"
					},
					new List<string> { "9", "8", "7", "6", "5", "10" },
					new List<int> { 2 }
		);

		m.AddQuestions("Rita is picking flowers ..",
					new List<string> {
						"Answer: {0}",
						"She picks yellow and blue flowers",
						"And the ratio she picks from ",
						"blue-to-yellow is 2:3",
						"After 50 picked flowers,",
						"how many of them are blue?"
					},
					new List<string> { "2/3:50", "30", "20", "40", "1/3:40", "0.5/30" },
					new List<int> { 2 }
		);

		m.AddQuestions("Roi is drinking coffe.. ",
					new List<string> {
						"Answer: {0}",
						"He drinks 2 cups(2dl in a cup)",
						"per hour and takes a trip to the john",
						"and empties 3dl once an hour.",
						"How much extra liquid does he have by lunch",
						"(after 3.5h)?"
					},
					new List<string> { "4", "5", "6", "7", "8", "9" },
					new List<int> { 2 }
		);
		m.AddQuestions("What animal...",
					new List<string> {
						"Answer: {0}",
						"Famously crossed the road",
						"to get to the other side?"
					},
					new List<string> { "Duck", "Fox", "Chicken", "Horse", "Dog" },
					new List<int> { 2 }
		);

		m.AddQuestions("What berry..",
					new List<string> {
						"Answer: {0}",
						"needed to cath up?"
					},
					new List<string> { "blueberry", "strawberry", "wildberry", "tomato", "muldberry" },
					new List<int> { 3 }
		);
	}


	public Minigame GetMinigame() {
		return dc.GetRandomMinigame();
	}
	*/

}
