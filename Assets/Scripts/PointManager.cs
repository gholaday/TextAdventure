using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour {

	public Text pointText;
	public float incrementSpeed = .1f;

	private int pointTotal = 0;
	private int displayedPointTotal;

	public void Awake()
	{
		displayedPointTotal = pointTotal;
	}
		
	public void AddPoints(int pointValue)
	{
		pointTotal += pointValue;
		StopCoroutine (IncrementDisplayedPointTotal ());
		StartCoroutine (IncrementDisplayedPointTotal ());
	}

	public int GetPointTotal()
	{
		return pointTotal;
	}

	IEnumerator IncrementDisplayedPointTotal()
	{
		while(displayedPointTotal != pointTotal)
		{
			displayedPointTotal++;
			pointText.text = "Score: " + displayedPointTotal;
			yield return new WaitForSeconds (incrementSpeed);
		}
	}
		
}
