using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeBody : MonoBehaviour {

  Rigidbody2D rb;
  public Transform target;
  public float speed;

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
  if(target == null) {
    GetComponent<Centipede>().dormant = false;
    this.enabled = false;
  }
    transform.position = Vector3.MoveTowards(transform.position,target.position,speed * Time.deltaTime);
	}
}
