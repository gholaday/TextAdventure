using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject {

	public string keyword;
	public string[] verbs;

	public abstract void RespondToInput (GameController controller, string inputVerbs, string inputNouns);
}
