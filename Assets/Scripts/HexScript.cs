using UnityEngine;
using System.Collections;

public class HexScript : MonoBehaviour {

	//Keep this consistant with GameStateScript
	enum COLOR {
		RED=0, 
		BLUE=1, 
		BLACK=2, 
		GREY=3
	};

	//public Texture2D redTexture;
	//public Texture2D blueTexture;
	public Material redMaterial;
	public Material blueMaterial;
	public Material blackMaterial;

	bool scaleY;		//True means we want to scale the Y value
	public int id;		//Hex id starting at 1 (boundary cells included
	COLOR color;

	public static int index = 1;

	GameScript globalObj;
	AudioManagerScript audioManager;

	public GameObject gameStateScript;

	// Use this for initialization
	void Start () {
		scaleY = false;
		id = index;
		index += 1;
		color = COLOR.GREY;

		//get the script associated with the object
		GameObject g = GameObject.Find ("GameManager");
		globalObj = g.GetComponent< GameScript >();

		GameObject g2 = GameObject.Find ("AudioManager");
		audioManager = g2.GetComponent< AudioManagerScript> ();

		//Make color based on boundary
		if (id == 1 || id == globalObj.boardSize + 2 || id == ((globalObj.boardSize + 2) * (globalObj.boardSize + 2)) || id == ((globalObj.boardSize + 1) * (globalObj.boardSize + 2) + 1)) {
			rise (2);
		}
		else if ( id <= globalObj.boardSize + 2 || id > (globalObj.boardSize + 1) * (globalObj.boardSize + 2) ) {
			rise(0);
		}
		else if ( (id % (globalObj.boardSize+2) == 0) || (id % (globalObj.boardSize+2) == 1) ) {
			rise(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//If we have to scale
		if (scaleY) {
			var scale = gameObject.transform.localScale;
			var pos = gameObject.transform.localPosition;

			// If scale is less than max of 2
			if (scale.y <= 2) {
				// Then increment scale and position
				scale.y += 0.01f;
				pos.y += 0.005f;
				gameObject.transform.localScale = scale;
				gameObject.transform.localPosition = pos;
			}
			else {
				scaleY = false;
			}
		}
	}

	public void rise(ref int turn)
	{
		//If empty cell, then 
		if(color == COLOR.GREY)
		{
			changeColor (turn);
			scaleY = true;

			//Alternate turn
			turn += 1;
			turn %= 2;
		}
	}

	//To set initial boundary size
	public void rise(int turn)
	{
		//If empty cell, then 
		if(color == COLOR.GREY)
		{
			changeColor (turn);
			scaleY = true;
		}
	}

	public void changeColor(int turn)
	{
		//Update the game board and see if someone wins
		gameStateScript.GetComponent<GameStateScript>().updateGameBoard(id, turn);
		audioManager.playBlockRise (gameObject.transform.position);

		switch (turn) {
		case 0:
			color = COLOR.RED;
			gameObject.GetComponent<Renderer> ().material = redMaterial;
			break;

		case 1:
			color = COLOR.BLUE;
			gameObject.GetComponent<Renderer> ().material = blueMaterial;
			break;

		case 2:
			color = COLOR.BLACK;
			gameObject.GetComponent<Renderer> ().material = blackMaterial;
			break;
		}
	}
}



//switch (color) {
//	
//case COLOR.RED:
//	gameStateScript.GetComponent<GameStateScript>().updateGameBoard(id, 0);
//	break;
//case COLOR.BLUE:
//	gameStateScript.GetComponent<GameStateScript>().updateGameBoard(id, 1);
//	break;
//case COLOR.BLACK:
//	gameStateScript.GetComponent<GameStateScript>().updateGameBoard(id, 2);
//	break;
//}
