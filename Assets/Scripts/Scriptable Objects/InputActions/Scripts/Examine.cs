using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Text Adventure/InputActions/Examine")]
public class Examine : InputAction {

	public override void RespondToInput (GameController controller, string inputVerbs, string inputNouns)
	{
		if(inputNouns.Length > 1)
		{
			controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (controller.interactableItems.examineDictionary, 
				inputVerbs, inputNouns));
		}
		else if(inputNouns.Length == 0)
		{
			controller.DisplayRoomText();
		}


	}
}
