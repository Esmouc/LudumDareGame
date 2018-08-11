using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	public int shieldResistance;

	public GameObject Bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnCollisionEnter2D (Collision2D col){
		
		shieldResistance--;

		GameObject go = (GameObject) Instantiate (Bullet, transform.position + transform.up * 1/6, Quaternion.identity);

		go.GetComponent<Bullet> ().direction = transform.up;

		go.transform.parent = this.gameObject.transform.parent;

		if (col.gameObject.tag == "Bullet")
			Destroy (col.gameObject);

		if (shieldResistance == 0)
			Destroy (this.gameObject);
		
	}
}
