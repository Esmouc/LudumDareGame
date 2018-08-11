using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPiece : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2d;

	private bool collided = false;

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		//rb2d.velocity = Vector2.down * speed * Time.deltaTime;
		
	}

	void Update () {

		if (collided)
			if (rb2d.velocity.magnitude == 0)
				rb2d.bodyType = RigidbodyType2D.Static;

	}


	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "DataPiece") {
			collided = true;
		}
	}

}
