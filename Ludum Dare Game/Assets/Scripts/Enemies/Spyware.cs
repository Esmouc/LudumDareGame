using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyware : Enemy {

	public float shieldSpeed;

	public GameObject shields;

	// Use this for initialization
	void Start () {

		rb2d.velocity = Vector2.down * speed * Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update () {

		shields.transform.RotateAround (transform.position, -Vector3.forward, shieldSpeed * Time.deltaTime);
		
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "PlayerBullet") {
			lives--;
		}

		if (lives == 0 ){
			GameManager.instance.score += score;
			Destroy (this.gameObject);
		}
		
		if (col.gameObject.tag == "DataPiece") {
			DataPiece dp = col.gameObject.GetComponent<DataPiece> ();
			if (dp != null){
				if (dp.landed){
					GameManager.instance.corruption_level += corruptionLevel;
					Destroy (gameObject);
				}
			}
		}
	}
}
