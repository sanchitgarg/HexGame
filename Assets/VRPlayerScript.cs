using UnityEngine;
using System.Collections;

public class VRPlayerScript : MonoBehaviour {

	public float force;
	public float torque;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		var rb = gameObject.GetComponent<Rigidbody>();
		Vector3 r = rb.rotation.eulerAngles;
		rb.rotation = Quaternion.Euler (0, r.y, 0);

		//VR player controls
		if (Input.GetKey (KeyCode.W)) {
			rb.AddRelativeForce(0, 0, force);
		}
		if (Input.GetKey (KeyCode.S)) {
			rb.AddRelativeForce(0, 0, -force);
		}
		if (Input.GetKey (KeyCode.D)) {
			rb.freezeRotation = false;
			rb.AddTorque(0,torque,0);
		}
		if (Input.GetKey (KeyCode.A)) {
			rb.freezeRotation = false;
			rb.AddTorque(0,-torque,0);
		}

		//Freeze rotation and translation
		if (Input.GetKeyDown (KeyCode.R)) {
			rb.velocity = new Vector3(0,0,0);
			rb.freezeRotation = true;
		}

		//Toggle camera
		if (Input.GetKeyDown (KeyCode.C)) {
			bool curr = gameObject.GetComponentInChildren<Camera>().enabled;
			gameObject.GetComponentInChildren<Camera>().enabled = !curr;
		}
	}
}
