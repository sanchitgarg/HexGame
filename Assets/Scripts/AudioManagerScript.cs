using UnityEngine;
using System.Collections;

public class AudioManagerScript : MonoBehaviour {

	public AudioClip blockRise;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playBlockRise(Vector3 pos)
	{
		AudioSource.PlayClipAtPoint (blockRise, pos);
	}
}
