using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	public Vector2 direction;

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
    rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	  public void UpdateVelocity()
	  {
	    rb2d.velocity = direction * speed * Time.deltaTime;
	  }


	  public void OnTriggerEnter2D(Collider2D collision)
	  {
	    Destroy(gameObject);
	  }


	void OnCollisionEnter2D (Collision2D col){

		Destroy (this.gameObject);

	}

}
