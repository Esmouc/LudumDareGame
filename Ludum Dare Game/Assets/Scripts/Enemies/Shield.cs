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

		if (col.gameObject.tag == "PlayerBullet" || col.gameObject.tag == "DataPiece") {
			
      GameManager.instance.AudioManager.PlaySFX("EnemyShot");  
			GameObject go = (GameObject)Instantiate (Bullet, transform.position + transform.up * 1 / 4, Quaternion.identity);
			go.GetComponent<Bullet> ().direction = transform.up;
			go.GetComponent<Bullet> ().UpdateVelocity ();

		}

		if (shieldResistance == 0)
			Destroy (this.gameObject);
		
	}
}
