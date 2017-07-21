using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;


	private Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> ();
	private GameController controller;
	private Map map;

	void Awake()
	{
		controller = GetComponent<GameController> ();
		map = FindObjectOfType<Map> ();
	}

	public void UnpackExits()
	{
		foreach (Exit exit in currentRoom.exits) 
		{
			exitDictionary.Add (exit.keyString, exit.valueRoom);
			string modifiedDescription = exit.description;

			if(exit.description.Contains(exit.keyString))
			{
				modifiedDescription = modifiedDescription.Replace (exit.keyString, string.Format ("<color=orange>{0}</color>", exit.keyString));
			}

			controller.interactableDescriptions.Add (modifiedDescription);
		}
	}

	public void ChangeRooms(string directionNoun)
	{
		if (exitDictionary.ContainsKey(directionNoun))
		{
			currentRoom = exitDictionary [directionNoun];

			if(map.mapRooms.ContainsKey(currentRoom.name))
			{
				map.ChangeCurrentMapRoom (map.mapRooms [currentRoom.name]);
			}

			controller.LogStringWithReturn ("You go " + directionNoun);
			controller.DisplayRoomText ();
		}
		else
		{
			controller.LogStringWithReturn ("You cannot proceed " + directionNoun);
		}
	}

	public void ClearExits()
	{
		exitDictionary.Clear ();
	}
}
