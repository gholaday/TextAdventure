using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

	public List<InteractableObject> usableItemList;

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string> ();

	[HideInInspector]
	public List<string> nounsInRoom = new List<string> ();

	Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse> ();
	List<string> nounsInInventory = new List<string> ();
	GameController controller;
	PointManager pointManager;

	void Awake()
	{
		controller = GetComponent<GameController> ();
		pointManager = GetComponent<PointManager> ();
	}

	public string GetObjects(InteractableObject interactable)
	{
		if (!nounsInInventory.Contains(interactable.noun))
		{
			nounsInRoom.Add (interactable.noun);
			return interactable.description;
		}

		return null;
	}

	public void AddActionResponses()
	{
		foreach(string noun in nounsInInventory)
		{
			InteractableObject obj = GetInteractableObjectFromUsableList (noun);
			if (obj == null)
				continue;

			foreach(Interaction interaction in obj.interactions)
			{
				if (interaction.actionResponse == null)
					continue;

				if(!useDictionary.ContainsKey(noun))
				{
					useDictionary.Add (noun, interaction.actionResponse);
				}
			}
		}
	}

	InteractableObject GetInteractableObjectFromUsableList(string noun)
	{
		foreach(InteractableObject obj in usableItemList)
		{
			if(obj.noun == noun)
			{
				return obj;
			}
		}

		return null;
	}


	public void DisplayInventory()
	{
		controller.LogStringWithReturn ("You look in your backpack, inside you have: ");

		foreach(string noun in nounsInInventory)
		{
			controller.LogStringWithReturn (noun);
		}
	}

	public void ClearCollections()
	{
		examineDictionary.Clear ();
		takeDictionary.Clear ();
		nounsInRoom.Clear ();
	}

	public Dictionary<string, string> Take(string inputNouns)
	{
		if(nounsInRoom.Contains(inputNouns))
		{
			nounsInInventory.Add (inputNouns);
			AddActionResponses ();
			nounsInRoom.Remove (inputNouns);

			InteractableObject item = usableItemList.Find (i => i.noun == inputNouns);
			if(item != null)
			{
				pointManager.AddPoints (item.takePointValue);
			}
				
			return takeDictionary;
		}
		else
		{
			controller.LogStringWithReturn ("There is no " + inputNouns + " here to take.");
			return null;
		}
	}

	public void UseItem(string inputNouns)
	{
		if(nounsInInventory.Contains(inputNouns))
		{
			if(useDictionary.ContainsKey(inputNouns))
			{
				bool actionResult = useDictionary [inputNouns].DoActionResponse (controller);

				if (!actionResult) 
				{
					controller.LogStringWithReturn ("Hmmm. Nothing happened.");
				}
				else
				{
					InteractableObject item = usableItemList.Find (i => i.noun == inputNouns);
					if(item != null)
					{
						pointManager.AddPoints (item.usePointValue);
					}
				}
			}
			else
			{
				controller.LogStringWithReturn ("You can't use the " + inputNouns + ".");
			}
		}
		else
		{
			controller.LogStringWithReturn("There is no " + inputNouns + " in your inventory to use.");
		}
	}
}
