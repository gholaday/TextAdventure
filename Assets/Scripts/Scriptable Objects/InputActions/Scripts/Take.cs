using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Text Adventure/InputActions/Take")]
public class Take : InputAction {

	public override void RespondToInput (GameController controller, string inputVerbs, string inputNouns)
	{
		Dictionary<string, string> takeDictionary = controller.interactableItems.Take (inputNouns);

		if(takeDictionary != null)
		{
			controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (takeDictionary, inputVerbs, inputNouns));
		}
	}
}
