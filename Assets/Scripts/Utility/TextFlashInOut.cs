using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextFlashInOut : MonoBehaviour {

	public float flashTime = .5f;

	TextMeshProUGUI tmpGUI;
	TextMeshPro tmp;

	Color c;
	Color c2;

	void Awake()
	{
		tmpGUI = GetComponent<TextMeshProUGUI> ();
		tmp = GetComponent<TextMeshPro> ();

		if(tmpGUI != null)
		{
			c = tmpGUI.color;
			StartCoroutine (FlashInOut ());
		}
		else if(tmp != null)
		{
			c2 = tmp.color;
			StartCoroutine (FlashInOut ());
		}
			
	}

	void OnEnable()
	{
		StartCoroutine (FlashInOut ());
	}

	IEnumerator FlashInOut()
	{
		while(true)
		{
			if(tmpGUI != null)
			{
				tmpGUI.color = new Color (c.r, c.g, c.b, 0);
			}
			else if(tmp != null)
			{
				tmp.color = new Color (c2.r, c2.g, c2.b, 0);
			}

			yield return new WaitForSeconds (flashTime);

			if(tmpGUI != null)
			{
				tmpGUI.color = new Color (c.r, c.g, c.b, 1);
			}
			else if(tmp != null)
			{
				tmp.color = new Color (c2.r, c2.g, c2.b, 1);
			}

			yield return new WaitForSeconds (flashTime);
		}
	}
}
