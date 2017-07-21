using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapRoom : MonoBehaviour {

	public string roomName;
	public Vector3 roomPos;
	public bool discovered;

	TextMeshPro tmp;
	string text;

	void Awake()
	{
		tmp = GetComponent<TextMeshPro> ();
	}

	public void DiscoverRoom()
	{
		text = tmp.text;
		tmp.text = "";
		discovered = true;

		StartCoroutine ("Discover");
	}

	IEnumerator Discover()
	{
		yield return new WaitForSeconds (1f);

		foreach(char c in text)
		{
			tmp.text += c;
			yield return new WaitForSeconds (.1f);
		}
	}


}
