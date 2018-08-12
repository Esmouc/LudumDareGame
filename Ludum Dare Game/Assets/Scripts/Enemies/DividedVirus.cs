using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedVirus : Enemy {

  public float rotation_velocity;

	public bool CanDivide;

	public GameObject subViruses;

  private Vector3 random_target;



	// Use this for initialization
	void Start () {
		rb2d.velocity = -transform.up * speed * Time.deltaTime;
    random_target = new Vector3(0.0f, 0.0f, Random.Range(-90.0f, 90.0f));
	}
	
	// Update is called once per frame
	void Update () {
    transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(random_target),rotation_velocity);
    if(transform.rotation == Quaternion.Euler(random_target)) random_target = new Vector3(0.0f, 0.0f, Random.Range(-90.0f, 90.0f));
    rb2d.velocity = -transform.up * speed * Time.deltaTime;
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
			if (col.gameObject.GetComponent<DataPiece>().landed){
				GameManager.instance.corruption_level += corruptionLevel;
				Destroy (this.gameObject);
			}
		}

	}
}
