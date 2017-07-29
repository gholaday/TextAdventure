using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/Interactable Object")]
public class InteractableObject : ScriptableObject {

	public string noun = "name";
	[TextArea]
	public string description = "Description in room";
	public Interaction[] interactions;
	public int usePointValue = 0;
	public int takePointValue = 0;
}
