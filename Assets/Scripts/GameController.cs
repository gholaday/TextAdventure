using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GameController : MonoBehaviour {

	public Text displayText;
	public InputField inputField;
	public GameObject helpScreen;

	[Header("Colors")]
	public Color backgroundColor;
	public Color baseColor;
	public TextColors.TextColor exitsColor;
	public TextColors.TextColor itemsColor;

	public InputAction[] inputActions;

	[HideInInspector]
	public RoomNavigation navigation;
	[HideInInspector]
	public InteractableItems interactableItems;
	[HideInInspector]
	public Map map;

	[HideInInspector]
	public List<string> interactableDescriptions = new List<string> ();

	List<string> actionLog = new List<string>();

	bool helpScreenOpen = false;

	// Use this for initialization
	void Awake () 
	{
		navigation = GetComponent<RoomNavigation> ();	
		interactableItems = GetComponent<InteractableItems> ();
		map = FindObjectOfType<Map> ();
		displayText.color = baseColor;
	}
	
	// Update is called once per frame
	void Start () 
	{
		DisplayRoomText ();
		DisplayLoggedText ();

		if(map.mapRooms.ContainsKey(navigation.currentRoom.name))
		{
			map.ChangeCurrentMapRoom (map.mapRooms [navigation.currentRoom.name]);
		}
	}

	void Update()
	{
		if(helpScreenOpen)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				CloseHelpScreen ();
			}
		}
	}

	public void DisplayRoomText()
	{
		displayText.text = "";
		ClearCollectionsForNewRoom ();
		UnpackRoom ();

		string joinedInteractionDescriptions = string.Join("\n", interactableDescriptions.ToArray ());

		string combinedText = navigation.currentRoom.description + "\n\n" + joinedInteractionDescriptions;

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
				interactableDescriptions.Add (string.Format("{0}{1}</color>", TextColors.GetTag(itemsColor), description));
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

	public void DisableInput()
	{
		inputField.enabled = false;
	}

	public void EnableInput()
	{
		inputField.enabled = true;
		inputField.ActivateInputField ();
	}

	public void OpenHelpScreen()
	{
		helpScreenOpen = true;
		DisableInput ();
		helpScreen.SetActive (true);
	}

	public void CloseHelpScreen()
	{
		helpScreenOpen = false;
		EnableInput ();
		helpScreen.SetActive (false);
	}

}
