using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedVirus : Enemy {

	public bool CanDivide;

	public GameObject subViruses;

	// Use this for initialization
	void Start () {
		rb2d.velocity = Vector2.down * speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "Bullet") {

			lives--;

			if (lives == 0) {
				if (CanDivide) {
					subViruses.SetActive (true);
					subViruses.transform.parent = null;
				}
				Destroy (this.gameObject);
			}

		}

		if (col.gameObject.tag == "DataPiece") {
			Destroy (this.gameObject);
		}

	}
}
