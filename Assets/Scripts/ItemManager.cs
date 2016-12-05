using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class ItemManager : ScrollingObjectManager {

	public GameObject[] items;	 //Array of floor prefabs. Set in UI editor
	private List <GameObject> itemList = new List <GameObject> ();

	public int maxNumberOfItems = 1;


	// Use this for initialization
	void Start () {
		onBottomMarginReached = DestroyItem;
		InvokeRepeating("SpawnItem", 2.0f, 5f);
	}

	public void DestroyItem (GameObject item)
	{
		itemList.Remove(item);
		Destroy (item);
	}

	private void CreateItem (){
		int randomRow =  (int)Random.Range (0, GameManager.columns +1);
		print (" randomRow: " + randomRow);
		CreateItemAtRow (randomRow);
	}

	private void CreateItemAtRow (int rowNumber) 
	{
		GameObject toInstantiate = items[Random.Range (0,items.Length)];
		GameObject newItem = CreateInstance (toInstantiate, GetRowX (rowNumber), GameManager.topMargin);
		itemList.Add (newItem);
	}
		
	void Update () {
		foreach (GameObject item in (new List <GameObject> (itemList)))
			ScrollDown (item);
	}

	//TODO: make sure two items dont spawn at the same row
	private void SpawnItem () {
		if ( (maxNumberOfItems > itemList.Count) )
			CreateItem ();
	}
}
