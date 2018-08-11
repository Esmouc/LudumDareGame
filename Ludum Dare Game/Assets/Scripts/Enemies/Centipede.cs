using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour {

  public float speed = 150.0f;
  public float rotation_velocity = 150.0f;
  public float angle_factor;
  public float current_angle;
  public bool dormant;

  private Vector3 random_target;
  private Rigidbody2D rb;


	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();
    random_target = new Vector3(0.0f, 0.0f, Random.Range(-45.0f, 45.0f));

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
