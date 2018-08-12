using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPiece : MonoBehaviour {

	public float speed;

	private Rigidbody2D rb2d;

	private bool collided = false;

	private bool canBeDestroyed = false;

	public bool landed= false;

	public Color landedColor;
	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		//rb2d.velocity = Vector2.down * speed * Time.deltaTime;
		
	}

	void Update () {

		if (collided){
			if (rb2d.velocity.magnitude <= 0.05f){
				rb2d.mass = 10000000;
				landed = true;
				GetComponent<SpriteRenderer> ().color = landedColor;
					//rb2d.bodyType = RigidbodyType2D.Kinematic;
					//rb2d.velocity= Vector2.zero;
			}
		}

	}


	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "DataPiece") {
			collided = true;
		}

		if (col.gameObject.tag == "Bullet" && canBeDestroyed) {
			Destroy (gameObject);
		}
	}


	void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.tag == "FreeData") {
			canBeDestroyed = true;
			GetComponent<Animator> ().SetBool ("ToEliminate", true);
		}

	}

	void OnTriggerExit2D (Collider2D col) {

		if (col.gameObject.tag == "FreeData") {
			canBeDestroyed = false;
			GetComponent<Animator> ().SetBool ("ToEliminate", false);
		}

	}
}
