using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Text Adventure/InputActions/Go")]
public class Go : InputAction {

	public override void RespondToInput (GameController controller, string inputVerbs, string inputNouns)
	{
		controller.navigation.ChangeRooms (inputNouns);
	}
}
