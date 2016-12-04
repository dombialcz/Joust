using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {
	
	public LayerMask blockingLayer;

	public float speed = 0.1f;
	private bool moving;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;

	private ItemManager itemManager;

	void Start ()
	{
		//transform.position = new Vector2 (3.8f, 0f);
		itemManager = GetComponent<ItemManager>();
		moving = false;
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		boxCollider.enabled = true;
	}

	IEnumerator MoveTo (Vector2 end)
	{
		float moveFactor = 0f;
		while (transform.position != (Vector3)end) {
			moveFactor += speed;
			Vector3 newPosition = Vector3.Lerp (transform.position, end, moveFactor);
			this.transform.position = newPosition;
			yield return null;
		}
		moving = false;

	}

	void CheckForPickupMove <T> (int direction)
		where T: Component {
		RaycastHit2D hit;

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (GameManager.horizontalSpacing * direction, 0f);

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;

		if (hit.transform != null) {
			print ("hit");
			T hitComponent = hit.transform.GetComponent<T>();
			Pickup (hitComponent);
		}

		if (GameManager.IsInsideHorizontalBounds (end)) {
			StartCoroutine (MoveTo (end));
		} else {
			moving = false;
		}

	}

	void Pickup  <T> (T component)
	{
		print ("pickup");
		Item item = component as Item;
		float points = item.getPoints();
		GameManager.addPoints (points);
		print ("Current points: " + GameManager.getPoints());
		ItemManager.Destroy (item);
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		print ("on trigger");
		if(other.tag == "Item" )
		{
			print ("item tag boom");
			//Item item = (Item) other;
			float points = 10f;//item.getPoints();
			GameManager.addPoints (points);
			print ("Current points: " + GameManager.getPoints());
			//itemManager.DestroyItem (other.gameObject);
			other.gameObject.SetActive (false);
		}
	}

	void Update ()
	{
		int horizontal = 0;
		int vertical = 0;

		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		if (!moving && (horizontal != 0 || vertical != 0)) {
			moving = true;
			CheckForPickupMove<Item>(horizontal);
		}
	}

}
