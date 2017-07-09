using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;


	private Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> ();
	private GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController> ();
	}

	public void UnpackExits()
	{
		foreach (Exit exit in currentRoom.exits) 
		{
			exitDictionary.Add (exit.keyString, exit.valueRoom);
			controller.interactableDescriptions.Add (exit.description);
		}
	}

	public void ChangeRooms(string directionNoun)
	{
		if (exitDictionary.ContainsKey(directionNoun))
		{
			currentRoom = exitDictionary [directionNoun];
			controller.LogStringWithReturn ("You go " + directionNoun);
			controller.DisplayRoomText ();
		}
		else
		{
			controller.LogStringWithReturn ("There cannot proceed " + directionNoun);
		}
	}

	public void ClearExits()
	{
		exitDictionary.Clear ();
	}
}
