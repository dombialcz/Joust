using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	public float scrollSpeed;
	public float tileSizeZ;

	float posXprogress;

	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
		posXprogress = 0;
	}

	void Update ()
	{
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		//Vector3 updatedPosition = startPosition + Vector3.forward * newPosition;

		Vector3 pos;
		pos = transform.position;

		//posXprogress += Time.deltaTime * scrollSpeed;

		//pos.x = Mathf.Lerp(startPosition.x, startPosition.x+scrollSpeed, posXprogress);
		pos.y = Mathf.Repeat(-Time.time * scrollSpeed, tileSizeZ);
		transform.position = pos;

		//transform.position = Vector3.forward * Time.deltaTime * scrollSpeed; //updatedPosition;// 
	}
}
