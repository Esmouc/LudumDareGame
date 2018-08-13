using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("joystick button 1"))
			Close ();
	}

	public void Activate(){
		GetComponent<Animator> ().SetBool("In", true);
	}

	public void Close(){
		GetComponent<Animator> ().SetBool("In", false);
	}
}
