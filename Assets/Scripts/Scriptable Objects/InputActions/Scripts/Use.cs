﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/InputActions/Use")]
public class Use : InputAction {

	public override void RespondToInput (GameController controller, string inputVerbs, string inputNouns)
	{
		controller.interactableItems.UseItem (inputNouns);
	}
}
