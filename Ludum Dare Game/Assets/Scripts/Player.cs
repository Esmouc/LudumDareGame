using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public GameObject bullet;
  public float lineal_speed = 0.1f;
  public float rotation_speed = 100.0f;
  public float base_bullet_speed = 0.15f;
  public float rof = 0.1f;

  private Rigidbody2D rb;
  private float rotation;
  private float shot_cd;

	// Use this for initialization
	void Start () {
    rotation = 0.0f;
    transform.rotation = Quaternion.identity;
    shot_cd = 0.0f;
    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    rb = GetComponent<Rigidbody2D>();

	    float leftHorizontal = Input.GetAxis("Horizontal");
	    float leftVertical = -Input.GetAxis("Vertical");
		float rightHorizontal = Input.GetAxis("Mouse X");
		float rightVertical = -Input.GetAxis("Mouse Y");
	    float strafe = Input.GetAxis("Strafe");
	    bool shooting = Input.GetKey("joystick button 2");
	    if(shooting == false) shooting = Input.GetKey("space");

	    //float magnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
		float leftMagnitude = new Vector2(leftHorizontal, leftVertical).magnitude;
		float rightMagnitude = new Vector2(rightHorizontal, rightVertical).magnitude;

	    /*if(magnitude > 0.1f) {
	      float current_angle = Mathf.Atan2(horizontal,vertical);
	      current_angle = Mathf.Rad2Deg * current_angle;


	      //magnitude = new Vector2(horizontal, vertical).magnitude;

	      if(strafe < 0.5f) {
	        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0.0f,0.0f,rotation),Quaternion.Euler(0.0f,0.0f,current_angle),rotation_speed * Time.deltaTime * magnitude);
	        rotation = transform.rotation.eulerAngles.z;
	        //transform.Translate(-transform.up * lineal_speed * magnitude * Time.deltaTime, Space.World);
	        rb.velocity = -transform.up * lineal_speed * magnitude * Time.deltaTime;
	      } else {
	        //transform.Translate(new Vector3(horizontal,-vertical,0.0f) * lineal_speed * magnitude * Time.deltaTime, Space.World);
	        //rb.velocity = new Vector3(horizontal,-vertical,0.0f) * lineal_speed * magnitude * Time.deltaTime;
			rb.velocity = new Vector3(horizontal,-vertical,0.0f) * lineal_speed * magnitude * Time.deltaTime;
	      }
	    }else{
	      //transform.Translate(new Vector3(0.0f,-0.5f) * Time.deltaTime, Space.World);
	      rb.velocity = new Vector3(0.0f,-20.0f) * Time.deltaTime;
	    }*/

		if (leftMagnitude > 0.1f) {

			//transform.Translate(-transform.up * lineal_speed * magnitude * Time.deltaTime, Space.World);
			rb.velocity =  new Vector2(leftHorizontal, -leftVertical).normalized * lineal_speed * leftMagnitude * Time.deltaTime;

		} else {
			rb.velocity = new Vector2 (0, 0);
		}

		if (rightMagnitude > 0.05f) {

			float current_angle = Mathf.Atan2(rightHorizontal,-rightVertical);
			current_angle = Mathf.Rad2Deg * current_angle;

			rotation = transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0.0f,0.0f,rotation),Quaternion.Euler(0.0f,0.0f,current_angle),rotation_speed * Time.deltaTime * rightMagnitude);
		
			if (shot_cd >= rof) {
				GameObject temp_bullet = Instantiate(bullet, transform.position - transform.up/2.5f, Quaternion.Euler(0.0f, 0.0f, rotation));
				temp_bullet.GetComponent<Bullet>().direction = -transform.up;
				temp_bullet.GetComponent<Bullet>().speed = base_bullet_speed + lineal_speed * leftMagnitude;
				temp_bullet.GetComponent<Bullet>().UpdateVelocity();
				shot_cd = 0.0f;
			}

			if(shot_cd < rof) shot_cd += Time.deltaTime;
		
		}
	}
}
