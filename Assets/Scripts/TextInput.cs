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
			bool acceptable = false;

			controller.LogStringWithReturn (userInput);

			userInput = userInput.ToLower ();

			char[] delimeterCharacters = { ' ' };
			string[] seperated = userInput.Split (delimeterCharacters);

			if(seperated.Length > 1 || seperated[0] == "examine")
			{
				foreach(InputAction action in controller.inputActions)
				{
					if(action.keyword == seperated[0])
					{
						acceptable = true;
						action.RespondToInput (controller, seperated);
					}
				}

				if(acceptable == false)
				{
					controller.LogStringWithReturn ("You cannot do that. Type 'help'.");
				}
			}
			else
			{
				controller.LogStringWithReturn ("You cannot do that. Type 'help'.");
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
