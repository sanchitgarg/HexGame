using UnityEngine;
using System.Collections;

public class GameStateScript : MonoBehaviour {

	//Keep this consistant with Game State Script
	enum COLOR {
		RED=0,
		BLUE=1, 
		BLACK=2, 
		GREY=3
	};

	public int [][] board;
	GameScript globalObj;

	// Use this for initialization
	void Start () {
		//Get the game manager script
		GameObject g = GameObject.Find ("GameManager");
		globalObj = g.GetComponent< GameScript >();

		board = new int[globalObj.boardSize + 2][];

		for(int i=0; i < globalObj.boardSize + 2; ++i){
			board[i] = new int[globalObj.boardSize + 2];
			for(int j=0; j < globalObj.boardSize + 2; ++j){
				board[i][j] = 3;
			}
		}

		board [0] [0] = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void printBoard()
	{
		string output = " ";
		for (int i=0; i<globalObj.boardSize+2; ++i) {
			
			for (int j=0; j<globalObj.boardSize+2; ++j) {
				
				output += board [i] [j] + " ";
			}
			output += "\n";
		}
		
		Debug.Log (output);
	}

	public void updateGameBoard(int hexId, int col)
	{
		//Update the game board with the hexId

		if (board != null) {
			board [(hexId - 1) / (globalObj.boardSize + 2)] [(hexId - 1) % (globalObj.boardSize + 2)] = col;
			checkGameBoard ();
//			printBoard ();

		} else {
			Start();
		}
	}
	
	public void checkGameBoard()
	{
		bool [][] visited;

		//Create a visited matrix that acts as a hash map to save nodes visited
		visited = new bool[globalObj.boardSize+2][];
		for (int i=0; i<globalObj.boardSize+2; ++i) {

			visited[i] = new bool[globalObj.boardSize+2];
			for(int j=0; j<globalObj.boardSize+2; ++j)
			{
				visited[i][j] = false;
			}
		}

		//Check to see if red wins
		if(playerConnectRed(0, 1, 0, visited)) {
			Debug.Log("RED WINS");
			return;
		}

		//Reset visited matrix
		for (int i=0; i<globalObj.boardSize+2; ++i) {
			for(int j=0; j<globalObj.boardSize+2; ++j)
			{
				visited[i][j] = false;
			}
		}

		//Check to see if blue wins
		if(playerConnectBlue(1, 0, 1, visited)) {
			Debug.Log("BLUE WINS");
			return;
		}
	}
	
	bool playerConnectRed(int x, int y, int currCol, bool [][] visited)
	{
		if (x < 0 || x > globalObj.boardSize + 1 || y < 1 || y > globalObj.boardSize) {
			return false;
		}

		if (visited [x] [y]) {
			return false;
		}

		visited [x] [y] = true;

		//If hex is black or grey, return false
		if(board[x][y] != currCol) {
			return false;
		}

		if (x == globalObj.boardSize + 1) {
			return true;
		}

		//Recursion
		if(playerConnectRed(x + 1, y, currCol, visited)) {
			return true;
		}
		if(playerConnectRed(x + 1, y-1, currCol, visited)) {
			return true;
		}
		if(playerConnectRed(x, y + 1, currCol, visited)) {
			return true;
		}
		if(playerConnectRed(x - 1, y, currCol, visited)) {
			return true;
		}
		if(playerConnectRed(x - 1, y + 1, currCol, visited)) {
			return true;
		}
		if(playerConnectRed(x, y - 1, currCol, visited)) {
			return true;
		}

		return false;
	}


	bool playerConnectBlue(int x, int y, int currCol, bool [][] visited)
	{
		if (y < 0 || y > globalObj.boardSize + 1 || x < 1 || x > globalObj.boardSize) {
			return false;
		}
		
		if (visited [x] [y]) {
			return false;
		}
		
		visited [x] [y] = true;
		
		//If hex is black or grey, return false
		if(board[x][y] != currCol) {
			return false;
		}
		
		if (y == globalObj.boardSize + 1) {
			return true;
		}
		
		//Recursion
		if(playerConnectBlue(x, y + 1, currCol, visited)) {
			return true;
		}
		if(playerConnectBlue(x - 1, y + 1, currCol, visited)) {
			return true;
		}
		if(playerConnectBlue(x + 1, y, currCol, visited)) {
			return true;
		}
		if(playerConnectBlue(x + 1, y-1, currCol, visited)) {
			return true;
		}

		if(playerConnectBlue(x - 1, y, currCol, visited)) {
			return true;
		}

		if(playerConnectBlue(x, y - 1, currCol, visited)) {
			return true;
		}
		
		return false;
	}
}
