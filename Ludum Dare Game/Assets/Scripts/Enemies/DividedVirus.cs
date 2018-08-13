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
    transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(random_target),rotation_velocity * Time.deltaTime);
    if(transform.rotation == Quaternion.Euler(random_target)) random_target = new Vector3(0.0f, 0.0f, Random.Range(-90.0f, 90.0f));
    rb2d.velocity = -transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D (Collision2D col){

		if (col.gameObject.tag == "PlayerBullet") {

			lives--;

    if(lives == 0) {
    if(CanDivide) {
    subViruses.SetActive(true);
    subViruses.transform.parent = null;
    }
    GameManager.instance.AudioManager.PlaySFX("SubdividedVirus");
    GameManager.instance.score += score;
    Destroy(this.gameObject);

    GameObject ps = null;
    if(corruptionLevel == 20) {
      ps = Instantiate(Resources.Load("Large Divide"),transform.position,Quaternion.identity) as GameObject;
      Camera.main.GetComponent<Shake>().Shaking(0.2f);
    }
    if(corruptionLevel == 4) {
      ps = Instantiate(Resources.Load("Medium Divide"),transform.position,Quaternion.identity) as GameObject;
      Camera.main.GetComponent<Shake>().Shaking(0.05f);
    }
    if(corruptionLevel == 1) {
      ps = Instantiate(Resources.Load("Small Death"),transform.position,Quaternion.identity) as GameObject;
    }
			}

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
