using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public abstract class ScrollingObjectManager : MonoBehaviour {

	protected delegate void OnBottomMarginReached (GameObject scrollObject);
	protected OnBottomMarginReached onBottomMarginReached;

	protected GameObject CreateInstance (GameObject toInstantiate, float horizontal, float vertical)
	{
		GameObject instance =
			Instantiate (toInstantiate, new Vector3 ((horizontal + 0), vertical, 0f), Quaternion.identity) as GameObject;
		return instance;
	}

	protected void ScrollDown (GameObject scrollObject)
	{
		Vector3 position = scrollObject.transform.position;

		if (position.y > GameManager.bottomMargin ) {
			position.y -= GameManager.scrollStep;
			scrollObject.transform.position = position;
		} else {
			onBottomMarginReached (scrollObject);
		} 
	}

	protected void PutOnRowByNumber(int rowNumber, GameObject scrollingObject)
	{
		float x = GetRowX (rowNumber);
		
		Vector3 newPosition = new Vector3(scrollingObject.transform.position.x + x, GameManager.topMargin, 0f);
		scrollingObject.transform.position = newPosition;
	}

	protected float GetRowX (int rowNumber) 
	{
		float x = 0;
		if (rowNumber >0)
			x = (float)rowNumber * GameManager.horizontalSpacing;
		return x;
	}

	protected void DestroyChildren (GameObject parent)
	{
		var children = new List<GameObject>();
		foreach (Transform child in parent.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}
}
