﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour {

  public int number_of_bodies = 5;
  public float speed = 150.0f;
  public float rotation_velocity = 150.0f;
  public float angle_factor;
  public float current_angle;
  public bool dormant = false;
  public bool head;

  private Vector3 random_target;
  private Rigidbody2D rb;
  public Sprite body_sprite;



	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();
    random_target = new Vector3(0.0f, 0.0f, Random.Range(-45.0f, 45.0f));

    if(head) {
      head = false;
      GameObject go_body = null;
      GameObject previous_go = gameObject;
      for(int i = 0; i < number_of_bodies; ++i) {
        go_body = Instantiate(gameObject, transform.position + new Vector3(0.0f, 0.5f + i * 0.5f, 0.0f), Quaternion.identity);
        go_body.GetComponent<Centipede>().dormant = true;
        go_body.GetComponent<CentipedeBody>().target = previous_go.transform;
        go_body.GetComponent<SpriteRenderer>().sprite = body_sprite;
        previous_go = go_body;
      }
      GetComponent<CentipedeBody>().enabled = false;
    }

	}
	
	// Update is called once per frame
	void Update () {
    if(!dormant) {
      /*transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0.0f, 0.0f, angle_limit)), rotation_velocity * Time.deltaTime);
      if(Mathf.Approximately(transform.rotation.eulerAngles.z,360.0f - Mathf.Abs(angle_limit)) ||
        Mathf.Approximately(transform.rotation.eulerAngles.z, angle_limit)) angle_limit *= -1;*/

      transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(random_target),rotation_velocity * Time.deltaTime);
      if(transform.rotation == Quaternion.Euler(random_target)) random_target = new Vector3(0.0f, 0.0f, Random.Range(-45.0f, 45.0f));

      rb = GetComponent<Rigidbody2D>();
      //rb.velocity = -transform.up * speed * Time.deltaTime;
      transform.Translate(-transform.up * speed * Time.deltaTime,Space.World);
  
    }

	}
}
