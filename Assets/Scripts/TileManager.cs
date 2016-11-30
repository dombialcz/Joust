using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;


public class TileManager : MonoBehaviour {

	// speed of scrolling
	public float scrollStep = 0.01f;


	// texture dependent
	public static float horizontalSpacing = 1.92f;
	public static float verticalMultiplier = 3.2f;


	// scale dependent
	public int columns = 4; 										//Number of columns in our game board.
	public int rows = 2;											//Number of rows in our game board.
	public float bottomMargin = -2f * verticalMultiplier;
	public float topMargin = 4f * verticalMultiplier;


	public GameObject[] floorTiles;	 //Array of floor prefabs. Set in UI editor

	private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
	//private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.

	private Transform boardRow1;
	private Transform boardRow2;
	private GameObject row1;
	private GameObject row2;

	private List <GameObject> boardRow = new List <GameObject> ();	


	void Awake (){
		this.SetupScene ();
	}
		
	void BoardSetup ()
	{

		int rowCount = (int) Math.Ceiling( (topMargin-bottomMargin) / verticalMultiplier);
		print ("rowCount is: " + rowCount);
		while (rowCount > 0)
			AddRow (rowCount--);

	}

	float GetPosition (float verticalOffset ) 
	{
		return ((verticalOffset+1f) * (verticalMultiplier) + bottomMargin - scrollStep);// verticalOffset cannot be 0
	}

	void AddRow (int verticalOffset)
	{
		boardRow.Add (new GameObject ("row"));
		float startPosition = GetPosition (verticalOffset);
		setupRow (boardRow [boardRow.Count - 1], (float)(startPosition));
	}

	private void Cleanup (GameObject parent) {
		var children = new List<GameObject>();
		foreach (Transform child in parent.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));
	}

	private void setupRow (GameObject rowObject, float verticalOffset) {

		Cleanup (rowObject);
		Transform parent = rowObject.transform;

		Vector3 position = parent.position;
		position.y = verticalOffset;
		position.x = 0;
		parent.position = position;

		for(int x = 0; x <= columns; x++)
		{
			GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];

			//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
			float horizontal = x * horizontalSpacing -3f;
			float vertical = verticalOffset; //verticalOffset * verticalMultiplier -2f;
			GameObject instance =
				Instantiate (toInstantiate, new Vector3 ((horizontal + 0), vertical, 0f), Quaternion.identity) as GameObject;

			instance.transform.SetParent (parent);
		}


	}

	void SetupScene () 
	{
		BoardSetup ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//ScrollBackgroundRow (row2, "boardRow2");
		foreach (GameObject row in boardRow)
			ScrollBackgroundRow (row);
		
	}

	private void ScrollBackgroundRow (GameObject scrollObject) {
		Vector3 position = scrollObject.transform.position;

		if (position.y > bottomMargin ) {
			position.y -= scrollStep;
			//position.y = Mathf.Lerp(scrollObject.transform.position.y, -2f, 0.1f * Time.deltaTime);
			scrollObject.transform.position = position;
		} else {
			setupRow (scrollObject, GetPosition(boardRow.Count-1f));
		} 

	}

}
