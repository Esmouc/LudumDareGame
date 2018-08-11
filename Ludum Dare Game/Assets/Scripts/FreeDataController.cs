using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeDataController : MonoBehaviour {

  public Vector2 final_dimensions;
  public float scaling_time;

  private float first_time;
  private Vector2 original_scale;

	// Use this for initialization
	void Start () {
    first_time = Time.time;
    original_scale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		float t = (Time.time - first_time) / scaling_time;
    Vector3 new_scale = new Vector3(
      Mathf.SmoothStep(original_scale.x,final_dimensions.x,t),
      Mathf.SmoothStep(original_scale.y,final_dimensions.y,t),
      1.0f);
    transform.localScale = new_scale;
	}
}
