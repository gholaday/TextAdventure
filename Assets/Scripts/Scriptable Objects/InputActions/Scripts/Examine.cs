using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Text Adventure/InputActions/Examine")]
public class Examine : InputAction {

	public override void RespondToInput (GameController controller, string[] seperatedInputWords)
	{
		if(seperatedInputWords.Length > 1)
		{
			controller.LogStringWithReturn (controller.TestVerbDictionaryWithNoun (controller.interactableItems.examineDictionary, 
				seperatedInputWords [0], seperatedInputWords [1]));
		}
		else if(seperatedInputWords.Length == 1)
		{
			controller.DisplayRoomText();
		}


	}
}
