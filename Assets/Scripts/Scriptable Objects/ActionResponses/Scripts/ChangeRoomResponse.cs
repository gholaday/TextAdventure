using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Adventure/ActionResponses/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {

	public Room newRoom;

	public override bool DoActionResponse (GameController controller)
	{
		if(controller.navigation.currentRoom.roomName == requiredString)
		{
			controller.LogStringWithReturn (textResponse);
			controller.navigation.currentRoom = newRoom;
			controller.DisplayRoomText ();
			return true;
		}

		return false;
	}
}
