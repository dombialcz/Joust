using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class ItemManager : ScrollingObjectManager {

	public GameObject[] items;	 //Array of floor prefabs. Set in UI editor
	private List <GameObject> itemList = new List <GameObject> ();	

	public int maxNumberOfItems = 1;

	private float spawnTimer;

	// Use this for initialization
	void Start () {
		onBottomMarginReached = DestroyItem;
		spawnTimer = 0f;
		InvokeRepeating("SpawnItem", 2.0f, 5f);
	}

	public void DestroyItem (GameObject item)
	{
		itemList.Remove(item);
		Destroy (item);
	}

	private void CreateItem (){
		int randomRow =  (int)Random.Range (0, GameManager.rows);
		print (" randomRow: " + randomRow);
		CreateItemAtRow (randomRow);
	}

	private void CreateItemAtRow (int rowNumber) 
	{
		GameObject toInstantiate = items[Random.Range (0,items.Length)];
		GameObject newItem = CreateInstance (toInstantiate, GetRowX (rowNumber), GameManager.topMargin);
		itemList.Add (newItem);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject item in (new List <GameObject> (itemList))) {
			ScrollDown (item);
		}

	}

	private void SpawnItem () {
		if ( (maxNumberOfItems > itemList.Count) )
		{
			//CreateItem (Random.Range (0, GameManager.rows));
			CreateItem ();
		}
	}
}
