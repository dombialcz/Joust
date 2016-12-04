using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;


public class TileManager : ScrollingObjectManager {


	public GameObject[] floorTiles;	 //Array of floor prefabs. Set in UI editor
	private List <GameObject> boardRow = new List <GameObject> ();	

	void Start ()
	{
	}

	void Awake ()
	{
		onBottomMarginReached = SetupRowTop;
		this.SetupScene ();
	}

	void SetupScene () 
	{
		BoardSetup ();
	}


	void BoardSetup ()
	{
		int rowCount = (int) Math.Ceiling( (GameManager.topMargin-GameManager.bottomMargin) / GameManager.verticalMultiplier);

		print ("rowCount is: " + rowCount);
		while (rowCount > 0)
			AddRow (rowCount--);

	}

	float GetLoopingPosition (float verticalOffset ) 
	{
		return ((verticalOffset+1f) * (GameManager.verticalMultiplier) + GameManager.bottomMargin - GameManager.rowGap);// verticalOffset cannot be 0
	}

	void AddRow (int verticalOffset)
	{
		boardRow.Add (new GameObject ("row"));
		float startPosition = GetLoopingPosition (verticalOffset);
		SetupRow (boardRow [boardRow.Count - 1], (float)(startPosition));
	}

	private void SetupRowTop (GameObject rowObject) {
		SetupRow (rowObject, GetLoopingPosition(boardRow.Count-1f));
	}

	private void SetupRow (GameObject rowObject, float verticalOffset) {

		DestroyChildren (rowObject);
		Transform parent = rowObject.transform;

		Vector3 position = parent.position;
		position.y = verticalOffset;
		position.x = 0;
		parent.position = position;

		for(int x = 0; x <= GameManager.columns; x++)
		{
			GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

			float horizontal = x * GameManager.horizontalSpacing;
			float vertical = verticalOffset;
			GameObject instance =
				Instantiate (toInstantiate, new Vector3 ((horizontal + 0), vertical, 0f), Quaternion.identity) as GameObject;

			instance.transform.SetParent (parent);
		}
	}


	// Update is called once per frame
	void Update () {
		foreach (GameObject row in boardRow)
			ScrollDown (row);
	}
}
