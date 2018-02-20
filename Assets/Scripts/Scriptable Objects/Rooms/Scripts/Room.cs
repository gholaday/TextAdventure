using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/Room")]
public class Room : ScriptableObject {

    [UniqueIdentifier]
	public string id;
	public string roomName;
	[TextArea]
	public string description;
	public int pointValue = 0;
	public Exit[] exits;
	public InteractableObject[] interactableObjectsInRoom;

}
