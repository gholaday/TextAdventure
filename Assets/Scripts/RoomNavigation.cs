using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

	public Room currentRoom;

	private Dictionary<string, Room> exitDictionary = new Dictionary<string, Room> ();
	private GameController controller;
	private Map map;
	private List<Room> discoveredRooms = new List<Room>();
	private PointManager pointManager;

	void Awake()
	{
		controller = GetComponent<GameController> ();
		map = FindObjectOfType<Map> ();
		pointManager = GetComponent<PointManager> ();
		discoveredRooms.Add (currentRoom);
	}

	public void UnpackExits()
	{
		foreach (Exit exit in currentRoom.exits) 
		{
			exitDictionary.Add (exit.keyString, exit.valueRoom);
			string modifiedDescription = exit.description;

			if(exit.description.Contains(exit.keyString))
			{
				modifiedDescription = string.Format ("{0}{1}</color>", TextColors.GetTag(controller.exitsColor) ,modifiedDescription);
			}

			controller.interactableDescriptions.Add (modifiedDescription);
		}
	}

	public void ChangeRooms(string directionNoun)
	{
		if (exitDictionary.ContainsKey(directionNoun))
		{
			Exit exit = GetExitFromDirection (currentRoom, directionNoun);

			currentRoom = exitDictionary [directionNoun];

			if(!discoveredRooms.Contains(currentRoom))
			{
				pointManager.AddPoints (currentRoom.pointValue);
				discoveredRooms.Add (currentRoom);
			}

			if(map.mapRooms.ContainsKey(currentRoom.name))
			{
				map.ChangeCurrentMapRoom (map.mapRooms [currentRoom.name]);
			}
				
			if(exit != null && !string.IsNullOrEmpty(exit.transitionDescription))
			{
				controller.LogString (exit.transitionDescription);
			}

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

	public Exit GetExitFromDirection(Room room, string direction)
	{
		foreach(Exit exit in room.exits)
		{
			if (exit.keyString == direction)
				return exit;
		}

		return null;
	}
}
