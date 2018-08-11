using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPiece : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.velocity = Vector2.down * speed * Time.deltaTime;
		
	}


	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "DataPiece") {
			rb2d.velocity = Vector2.zero;
		}
	}

}
