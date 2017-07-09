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

	void Awake()
	{
		controller = GetComponent<GameController> ();
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

	public Dictionary<string, string> Take(string[] seperatedInputWords)
	{
		string noun = seperatedInputWords [1];

		if(nounsInRoom.Contains(noun))
		{
			nounsInInventory.Add (noun);
			AddActionResponses ();
			nounsInRoom.Remove (noun);
			return takeDictionary;
		}
		else
		{
			controller.LogStringWithReturn ("There is no " + noun + " here to take.");
			return null;
		}
	}

	public void UseItem(string[] seperatedInputWords)
	{
		string noun = seperatedInputWords [1];

		if(nounsInInventory.Contains(noun))
		{
			if(useDictionary.ContainsKey(noun))
			{
				bool actionResult = useDictionary [noun].DoActionResponse (controller);

				if (!actionResult) 
				{
					controller.LogStringWithReturn ("Hmmm. Nothing happened.");
				}
			}
			else
			{
				controller.LogStringWithReturn ("You can't use the " + noun + ".");
			}
		}
		else
		{
			controller.LogStringWithReturn("There is no " + noun + " in your inventory to use.");
		}
	}
}
