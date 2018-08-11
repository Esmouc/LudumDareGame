using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyware : Enemy {

	public float shieldSpeed;

	public GameObject shields;

	// Use this for initialization
	void Start () {

		rb2d = GetComponent<Rigidbody2D> ();

		rb2d.velocity = Vector2.down * speed * Time.deltaTime;

	}
	
	// Update is called once per frame
	void Update () {

		shields.transform.RotateAround (transform.position, -Vector3.forward, shieldSpeed * Time.deltaTime);
		
	}

	void OnCollisionEnter2D (Collision2D col){

		lives--;

		if (col.gameObject.tag == "Bullet")
			Destroy (col.gameObject);

		if (lives == 0 || col.gameObject.tag == "Ground")
			Destroy (this.gameObject);

	}
}
