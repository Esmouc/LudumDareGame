using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public GameObject bullet;
  public float lineal_speed = 0.1f;
  public float rotation_speed = 100.0f;
  public float base_bullet_speed = 0.15f;
  public float rof = 0.1f;

  
  private float rotation;
  private float shot_cd;

	// Use this for initialization
	void Start () {
    rotation = 0.0f;
    transform.rotation = Quaternion.identity;
    shot_cd = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = -Input.GetAxis("Vertical");
    float strafe = Input.GetAxis("Strafe");
    bool shooting = Input.GetKey("joystick button 2");

    //float magnitude = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
    float magnitude = new Vector2(horizontal, vertical).magnitude;

    if(magnitude > 0.1f) {
      float current_angle = Mathf.Atan2(horizontal,vertical);
      current_angle = Mathf.Rad2Deg * current_angle;


      //magnitude = new Vector2(horizontal, vertical).magnitude;

      if(strafe < 0.5f) {
        transform.rotation = Quaternion.RotateTowards(Quaternion.Euler(0.0f,0.0f,rotation),Quaternion.Euler(0.0f,0.0f,current_angle),rotation_speed * Time.deltaTime * magnitude);
        rotation = transform.rotation.eulerAngles.z;
        transform.Translate(-transform.up * lineal_speed * magnitude * Time.deltaTime, Space.World);
      } else {
        transform.Translate(new Vector3(horizontal,-vertical,0.0f) * lineal_speed * magnitude * Time.deltaTime, Space.World);
      }
    }else{
      transform.Translate(new Vector3(0.0f,-0.5f) * Time.deltaTime, Space.World);
    }

    if(shooting && shot_cd >= rof) {
      GameObject temp_bullet = Instantiate(bullet, transform.position - transform.up, Quaternion.Euler(0.0f, 0.0f, rotation));
      temp_bullet.GetComponent<Bullet>().direction = -transform.up;
      temp_bullet.GetComponent<Bullet>().speed = base_bullet_speed + lineal_speed * magnitude;
      temp_bullet.GetComponent<Bullet>().UpdateVelocity();
      shot_cd = 0.0f;
    }

    if(shot_cd < rof) shot_cd += Time.deltaTime;
	}
}
