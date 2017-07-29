using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	public InputField inputField;
	GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController> ();
		inputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void AcceptStringInput(string userInput)
	{
		if (userInput.Length > 0) 
		{
			// print user command to screen
			bool acceptable = false;

			controller.LogStringWithReturn (userInput);
			// convert input to lowercase, then loop through input actions, then loop through input actions keywords
			userInput = userInput.ToLower ();

			foreach(InputAction action in controller.inputActions)
			{
				foreach(string verb in action.verbs)
				{
					if(userInput.Contains(verb))
					{
						acceptable = true;
						string inputNouns = userInput.Replace(verb, "").Trim();
						action.RespondToInput(controller, verb, inputNouns);
					}

				}
			}

			if(acceptable == false)
			{
				controller.LogStringWithReturn("You cannot do that. Type 'help'.");
			}				
		}

		InputComplete ();

	}

	void InputComplete()
	{
		controller.DisplayLoggedText ();
		inputField.ActivateInputField ();
		inputField.text = null;
	}
}
