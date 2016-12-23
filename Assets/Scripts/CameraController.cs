using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float orthographicSize = 5;
	public float aspect = 1.33333f;


	//using orthographic camera.

		//Use Camera.main.nearClipPlane, Camera.main.farClipPlane in place of camera.nearClipPlane, camera.farClipPlane.

	void Start()
	{
		/*
		Camera.main.projectionMatrix = Matrix4x4.Ortho(
			-orthographicSize * aspect, orthographicSize * aspect,
			-orthographicSize, orthographicSize,
			Camera.main.nearClipPlane, Camera.main.farClipPlane);
			*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
