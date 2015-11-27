using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	public GameObject hexModel;
	public GameObject gameStateScript;
	public int boardSize;

	//Using int so that we can later extend the logic for AR-VR players.
	int turn;	// 0 : player 1 : RED
				// 1 : player 2 : BLUE


	void createGameBoard()
	{
		Vector3 pos = new Vector3 (0.0f, 0.0f, 0.0f);
		var rb = hexModel.transform.localScale;

		for (int i=0; i<boardSize+2; ++i) {
			pos.x = (rb.x * i);
			pos.z += 2.0f * rb.z;
			for (int j=0; j<boardSize+2; ++j) {
				
				pos.x += (2.0f * rb.x);
				Instantiate(hexModel, new Vector3(pos.x * 0.87f, pos.y, pos.z * 0.75f), Quaternion.Euler(0, 30, 0));
			}
		}
	}

	// Use this for initialization
	void Start () {

		//TODO : Uncomment the code below to generate the board again
		// You'll have to do this if you change the boardsize,
		// change anything in the hex prefab
		// Or want to change the spacing.

		//createGameBoard ();

		turn = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		//If left mouse click
		if (Input.GetMouseButtonDown (0)) {

			//gameStateScript.GetComponent<GameStateScript>().updateGameBoard(0, turn);
			//Get mouse point
			Vector2 pos = Input.mousePosition;
			//Cast Ray
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0.0f));
			RaycastHit hit;
			//Get hit point
			if(Physics.Raycast(ray, out hit))
			{
				//change the color of the 
				HexScript hex = hit.transform.gameObject.GetComponent<HexScript>();
				//Pasing by reference as increment only if we change the color
				hex.rise(ref turn);
			}
		}
	}
}
