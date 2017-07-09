using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text displayText;
	public InputAction[] inputActions;


	[HideInInspector]
	public RoomNavigation navigation;
	[HideInInspector]
	public InteractableItems interactableItems;

	[HideInInspector]
	public List<string> interactableDescriptions = new List<string> ();

	List<string> actionLog = new List<string>();

	// Use this for initialization
	void Awake () 
	{
		navigation = GetComponent<RoomNavigation> ();	
		interactableItems = GetComponent<InteractableItems> ();
	}
	
	// Update is called once per frame
	void Start () 
	{
		DisplayRoomText ();
		DisplayLoggedText ();
	}

	public void DisplayRoomText()
	{
		displayText.text = "";
		ClearCollectionsForNewRoom ();
		UnpackRoom ();

		string joinedInteractionDescriptions = string.Join ("\n", interactableDescriptions.ToArray ());

		string combinedText = navigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

		LogStringWithReturn (combinedText);

	}

	public void LogStringWithReturn (string s)
	{
		actionLog.Add (s + "\n");
	}

	public void DisplayLoggedText()
	{
		string logAsText = string.Join ("\n", actionLog.ToArray ());

		displayText.text = logAsText;
	}

	void UnpackRoom()
	{
		navigation.UnpackExits ();
		PrepareObjects (navigation.currentRoom);
	}

	void PrepareObjects(Room currentRoom)
	{
		foreach(InteractableObject obj in currentRoom.interactableObjectsInRoom)
		{
			string description = interactableItems.GetObjects (obj);
			if(description != null)
			{
				interactableDescriptions.Add (description);
			}

			foreach(Interaction interaction in obj.interactions)
			{
				if(interaction.inputAction.keyword == "examine")
				{
					interactableItems.examineDictionary.Add (obj.noun, interaction.textResponse);
				}

				if(interaction.inputAction.keyword == "take")
				{
					interactableItems.takeDictionary.Add (obj.noun, interaction.textResponse);
				}
			}
		}
	}

	public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDict, string verb, string noun)
	{
		if(verbDict.ContainsKey(noun))
		{
			return verbDict [noun];
		}

		return "You can't " + verb + " " + noun + "!";
 	}

	void ClearCollectionsForNewRoom()
	{
		interactableItems.ClearCollections ();
		interactableDescriptions.Clear ();
		navigation.ClearExits ();
	}

}
