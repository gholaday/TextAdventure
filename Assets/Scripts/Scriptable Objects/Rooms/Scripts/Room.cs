using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/Room")]
public class Room : ScriptableObject {

	[TextArea]
	public string description;
	public string roomName;
	public int pointValue = 0;
	public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;



}
