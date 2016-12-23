using UnityEngine;
using System.Collections;
using System;

public static class GameManager {
	
	// speed of scrolling
	public static float scrollStep = 0.05f;

	// texture dependent
	public static float horizontalSpacing = 1.92f;
	public static float verticalMultiplier = 3.2f;

	// scale dependent
	public static int columns = 4; 										//Number of columns in our game board.
	public static int rows = 2;											//Number of rows in our game board.
	public static float bottomMargin = -3f * verticalMultiplier; //-2f
	public static float topMargin = 6f * verticalMultiplier; // 4f

	public static float rowGap = verticalMultiplier %  (Math.Abs (topMargin) - Math.Abs (bottomMargin)) +scrollStep;


	private static float health = 0;
	private static float points = 0;

	public static bool IsInsideHorizontalBounds (Vector2 point) 
	{
		return (point.x > -0.5f && point.x < (horizontalSpacing * columns));
	}

	public static void addPoints(float newPoints){
		points += newPoints;
	}
	public static float getPoints () {
		return points;
	}
		

}
