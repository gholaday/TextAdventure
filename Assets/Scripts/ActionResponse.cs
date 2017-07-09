using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject {

	public string requiredString;

	[TextArea]
	public string textResponse;

	public abstract bool DoActionResponse (GameController controller);

}
