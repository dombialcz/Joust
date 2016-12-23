using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour {
	
	public LayerMask blockingLayer;

	public float speed = 0.1f;
	private bool moving;

	private BoxCollider2D boxCollider;
	private Rigidbody2D rb2d;

	private ItemManager itemManager;

	private Text textPoints;
	private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.

	private bool blinking;

	void Start ()
	{
		//transform.position = new Vector2 (3.8f, 0f);
		itemManager = GetComponent<ItemManager>();
		moving = false;
		boxCollider = GetComponent<BoxCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		boxCollider.enabled = true;

		textPoints = GameObject.Find("TextPoints").GetComponent<Text>();

		//Set the text of levelText to the string "Day" and append the current level number.
		textPoints.text = "Points: 0";

		blinking = false;

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

		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (GameManager.horizontalSpacing * direction, 0f);

		//CheckCollision <T> (start, end);

		if (GameManager.IsInsideHorizontalBounds (end)) {
			StartCoroutine (MoveTo (end));
		} else {
			moving = false;
		}

	}

	// TODO: remove this or reuse differently
	/*
	void CheckCollision <T>  (Vector2 start, Vector2 end) 
		where T: Component {
		RaycastHit2D hit;

		boxCollider.enabled = false;
		hit = Physics2D.Linecast (start, end, blockingLayer);
		boxCollider.enabled = true;

		if (hit.transform != null) {
			print ("hit");
			T hitComponent = hit.transform.GetComponent<T>();
			Pickup (hitComponent);
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
	*/

	private void OnTriggerEnter2D (Collider2D other)
	{
		print ("on trigger");
		if(other.tag == "Item" )
		{
			float points = other.gameObject.GetComponent<Item>().points;;
			GameManager.addPoints (points);
			print ("Current points: " + GameManager.getPoints());
			other.gameObject.SetActive (false);

			textPoints.text = "Points: "+ GameManager.getPoints();
			StartCoroutine (Blink()); // THIS IS HERE FOR TEST ONLY
		}
	}

	// TODO: This should be renamed to something more appropriate for losing life
	// TODO: This is proof of concept
	IEnumerator Blink () {
		if (!blinking) {
			blinking = true;
			Color color = this.GetComponent<SpriteRenderer> ().color;

			for (int i = 4; i > 0; i--) {
				color.a = 0f;
				this.GetComponent<SpriteRenderer> ().color = color;
				yield return new WaitForSeconds (0.15f);

				color.a = 1f;
				this.GetComponent<SpriteRenderer> ().color = color;
				yield return new WaitForSeconds (0.15f);
			}
			blinking = false;
		}

	}

	void Update ()
	{
		int horizontal = 0;
		int vertical = 0;


		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		vertical = (int) (Input.GetAxisRaw ("Vertical"));

		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0)
		{
			//Store the first touch detected.
			Touch myTouch = Input.touches[0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}

			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
			{
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;

				//Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;

				//Calculate the difference between the beginning and end of the touch on the y axis.
				float y = touchEnd.y - touchOrigin.y;

				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				touchOrigin.x = -1;

				//Check if the difference along the x axis is greater than the difference along the y axis.
				if (Mathf.Abs(x) > Mathf.Abs(y))
					//If x is greater than zero, set horizontal to 1, otherwise set it to -1
					horizontal = x > 0 ? 1 : -1;
				else
					//If y is greater than zero, set horizontal to 1, otherwise set it to -1
					vertical = y > 0 ? 1 : -1;
			}
		}

		#endif //End of mobile platform dependendent compilation section started above with #elif
		//Check if we have a non-zero value for horizontal or vertical

		if (!moving && (horizontal != 0 || vertical != 0)) {
			moving = true;
			CheckForPickupMove<Item>(horizontal);
		}
	}

}
