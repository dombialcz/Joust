using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public LayerMask blockingLayer;

	public float speed = 1f;
	public float step = TileManager.horizontalSpacing;
	private bool moving;
	private Vector3 pos;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		moving = false;
	}

	IEnumerator Move (int direction) {
		moving = true;

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (step * direction, 0f);
		//float inverseMoveTime = 1f / moveTime;
		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();

		float sqrRemainingDistance = (transform.position - (Vector3)end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (transform.position, end, speed* Time.deltaTime); //inverseMoveTime * Time.deltaTime
			rb2d.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - (Vector3)end).sqrMagnitude;
			yield return null;
		}

		moving = false;

	}

	void Update () {

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		if ( (horizontal != 0 || vertical != 0) && !moving) {
			StartCoroutine(Move(horizontal));
		}
	}

}
