using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	
	public LayerMask blockingLayer;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;

	public float points = 10f;
	public float health = 0f;

	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	public float getPoints(){
		return this.points;
	}
	public float getHealth () {
		return this.health;
	}
}
