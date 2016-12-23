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
		InvokeRepeating("SpawnItem", 0f, 0.05f);
	}

	public void DestroyItem (GameObject item)
	{
		itemList.Remove(item);
		Destroy (item);
	}

	// Work in progress
	private void CreateItem (){
		// TODO: choose row at random from available rows
		int rowNumber = 1;// (int)Random.Range (0, GameManager.columns +1);

		// TODO: create a list for rows, choose one randomly and try to create item there
		// if fails, pick another one, until the list is exhausted (do not repeat indefinately!)
		if (!CreateItemAtRow (rowNumber)) {
			CreateItemAtRow (2);
		}
	}

	private bool CreateItemAtRow (int rowNumber) 
	{
		Vector2 spawnPoint = new Vector2(GetRowX (rowNumber), GameManager.topMargin);

		// Dont spawn near another object
		// TODO: the circle is too large currently
		if (Physics2D.OverlapCircle(spawnPoint, 10f)) {
			return false;
		} else {
			GameObject toInstantiate = items [Random.Range (0, items.Length)];
			GameObject newItem = CreateInstance (toInstantiate, GetRowX (rowNumber), GameManager.topMargin);
			newItem.GetComponent<BoxCollider2D>().enabled = true;
			itemList.Add (newItem);
			return true;
		}
	}
		
	void Update () {
		foreach (GameObject item in (new List <GameObject> (itemList)))
			ScrollDown (item);
	}

	private void SpawnItem () {
		if ( (maxNumberOfItems > itemList.Count) )
			CreateItem ();
	}
}
