using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenSequence : MonoBehaviour {

	public GameObject screenOverlay;
	public TextMeshPro skull;
	public TextMeshPro pressEnter;

	void Awake()
	{
		Hide ();
		StartCoroutine(FadeInScreenOverlay ());
	}

	void Hide()
	{
		skull.color = new Color (skull.color.r, skull.color.g, skull.color.b, 0);
	}

	void Update()
	{
		if(Input.GetKey(KeyCode.Return))
		{
			SceneManager.LoadScene ("Main");
		}
	}

	IEnumerator FadeInScreenOverlay()
	{
		SpriteRenderer sr = screenOverlay.GetComponent<SpriteRenderer> ();
		Color c = sr.color;

		for(int i=1;i<15;i++)
		{
			sr.color = new Color (c.r, c.g, c.b, 1-(i/15f));
			yield return new WaitForSeconds (.25f);
		}

		sr.color = Color.clear;

		StartCoroutine (FadeInSkull ());

	}

	IEnumerator FadeInSkull()
	{
		Color c = skull.color;
		for(int i=1;i<15;i++)
		{
			skull.color = new Color (c.r, c.g, c.b, (i/15f));
			yield return new WaitForSeconds (.25f);
		}

		StartCoroutine (MoveTitleUp ());
	}

	IEnumerator MoveTitleUp()
	{
		RectTransform rect = gameObject.GetComponent<RectTransform> ();
		for(int i=0;i<10;i++)
		{
			rect.position += new Vector3 (0, .5f, 0);
			yield return new WaitForSeconds (.25f);
		}

		StartCoroutine (TypeText ());
	}

	IEnumerator TypeText()
	{
		string t = "Press Enter To Continue";

		foreach(char c in t)
		{
			pressEnter.text += c;
			yield return new WaitForSeconds (.1f);
		}

		for (int i = 0; i < 100000; i++) 
		{
			if(i % 2 == 0)
			{
				pressEnter.text = t + "_";
			}
			else 
			{
				pressEnter.text = t + "  ";
			}

			yield return new WaitForSeconds (.25f);
		}

	}



}
