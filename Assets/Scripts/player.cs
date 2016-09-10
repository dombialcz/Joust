using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public float speedModifier = 10;
	public float step = 0.5f;

	private bool moving;

	private Vector3 pos;
	private float posXstart;
	private float posXprogress;
	private float posXend;

	private float posYstart;
	private float posyend;

	
	
	// Use this for initialization
	void Start () {
		moving = false;
		posXprogress = 0;
	}

	void move () {
		pos = transform.position;


		pos.x = Mathf.Lerp(posXstart, posXend, posXprogress);


		transform.position = pos;
		
		if ( pos.x == posXend) {
			moving = false;
			posXprogress = 0f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			posXprogress = 0f;
			moving = true;
			posXstart = transform.position.x;
			posXend = posXstart - step;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			posXprogress = 0f;
			posXstart = transform.position.x;
			posXend = posXstart + step;
			moving = true;
		} 

		if (moving) {
			posXprogress += Time.deltaTime * speedModifier;
			move ();
		}
	}
}
