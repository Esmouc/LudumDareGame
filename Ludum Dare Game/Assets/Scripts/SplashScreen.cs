using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

	public GameObject text;
	public GameObject finalPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (text.transform.position.y < finalPos.transform.position.y){
			SceneManager.LoadScene (1);
		}

	}
}
